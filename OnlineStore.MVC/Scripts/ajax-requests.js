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
            url: '/cart/add',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
                quantity: qty
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.success == false) {
                    alert("An arror occurred.");
                }
                else {
                    $.ajax({
                        url: '/cart/updateminicart',
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
            url: '/home/subscribe',
            type: 'post',
            dataType: 'json',
            data:
            {
                Email: validatedEmail
            },
            error: function (response) {
                if (response.responseJSON.data[0].key === "Email")
                    $('#subscribeResponse').text(response.responseJSON.data[0].errors);
                else
                    $('#subscribeResponse').text("An error occurred.");
            },
            success: function (result) {
                if (result.success == false)
                    $('#subscribeResponse').text(result.errors);
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
        let errorName = $('#contactFormNameError span');
        let errorEmail = $('#contactFormEmailError span');
        let errorMessage = $('#contactFormMessageError span');

        $('#contactFormError').css('display', 'none');
        $('#contactFormNameError').css('display', 'none');
        $('#contactFormEmailError').css('display', 'none');
        $('#contactFormErrorError').css('display', 'none');
        error.text('');
        errorName.text('');
        errorEmail.text('');
        errorMessage.text('');

        if (!validatedEmail || !name || !message) {

            $('#contactFormError').css('display', 'block');
            return error.text('Invalid data.');
        }

        $.ajax({
            url: '/home/sendcontactrequest',
            type: 'post',
            dataType: 'json',
            data:
            {
                ContactName: name,
                Email: validatedEmail,
                Message: message
            },
            error: function (response) {
                response.responseJSON.data.forEach((el) => {
                    switch (el.key) {
                        case "ContactName":
                            $('#contactFormNameError').css('display', 'block');
                            errorName.text(el.errors);
                            break;
                        case "Email":
                            $('#contactFormEmailError').css('display', 'block');
                            errorEmail.text(el.errors);
                            break;
                        case "Message":
                            $('#contactFormMessageError').css('display', 'block');
                            errorMessage.text(el.errors);
                            break;
                        default:
                            $('#contactFormError').css('display', 'block');
                            error.text(el.errors);
                            break;
                    }
                })
            },
            success: function (result) {
                if (result.success == true) {
                    $('#contactName').val('');
                    $('#contactEmail').val('');
                    $('#contactMessage').val('');

                    $('#contactFormSuccess').css('display', 'block');
                    setTimeout(function () {
                        $('#contactFormSuccess').css('display', 'none');
                    }, 2000);
                }
                else {
                    $('#contactFormError').css('display', 'block');
                }
            }
        });
    });
});