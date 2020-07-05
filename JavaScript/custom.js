function setToastrGlobalOption() {
    toastr.options.positionClass = 'toast-top-right';
    toastr.options.timeOut = 2500;
    toastr.options.fadeOut = 2500;
    toastr.options.fadeIn = 250;
    return;
}

$("#txtSubmit").click(function () {

    $("#form1").submit(function (e) {
        toastr["info"]("Hello sub Click");
        var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');
        var jobValObj = $("#txtValue");

        if ($("#ddlPermitType option:selected").selectedIndex === 0 || $("#ddlPermitType option:selected").selectedIndex === undefined) {
            toastr["info"]("Please select a permit type.");
            e.preventDefault(e);
        }

        if (isNaN(jobVal) || jobVal === 0) {
            jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
            toastr["info"]("Please enter a dollar amount.");
            e.preventDefault(e);
        }

    });
});

$("#txtValue")
    .focusout(function formatJobValue() {
        var jobValObj = $("#txtValue");
        var jobVal = jobValObj.val().trim().replace(',', '').replace('$', '');

        if (isNaN(jobVal) || jobVal < 0) {
            jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(" "));
            toastr["info"]("Please enter a valid dollar amount.");
        } else {
            jobValObj.val(new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(jobVal));
        }

    });