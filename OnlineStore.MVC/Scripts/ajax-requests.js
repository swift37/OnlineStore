$(document).ready(function () {

    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            );
    };

    const checkWishlistQuantity = () => {
        let qty = parseInt($('#wishlistQuantity').text());
        if (qty > 9) {
            let counter = $('#wishlistQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    };

    const checkCartQuantity = () => {
        let qty = parseInt($('#cartQuantity').text());
        if (qty > 9) {
            let counter = $('#cartQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    };

    $('.add-to-cart-btn, .item-add-to-cart').click(function () {
        let qty = $('#productQuantity').val();
        if (!qty) qty = 1;

        $.ajax({
            url: '/cart/add',
            type: 'put',
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
                    alert("An error occurred.");
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

    $('.qty-value').change(function () {
        $.ajax({
            url: '/cart/update',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
                quantity: $(this).val()
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                if (result.success == true)
                    location.reload();
            }
        });
    });

    $('.qty-value.wl').change(function () {
        $.ajax({
            url: '/wishlist/update',
            type: 'post',
            dataType: 'json',
            data:
            {
                id: $(this).data('itemid'),
                productId: $(this).data('productid'),
                quantity: $(this).val()
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                if (result.success == true)
                    location.reload();
            }
        });
    });

    $('.item-remove.from-cart').click(function () {
        $.ajax({
            url: '/cart/remove',
            type: 'delete',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                if (result.success == true)
                    location.reload();
            }
        });
    });

    $('.item-remove.from-minicart').click(function () {
        $.ajax({
            url: '/cart/remove',
            type: 'delete',
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
                    url: '/cart/updateminicart',
                    success: function (data) {
                        $("#miniCart").html(data);
                        checkCartQuantity();
                    }
                });
            }
        });
    });

    $('.add-to-wishlist-btn').click(function () {
        $.ajax({
            url: '/wishlist/add',
            type: 'put',
            dataType: 'json',
            data:
            {
                productId: $(this).data('itemid'),
                quantity: 1
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.success == false)
                    alert(result.errors.toString());
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

    $('.item-remove.from-wishlist').click(function () {
        $.ajax({
            url: '/wishlist/remove',
            type: 'delete',
            dataType: 'json',
            data:
            {
                itemId: $(this).data('itemid'),
            },
            error: function () {
                alert('Error occured.');
            },
            success: function (result) {
                if (result.success == false)
                    alert(result.errors.toString());
                else
                    location.reload();
            }
        });
    });

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
                    error.text(result.errors.toSting());
                    $('#contactFormError').css('display', 'block');
                }
            }
        });
    });

    $('.review-btn').click(function () {
        let error = $('#reviewError span');
        let titleError = $('#reviewTitleError span');
        let messageError = $('#reviewMessageError span');

        $('#reviewError').css('display', 'none');
        $('#reviewTitleError').css('display', 'none');
        $('#reviewMessageError').css('display', 'none');
        error.text('');
        titleError.text('');
        messageError.text('');

        let rating = 0;
        $('#reviewRating').children('input').each((i, el) => {
            if ($(el).prop('checked'))
                rating = $(el).prop('id').slice(4);
        });

        $.ajax({
            url: '/catalog/createreview',
            type: 'post',
            dataType: 'json',
            data:
            {
                ProductId: $('#reviewProduct').val(),
                Title: $('#reviewTitle').val(),
                Content: $('#reviewMessage').val(),
                Rating: rating
            },
            error: function (response) {
                response.responseJSON.data.forEach((el) => {
                    switch (el.key) {
                        case "Title":
                            $('#reviewTitleError').css('display', 'block');
                            errorEmail.text(el.errors);
                            break;
                        case "Content":
                            $('#reviewMessageError').css('display', 'block');
                            errorMessage.text(el.errors);
                            break;
                        default:
                            $('#reviewError').css('display', 'block');
                            error.text(el.errors);
                            break;
                    }
                })
            },
            success: function (result) {
                if (result.success == true) {
                    $('#reviewTitle').val('');
                    $('#reviewMessage').val('');
                    $('#reviewRating').children('input').each((i, el) =>
                        $(el).prop('checked', false));

                    $('#reviewSuccess').css('display', 'block');
                    setTimeout(function () {
                        $('#reviewSuccess').css('display', 'none');
                    }, 2000);
                }
                else {
                    error.text(result.errors.toSting());
                    $('#reviewError').css('display', 'block');
                }
            }
        });
    });
});