$(document).ready(function () {

    $('.js-example-basic-single').select2({
        allowClear: true
    });
    $('.select2-shop-customer').select2({
        width: '102%'
    });
    $('#slGroupCustomer').select2({ width: '100%' });
    $('[type="checkbox"]').change(function () {

        if (this.checked) {
            $('[type="checkbox"]').not(this).prop('checked', false);
        }
    });

   
});