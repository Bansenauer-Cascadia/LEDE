$(document).ready(function () {
    initPopup();
    $(document).ajaxComplete(function () {
        initPopup(); 
    });

    $(document).on('click', '#filesubmit', function (e) {
    });

    function initPopup () {
        $('.upload').magnificPopup({
            type: 'ajax',
            closeOnBgClick: false,
            closeOnContentClick: false
        });
    }
});