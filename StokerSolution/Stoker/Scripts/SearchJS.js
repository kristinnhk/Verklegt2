$(document).ready(function (){
    $("#searchbarInSearchView").keyup(function () {
        if (!$("#searchbarInSearchView").val()) {
            return false;
        }

        /*$.ajax({
            type: 'POST',
            url: '/Search/SearchJson',
            data: theForm.serialize(),
        })*/
            
        else{
            var theForm = $("#searchbarInSearchView");
            var returnString = { 'Search': $('#searchbarInSearchView').val().trim() };
            var posting = $.post('/Search/SearchJson/', returnString);
            
        posting.done(function (result) {
            $("#userTable tbody tr").remove();
            for(var i = 0; i < result.Users.length; i++) {
                $("#userTable tbody").append('<tr><td>' + (i+1) + '</td><td><a>' + result.Users[i].firstName  + " " + result.Users[i].lastName + '</a></td></tr>');
            }
            $("#interestTable tbody tr").remove();
            for (var i = 0; i < result.interests.length; i++) {
                $("#interestTable tbody").append('<tr><td>' + (i+1) + '</td><td><a>' + result.interests[i].name  + '</a></td><td>' + result.interests[i].numberOfUsersInterested + '</td></tr>');
            }
            $("#groupTable tbody tr").remove();
            for (var i = 0; i < result.groups.length; i++) {
                $("#groupTable tbody").append('<tr><td>' + (i + 1) + '</td><td><a>' + result.groups[i].title + '</a></td><td>' + result.groups[i].numberOfGroupMembers + '</td></tr>');
            }
        })
        posting.fail(function () {
            alert("Error");
        });
            return false;
        }
    })
});