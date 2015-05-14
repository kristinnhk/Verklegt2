$(document).ready(function () {
    document.getElementById("submitInterest").disabled = true;
    $("#submitInterest").val("Title or About me can not be empty");

    function isValid() {
        
            if ($('#name').val().trim() != '') {
                document.getElementById("submitInterest").disabled = false;
                $("#submitInterest").val("Create interest");
            }
            else {
                document.getElementById("submitInterest").disabled = true;
                $("#submitInterest").val("Title or About me can not be empty");
            }
        
       
            //document.getElementById("submitInterest").disabled = true;
            //$("#submitInterest").val("Title or About me can not be empty");
        
    };
    $('#name').keyup(isValid);
});