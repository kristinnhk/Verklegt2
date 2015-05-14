$(document).ready(function () {
    var interestString = { 'groupID': $('#idGroup').val() };
    var isFollowing = $.post('/GroupProfile/IsGroupMember/', interestString);
    var following = isFollowing.done(function (result) {
        if (result == "True") {
            $('#groupButton').attr('value', 'Leave group');
        }
    });
    $("#groupButton").click(function () {
        if ($(this).val() == "Join group") {
            $(this).attr('value', 'Leave group');
            var returnString = { 'groupID': $('#idGroup').val() };
            var posting = $.post('/GroupProfile/JoinGroup/', returnString);
        } else {
            $(this).attr('value', 'Join group');
            var returnString = { 'groupID': $('#idGroup').val() };
            var posting = $.post('/GroupProfile/LeaveGroup/', returnString);
        }
    })
});