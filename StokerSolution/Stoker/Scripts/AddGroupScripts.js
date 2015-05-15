$(document).ready(function ()
{
    //checks if either the title or about me of the group
    //fields are empty and doesnt let the user update
    //the information if they are.
    function isValid()
    {
        if ($('#about').val().trim() != '')
        {
            if ($('#title').val().trim() != '')
            {
                document.getElementById("submitGroup").disabled = false;
                $("#submitGroup").val("Create group");
            }
            else
            {
                document.getElementById("submitGroup").disabled = true;
                $("#submitGroup").val("Title or About me can not be empty");
            }
        }
        else
        {
            document.getElementById("submitGroup").disabled = true;
            $("#submitGroup").val("Title or About me can not be empty");
        }
    };
    isValid();
    $('#about').keyup(isValid);
    $('#title').keyup(isValid);
});