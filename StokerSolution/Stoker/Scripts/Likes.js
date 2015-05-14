$(document).ready(function () {
    var likeString = { 'threadID': $('#threadID').val() };
    var isFollowing = $.post('/Stoker/IsLikedThread/', likeString);
    var following = isFollowing.done(function (result) {
        if (result == "True") {
            $('#likebutton').attr('value', 'Unlike');
        }
    });
    $("#likebutton").click(function () {
        if ($(this).val() == "Like") {
            $(this).attr('value', 'Unlike');
            var returnString = { 'threadID': $('#threadID').val() };
            var posting = $.post('/Stoker/LikeThread/', returnString);
        } else {
            $(this).attr('value', 'Follow');
            var returnString = { 'threadID': $('#threadID').val() };
            var posting = $.post('/Stoker/UnLikeThread/', returnString);
        }
    })
});