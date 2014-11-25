$(function () {
    $('.edit').on('click', function () {
        selectedrow = $(this).parent().parent();
        selectedrow.attr('class', 'selected');
        selectedrow.siblings('.selected').attr('class', '');
        selectedrow.siblings(':not(.first)').hide().slideUp();
        $(this).parent().siblings().css('font-size', '200%');

        var $cancelEdit = $('<button id="cancelEdit" class="btn btn-sm">Switch Cohort</button>');
        $(this).after($cancelEdit);
        $(this).css('display', 'none');

        $('#cancelEdit').on('click', function () {
            selectedrow = $(this).parent().parent();
            selectedrow.siblings(':not(.first)').toggle();
            $(this).siblings().css('display', 'inline');
            $(this).remove();
            selectedrow.children().css('font-size', '100%');
            selectedrow.removeClass('selected');
            $('#CohortDetails').empty(); 
        });
    });
});