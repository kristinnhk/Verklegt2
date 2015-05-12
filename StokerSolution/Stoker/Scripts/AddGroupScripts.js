$(document).ready(function () {
    document.getElementById("submitGroup").disabled = true;
    $("#submitGroup").val("Title or About me can not be empty");

    function isValid() {
        if ($('#about').val().trim() != '') {
            if ($('#title').val().trim() != '') {
                document.getElementById("submitGroup").disabled = false;
                $("#submitGroup").val("Create group");
            }
            else {
                document.getElementById("submitGroup").disabled = true;
                $("#submitGroup").val("Title or About me can not be empty");
            }
        }
        else {
            document.getElementById("submitGroup").disabled = true;
            $("#submitGroup").val("Title or About me can not be empty");
        }
    };
    $('#about').keyup(isValid);
    $('#title').keyup(isValid);
});