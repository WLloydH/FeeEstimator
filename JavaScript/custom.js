
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

function formatFees() {

    const menuItem = (label, cost) => cost.padStart(40 - label.length, ".");
   

    const plansFee = $("#lblPlansFee");
    const plansFeeText = plansFee.text().split(":");
    if (plansFeeText.length > 1) {
         $('<div>' + menuItem(plansFeeText[0],plansFeeText[1]) + '</div>').insertAfter(plansFee);


        const permitFee = $("#lblPermitFee");
        const permitFeeText = permitFee.text().split(":");
        if (permitFeeText.length > 1) {
             $('<div>' + menuItem(permitFeeText[0], permitFeeText[1]) + '</div>').insertAfter(permitFee);
            permitFee.remove();
        }


        const trades = $("#lblTrades");
        const tradesText = trades.text().split(":");
        if (tradesText.length > 1) {
             $('<div">' + menuItem(tradesText[0], tradesText[1]) + '</block>').insertAfter(trades);
            trades.remove();
        }

        const stateFee = $("#lblStateFee");
        const stateFeeText = stateFee.text().split(":");
        if (stateFeeText.length > 1) {
             $('<div">' + menuItem(stateFeeText[1]) + '</div>').insertAfter(stateFee);
            stateFee.remove();
        }

        const total = $("#lblTotal");
        const totalText = total.text().split(":");
        if (totalText.length > 1) {
            $('<div">' + menuItem(stateFeeText[1]) + '</div>').insertAfter(total);
            total.remove();
        }

    }
}


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



$(window).ready(function() {
    var d = new Date();
    $('#Copyright').html('Copyright '.concat(d.getFullYear()).concat(' all rights reserved'));
    formatFees();

});
