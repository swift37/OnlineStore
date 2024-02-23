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

    const prepareToast = (headline, details, isFail) => {
        if (isFail)
            $('#toastNotif .t-icon').removeClass('bx-check').addClass('bx-x');
        else
            $('#toastNotif .t-icon').removeClass('bx-x').addClass('bx-check');

        $('#toastNotif .headline').text(headline);
        $('#toastNotif .details').text(details);
        $('#toastNotif').addClass('active');
    };

    const resetToast = () => {
        $('#toastNotif').removeClass('active');
        return setAsyncTimeout(() => {
            $('#toastNotif .progress-line').removeAttr('style');
            $('#toastNotif .headline').text('');
            $('#toastNotif .details').text('');
        }, 500);
    };

    const showToast = async (headline, details, isFail) => {
        if (!headline || !details) return;

        if ($('#toastNotif').hasClass('active')) {
            $('#toastNotif .progress-line')
                .queue((next) => {
                    prepareToast(headline, details, isFail);
                    next();
                })
                .animate({ right: '100%' }, 3000)
                .queue(async (next) => {
                    await resetToast();
                    next();
                });
        } else {
            prepareToast(headline, details, isFail);
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
            $(el).removeClass('visible');
            $(el).children('span').text('');
        });

        $(modal).find('input[type=radio], input[type=checkbox]').each((i, el) => $(el).prop('checked', false));
        $(modal).find('input, textarea').each((i, el) => $(el).val(''));
    }

    const productToCart = (e) => {
        let qty = $('#productQuantity').val();
        if (!qty) qty = 1;

        $.ajax({
            url: '/cart/add',
            type: 'post',
            dataType: 'json',
            data:
            {
                productId: $(e.currentTarget).data('itemid'),
                quantity: qty
            },
            error: function () {
                showToast('Failure', 'An error occurred while adding an item to your cart', true);
            },
            success: function (result) {
                if (result.success == false) {
                    showToast('Failure', 'An error occurred while adding an item to your cart', true);
                }
                else {
                    $.ajax({
                        url: '/cart/updateminicart',
                        success: function (data) {
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'The item has been added to your cart');
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
                showToast('Failure', 'An error occurred while adding an item to your cart', true);
            },
            success: function (result) {
                if (result.success == false) {
                    showToast('Failure', 'An error occurred while adding an item to your cart', true);
                }
                else {
                    $.ajax({
                        url: '/cart/updateminicart',
                        success: function (data) {
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'The item has been added to your cart');
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
                showToast('Failure', 'An error occurred while adding items to your cart', true);
            },
            success: function (result) {
                if (result.success == false) {
                    showToast('Failure', 'An error occurred while adding items to your cart', true);
                }
                else {
                    $.ajax({
                        url: '/cart/updateminicart',
                        success: function (data) {
                            $('#userHub .mini-cart-component').replaceWith(data);
                            checkCartQuantity();

                            showToast('Success', 'Items have been added to your cart');
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
                    showToast('Success', 'The item has been added to your wishlist');
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
                showToast('Failure', 'An error occurred while adding an item to your wishlist', true);
            },
            success: function (result) {
                if (result.success == false)
                    showToast('Failure', result.errors.toString(), true);
                else {
                    let newQty = parseInt($('#wishlistQuantity').text()) + 1;
                    $('#wishlistQuantity').text(newQty);
                    checkWishlistQuantity();
                    showToast('Success', 'The item has been added to your wishlist');

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
        if (!validatedEmail)
            return showToast('Failure', 'Invalid email address', true);

        $.ajax({
            url: '/home/subscribe',
            type: 'post',
            data:
            {
                Email: validatedEmail
            },
            error: function (response) {
                showToast('Failure', response.responseJSON.errors[0].errorMessage, true);
            },
            success: function (result) {
                input.val('');
                showToast('Success', 'Newsletter subscription has been succeeded');
            }
        });
    });

    $('.contact-btn').click(function () {
        let name = $('#contactName').val();
        let email = $('#contactEmail').val();
        let message = $('#contactMessage').val();
        let validatedEmail = validateEmail(email);

        let errorAreas = $(this).parent().find('.errors-area');
        let fields = $(this).parent().find('input, textarea');

        errorAreas.each((i, el) => {
            $(el).removeClass('visible');
            $(el).children('span').text('');
        });

        if (!validatedEmail || !name || !message)
            return showToast('Failure', 'Invalid data has been entered', true);

        $.ajax({
            url: '/home/sendcontactrequest',
            type: 'post',
            data:
            {
                ContactName: name,
                Email: validatedEmail,
                Message: message
            },
            error: function (response) {
                response.responseJSON.errors.forEach((el) => {
                    switch (el.propertyName) {
                        case "ContactName":
                            $(errorAreas[0]).addClass('visible');
                            $(errorAreas[0]).children('span').text(el.errorMessage);
                            break;
                        case "Email":
                            $(errorAreas[1]).addClass('visible');
                            $(errorAreas[1]).children('span').text(el.errorMessage);
                            break;
                        case "Message":
                            $(errorAreas[2]).addClass('visible');
                            $(errorAreas[2]).children('span').text(el.errorMessage);
                            break;
                        default:
                            showToast('Failure', el.errorMessage, true);
                            break;
                    }
                })
            },
            success: function () {
                fields.each((i, el) => $(el).val(''));
                showToast('Success', 'Your contact request has been sent successfully');
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
            data:
            {
                OrderId: $(modal).find('.review-order').val(),
                ProductId: $(modal).find('.review-product').val(),
                Name: $(modal).find('.review-name').val(),
                Content: $(modal).find('.review-content').val(),
                Rating: rating
            },
            error: function (response) {
                response.responseJSON.errors.forEach((el) => {
                    switch (el.propertyName) {
                        case "Name":
                            $(modal).find('.review-name-error').addClass('visible');
                            $(modal).find('.review-name-error span').text(el.errorMessage);
                            break;
                        case "Content":
                            $(modal).find('.review-content-error').addClass('visible');
                            $(modal).find('.review-content-error span').text(el.errorMessage);
                            break;
                        default:
                            showToast('Failure', el.errorMessage, true);
                            break;
                    }
                })
            },
            success: function () {
                $(modal).removeClass('show');
                resetModal($(modal));
                showToast('Success', 'Your review has been posted successfully');
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
            data:
            {
                Id: $(modal).find('.review-identifier').val(),
                Content: $(modal).find('.review-content').val(),
                Rating: rating
            },
            error: function (response) {
                response.responseJSON.errors.forEach((el) => {
                    switch (el.propertyName) {
                        case "Content":
                            $(modal).find('.review-content-error').addClass('visible');
                            $(modal).find('.review-content-error span').text(el.errorMessage);
                            break;
                        default:
                            showToast('Failure', el.errorMessage, true);
                            break;
                    }
                })
            },
            success: function () {
                $(modal).removeClass('show');
                resetModal($(modal));
                showToast('Success', 'Your review has been changed successfully');
            }
        });
    });
});