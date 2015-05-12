$(document).ready(function () {
    //Sends an ajax post request to the controller to update the about me section of the users page.
    $('#groupIDText').hide();
    if (!$('textarea#AboutTextarea').val().trim()) { //if the about section is empty
        $('#AboutGroupError').text("About me can not be empty");
    }
    else {
        $('#AboutGroupError').text("");
    }

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