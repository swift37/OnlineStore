$(document).ready(function () {

    /*$('.ui.dropdown').dropdown();*/

    $('.ui.dropdown').dropdown({
        on: 'hover'
    });

    $('.special.cards .image').dimmer({
        on: 'hover'
    });

    $('.special-card-column .image').dimmer({
        on: 'hover'
    });

    checkCartQuantity();

    $('.menu-button').click(function () {
        if ($('.menu-button').hasClass('w--open'))
        {
            $('.menu-button').removeClass('w--open');
            $('w-nav-overlay').css('display', 'none');
        }
        else
        {
            $('.menu-button').addClass('w--open');
            $('w-nav-overlay').css('display', 'block');
        }
    });

    //$('.nav-link').click(function () {
    //    $(this).addClass('w--current');
    //    $(this).siblings().removeClass('w--current');
    //});

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

    function checkCartQuantity() {
        let qty = parseInt($('#cartQuantity').text());
        if (qty > 9) {
            let counter = $('#cartQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    }

    $('.add-to-cart-btn').click(function () {
        let qty = $('#productQuantity').val();

        $.ajax({
            url: '/Product/AddToCart',
            type: 'post',
            dataType: 'json',
            data:
            {
                id: $(this).data('itemid'),
                qty: qty
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
                    let newQty = parseInt($('#cartQuantity').text()) + parseInt(qty);
                    if (newQty > 9) {
                        let counter = $('#cartQuantity').parent('.counter');
                        if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
                    }
                    $('#cartQuantity').text(newQty);
                    checkCartQuantity();
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

    $('#termsCheckbox').click(function () {
        if ($(this).is(':checked')) {
            $('#checkoutBtn').removeAttr('disabled');
        }
        else {
            $('#checkoutBtn').attr('disabled', true);
        }
    });

    $('.products.nav-link').popup({
        popup: $('.products.popup'),
        hoverable: true,
        delay: {
            show: 0,
            hide: 100
        }
    });

    $('.contact.nav-link').popup({
        popup: $('.contact.popup'),
        hoverable: true,
        delay: {
            show: 0,
            hide: 100
        }
    });

});
