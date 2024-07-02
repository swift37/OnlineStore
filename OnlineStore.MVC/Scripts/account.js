$(document).ready(function () {
    $('.tab-switch li').click(function () {
        $(this).siblings().removeClass('active');
        $(this).addClass('active');

        let id = $(this).prop('id').slice(1);

        if (id == 0 && $('.form-buttons').hasClass('active'))
            $(this).closest('.content-box').addClass('changed');
        else
            $(this).closest('.content-box').removeClass('changed');

        $('.tab-content').children().each((i, el) => {
            if (i == id)
                $(el).addClass('active');
            else
                $(el).removeClass('active');
        });
    });

    $('.tab form input').change(function () {
        $(this).closest('.content-box').addClass('changed');
        $('.form-buttons').addClass('active');
    });

    $('.tab form button').click(function () {
        $(this).closest('.content-box').removeClass('changed');
        $(this).parent().removeClass('active');
    });
});