


$(document).ready(function () {

    //if about me section is empty you can not submit changes.
    if ($('#AboutTextarea').val().trim() != '') {
        document.getElementById("AboutSubmit").disabled = false;
        $('#AboutSubmit').text("Change about me");
    }
    else {
        document.getElementById("AboutSubmit").disabled = true;
        $('#AboutSubmit').text("About me section can not be empty");
    }
    //Sends an ajax post request to the controller to update the about me section of the users page.
    $('#AboutSubmit').click(function ()
    {
            var returnString = { 'aboutMe': $('textarea#AboutTextarea').val().trim() };
            var posting = $.post('/UserSettings/UpdateAboutMe/', returnString);
    });

    $('#deleteGroupButton').click(function ()
    {

        var data = { 'groupIds[]': [] };
        $(".userGroups:checked").each(function () {
            data['groupIds[]'].push($(this).val());
        });
        var posting = $.post('/UserSettings/DeleteUserGroups/', data);

        posting.done(function ()
        {
            $('#groupDiv').load(document.URL + ' #groupDiv');
        });
    });

    $('#deleteInterestButton').click(function () {

        var data = { 'interestIds[]': [] };
        $(".interestGroups:checked").each(function () {
            data['interestIds[]'].push($(this).val());
        });
        var posting = $.post('/UserSettings/DeleteUserInterests/', data);

        posting.done(function () {
            $('#interestDiv').load(document.URL + ' #interestDiv');
        });
    });

    function isValid() {
        if ($('#AboutTextarea').val().trim() != '') {
            document.getElementById("AboutSubmit").disabled = false;
            $('#AboutSubmit').text("Change about me");
        }
        else {
            document.getElementById("AboutSubmit").disabled = true;
            $('#AboutSubmit').text("About me section can not be empty");
        }
    };
    $('#AboutTextarea').keyup(isValid);

    function checkInputFile() {
        if (document.getElementById("ImageFile").files.length == 0) {
            document.getElementById("photoSubmit").disabled = true;
        }
        else {
            document.getElementById("photoSubmit").disabled = false;
        }
    }
    checkInputFile();

    $(document).on('change', '#ImageFile', function () { checkInputFile(); });
});