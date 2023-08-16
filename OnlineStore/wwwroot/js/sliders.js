var swiper = new Swiper(".main-slider", {
    scrollbar: {
        el: ".swiper-scrollbar",
        hide: true,
    },
    autoplay: {
        delay: 10000,
        disableOnInteraction: false,
    },
    loop: true,
    speed: 500
});

var swiper = new Swiper(".events-slider", {
    slidesPerView: 3,
    spaceBetween: 15,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    }
});