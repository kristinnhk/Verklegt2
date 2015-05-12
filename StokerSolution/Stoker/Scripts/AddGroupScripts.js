$(document).ready(function () {
    document.getElementById("submitGroup").disabled = true;

    function isValid() {
        if ($('#about').val().trim() != '') {
            if ($('#title').val().trim() != '') {
                document.getElementById("submitGroup").disabled = false;
            }
            else {
                document.getElementById("submitGroup").disabled = true;
            }
        }
        else {
            document.getElementById("submitGroup").disabled = true;
        }
    };
    //$('#about').keydown(isValid());
    $('#about').keyup(isValid);
    $('#title').keyup(isValid);
});