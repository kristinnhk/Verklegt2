


$(document).ready(function ()
{

    //if about me section is empty you can not submit changes.
    if ($('#AboutTextarea').val().trim() != '')
    {
        document.getElementById("AboutSubmit").disabled = false;
        $('#AboutSubmit').text("Change about me");
    }
    else
    {
        document.getElementById("AboutSubmit").disabled = true;
        $('#AboutSubmit').text("About me section can not be empty");
    }

    //Sends an ajax post request to the controller to update the about me section of the users page.
    $('#AboutSubmit').click(function ()
    {
            var returnString = { 'aboutMe': $('textarea#AboutTextarea').val().trim() };
            var posting = $.post('/UserSettings/UpdateAboutMe/', returnString);
    });

    //checks if the about me textarea is empty and doesn't let the user
    //change information about himself if the field is empty
    function isValid()
    {
        if ($('#AboutTextarea').val().trim() != '')
        {
            document.getElementById("AboutSubmit").disabled = false;
            $('#AboutSubmit').text("Change about me");
        }
        else
        {
            document.getElementById("AboutSubmit").disabled = true;
            $('#AboutSubmit').text("About me section can not be empty");
        }
    };
    $('#AboutTextarea').keyup(isValid);

    //function that checks if a user has added an image to add to his profile
    function checkInputFile()
    {
        if (document.getElementById("ImageFile").files.length == 0)
        {
            document.getElementById("photoSubmit").disabled = true;
        }
        else
        {
            document.getElementById("photoSubmit").disabled = false;
        }
    }
    checkInputFile();

    //checks constantly if the file field changes.
    $(document).on('change', '#ImageFile', function () { checkInputFile(); });
});