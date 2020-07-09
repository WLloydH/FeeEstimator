
var myClearwater = myClearwater || {}; 
myClearwater.SubmitValid = true;

myClearwater.valid = function () {
    myClearwater.checkValues();
    return myClearwater.SubmitValid;
};


(myClearwater.setToastrGlobalOption = function() {
    toastr.options.positionClass = 'toast-top-right';
    toastr.options.fadeOut = 3000;
     toastr.options.fadeIn = 5000;
    toastr.options.preventDuplicates = true;
    return;
})();



$("#txtSubmit").click(function () {
        myClearwater.checkValues();
    }
);


$("#txtValue").focusout(function formatJobValue() {
    var jobValObj = $("#txtValue");
    var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');

    if (isNaN(jobVal) || jobVal < 0 || jobVal === '') {
        jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
        toastr["info"]("Please enter a valid dollar amount.");
    } else {
        jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' })
            .format(jobVal));
    }

});

myClearwater.checkValues = function(){
    var jobValObj = $("#txtValue");
    var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');
    myClearwater.valid = true;
    if ($("#ddlPermitType option:selected").text().indexOf("Select") !== -1) {
        toastr["info"]("Please select a permit type.");
        myClearwater.SubmitValid = false;
    }

    if (isNaN(jobVal) || jobVal === 0 || jobVal.length === 0) {
        jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
        toastr["info"]("Please enter a dollar amount.");
        myClearwater.SubmitValid = false;
    }
}



$(document).ready(function() {
    var d = new Date();
    $('#Copyright').html('Copyright '.concat(d.getFullYear()).concat(' all rights reserved'));

});
