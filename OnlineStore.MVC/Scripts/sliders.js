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

var swiper = new Swiper(".brands-slider", {
    slidesPerView: 7,
    spaceBetween: 50,
    grabCursor: true,
    speed: 500,
    navigation: {
        nextEl: "#brandsSlNext",
        prevEl: "#brandsSlPrev"
    },
    breakpoints: {
        0: {
            slidesPerView: 1,
        },
        440: {
            slidesPerView: 2,
        },
        600: {
            slidesPerView: 3,
        },
        760: {
            slidesPerView: 4,
        },
        960: {
            slidesPerView: 5,
        },
        1160: {
            slidesPerView: 6,
        },
        1360: {
            slidesPerView: 7,
        }
    }
});