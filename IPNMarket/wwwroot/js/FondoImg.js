$(function () {
    $('.btn').click(function () {
        $('body').hasClass('open') ? $('body').removeClass('open') : $('body').addClass('open')
    })
})