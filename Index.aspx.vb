
Imports System.Data.SqlClient

Partial Class GovDeptsPlanningIndex
    Inherits System.Web.UI.Page
    Private cnStr As ConnectionStringSettingsCollection
    Private AppSettings As NameValueCollection
    Dim TradeNumbers As Integer

    Public Sub New()
        cnStr = System.Configuration.ConfigurationManager.ConnectionStrings
        AppSettings = System.Configuration.ConfigurationManager.AppSettings
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        Dim cmdCommon As New SqlCommand
        Dim drCommon As SqlDataReader = Nothing

        pnlResults.Visible = False
        Commercial.Checked = True

        lblExplainState.Text = "*   State Fee includes <i>Fl Building Commission </i> <u>and</u> <i> Fl Building Code Administration and Inspectors Board</i>. They are 3% of the Permit Fees, Plans Fee and Trades Fee or a minimum of $2."
        lblDisclaimer.Text = "Disclaimer:   The above fee estimate does not include other fees such as impact fees, fire fees, right-of-way, tree removal and clearing and grubbing fees, some of which could be substantial.  For information concerning fire fees please call 727-562-4327 and for more information on other fees please call 727-562-4567. The City of Clearwater assumes no liability for any errors, omissions, or inaccuracies in the information provided. "
        Try
            With cmdCommon
                .Connection = New SqlConnection(cnStr.Item("cnFeeEstimator").ConnectionString)
                .Connection.Open()
                .CommandType = CommandType.StoredProcedure
                .CommandText = "spGetFeeTypes"
                drCommon = .ExecuteReader
            End With

            With ddlPermitType
                .DataSource = drCommon
                .DataValueField = "CASE_TYPE"
                .DataTextField = "PERMIT_TYPE"
                .DataBind()
            End With
        Catch err As Exception
            lblMessage.Text = "Error retrieving domains - Contact IT Help Desk<br>" &
             "Details: " & err.Message
        Finally
            If Not IsNothing(drCommon) Then
                If Not drCommon.IsClosed Then drCommon.Close()
                drCommon = Nothing
            End If
            If Not IsNothing(cmdCommon.Connection) Then
                If Not cmdCommon.Connection.State = ConnectionState.Closed Then cmdCommon.Connection.Close()
            End If
            cmdCommon = Nothing
        End Try


    End Sub

    Protected Sub txtSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubmit.Click

        Session("PermitType") = ddlPermitType.SelectedIndex.ToString()
        lblMessage.Text = Session("PermitType")

        lblMessage.Text = ""
        pnlResults.Visible = False

        Dim resCom As String
        Dim tradeNumbers As Integer = 0
        Dim overTheCounter As Integer = 0

        If Not (IsNumeric(Trim(txtValue.Text))) Then
            lblMessage.Text = "Please enter a numeric job value"
            Exit Sub
        End If

        If ddlPermitType.SelectedIndex = 0 Then
            lblMessage.Text = "Please enter a valid permit type"
            Exit Sub
        End If

        If Trim(txtValue.Text) = "" Then
            lblMessage.Text = "Please enter a valid permit value"
            Exit Sub
        End If

        If Not (Commercial.Checked) And Not (Residential.Checked) Then
            lblMessage.Text = "Please select commercial or residential property"
            Exit Sub
        End If

        Session("PermitValue") = Double.Parse(Replace(Trim(txtValue.Text), "$", ""))

        If Commercial.Checked Then resCom = "Commercial"
        If Residential.Checked Then resCom = "Residential"

        If ckbBuilding.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ckbElectric.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ckbRoof.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ckbMechanic.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ckbPlumbing.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ckbGas.Checked Then
            tradeNumbers = tradeNumbers + 1
        End If

        If ddlPermitType.SelectedValue.Substring(0, 16) <> "Over the Counter" Then
            overTheCounter = 0
        Else
            overTheCounter = 1
        End If

        Dim cmdCalculate As New SqlCommand
        Dim drCalculate As SqlDataReader

        Try
            With cmdCalculate
                .Connection = New SqlConnection(cnStr.Item("cnFeeEstimator").ConnectionString)
                .Connection.Open()
                .CommandType = CommandType.StoredProcedure
                .CommandText = "spCalculateFee"
                .Parameters.Add("@ResCom", SqlDbType.VarChar).Value = resCom
                .Parameters.Add("@Value", SqlDbType.Int).Value = Session("PermitValue")
                .Parameters.Add("@Trades", SqlDbType.Int).Value = tradeNumbers
                .Parameters.Add("@OverTheCounter", SqlDbType.Int).Value = overTheCounter
                drCalculate = .ExecuteReader(CommandBehavior.CloseConnection)
            End With

            If drCalculate.Read Then
                Session("PermitFee") = drCalculate.Item("PermitFee")
                Session("PlansFee") = drCalculate.Item("PlansFee")
                Session("Trades") = drCalculate.Item("Trades")
                Session("StateFee") = drCalculate.Item("StateFee")
                Session("Total") = drCalculate.Item("Total")


                If overTheCounter = 0 Then
                    lblPlansFee.Text = "Review Fee: " & FormatCurrency(Session("PlansFee"))
                Else
                    lblPlansFee.Text = ""
                End If
                pnlResults.Visible = True
                lblEstimate.Text = "Estimated Fees"
                lblPermitFee.Text = "Permit Fee : " & FormatCurrency(Session("PermitFee"))
                lblStateFee.Text = "State Fee :  " & FormatCurrency(Session("StateFee"))
                lblTrades.Text = "Trades Fee : " & FormatCurrency(Session("Trades"))
                lblTotal.Text = "Total :   " & FormatCurrency(Session("Total"))

            End If

        Catch ex As Exception
            lblMessage.Text &= "A system error has occurred. Please contact the helpdesk:<br> " + ex.Message
        End Try


    End Sub


    Protected Sub txtReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtReset.Click
        Clear()
    End Sub

    Public Sub Clear()
        ddlPermitType.SelectedIndex = 0
        txtValue.Text = ""
        Commercial.Checked = True
        Residential.Checked = False
        Session("PlansFee") = ""
        Session("PermitFee") = ""
        Session("Surcharge") = ""
        Session("PermitValue") = 0
        lblMessage.Text = ""
        TradeNumbers = 0
        ckbBuilding.Checked = False
        ckbElectric.Checked = False
        ckbRoof.Checked = False
        ckbMechanic.Checked = False
        ckbPlumbing.Checked = False
        ckbGas.Checked = False
        lblPermitFee.Text = ""
        lblTrades.Text = ""
        lblPlansFee.Text = ""
        lblStateFee.Text = ""
        lblTotal.Text = ""
        lblEstimate.Text = ""
        pnlResults.Visible = False
    End Sub
End Class
