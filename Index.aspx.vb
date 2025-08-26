// 1. Define a Model to hold the data
public class PermitFeeModel
{
    public string PermitType { get; set; }
    public double PermitValue { get; set; }
    public bool IsCommercial { get; set; }
    public bool IsResidential { get; set; }
    public bool IsOverTheCounter { get; set; }
    public int TradeNumbers { get; set; }
}

public class FeeCalculationResult
{
    public decimal PermitFee { get; set; }
    public decimal PlansFee { get; set; }
    public decimal TradesFee { get; set; }
    public decimal StateFee { get; set; }
    public decimal Total { get; set; }
}

// 2. Create a dedicated service for the business logic
public class FeeCalculatorService
{
    private readonly string _connectionString;

    public FeeCalculatorService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<FeeCalculationResult> CalculateFees(PermitFeeModel model)
    {
        // Parameter validation would go here
        
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("spCalculateFee", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@ResCom", model.IsCommercial ? "Commercial" : "Residential");
        command.Parameters.AddWithValue("@Value", model.PermitValue);
        command.Parameters.AddWithValue("@Trades", model.TradeNumbers);
        command.Parameters.AddWithValue("@OverTheCounter", model.IsOverTheCounter ? 1 : 0);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        
        if (await reader.ReadAsync())
        {
            return new FeeCalculationResult
            {
                PermitFee = reader.GetDecimal(reader.GetOrdinal("PermitFee")),
                PlansFee = reader.GetDecimal(reader.GetOrdinal("PlansFee")),
                TradesFee = reader.GetDecimal(reader.GetOrdinal("Trades")),
                StateFee = reader.GetDecimal(reader.GetOrdinal("StateFee")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total"))
            };
        }

        return null;
    }
}
