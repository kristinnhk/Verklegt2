$(document).ready(function () {

    //checks if user is a member of a group
    var groupString = { 'groupID': $('#idGroup').val() };
    var isMember = $.post('/GroupProfile/IsGroupMember/', groupString);
    var following = isMember.done(function (result)
    {
        if (result == "True")
        {
            $('#groupButton').attr('value', 'Leave group');
        }
    });

    //joins or leaves user from a group depending on what he was before
    $("#groupButton").click(function ()
    {
        if ($(this).val() == "Join group")
        {
            $(this).attr('value', 'Leave group');
            var returnString = { 'groupID': $('#idGroup').val() };
            var posting = $.post('/GroupProfile/JoinGroup/', returnString);
        }
        else
        {
            $(this).attr('value', 'Join group');
            var returnString = { 'groupID': $('#idGroup').val() };
            var posting = $.post('/GroupProfile/LeaveGroup/', returnString);
        }
    })
});