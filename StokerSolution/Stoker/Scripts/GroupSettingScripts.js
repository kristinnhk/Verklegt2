$(document).ready(function () {
    //Sends an ajax post request to the controller to update the about me section of the users page.
    if ($('#AboutTextarea').val().trim() == '') { //if the about section is empty
        document.getElementById("AboutSubmit").disabled = true;
        $("#AboutSubmit").text("About me section can not be empty");
    }

    function isValid() {
        if ($('#AboutTextarea').val().trim() != '') {
            document.getElementById("AboutSubmit").disabled = false;
            $("#AboutSubmit").text("Change about group");
            
        }
        else {
            document.getElementById("AboutSubmit").disabled = true;
            $("#AboutSubmit").text("About me section can not be empty");
        }
    };
    $('#AboutTextarea').keyup(isValid);



    $('#AboutSubmit').click(function () {
        if (!$('textarea#AboutTextarea').val().trim()) { //if the about section is empty
            $('#AboutGroupError').text("About me can not be empty");
        }
        else {
            $('#AboutGroupError').text("");
            var returnString = {
                'aboutGroup': $('textarea#AboutTextarea').val().trim(), 'groupID': $('#groupIDText').text()
            };
            var posting = $.post('/GroupSettings/UpdateAboutGroup/', returnString);

        }
    });
});