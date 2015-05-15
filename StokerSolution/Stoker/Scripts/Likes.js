$(document).ready(function () {

    function getLikeButtonValues ()
    {
        var likes = [];
        $(".likeButton").each(function () {
            var value = $(this).val();
            likes.push(value.toString());
        });
        return likes;
    }
    function checkLikes ()
    {
        var likes = getLikeButtonValues();
        for (var i = 0; i < likes.length; i++) {
            var str = likes[i];
            var postString = { "threadID": likes[i] };
            var isLiked = $.post('/Stoker/IsLikedThread/', postString);
            isLiked.done(function (result) {
                if (result == true) {
                    console.log(str + "BLA");
                    $('.likeButton[value=' + str + ']').html("Unlike");
                }
                else
                {
                    console.log(str + "BLA");
                    $('.likeButton[value=' + str + ']').html("Like");
                }
            });
        };
    }
    checkLikes();
    function updateLikes()
    {
        var likes = getLikeButtonValues();
        for(var i = 0; i < likes.length; i++)
        {
            var checkString = ".likeButton[value='" + likes[i] + "']";
            var returnString = { 'threadID': likes[i] };
            var posting = $.post('/Stoker/NumberOfLikes/', returnString);
            posting.done(function (result)
            {
                $(checkString).next().text("Upvotes: " + result);
            });
        }
    }
    $(".likeButton").click(function () 
    {
        if ($(this).html() == "Upvote") 
        {
            $(this).html('Downvote');
            var turnString = { 'threadID': $(this).val() };
            var posting = $.post('/Stoker/LikeThread/', turnString);
            posting.done(function ()
            {
                updateLikes();
                checkLikes();
            });
            
        }
        else
        {
            $(this).html('Upvote');
            var turnString = { 'threadID': $(this).val() };
            var posting = $.post('/Stoker/UnLikeThread/', turnString);
            posting.done(function ()
            {
                updateLikes();
                checkLikes();
            });
        }
    });
});