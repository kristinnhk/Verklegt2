$(document).ready(function () {
    var likeString = { 'commentID': $('#commentID').text() };
    var isFollowing = $.post('/Stoker/IsLikedComment/', likeString);
    var following = isFollowing.done(function (result) {
        if (result == "True") {
            $('#likebutton').attr('value', 'Unlike');
        }
    });
    $("#likebutton").click(function () {
        if ($(this).val() == "Like") {
            $(this).attr('value', 'Unlike');
            var returnString = { 'id': $('#commentID').val() };
            var posting = $.post('/Stoker/LikeComment/', returnString);
        } else {
            $(this).attr('value', 'Follow');
            var returnString = { 'id': $('#commentID').val() };
            var posting = $.post('/Stoker/UnLikeComment/', returnString);
        }
    })
});