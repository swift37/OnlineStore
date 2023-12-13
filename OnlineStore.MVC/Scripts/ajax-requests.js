$(document).ready(function () {

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
                    $.ajax({
                        url: '/Product/UpdateMiniCart',
                        success: function (data) {
                            $("#miniCart").html(data);
                            checkCartQuantity();
                        }
                    });
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

    $('.qty-value').change(function () {
        $.ajax({
            url: '/Product/UpdateCart',
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
                if (result.error == true) {
                    alert(result.message);
                }
                else {
                    location.reload();
                }
            }
        });
    });

    $('.item-remove.from-cart').click(function () {
        $.ajax({
            url: '/Product/RemoveFromCart',
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

    $('.item-remove.from-minicart').click(function () {
        $.ajax({
            url: '/Product/RemoveFromCart',
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
                $.ajax({
                    url: '/Product/UpdateMiniCart',
                    success: function (data) {
                        $("#miniCart").html(data);
                        checkCartQuantity();
                    }
                });
            }
        });
    });

    $('.item-remove.from-wishlist').click(function () {
        $.ajax({
            url: '/Product/RemoveFromWishlist',
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

    function validateEmail(email) {
        return String(email)
            .toLowerCase()
            .match(
                /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            );
    };

    $('.subscribe-btn').click(function () {
        let input = $(this).parent().find('input');
        let email = input.val();
        let validatedEmail = validateEmail(email);
        if (!validatedEmail) return alert('Invalid email address.');

        $.ajax({
            url: '/Home/SubscribeToNewsletter',
            type: 'post',
            dataType: 'json',
            data:
            {
                email: validatedEmail
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.error == true)
                    alert(result.message);
                else
                    input.val('');
            }
        });
    });

    $('.contact-btn').click(function () {
        let name = $('#contactName').val();
        let email = $('#contactEmail').val();
        let message = $('#contactMessage').val();
        let validatedEmail = validateEmail(email);
        let error = $('#contactFormError span');
        $('#contactFormError').css('display', 'none');
        error.text('');

        if (!validatedEmail || !name || !message) {

            $('#contactFormError').css('display', 'block');
            return error.text('Invalid data.');
        }

        $.ajax({
            url: '/Home/SendContactRequest',
            type: 'post',
            dataType: 'json',
            data:
            {
                name: name,
                email: validatedEmail,
                message: message
            },
            error: function () {
                $('#contactFormError').css('display', 'block');
                error.text('Error occurred.');
            },
            success: function (result) {
                if (result.error == true) {
                    $('#contactFormError').css('display', 'block');
                    error.text(result.message);
                }
                else {
                    $('#contactName').val('');
                    $('#contactEmail').val('');
                    $('#contactMessage').val('');
                    $('#contactFormSuccess').css('display', 'block');
                    setTimeout(function () {
                        $('#contactFormSuccess').css('display', 'none');
                    }, 2000);
                }
            }
        });
    });
});