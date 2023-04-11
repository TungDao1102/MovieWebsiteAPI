var $ = document.querySelector.bind(document)
var $$ = document.querySelectorAll.bind(document)
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
     //   item.onclick = showModal
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


// Carousel 
function sliderStyle1(options) {
    const carouselMove = $(options.carouselMoveSelector)
 //   const carouselWidth = $(options.carouselSelector).offsetWidth
    const prevBtn = $(options.prevBtnSelector)
    const nextBtn = $(options.nextBtnSelector)
  //  prevBtn.style.display = 'none'

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



    //for (let i = 1; i <= carouselItems.length; i++) {
    //    if ((i + 1) % 6 == 0) {
    //        carouselItems[i].onmouseover = function () {
    //            carouselItems[i].style.transform = `translateX(-${carouselItems[i].offsetWidth / 2}px)`
    //            carouselItems[i - 1].style.opacity = '0'
    //        }
    //        carouselItems[i].onmouseout = function () {
    //            carouselItems[i].style.transform = 'translateX(0)'
    //            carouselItems[i - 1].style.opacity = '1'
    //        }
    //    }
    //}

}

const movieLists = [];
// const moviesWithValidProps = movieList.filter(item => item.movie && item.movie.movieId && item.listGenres && item.listActor);
for (let i = 0; i < movieList.length; i++) {
    const item = movieList[i];
    const index = i + 1;
    const id = index;
    const name = item.movie.movieName;
    const category = item.listGenres?.map(genre => genre.genresName).join(', ');
    const update = "1";
    const imgMin = ('https://image.tmdb.org/t/p/w500') + "" + item.movie.posterPath;
    const imgMax = ('https://image.tmdb.org/t/p/w500') + "" + item.movie.posterPath;
    const video = ('https://2embed.org/embed/movie?tmdb=') + "" + item.movie.movieId;
    const actor = item.listActor?.map(actor => actor.actorName).join(', ');
    const actorAvt = item.listActor?.map(actor => `https://image.tmdb.org/t/p/w500${actor.avartar}`).join(', ');
    const overview = item.movie.overView;
    const topview = item.topView?.map(topview => topview.movieName).join(', ');


    const movies = {
        id,
        name,
        category,
        update,
        imgMin,
        imgMax,
        video,
        actor,
        actorAvt,
        overview,
        topview
    };

    movieLists.push(movies);
}

sliderStyle1({
    movies: movieLists.slice(0, 20),
    carouselSelector: '#carousel-1',
    carouselMoveSelector: '#carousel-1 .carousel-move',
    prevBtnSelector: '#carousel-1 .carousel_btn-prev',
    nextBtnSelector: '#carousel-1 .carousel_btn-next',
    carouselItemsSelector: '#carousel-1 .carousel_item'
});




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


var iframe1 = document.getElementById('linkphim');
var dienvien6 = document.getElementById('dienvien6');
var dienvien1 = document.getElementById('dienvien1');
var dienvien2 = document.getElementById('dienvien2');
var dienvien3 = document.getElementById('dienvien3');
var dienvien4 = document.getElementById('dienvien4');
var dienvien5 = document.getElementById('dienvien5');
var name1 = document.getElementById('name1');
var name2 = document.getElementById('name2');
var name3 = document.getElementById('name3');
var name4 = document.getElementById('name4');
var name5 = document.getElementById('name5');
var name6 = document.getElementById('name6');
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
            if (index >= 20 && index <= 39) {

                if (userRole === "1" || userRole === "2") {
                    // code thực hiện khi có quyền truy cập
                    iframe1.setAttribute('src', movieLists[item.id - 1].video);
                    $('.play_area-title').innerHTML = `${movieLists[item.id - 1].name}`
                    $('.content-info_title').innerHTML = `${movieLists[item.id - 1].name}`
                    $('.content-info_category').innerHTML = `Thể loại: ${movieLists[item.id - 1].category}`
                    $('.content-info_decs').innerHTML = `Miêu Tả: ${movieLists[item.id - 1].overview}`
                    dienvien6.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[0] : '');
                    dienvien1.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[1] : '');
                    dienvien2.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[2] : '');
                    dienvien3.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[3] : '');
                    dienvien4.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[4] : '');
                    dienvien5.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[5] : '');
                    name1.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[0] : ''}`;
                    name2.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[1] : ''}`;
                    name3.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[2] : ''}`;
                    name4.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[3] : ''}`;
                    name5.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[4] : ''}`;
                    name6.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[5] : ''}`;
                    //   showAnh()
                    showModal()
                } else {
                    // code thực hiện khi không có quyền truy cập
                    alert("Bạn cần nâng cấp tài khoản để thực hiện chức năng này");
                }
            }
            else {
                iframe1.setAttribute('src', movieLists[item.id - 1].video);
                $('.play_area-title').innerHTML = `${movieLists[item.id - 1].name}`
                $('.content-info_title').innerHTML = `${movieLists[item.id - 1].name}`
                $('.content-info_category').innerHTML = `Thể loại: ${movieLists[item.id - 1].category}`
                $('.content-info_decs').innerHTML = `Miêu Tả: ${movieLists[item.id - 1].overview}`
                dienvien6.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[0] : '');
                dienvien1.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[1] : '');
                dienvien2.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[6] : '');
                dienvien3.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[3] : '');
                dienvien4.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[4] : '');
                dienvien5.setAttribute('src', movieLists[item.id - 1].actorAvt ? movieLists[item.id - 1].actorAvt.split(', ')[5] : '');
                name1.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[0] : ''}`;
                name2.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[1] : ''}`;
                name3.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[6] : ''}`;
                name4.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[3] : ''}`;
                name5.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[4] : ''}`;
                name6.innerHTML = `${movieLists[item.id - 1].actor ? movieLists[item.id - 1].actor.split(', ')[5] : ''}`;


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
sliderStyle3({
    movies: movieLists.slice(0, 10),

    carouselMoveSelector: '.play_content-rank-list'
});


function sliderStyle3(options) {
    const carouselMove = $(options.carouselMoveSelector);

    function render() {
        let html = '';
        options.movies.forEach((movie, index) => {
            html += `
                <li class="play_content-rank-item ">
                    <a href=" " class="play_content-rank-link ">
                        <p>
                            <span>${index + 1}</span> ${movie.topview.split(', ')[index]}
                        </p>
                        <img src="../ViewerAssets/assets/img/rank-img${index + 1}.jpg " alt=" ">
                    </a>
                </li>
            `;
        });
        carouselMove.innerHTML = html;
    }
    render();
}

