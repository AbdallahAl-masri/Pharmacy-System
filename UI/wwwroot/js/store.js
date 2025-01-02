function checkDate() {
    const date1 = document.getElementById('txtProductionDate').value;
    const date2 = document.getElementById('txtExpiaryDate').value;

    if (date1 > date2) {
        document.getElementById('divError').style.display = 'block';
        return false;
    }
    else {
        document.getElementById('divError').style.display = 'none';
        return true;
    }
}

function CalculateTax() {
    var taxValue = document.getElementById('txtTaxValue').value;
    var CostPrice = document.getElementById('txtCostPrice').value;

    document.getElementById('txtSellingPriceBeforeTax').value = CostPrice;
    document.getElementById('txtSellingPriceAfterTax').value = (((Number.parseFloat(taxValue / 100)) * CostPrice) + Number.parseFloat(CostPrice)).toFixed(2);
}

function OnlyDecimal(evt) {
    var e = evt || event;
    var charCode = e.which || e.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode === 46)) {
        return true;
    }
    return false;
}