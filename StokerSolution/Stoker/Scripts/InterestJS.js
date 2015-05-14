$(document).ready(function () {
    var interestString = { 'interestID': $('#interestIDText').text() };
    var isFollowing = $.post('/Interest/IsFollowing/', interestString);
    var following = isFollowing.done(function (result)
    {
        if (result == "True") {
            $('#interestButton').attr('value', 'Unfollow');
        }
    });
    $("#interestButton").click(function ()
    {
        if ($(this).val() == "Follow") {
            $(this).attr('value', 'Unfollow');
            var returnString = { 'id': $('#idInterest').val() };
            var posting = $.post('/Interest/FollowInterest/', returnString);
        } else {
            $(this).attr('value', 'Follow');
            var returnString = { 'id': $('#idInterest').val() };
            var posting = $.post('/Interest/UnFollowInterest/', returnString);
        }
    })
});