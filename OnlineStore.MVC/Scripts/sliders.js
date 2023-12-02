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

var swiper = new Swiper(".products-slider", {
    slidesPerView: 4,
    spaceBetween: 15,
    centerSlide: true,
    grabCursor: true,
    speed: 500,
    navigation: {
        nextEl: "#ftProductsSlNext",
        prevEl: "#ftProductsSlPrev"
    },
    breakpoints: {
        0: {
            slidesPerView: 1,
        },
        768: {
            slidesPerView: 2,
        },
        1080: {
            slidesPerView: 3,
        },
        1430: {
            slidesPerView: 4,
        }
    }
});

var swiper = new Swiper(".events-slider", {
    slidesPerView: 3,
    spaceBetween: 15,
    grabCursor: true,
    speed: 500,
    navigation: {
        nextEl: "#eventsSlNext",
        prevEl: "#eventsSlPrev"
    },
    breakpoints: {
        0: {
            slidesPerView: 1,
        },
        768: {
            slidesPerView: 2,
        },
        991: {
            slidesPerView: 3,
        }
    }
});