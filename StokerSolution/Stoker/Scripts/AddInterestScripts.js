$(document).ready(function ()
{
    //checks if the input is empty or not
    function isValid()
    {
        
        if ($('#name').val().trim() != '')
        {
            document.getElementById("submitInterest").disabled = false;
            $("#submitInterest").val("Create interest");
        }
        else
        {
            document.getElementById("submitInterest").disabled = true;
            $("#submitInterest").val("Interest name can not be empty");
        }
    };
    isValid();
    $('#name').keyup(isValid);
});