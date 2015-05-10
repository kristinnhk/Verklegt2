$(document).ready(function (){
    $("#searchbarInSearchView").keyup(function () {
        if (!$("#searchbarInSearchView").val()) {
            return false;
        }
        var theForm = $("#searchbarInSearch");
        $.ajax({
            type: 'POST',
            url: '/Search/SearchJson',
            data: theForm.serialize(),
        }).done(function (result) {
            $("#userTable tbody tr").remove();
            for (var i = 0; i < result.Users.length; i++) {
                $("#userTable tbody").append('<tr> <a>' + result.Users[i].firstName + result.Users[i].lastName + '</a> </tr>');
            }
        }).fail(function () {
            alert("Error");
        });
        return false;
    });
});