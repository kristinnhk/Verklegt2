


$(document).ready(function () { 
    $('#AboutSubmit').click(function () {
        if (!$("#AboutTextarea").val()) { //if the about is empty
            return false;
        }
        $('#AboutTextarea').hide();
       // var about = $('#AboutTextarea').val()
    });
});