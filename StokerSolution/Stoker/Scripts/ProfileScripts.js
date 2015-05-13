$(document).ready(function () {
    $('#acceptFriendRequest').click(function () {
        var returnString = { 'userID': $('#acceptFriendRequest').val() };
        var posting = $.post('/Profile/Addfriend/', returnString);
    });
});