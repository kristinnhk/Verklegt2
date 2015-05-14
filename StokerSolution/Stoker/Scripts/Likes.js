$(document).ready(function () {

    var likes = [];

   $(".likeButton").each(function ()
    {
        var value = $(this).val();
        likes.push(value);
   });

   var likesString = likes.map(function (obj) {
       return obj.toString();
   });
    for(var i = 0; i < likes.length; i++)
    {
        var postString = { "threadID": likes[i] };
        var isLiked = $.post('/Stoker/IsLikedThread/', postString);
        isLiked.done(function (result)
        {
            
            
            if (result != "True")
            {
                var likeString = ".likeButton[value='" + likesString[i] + "'";
                alert(likeString);
                //$(".likeButton[value=")
            }
        });
    };

   /* var isFollowing = $.post('/Stoker/IsLikedThread/', likeString);
    var following = isFollowing.done(function (result) 
    {
        if (result == "True") 
        {
            $('#likebutton').attr('value', 'Unlike');
        }
    });
    $("#likebutton").click(function () 
    {
        if ($(this).val() == "Like") 
        {
            $(this).attr('value', 'Unlike');
            var returnString = { 'threadID': $('#threadID').val() };
            var posting = $.post('/Stoker/LikeThread/', returnString);
        } else 
        {
            $(this).attr('value', 'Follow');
            var returnString = { 'threadID': $('#threadID').val() };
            var posting = $.post('/Stoker/UnLikeThread/', returnString);
        }
    });*/
});