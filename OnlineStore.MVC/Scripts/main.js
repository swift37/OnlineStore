$(document).ready(function () {

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

    checkCartQuantity();

    checkWishlistQuantity();

    $('#userHub').on('click', '.side-menu-component .component-trigger', function () {
        setTimeout(function () {
            $('#userHub .side-menu-component .site-off').addClass('show-menu');
        }, 250);
    });

    $('#userHub').on('click', '.side-menu-component .sm-close', function () {
        $('#userHub .side-menu-component .site-off').removeClass('show-menu');
    })

    $('#userHub').on('click', '.side-menu-component .expand-btn', function () {
        let item = $(this).parents('.link-item');
        $(item).siblings().removeClass('expand');
        $(item).hasClass('expand') ? $(item).removeClass('expand') : $(item).addClass('expand');
    });

    $('#userHub').on('click', '.mini-cart-component .component-trigger', function () {
        setTimeout(function () {
            $('#userHub .mini-cart-component .mini-cart').addClass('show');
        }, 250);
    });

    $('#userHub').on('click', '.mini-cart-component .mc-close', function () {
        $('#userHub .mini-cart-component .mini-cart').removeClass('show');
    })

    $('#userHub').on('click', '.account-component .component-trigger', function () {
        setTimeout(function () {
            $('#userHub .account-component .dropdown-menu').addClass('show');
        }, 250);
    });

    $('#userHub').on('click', '.account-component .ac-close', function () {
        $('#userHub .account-component .dropdown-menu').removeClass('show');
    })

    $(document).click(function (e) {
        if (!$('.site-off').has(e.target).length && $('.site-off').hasClass('show-menu'))
            $('.site-off').removeClass('show-menu');

        if (!$('.filter').has(e.target).length && $('.filter').hasClass('show'))
            $('.filter').removeClass('show');

        if (!$('.mini-cart-component .mini-cart').has(e.target).length && $('.mini-cart-component .mini-cart').hasClass('show'))
            $('.mini-cart-component .mini-cart').removeClass('show');

        if (!$('.account-component .dropdown-menu').has(e.target).length && $('.account-component .dropdown-menu').hasClass('show'))
            $('.account-component .dropdown-menu').removeClass('show');
    });

    $('.filter-trigger').click(function () {
        setTimeout(function () {
            $('.filter').addClass('show');
            console.log('add');
        }, 250);
    });

    $('.qty-minus, .qty-plus').click(function () {
        let qty = $(this).parent().find('input');
        let oldValue = $(this).parent().find('input').val();
        if ($(this).hasClass('qty-plus'))
            $(qty).val(++oldValue);
        else
            $(qty).val(--oldValue);
        if ($(qty).val() < 1 || $(qty).val() > 999) $(qty).val(1);
        qty.trigger('change');
    });





    let currentStep = 0;

    const updateBtn = () => {
        if (currentStep === 4) {
            $('#endBtn').prop('disabled', true);
            $('#nextBtn').prop('disabled', true);
            $('#startBtn').prop('disabled', false);
            $('#prevBtn').prop('disabled', false);
        }
        else if (currentStep === 0) {
            $('#startBtn').prop('disabled', true);
            $('#prevBtn').prop('disabled', true);
            $('#endBtn').prop('disabled', false);
            $('#nextBtn').prop('disabled', false);
        }
        else {
            $('#endBtn').prop('disabled', false);
            $('#nextBtn').prop('disabled', false);
            $('#startBtn').prop('disabled', false);
            $('#prevBtn').prop('disabled', false);
        }
    };

    $.each($('.page-link'), function (index) {
        $(this).click(function (e) {
            e.preventDefault();
            currentStep = index;
            $(this).siblings().removeClass('active');
            $(this).addClass('active');
            updateBtn();
        });
    });

    $('.prevNext').click(function () {
        currentStep += $(this).attr('id') === "nextBtn" ? 1 : -1;
        $('.page-link').each(function (index) {
            console.log(index, currentStep);
            $(this).toggleClass('active', index === currentStep);
            updateBtn();
        });
    });

    $('#startBtn').click(function () {
        $('.page-link').first().siblings().removeClass('active');
        $('.page-link').first().addClass('active');
        currentStep = 0;
        updateBtn();
    });

    $('#endBtn').click(function () {
        $('.page-link').last().siblings().removeClass('active');
        $('.page-link').last().addClass('active');
        currentStep = 4;
        updateBtn();
    });



    

    $('#allItems').change(function () {
        if ($(this).is(':checked'))
            $('.line-check input').prop('checked', true);
        else
            $('.line-check input').prop('checked', false);
    });

    const resetModal = (modal) => {
        $(modal).find('.errors-area').each((i, el) => {
            $(el).css('display', 'none');
            $(el).children('span').text('');
        });

        $(modal).find('input[type=radio], input[type=checkbox]').each((i, el) => $(el).prop('checked', false));
        $(modal).find('input, textarea').each((i, el) => $(el).val(''));
    }

    $('.create-review-btn').click(function () {
        let modal = $('#createReviewModal');
        resetModal($(modal));
        $(modal).find('.review-order').val($(this).data('orderid'));
        $(modal).find('.review-product').val($(this).data('productid'));
        $(modal).addClass('show');
    });
    
    $('.update-review-btn').click(function () {
        let modal = $('#updateReviewModal');
        resetModal($(modal));
        $(modal).find('.review-identifier').val($(this).data('reviewid'));
        $(modal).find('.review-rating input').each((i, el) => {
            if ($(el).prop('id').slice(6) == $(this).data('rating'))
                $(el).prop('checked', true);
        });
        $(modal).find('.review-content').val($(this).closest('.reviewed').find('.full-review').text());
        $(modal).addClass('show');
    });

    $('.close-modal').click(function () {
        let modal = $(this).closest('.modal-container');
        $(modal).removeClass('show');
        resetModal($(modal));
    });

    $('.more').click(function () {
        $(this).hide();
        $(this).closest('.short-review').hide();
        $(this).closest('.full-review').show();
    });

    $('.pswd-hide').click(function () {
        let field = $(this).parent().children('input');
        if ($(field).prop('type') === 'password') {
            $(field).prop('type', 'text');
            $(this).removeClass('bx-hide').addClass('bx-show');
        }
        else {
            $(field).prop('type', 'password');
            $(this).removeClass('bx-show').addClass('bx-hide');
        }
    });
});
