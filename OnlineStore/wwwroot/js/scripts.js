$(document).ready(function () {

    $('.ui.dropdown').dropdown();

    $('#productCategorySelect').change(function () {
        var categoryId = $(this).val();

        $.ajax({
            url: '/Admin/GetSubCategories',
            type: 'post',
            dataType: 'json',
            data:
            {
                id: categoryId
            },
            error: function () { },
            success: function (result) {
                $('#productSubcategorySelect').empty();
                $.each(result, function (index, item) {
                    $('#productSubcategorySelect').append(
                        '<option value="' + item.value + '">' + item.text + '</option>');

                    $('#productSubcategorySelect').parent('.field').removeClass('hidden');
                    $('#productSubcategorySelect').dropdown();
                });
            }
        });

    });

    $('#add-spec').click(function () {
        let specsCount = $('.product-specs-list .two.fields').length;

        let element = '<div class="field"><label>Specs</label>' +
            '<div class="two fields"><div class="field">' +
            '<input name="ProductDetails[' + specsCount + '].PropertyName" type="text" placeholder="Name"></div><div class="field">' + 
            '<input name="ProductDetails[' + specsCount + '].PropertyValue" type="text" placeholder="Value"></div></div></div>';

        $('.product-specs-list').append(element);
    });

});
