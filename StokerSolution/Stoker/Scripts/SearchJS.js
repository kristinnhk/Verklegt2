

$(document).ready(function () {
    //http://stackoverflow.com/questions/1909441/jquery-keyup-delay delay function from Stackoverflow user CMS
    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
    $("#searchbarInSearchView").keyup(function () {
        delay(function () {

        $("#loader").show();
        //$("#searchbarInSearchView").attr("disabled", "disabled");
        if (!$("#searchbarInSearchView").val()) {
            $("#loader").hide();
            $("#userTable tbody tr").remove();
            $("#interestTable tbody tr").remove();
            $("#groupTable tbody tr").remove();
            // $("#searchbarInSearchView").removeAttr("disabled");
            return false;
        }
        else {
            var theForm = $("#searchbarInSearchView");
            var returnString = { 'Search': $('#searchbarInSearchView').val().trim() };
            var posting = $.post('/Search/SearchJson/', returnString);

            posting.done(function (result) {
                //$("#searchbarInSearchView").removeAttr("disabled");
                $("#loader").hide();
                $("#userTable tbody tr").remove();
                for (var i = 0; i < result.Users.length; i++) {
                    $("#userTable tbody").append('<tr><td>' + (i + 1) + '</td><td><a href="/Search/RedirectToProfile?userId=' +
                        result.Users[i].Id + '">' + result.Users[i].firstName + " " + result.Users[i].lastName + '</a></td></tr>');
                }
                $("#interestTable tbody tr").remove();
                for (var i = 0; i < result.interests.length; i++) {
                    $("#interestTable tbody").append('<tr><td>' + (i + 1) + '</td><td><a href="/Search/RedirectToInterest?interestId=' +
                        result.interests[i].interestID + '">' + result.interests[i].name + '</a></td><td>' + result.interests[i].numberOfUsersInterested + '</td></tr>');
                }
                $("#groupTable tbody tr").remove();
                for (var i = 0; i < result.groups.length; i++) {
                    $("#groupTable tbody").append('<tr><td>' + (i + 1) + '</td><td><a href="/GroupProfile/GroupProfile?groupId=' +
                        result.groups[i].groupID + '">' + result.groups[i].title + '</a></td><td>' + result.groups[i].numberOfGroupMembers + '</td></tr>');
                }
            })
            posting.fail(function () {
                alert("Error");
            });
            return false;
        }
        }, 500);
    })
});