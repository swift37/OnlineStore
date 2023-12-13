$(document).ready(function () {

    checkCartQuantity();

    checkWishlistQuantity();

    copyMenu();

    setSortBySlValue();

    setItemsPerPageSlValue();

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
        if ($(this).hasClass('expand')) {
            $(this).removeClass('expand');
        }
        else {
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
    };

    function checkCartQuantity() {
        let qty = parseInt($('#cartQuantity').text());
        if (qty > 9) {
            let counter = $('#cartQuantity').parent('.counter');
            if (!counter.hasClass('two-counter')) counter.addClass('two-counter');
        }
    };

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
        } else {
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



    function setSortBySlValue() {
        let url = new window.URL(document.location);
        let param = url.searchParams.get("sortBy");
        console.log(param);
        if (param != null) {
            var value = $('.item-sortir').find('[data-option="' + param + '"]').text();
            $('.item-sortir .selected-value').text(value);
        }
    }

    function setItemsPerPageSlValue() {
        let url = new window.URL(document.location);
        let param = url.searchParams.get("itemsperpage");
        if (param != null)
            $('.item-options .selected-value').text(param);
    }


    $('.item-sortir li').click(function () {
        let sortParam = $(this).data('option');
        if (!sortParam) sortParam = 0;

        let url = new window.URL(document.location);
        url.searchParams.set("sortBy", sortParam);
        location.replace(url);
    });

    $('.item-options li').click(function () {
        let itemsPerPage = $(this).data('option');
        if (!itemsPerPage) itemsPerPage = 15;

        let url = new window.URL(document.location);
        url.searchParams.set("itemsperpage", itemsPerPage);
        location.replace(url);
    });

    $('#termsCheckbox').click(function () {
        if ($(this).is(':checked')) {
            $('#checkoutBtn').removeAttr('disabled');
        }
        else {
            $('#checkoutBtn').attr('disabled', true);
        }
    });

});
