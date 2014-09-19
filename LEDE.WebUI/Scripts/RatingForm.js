$(function () {
    $(document).on('click', '#addother', function (e) {
        e.preventDefault();
        var ratings = $('#ratingform').serialize();
        $.post('AddOther', ratings, function (data) {
            $('#otherratings').html(data);
        });
    });

    $(document).on('click', '#cancelother', function (e) {
        e.preventDefault();
        var $parentrow = $(this).parents('tr');
        var $tablerows = $parentrow.siblings('tr').length;
        if ($tablerows == 1) {
            $parentrow.siblings('tr').remove();
        }
        $parentrow.remove();
    });

    $(document).on('click', '#addimpact', function (e) {
        e.preventDefault();
        $('#impacttable').css('display', 'block');
    });

    $(document).on('click', '#cancelimpact', function (e) {
        e.preventDefault();
        $('#impacttable').css('display', 'none');
    });

    function validate(value) {
        var valid = true;
        var valueint = parseInt(value);
        var valuedouble = parseFloat(value);
        if ((isNaN(valueint) || valuedouble % 1 != 0) && value.trim() != "") {
            return "Please Enter an Integer Rating";
        }
        if (valueint < 0 || valueint > 3) {
            return "Please Enter an Integer Rating Between 0 and 3";
        }
        else {
            return "Valid";
        }
    }

    $('#ratingsubmit').click(function (e) {
        var isvalid = true;
        $('#othervalidation').empty();
        var list = "<ul>";
        $('.other').each(function () {
            var validscore = validate(this.value);
            if (validscore != "Valid") {
                isvalid = false;
                list += "<li>" + validscore + "</li>";
            }
        });
        list += "</ul>"
        if (!isvalid) {
            e.preventDefault();
            $('#othervalidation').append(list);
        }
    });
});