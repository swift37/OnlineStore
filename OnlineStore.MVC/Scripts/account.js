$(document).ready(function () {
    $('.tab-switch li').click(function () {
        $(this).siblings().removeClass('active');
        $(this).addClass('active');

        let id = $(this).prop('id').slice(1);
        $('.tab-content').children().each((i, el) => {
            if (i == id)
                $(el).addClass('active');
            else
                $(el).removeClass('active');
        });
    });
});