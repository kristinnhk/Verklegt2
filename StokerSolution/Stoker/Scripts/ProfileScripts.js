$(document).ready(function ()
{
    //sends a post request to a controller that sends a
    //friend request to another user
    $('#acceptFriendRequest').click(function ()
    {
        var returnString = { 'userID': $('#acceptFriendRequest').val() };
        var posting = $.post('/Profile/Addfriend/', returnString);
    });
});