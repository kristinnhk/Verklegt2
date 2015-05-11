


$(document).ready(function () {
    //Sends an ajax post request to the controller to update the about me section of the users page.
    $('#AboutSubmit').click(function ()
    {
        alert("worked");
        if (!$('textarea#AboutTextarea').val().trim()) { //if the about section is empty
            $('#AboutMeError').text("About me can not be empty");
        }
        else {
            $('#AboutMeError').text("");
            var returnString = { 'aboutMe': $('textarea#AboutTextarea').val().trim() };
            $.post('/UserSettings/UpdateAboutMe/', returnString);
        }
    });

    $('#deleteGroupButton').click(function ()
    {

        var data = { 'groupIds[]': [] };
        $(".userGroups:checked").each(function () {
            alert($(this).val());
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
});