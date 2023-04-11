var $ = document.querySelector.bind(document)
var $$ = document.querySelectorAll.bind(document)
//var idphim = document.getElementById('idphim').getAttribute("data-my-variable");
//var anhphim = ('https://image.tmdb.org/t/p/w500') + "" + document.getElementById('urlphim').getAttribute("data-my-variable");
//var tenphim = document.getElementById('tenphim').getAttribute("data-my-variable");
//var xemphim = ('https://2embed.org/embed/movie?tmdb=') + "" + idphim;
document.cookie = "cookie_name=cookie_value; SameSite=None; secure";


// Header
const header = $('.header')
window.onscroll = function () {
    header.classList.toggle('black', window.scrollY > 0)
    $('.backtop-btn').classList.toggle('visibility', window.scrollY > screen.availHeight - header.offsetHeight)
}

// Navbar
const navBtn = $('.navbar_btn')
const navOverlay = $('.navbar_overlay')
const navBlock = $('.navbar_block')
const navClose = $('.navbar_close')
const navTitles = $$('.navbar_menu-item-title')

navBtn.onclick = () => {
    navOverlay.style.display = 'block'
    navBlock.classList.add('active')

    navClose.onclick = () => {
        navOverlay.style.display = 'none'
        navBlock.classList.remove('active')
    }
    navOverlay.onclick = () => {
        navOverlay.style.display = 'none'
        navBlock.classList.remove('active')
    }
}

navTitles.forEach((navTitle) => {
    navTitle.onclick = function () {
        this.classList.toggle('active')
    }
})

// Tab
const tabs = $$('.header_tab-item');
const panes = $$('.header_tab-pane');
const tabActive = $('.header_tab-item.active');
const tabLine = $('.header_tabs .line');

tabLine.style.width = tabActive.offsetWidth + 'px'
tabLine.style.left = tabActive.offsetLeft + 'px'
tabs.forEach((tab, index) => {
    const pane = panes[index];
    tab.onclick = function () {
        $('.header_tab-item.active').classList.remove('active')
        $('.header_tab-pane.active').classList.remove('active')
        tabLine.style.width = this.offsetWidth + 'px'
        tabLine.style.left = this.offsetLeft + 'px'
        this.classList.add('active')
        pane.classList.add('active')
    }
})

// Search
const search = $('.header_search')

$('.header_search-input').onclick = () => {
    search.classList.add('active')
    $('.header_search-overlay').onclick = function () {
        search.classList.remove('active')
    }
}

$('.header_icon-search > .header_icon').onclick = () => {
    search.classList.add('active')
    $('.header_search-overlay').onclick = function () {
        search.classList.remove('active')
    }
}

// Billboard
const billboardVideoWrap = $('.billboard_backdrop-video')
const billboardVideo = $('.billboard_backdrop-video > video')
const billboardControl = $('.billboard_content-control-btn')

function billboardVideoPlay() {
    billboardVideoWrap.style.display = 'block'
    billboardControl.onclick = function () {
        if ($('.billboard_content-control-btn > i.mute.active')) {
            billboardVideo.muted = '';
            $('.billboard_content-control-btn > i.mute.active').classList.remove('active');
            $('.billboard_content-control-btn > i.unmute').classList.add('active');
        } else
            if ($('.billboard_content-control-btn > i.unmute.active')) {
                billboardVideo.muted = 'muted'
                $('.billboard_content-control-btn > i.unmute.active').classList.remove('active');
                $('.billboard_content-control-btn > i.mute').classList.add('active');
            }
    }
}

billboardVideo.onplay = billboardVideoPlay()

billboardVideo.onended = function billboardVideoEnded() {
    billboardVideoWrap.style.display = 'none'
    if ($('.billboard_content-control-btn > i.mute.active')) {
        $('.billboard_content-control-btn > i.mute.active').classList.remove('active');
    }
    if ($('.billboard_content-control-btn > i.unmute.active')) {
        $('.billboard_content-control-btn > i.unmute.active').classList.remove('active');
    }
    $('.billboard_content-control-btn > i.reload').classList.add('active')
    if ($('.billboard_content-control-btn > i.reload.active')) {
        billboardControl.onclick = function () {
            $('.billboard_content-control-btn > i.reload.active').classList.remove('active')
            $('.billboard_content-control-btn > i.unmute').classList.add('active');
            billboardVideo.play()
            billboardVideo.muted = ''
            billboardVideoPlay()
        }
    }
}

// Modal
function joinVipModal() {
    const registerVip = [$('.header_tagvip-btn'), $('.container_banner')]
    const modal = $('#modal-join-vip')
    const modalContainer = $('#modal-join-vip .modal-container')
    const modalClose = $('#modal-join-vip .modal_close')
    const modalPackages = $$('.modal_package')

    function showModal() {
        modal.classList.add('open')
    }

    function hideModal() {
        modal.classList.remove('open')
    }

    for (const item of registerVip) {
        item.onclick = showModal
    }

    modalClose.onclick = hideModal

    modal.onclick = hideModal

    modalContainer.onclick = function (event) {
        event.stopPropagation()
    }

    modalPackages.forEach((modalPackage, index) => {
        modalPackage.onclick = function () {
            $('.modal_package.active').classList.remove('active')
            this.classList.add('active')
        }
    })
}

function ShowVip() {
    document.getElementById('vippc').onclick = () => {
        //  this.classList.add("header_tagvip-btn")
    }
}

setTimeout(joinVipModal, 2000)

function downAppModal() {
    const downBtn = $('.header_icon-wrap:nth-child(3) .header_icon')
    const modal = $('#modal-down-app')
    const modalContainer = $('#modal-down-app .modal-container')
    const modalClose = $('#modal-down-app .modal_close')

    function showModal() {
        modal.classList.add('open')
    }

    function hideModal() {
        modal.classList.remove('open')
    }

    downBtn.onclick = showModal

    modalClose.onclick = hideModal

    modal.onclick = hideModal

    modalContainer.onclick = function (event) {
        event.stopPropagation()
    }
}
downAppModal()

var elements = document.querySelectorAll('span[data-value]');
const movieList = [];
let i = 0;
let j = 1;
while (i < elements.length) {
    const id = j;
    const name = elements[i + 1].dataset.value;
    const category = elements[i + 1].dataset.value;
    const update = "1";
    const imgMin = ('https://image.tmdb.org/t/p/w500') + "" + (elements[i + 2].dataset.value);
    const imgMax = ('https://image.tmdb.org/t/p/w500') + "" + (elements[i + 2].dataset.value);
    const video = ('https://2embed.org/embed/movie?tmdb=') + "" + elements[i].dataset.value;

    const movies = {
        id,
        name,
        category,
        update,
        imgMin,
        imgMax,
        video
    };
    movieList.push(movies);
    i += 3;
    j += 1;
}
    // Movies List


    // Carousel 
    function sliderStyle1(options) {
        const carouselMove = $(options.carouselMoveSelector)
        const carouselWidth = $(options.carouselSelector).offsetWidth
        const prevBtn = $(options.prevBtnSelector)
        const nextBtn = $(options.nextBtnSelector)
        prevBtn.style.display = 'none'

        function render() {

            const htmls = options.movies.map((movie, index) => {
                return `
            <div id="${movie.id}" class="carousel_item col l-2 m-3 c-4">
                    <div class="carousel_item-images">
                        <div class="carousel_img-min">
                            <img src="${movie.imgMin}" alt="">
                            <div class="carousel_img-min-overlay carousel_img-overlay">
                                <span>${movie.update} tập</span>
                            </div>
                        </div>
                        <div class="carousel_img-max">
                            <img src="${movie.imgMax}" alt="">
                            <div class="carousel_img-max-overlay carousel_img-overlay">
                                <span><span>Thể loại:</span> ${movie.category}</span>
                                <div class="btns-play-and-add">
                                    <span class="btn-play">
                                        <i class="fas fa-play"></i>
                                    </span>
                                    <span class="btn-add">
                                        <i class="far fa-bookmark">
                                            <i class="fas fa-plus"></i>
                                        </i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="carousel_item-title">
                        <p>${movie.name}</p>
                    </div>
                </div>
            `
            })
            carouselMove.innerHTML = htmls.join('')

        }
        render()

        const carouselItems = $$(options.carouselItemsSelector)
        const carouselMoveQuantity = Math.round(carouselItems.length / (carouselMove.getBoundingClientRect().width / carouselItems[0].getBoundingClientRect().width))
        var l = 0
        nextBtn.onclick = () => {
            prevBtn.style.display = 'block'
            l++;
            if (l < carouselMoveQuantity) {
                carouselMove.style.transform = `translateX(calc(0px - ${carouselWidth}px * ${l}))`
                if (l == carouselMoveQuantity - 1) {
                    nextBtn.style.display = 'none'
                }
            } else {
                l = carouselMoveQuantity - 1
                nextBtn.style.display = 'none'
            }
        }

        prevBtn.onclick = () => {
            nextBtn.style.display = 'block'
            l--;
            if (l >= 0) {
                carouselMove.style.transform = `translateX(calc(0px - ${carouselWidth}px * ${l}))`
                if (l == 0) {
                    prevBtn.style.display = 'none'
                }
            } else {
                l = 0
                prevBtn.style.display = 'none'
            }
        }



        for (let i = 1; i <= carouselItems.length; i++) {
            if ((i + 1) % 6 == 0) {
                carouselItems[i].onmouseover = function () {
                    carouselItems[i].style.transform = `translateX(-${carouselItems[i].offsetWidth / 2}px)`
                    carouselItems[i - 1].style.opacity = '0'
                }
                carouselItems[i].onmouseout = function () {
                    carouselItems[i].style.transform = 'translateX(0)'
                    carouselItems[i - 1].style.opacity = '1'
                }
            }
        }

    }
sliderStyle1({
    movies: movieList.slice(0, 20),
    carouselSelector: '#carousel-1',
    carouselMoveSelector: '#carousel-1 .carousel-move',
    prevBtnSelector: '#carousel-1 .carousel_btn-prev',
    nextBtnSelector: '#carousel-1 .carousel_btn-next',
    carouselItemsSelector: '#carousel-1 .carousel_item'
});


    sliderStyle1({
        movies: [
            movieList[40],
            movieList[41],
            movieList[42],
            movieList[43],
            movieList[44],
            movieList[45],
            movieList[46],
            movieList[47],
            movieList[48],
            movieList[49],
            movieList[50],
            movieList[51],
            movieList[52],
            movieList[53],
            movieList[54],
            movieList[55],
            movieList[56],
            movieList[57],
            movieList[58],
            movieList[59]
        ],
        carouselSelector: '#carousel-2',
        carouselMoveSelector: '#carousel-2 .carousel-move',
        prevBtnSelector: '#carousel-2 .carousel_btn-prev',
        nextBtnSelector: '#carousel-2 .carousel_btn-next',
        carouselItemsSelector: '#carousel-2 .carousel_item'
    })

    sliderStyle1({
        movies: [
            movieList[20],
            movieList[21],
            movieList[22],
            movieList[23],
            movieList[24],
            movieList[25],
            movieList[26],
            movieList[27],
            movieList[28],
            movieList[29],
            movieList[30],
            movieList[31],
            movieList[32],
            movieList[33],
            movieList[34],
            movieList[35],
            movieList[36],
            movieList[37],
            movieList[38],
            movieList[39]
        ],
        carouselSelector: '#carousel-4',
        carouselMoveSelector: '#carousel-4 .carousel-move',
        prevBtnSelector: '#carousel-4 .carousel_btn-prev',
        nextBtnSelector: '#carousel-4 .carousel_btn-next',
        carouselItemsSelector: '#carousel-4 .carousel_item'
    })

    sliderStyle1({
        movies: [
            movieList[60],
            movieList[61],
            movieList[62],
            movieList[63],
            movieList[64],
            movieList[65],
            movieList[66],
            movieList[67],
            movieList[68],
            movieList[69],
            movieList[70],
            movieList[71],
            movieList[72],
            movieList[73],
            movieList[74],
            movieList[75],
            movieList[76],
            movieList[77],
            movieList[78],
            movieList[79]
        ],
        carouselSelector: '#carousel-6',
        carouselMoveSelector: '#carousel-6 .carousel-move',
        prevBtnSelector: '#carousel-6 .carousel_btn-prev',
        nextBtnSelector: '#carousel-6 .carousel_btn-next',
        carouselItemsSelector: '#carousel-6 .carousel_item'
    })

    sliderStyle1({
        movies: [
            movieList[4],
            movieList[18],
            movieList[19],
            movieList[20],
            movieList[21],
            movieList[26],
            movieList[29],
            movieList[30],
            movieList[31],
            movieList[33],
            movieList[36],
            movieList[38],
            movieList[43],
            movieList[53],
            movieList[56],
            movieList[59],
            movieList[61],
            movieList[64],
            movieList[66],
            movieList[76]
        ],
        carouselSelector: '#carousel-7',
        carouselMoveSelector: '#carousel-7 .carousel-move',
        prevBtnSelector: '#carousel-7 .carousel_btn-prev',
        nextBtnSelector: '#carousel-7 .carousel_btn-next',
        carouselItemsSelector: '#carousel-7 .carousel_item'
    })

    sliderStyle1({
        movies: [
            movieList[0],
            movieList[5],
            movieList[7],
            movieList[11],
            movieList[12],
            movieList[13],
            movieList[14],
            movieList[16],
            movieList[30],
            movieList[35],
            movieList[36],
            movieList[40],
            movieList[41],
            movieList[50],
            movieList[58],
            movieList[51],
            movieList[66],
            movieList[69],
            movieList[71],
            movieList[77]
        ],
        carouselSelector: '#carousel-8',
        carouselMoveSelector: '#carousel-8 .carousel-move',
        prevBtnSelector: '#carousel-8 .carousel_btn-prev',
        nextBtnSelector: '#carousel-8 .carousel_btn-next',
        carouselItemsSelector: '#carousel-8 .carousel_item'
    })

    sliderStyle1({
        movies: [
            movieList[6],
            movieList[13],
            movieList[15],
            movieList[16],
            movieList[24],
            movieList[27],
            movieList[32],
            movieList[36],
            movieList[39],
            movieList[45],
            movieList[52],
            movieList[54],
            movieList[57],
            movieList[58],
            movieList[59],
            movieList[60],
            movieList[64],
            movieList[65],
            movieList[68],
            movieList[74]
        ],
        carouselSelector: '#carousel-9',
        carouselMoveSelector: '#carousel-9 .carousel-move',
        prevBtnSelector: '#carousel-9 .carousel_btn-prev',
        nextBtnSelector: '#carousel-9 .carousel_btn-next',
        carouselItemsSelector: '#carousel-9 .carousel_item'
    })

    function sliderStyle2(options) {
        const carouselMove = $(options.carouselMoveSelector)
        const carouselWidth = $(options.carouselSelector).offsetWidth
        const prevBtn = $(options.prevBtnSelector)
        const nextBtn = $(options.nextBtnSelector)
        prevBtn.style.display = 'none'

        if ($(`${options.carouselSelector}.carousel-rank`)) {
            function render() {
                const htmls = options.movies.map((movie, index) => {
                    return `
                    <div class="carousel_item col l-3 m-4 c-6">
                        <div class="carousel_rank-img">
                            <img src="${movie.img}" alt="">
                            <div class="carousel_rank-overlay">
                                <div class="btns-play-and-add">
                                    <span class="btn-play">
                                        <i class="fas fa-play"></i>
                                    </span>
                                    <span class="btn-add">
                                        <i class="far fa-bookmark">
                                            <i class="fas fa-plus"></i>
                                        </i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="carousel_rank-block">
                            <img src="${movie.rank}" alt="" class="carousel_rank-number">
                            <div class="carousel_rank-content">
                                <p class="carousel_rank-title">${movie.name}</p>
                                <p class="carousel_rank-update">${movie.update} tập</p>
                            </div>
                        </div>
                    </div>
                `
                })
                carouselMove.innerHTML = htmls.join('')
            }
            render()
        }

        if ($(`${options.carouselSelector}.carousel-comingsoon`)) {
            function render() {
                const htmls = options.movies.map((movie, index) => {
                    return `
                    <div class="carousel_item col l-2-4 m-3 c-4">
                        <div class="carousel-comingsoon_timeline">
                            <div class="comingsoon_timeline-line"></div>
                            <div class="comingsoon_timeline-point"></div>
                            <div class="comingsoon_timeline-time">
                                <p class="comingsoon_timeline-date">${movie.date}</p>
                                <p class="comingsoon_timeline-day">${movie.day}</p>
                            </div>
                        </div>
                        <div class="carousel-comingsoon_poster">
                            <div class="comingsoon_poster-img-wrap">
                                <img src="${movie.img}" alt="">
                                <div class="comingsoon_poster-img-overlay">
                                    <div class="btns-play-and-add">
                                        <span class="btn-play">
                                            <i class="fas fa-play"></i>
                                        </span>
                                        <span class="btn-add">
                                            <i class="far fa-bookmark">
                                                <i class="fas fa-plus"></i>
                                            </i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="comingsoon_poster-content">
                                <div class="comingsoon_poster-title">${movie.name} </div>
                                <div class="comingsoon_poster-hagtag">
                                    <span>${movie.hagtag[0]}</span>
                                    <span>${movie.hagtag[1]}</span>
                                </div>
                                <div class="comingsoon_poster-info">
                                    <p>
                                        <span>Đạo diễn: </span>${movie.director}
                                    </p>
                                    <p>
                                        <span>Diễn viên: </span>${movie.actor}
                                    </p>
                                    <p>
                                        <span>Miêu tả: </span>${movie.decs}
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="carousel-comingsoon_title">
                            <p>${movie.name}</p>
                        </div>
                    </div>
                `
                })
                carouselMove.innerHTML = htmls.join('')
            }
            render()
        }

        const carouselItems = $$(options.carouselItemsSelector)
        const carouselMoveQuantity = Math.round(carouselItems.length / (carouselMove.getBoundingClientRect().width / carouselItems[0].getBoundingClientRect().width))
        var l = 0
        nextBtn.onclick = () => {
            prevBtn.style.display = 'block'
            l++;
            if (l < carouselMoveQuantity) {
                carouselMove.style.transform = `translateX(calc(0px - ${carouselWidth}px * ${l}))`
                if (l == carouselMoveQuantity - 1) {
                    nextBtn.style.display = 'none'
                }
            } else {
                l = carouselMoveQuantity - 1
                nextBtn.style.display = 'none'
            }
        }

        prevBtn.onclick = () => {
            nextBtn.style.display = 'block'
            l--;
            if (l >= 0) {
                carouselMove.style.transform = `translateX(calc(0px - ${carouselWidth}px * ${l}))`
                if (l == 0) {
                    prevBtn.style.display = 'none'
                }
            } else {
                l = 0
                prevBtn.style.display = 'none'
            }
        }
    }

    const rankList = [{
        name: 'Bí Mật Nơi Góc Tối',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img1.jpg',
        rank: './ViewerAssets/assets/img/rank-number1.png'
    },
    {
        name: 'One Piece (Đảo Hải Tặc)',
        update: '997',
        img: './ViewerAssets/assets/img/rank-img2.jpg',
        rank: './ViewerAssets/assets/img/rank-number2.png'
    },
    {
        name: 'Hạ Tiên Sinh Lưu Luyến Không Quên (Nỗi Vương Vấn Của Hạ Tiên Sinh)',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img3.jpg',
        rank: './ViewerAssets/assets/img/rank-number3.png'
    },
    {
        name: 'Bạn Gái Lầu Dưới Xin Hãy Ký Nhận',
        update: '36',
        img: './ViewerAssets/assets/img/rank-img4.jpg',
        rank: './ViewerAssets/assets/img/rank-number4.png'
    },
    {
        name: '【Thuyết minh】Một Đời Một Kiếp (Nhất Sinh Nhất Thế)',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img5.jpg',
        rank: './ViewerAssets/assets/img/rank-number5.png'
    },
    {
        name: 'Trường An như cố',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img6.jpg',
        rank: './ViewerAssets/assets/img/rank-number6.png'
    },
    {
        name: 'Bác sĩ xứ lạ',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img7.jpg',
        rank: './ViewerAssets/assets/img/rank-number7.png'
    },
    {
        name: 'Nửa Là Đường Mật Nửa Là Đau Thương',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img8.jpg',
        rank: './ViewerAssets/assets/img/rank-number8.png'
    },
    {
        name: 'Đối tác đáng ngờ',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img9.jpg',
        rank: './ViewerAssets/assets/img/rank-number9.png'
    },
    {
        name: 'Học viện quân sự Liệt Hỏa',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img10.jpg',
        rank: './ViewerAssets/assets/img/rank-number10.png'
    },
    {
        name: 'Bí Mật Nơi Góc Tối',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img11.jpg',
        rank: './ViewerAssets/assets/img/rank-number11.png'
    },
    {
        name: 'Bí Mật Nơi Góc Tối',
        update: '24',
        img: './ViewerAssets/assets/img/rank-img12.jpg',
        rank: './ViewerAssets/assets/img/rank-number12.png'
    },
    ]

    sliderStyle2({
        movies: rankList,
        carouselSelector: '#carousel-3',
        carouselMoveSelector: '#carousel-3 .carousel-move',
        prevBtnSelector: '#carousel-3 .carousel_btn-prev',
        nextBtnSelector: '#carousel-3 .carousel_btn-next',
        carouselItemsSelector: '#carousel-3 .carousel_item'
    })

    const comingsoonList = [{
        date: '11-7',
        day: 'Chủ Nhật',
        name: 'Anh Là Hiệp Sĩ Bóng Đêm Của Em',
        img: './ViewerAssets/assets/img/poster1.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-8',
        day: 'Thứ Hai',
        name: 'Đương Gia Chủ Mẫu',
        img: './ViewerAssets/assets/img/poster2.jpg',
        hagtag: ['Tiếng Phổ Thông', 'Cận Đại'],
        director: 'Yu Zheng',
        actor: 'Angel, Trương Tuệ Văn',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-8',
        day: 'Thứ Hai',
        name: 'Thần tượng: Cuộc đảo chính',
        img: './ViewerAssets/assets/img/poster3.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-9',
        day: 'Thứ Ba',
        name: 'Love At Night',
        img: './ViewerAssets/assets/img/poster4.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-9',
        day: 'Thứ Ba',
        name: 'Jo Yi Và Ám Hành Ngự Xử',
        img: './ViewerAssets/assets/img/poster5.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-11',
        day: 'Thứ Năm',
        name: 'Góc khuất học đường',
        img: './ViewerAssets/assets/img/poster6.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: '11-16',
        day: 'Thứ Ba',
        name: 'Gia Nam Truyện',
        img: './ViewerAssets/assets/img/poster7.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: 'Xin hãy đón đợi!',
        day: '&nbsp',
        name: 'Chỉ Là Quan Hệ Hôn Nhân',
        img: './ViewerAssets/assets/img/poster8.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: 'Xin hãy đón đợi!',
        day: '&nbsp',
        name: 'Đãi Vàng',
        img: './ViewerAssets/assets/img/poster9.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    },
    {
        date: 'Xin hãy đón đợi!',
        day: '&nbsp',
        name: 'Ai Là Hung Thủ',
        img: './ViewerAssets/assets/img/poster10.jpg',
        hagtag: ['Tiếng Hàn', 'Thanh xuân'],
        director: 'Ahn Ji-Sook',
        actor: 'Lee Jun-Young',
        decs: 'Bộ phim Hàn Quốc “Em Sẽ Trở Thành Ban Đêm Của Anh” kể về câu chuyện ngọt ngào lại hồi hộp giữa nữ bác sĩ và năm thành viên ban nhạc.'
    }
    ]

    sliderStyle2({
        movies: comingsoonList,
        carouselSelector: '#carousel-5',
        carouselMoveSelector: '#carousel-5 .carousel-move',
        prevBtnSelector: '#carousel-5 .carousel_btn-prev',
        nextBtnSelector: '#carousel-5 .carousel_btn-next',
        carouselItemsSelector: '#carousel-5 .carousel_item'
    })

var iframe1 = document.getElementById('linkphim');
//var UserRole = "";
//$.ajax({
//    url: '@Url.Action("GetSessionData", "Access")',
//    type: 'GET',
//    dataType: 'json',
//    success: function (data) {
//        UserRole = data.userRole;
//        // Sử dụng sessionUserName trong file js
//    },
//    error: function () {
//        console.log('Error occurred while retrieving session data');
//    }
//});
//var UserRole = document.getElementById('userRole').getAttribute("data-user-role");
var userRole = $('#userRole').getAttribute('data-user-role');


function playModal() {
    const modal = $('#modal-play')
    const modalContainer = $('#modal-play .modal-container')
    const modalClose = $('#modal-play .modal_close')
    const modalVideo = $('#modal-play video')

    function showModal() {
        modal.classList.add('open')
    }

    function hideModal() {
        modal.classList.remove('open')
        modalVideo.pause()
        iframe1.contentWindow.postMessage('mute', '*');
        iframe1.contentWindow.postMessage('pause', '*');
    }

    for (let i = 0; i < $$('.carousel_item').length; i++) {
        const item = $$('.carousel_item')[i]
        item.setAttribute('data-index', i)
        item.onclick = () => {
            const index = item.getAttribute('data-index')
            if (index >=20 && index <= 39) { 

                if (userRole === "1" || userRole === "2") {
                    // code thực hiện khi có quyền truy cập
                    iframe1.setAttribute('src', movieList[item.id - 1].video);
                    $('.play_area-title').innerHTML = `${movieList[item.id - 1].name}`
                    $('.content-info_title').innerHTML = `${movieList[item.id - 1].name}`

                    //   showAnh()
                    showModal()
                } else {
                    // code thực hiện khi không có quyền truy cập
                    alert("Bạn cần nâng cấp tài khoản để thực hiện chức năng này");
                }
            }
            else {
                // code thực hiện khi i không nằm trong phạm vi từ 1 đến 5
                iframe1.setAttribute('src', movieList[item.id - 1].video);
                $('.play_area-title').innerHTML = `${movieList[item.id - 1].name}`
                $('.content-info_title').innerHTML = `${movieList[item.id - 1].name}`

                //  showAnh()
                showModal()
            }
        }
    }

    modalClose.onclick = hideModal

    modal.onclick = hideModal
    
    modalContainer.onclick = function (event) {
        event.stopPropagation()
    }
}
setTimeout(playModal, 2000)

//function showVideo() {
//    document.getElementById("anh").style.display = "none";
//    document.getElementById("video").style.display = "inline-block";
//}

//function showAnh() {
//    document.getElementById("video").style.display = "none";
//    document.getElementById("anh").style.display = "inline-block";
//}