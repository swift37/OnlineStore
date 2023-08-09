$(document).ready(function () {

    $('.ui.dropdown').dropdown();

    $('select.dropdown').dropdown();

    $('.special.cards .image').dimmer({
        on: 'hover'
    });

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

        let element = '<div class="field spec-field"><label>Specs</label>' +
            '<div class="two fields"><div class="field">' +
            '<input name="ProductDetails[' + specsCount + '].PropertyName" type="text" placeholder="Name"></div><div class="field">' + 
            '<input name="ProductDetails[' + specsCount + '].PropertyValue" type="text" placeholder="Value"></div>' +
            '<i class="remove circle icon remove-spec"></i></div></div>';

        $('.product-specs-list').append(element);
    });

    $('.remove-spec').click(function () {
        $(this).closest('.spec-field').remove();

        $.each($('.spec-field'), function (index, item) {
            console.log(index);
            $(item).find('.property-name').attr('name', 'ProductDetails[' + index + '].PropertyName');
            $(item).find('.property-value').attr('name', 'ProductDetails[' + index + '].PropertyValue');
            $(item).find('.property-id').attr('name', 'ProductDetails[' + index + '].Id');
        });
    });

    $('.buy-ref').click(function () {
        //let qty = $('.buy-qty').val();

        $.ajax({
            url: '/Product/AddToCart',
            type: 'post',
            dataType: 'json',
            data:
            {
                id: $(this).data('itemid'),
                qty: 1
            },
            error: function ()
            {
                alert('Error occurred.');
            },
            success: function (result)
            {
                if (result.error == true)
                {
                    $('.buy-result').text(result.message);
                }
                else
                {
                    let newQty = parseInt($('.cart-quantity').text()) + 1;
                    $('.cart-quantity').text(newQty);
                }
            }
        });
    });

    $('.cart-line-qty').bind('keyup click', function () {
        $.ajax({
            url: '/Order/UpdateCart',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
                qty: $(this).val()
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                $('.cart-total').text(result.CartPrice);
                location.reload();
                //$(this).closest('tr').find('td.line-total').text(result.LinePrice);
            }
        });
    });

    $('.remove-cart-item-btn').click(function () {
        $.ajax({
            url: '/Order/RemoveFromCart',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                if (result) location.reload();
            }
        });
    });

    //$(document).ready(function () {
    //    card = new Skeuocard($("#skeuocard"));
    //});

    $('#termsCheckbox').click(function () {
        if ($(this).is(':checked')) {
            $('#checkoutBtn').removeAttr('disabled');
        }
        else {
            $('#checkoutBtn').attr('disabled', true);
        }
    });

    $('.products.item').popup({
        popup: '.products.popup',
        hoverable: true,
        delay: {
            show: 0,
            hide: 100
        }
    });

    $('.contact.item').popup({
        popup: '.contact.popup',
        hoverable: true,
        delay: {
            show: 0,
            hide: 100
        }
    });

});
