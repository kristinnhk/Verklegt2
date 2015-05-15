$(document).ready(function ()
{
    //checks if the about text area is empty
    //and prevents the user from updating information if it is
    function isValid()
    {
        if ($('#AboutTextarea').val().trim() != '')
        {
            document.getElementById("AboutSubmit").disabled = false;
            $("#AboutSubmit").text("Change about group");
            
        }
        else
        {
            document.getElementById("AboutSubmit").disabled = true;
            $("#AboutSubmit").text("About me section can not be empty");
        }
    };

    isValid();

    //updates the result of isValid when the about text area changes
    $('#AboutTextarea').keyup(isValid);

    //sends a post request to update the about group section
    $('#AboutSubmit').click(function ()
    {
        if (!$('textarea#AboutTextarea').val().trim())
        { //if the about section is empty
            $('#AboutGroupError').text("About me can not be empty");
        }
        else
        {
            $('#AboutGroupError').text("");
            var returnString = {
                'aboutGroup': $('textarea#AboutTextarea').val().trim(), 'groupID': $('#groupIDText').text()
            };
            var posting = $.post('/GroupSettings/UpdateAboutGroup/', returnString);

        }
    });

    //function that checks if a user has added an image to add to the groups profile
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
    $(document).on('change', '#ImageFile', function () {checkInputFile(); });
});