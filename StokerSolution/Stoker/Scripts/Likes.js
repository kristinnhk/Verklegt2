﻿$(document).ready(function () {
    function checkLikes ()
    {
        var likes = [];
        $(".likeButton").each(function () {
            var value = $(this).val();
            likes.push(value.toString());
        });
        for (var i = 0; i < likes.length; i++) {
            var str = likes[i];
            console.log("bla" + likes[i]);
            var postString = { "threadID": likes[i] };
            var isLiked = $.post('/Stoker/IsLikedThread/', postString);
            isLiked.done(function (result) {
                if (result != 0) {
                    $('.likeButton[value=' + str + ']').text("Unlike");
                }
                else
                {
                    $('.likeButton[value=' + str + ']').text("Like");
                }
            });
        };
    }
    checkLikes();
    function updateLikes()
    {
        var likes = [];
        $(".likeButton").each(function () {
            var value = $(this).val();
            likes.push(value.toString());
        });
        for(var i = 0; i < likes.length; i++)
        {
            var checkString = ".likeButton[value='" + likes[i] + "']";
            var returnString = { 'threadID': likes[i] };
            var posting = $.post('/Stoker/NumberOfLikes/', returnString);
            posting.done(function (result)
            {
                $(checkString).next().text("Likes: " + result);
            });
        }
    }
    $(".likeButton").click(function () 
    {
        if ($(this).text() == "Like") 
        {
            $(this).attr('text', 'Unlike');
            var turnString = { 'threadID': $(this).val() };
            var posting = $.post('/Stoker/LikeThread/', turnString);
            posting.done(function ()
            {
                checkLikes();
                updateLikes();
            });
            
        } else 
        {
            $(this).attr('text', 'Like');
            var turnString = { 'threadID': $(this).val() };
            var posting = $.post('/Stoker/UnLikeThread/', turnString);
            posting.done(function ()
            {
                checkLikes();
                updateLikes();
            });
        }
    });
});