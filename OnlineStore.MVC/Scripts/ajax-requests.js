$(document).ready(function () {

    const setAsyncTimeout = (fn, timeInMs) =>
        new Promise((resolve) => setTimeout(() => resolve(fn()), timeInMs))

    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
            );
    };

    const checkWishlistQuantity = () => {
        let qty = parseInt($('#wishlistQuantity').text());
        let counter = $('#wishlistQuantity').parent('.counter');
        if (qty > 9)
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        else
            if (counter.hasClass('two-counter')) counter.removeClass('two-counter');
    };

    const checkCartQuantity = () => {
        let qty = parseInt($('#cartQuantity').text());
        if (qty > 9) {
            let counter = $('#cartQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    };

    const showToast = async (headline, details) => {
        if (!headline || !details) return;

        if ($('#toastNotif').hasClass('active')) {
            $('#toastNotif .progress-line')
                .queue((next) => {
                    prepareToast(headline, details);
                    next();
                })
                .animate({ right: '100%' }, 3000)
                .queue(async (next) => {
                    await resetToast();
                    next();
                });
        } else {
            prepareToast(headline, details);
            $('#toastNotif .progress-line')
                .animate({ right: '100%' }, 3000)
                .queue(async (next) => {
                    await resetToast();
                    next();
                });
        }
    };

    const resetModal = (modal) => {
        $(modal).find('.errors-area').each((i, el) => {
            $(el).css('display', 'none');
            $(el).children('span').text('');
        });

        $(modal).find('input[type=radio], input[type=checkbox]').each((i, el) => $(el).prop('checked', false));
        $(modal).find('input, textarea').each((i, el) => $(el).val(''));
    }

    const productToCart = () => {
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
                    alert("An error occurred.");
                }
                else {
                    $.ajax({
                        url: '/cart/updateminicart',
                        success: function (data) {
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'The item has been added to your cart.');
                        }
                    });
                }
            }
        });
    }

    $('.actions .to-cart').click(productToCart);

    $('.products.main').on('click', '.to-cart', productToCart);

    $('.to-cart-from-wl').click(function () {
        let qtyControl = $(this).parent().find('.qty-value');
        console.log($(qtyControl));
        $.ajax({
            url: '/cart/add',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(qtyControl).data('productid'),
                quantity: $(qtyControl).val()
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
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'The item has been added to your cart.');
                        }
                    });
                }
            }
        });
    });

    $('.selected-to-cart').click(function () {
        let items = [];
        $('.products-list-line').each((i, el) => {
            if ($(el).find('.line-check input').is(':checked'))
                items.push({
                    productId: $(el).find('.qty-value').data('productid'),
                    quantity: $(el).find('.qty-value').val()
                });
        });

        $.ajax({
            url: '/cart/addrange',
            type: 'post',
            dataType: 'json',
            data:
            {
                items: items
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
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'Items have been added to your cart.');
                        }
                    });
                }
            }
        });
    });

    $('.remove-selected').click(function () {
        let itemIds = [];
        $('.products-list-line').each((i, el) => {
            if ($(el).find('.line-check input').is(':checked'))
                itemIds.push($(el).find('.qty-value').data('itemid'));    
        });

        $.ajax({
            url: '/wishlist/removerange',
            type: 'delete',
            dataType: 'json',
            data:
            {
                itemIds: itemIds,
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

    $('.qty-value.cart').change(function () {
        $.ajax({
            url: '/cart/update',
            type: 'put',
            dataType: 'json',
            data:
            {
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

    $('.qty-value.wl').change(function () {
        $.ajax({
            url: '/wishlist/update',
            type: 'put',
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
                        $('#userHub .mini-cart-component').replaceWith(data);
                        checkCartQuantity();
                    }
                });
            }
        });
    });

    const updWishlistSuccess = (productId, button) => {
        checkWishlistQuantity();

        $.ajax({
            url: '/wishlist/updatewishlistbutton',
            type: 'get',
            data:
            {
                productId,
                text: $(button).find('span').text()
            },
            success: function (data) {
                $(button).replaceWith(data);
            }
        });
    };

    const productToWishlist = (productId, button) => {
        $.ajax({
            url: '/wishlist/add',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId,
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
                    $('#wishlistQuantity').text(newQty);
                    updWishlistSuccess(productId, button);
                }
            }
        });
    };

    const itemFromWishlist = (itemId, productId, button) => {
        $.ajax({
            url: '/wishlist/remove',
            type: 'delete',
            dataType: 'json',
            data:
            {
                itemId
            },
            error: function () {
                alert('Error occurred.');
            },
            success: function (result) {
                if (result.success == false)
                    alert(result.errors.toString());
                else {
                    let newQty = parseInt($('#wishlistQuantity').text()) - 1;
                    $('#wishlistQuantity').text(newQty);
                    updWishlistSuccess(productId, button);
                }
            }
        });
    };

    $('.products.main').on('click', '.wishlist-btn button', function () {
        let button = $(this);
        let itemId = $(button).data('itemid');
        let productId = $(button).data('productid');

        if ($(button).hasClass('to-wl'))
            productToWishlist(productId, button);
        else
            itemFromWishlist(itemId, productId, button);
    });

    $('.to-wishlist').click(function () {
        let productId = $(this).data('itemid');
        let button = $(this).parent();

        $.ajax({
            url: '/wishlist/add',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId,
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
                    $('#wishlistQuantity').text(newQty);
                    checkWishlistQuantity();

                    $.ajax({
                        url: '/wishlist/updatewishlistbutton',
                        type: 'get',
                        data:
                        {
                            productId
                        },
                        success: function (data) {
                            $(button).html(data);
                        }
                    });
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

    $('#createReviewBtn').click(function () {
        let modal = $(this).closest('.modal-container');
        let rating = 0;
        $(modal).find('.review-rating input').each((i, el) => {
            if ($(el).is(':checked'))
                rating = $(el).prop('id').slice(6);
        });

        $.ajax({
            url: '/account/reviews/create-review',
            type: 'post',
            dataType: 'json',
            data:
            {
                OrderId: $(modal).find('.review-order').val(),
                ProductId: $(modal).find('.review-product').val(),
                Name: $(modal).find('.review-name').val(),
                Content: $(modal).find('.review-content').val(),
                Rating: rating
            },
            error: function (response) {
                response.responseJSON.data.forEach((el) => {
                    switch (el.key) {
                        case "Name":
                            $(modal).find('.review-name-error').css('display', 'block');
                            $(modal).find('.review-name-error span').text(el.errors);
                            break;
                        case "Content":
                            $(modal).find('.review-content-error').css('display', 'block');
                            $(modal).find('.review-content-error span').text(el.errors);
                            break;
                        default:
                            $(modal).find('.modal-error').css('display', 'block');
                            $(modal).find('.modal-error span').text(el.errors);
                            break;
                    }
                })
            },
            success: function (result) {
                if (result.success == true) {
                    $(modal).find('.modal-succes').css('display', 'block');
                    setTimeout(function () {
                        $(modal).find('.modal-succes').css('display', 'none');
                    }, 2000);

                    $(modal).removeClass('show');
                    resetModal($(modal));
                }
                else {
                    $(modal).find('.modal-error span').text(result.errors.toSting());
                    $(modal).find('.modal-error').css('display', 'block');
                }
            }
        });
    });

    $('#updateReviewBtn').click(function () {
        let modal = $(this).closest('.modal-container');
        let rating = 0;
        $(modal).find('.review-rating input').each((i, el) => {
            if ($(el).is(':checked'))
                rating = $(el).prop('id').slice(6);
        });

        $.ajax({
            url: '/account/reviews/update-review',
            type: 'put',
            dataType: 'json',
            data:
            {
                Id: $(modal).find('.review-identifier').val(),
                Content: $(modal).find('.review-content').val(),
                Rating: rating
            },
            error: function (response) {
                response.responseJSON.data.forEach((el) => {
                    switch (el.key) {
                        case "Content":
                            $(modal).find('.review-content-error').css('display', 'block');
                            $(modal).find('.review-content-error span').text(el.errors);
                            break;
                        default:
                            $(modal).find('.modal-error').css('display', 'block');
                            $(modal).find('.modal-error span').text(el.errors);
                            break;
                    }
                })
            },
            success: function (result) {
                if (result.success == true) {
                    $(modal).find('.modal-succes').css('display', 'block');
                    setTimeout(function () {
                        $(modal).find('.modal-succes').css('display', 'none');
                    }, 2000);

                    $(modal).removeClass('show');
                    resetModal($(modal));
                }
                else {
                    $(modal).find('.modal-error span').text(result.errors.toSting());
                    $(modal).find('.modal-error').css('display', 'block');
                }
            }
        });
    });
});