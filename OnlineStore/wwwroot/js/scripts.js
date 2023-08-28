$(document).ready(function () {

    $('.ui.dropdown').dropdown({
        on: 'hover'
    });

    checkCartQuantity();

    checkWishlistQuantity();

    copyMenu();

    $('.side-menu-button').click(function () {
        $('.site-off').addClass('show-menu');
    });

    $('.t-close').click(function () {
        $('.site-off').removeClass('show-menu');
    })

    function copyMenu() {
        let dptCategories = $('.dpt-categories').html();
        $('.departments').html(dptCategories);

        let rightMenu = $('.nav-menu.right').html();
        $('.off-canvas nav').html(rightMenu);
    }

    $('.link-item.has-child').click(function () {
        $(this).siblings().removeClass('expand');
        if ($(this).hasClass('expand'))
        {
            $(this).removeClass('expand');
        }
        else
        {
            $(this).addClass('expand');
        }
    });

    $('.has-child .icon-small').click(function () {
        $(this).parent('.has-child').siblings().removeClass('expand');
        if ($(this).parent('.has-child').hasClass('expand')) {
            $(this).parent('.has-child').removeClass('expand');
        }
        else {
            $(this).parent('.has-child').addClass('expand');
        }
    });

    $('.filter-trigger').click(function () {
        setTimeout(function () {
            $('.filter').addClass('show');
            console.log('add');
        }, 250);
    });

    $('.cart-button').click(function () {
        setTimeout(function () {
            $('.mini-cart').addClass('show');
        }, 250);
    });

    $(document).click(function (e) {
        if (!$('.filter').has(e.target).length && $('.filter').hasClass('show')) {
            $('.filter').removeClass('show');
        }

        if (!$('.mini-cart').has(e.target).length && $('.mini-cart').hasClass('show')) {
            $('.mini-cart').removeClass('show');
        }
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

    function checkWishlistQuantity() {
        let qty = parseInt($('#wishlistQuantity').text());
        if (qty > 9) {
            let counter = $('#wishlistQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    }

    function checkCartQuantity() {
        let qty = parseInt($('#cartQuantity').text());
        if (qty > 9) {
            let counter = $('#cartQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    }

    $('.add-to-cart-btn, item-add-to-cart').click(function () {
        let qty = $('#productQuantity').val();
        if (!qty) qty = 1;

        $.ajax({
            url: '/Product/AddToCart',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
                qty: qty
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.error == true) {
                    alert(result.message);
                }
                else {
                    let newQty = parseInt($('#cartQuantity').text()) + parseInt(qty);
                    console.log(qty);
                    console.log(newQty);
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

    $('.add-to-wishlist-btn').click(function () {
        $.ajax({
            url: '/Product/AddToWishlist',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid')
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.error == true) {
                    alert(result.message);
                }
                else {
                    let newQty = parseInt($('#wishlistQuantity').text()) + 1;
                    if (newQty > 9) {
                        let counter = $('#wishlistQuantity').parent('.counter');
                        if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
                    }
                    $('#wishlistQuantity').text(newQty);
                    checkWishlistQuantity();
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
