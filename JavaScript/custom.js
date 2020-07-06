
var myClearwater = myClearwater || {};
myClearwater.SubmitValid = false;

myClearwater.valid = function () {
    return myClearwater.SubmitValid;
};
$(document).ready(function() {
    var d = new Date();
    $('#Copyright').html('Copyright '.concat(d.getFullYear()).concat(' all rights reserved'));
    setToastrGlobalOption();


    function setToastrGlobalOption() {
        toastr.options.positionClass = 'toast-top-right';
        toastr.options.fadeOut = 3000;
        toastr.optionstimeOut = 5000;
        toastr.options.fadeIn = 3000;
        toastr.options.preventDuplicates = true;
        return;
    }


    $("#txtSubmit").click(function checkValues() {

        var jobValObj = $("#txtValue");
        var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');
        myClearwater.valid = true;
        if ( $("#ddlPermitType option:selected").text().indexOf("Select") !== -1) {
            toastr["info"]("Please select a permit type.");
            myClearwater.SubmitValid = false;
        } else {

            if ($("#ddlPermitType option:selected").selectedIndex === 0 || $("#ddlPermitType option:selected").selectedIndex === undefined) {
                toastr["info"]("Please select a permit type.");
                myClearwater.SubmitValid = false;
            }
        }

        if(isNaN(jobVal) || jobVal === 0 || jobVal.length === 0) {
                jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
                toastr["info"]("Please enter a dollar amount.");
                myClearwater.SubmitValid = false;
            }  
         });
 

$("#txtValue").focusout(function formatJobValue() {
        var jobValObj = $("#txtValue");
        var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');

    if (isNaN(jobVal) || jobVal < 0 ||  jobVal==='') {
            jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
            toastr["info"]("Please enter a valid dollar amount.");
        } else {
            jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' })
                .format(jobVal));
        }

});
});


