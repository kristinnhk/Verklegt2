
$(document).ready(function () {
    $("#orderBy, #filterBy").change(function () {
        $("#loader1").show();
        var order = $("#orderBy option:selected").val();
        var filter = $("#filterBy option:selected").val();
        var profileID = $("#profileID").val();
        var threadsShown = "0";
        var returnstring = { 'orderBy': order, 'filterBy': filter, 'threadsShown': threadsShown, 'profileID': profileID };
        var posting = $.post('/Stoker/GetMoreNews/', returnstring);
        posting.done(function (result) {
            $('.mainNewsFeed .threadItem').remove();
            for (var i = 0; i < result.threads.length; i++) {
                var date = new Date(parseInt(result.threads[i].dateCreated.substr(6))).toUTCString();
                var newsFeedItem = '<div class="threadItem well well-lg" > <div class="threadTop"> <span class="threadTitle">' +
                    result.threads[i].title + '</span><span class="threadUsername">Posted by: <a href="/Profile/FriendIndex?userID=' +
                    result.threads[i].originalPoster.Id + '">' + result.threads[i].originalPoster.firstName + ' ' + result.threads[i].originalPoster.lastName +
                    '</a> </span> <span>' + date + '</span> <span id="UserLike"><input type="button" id="likebutton" name="likeButton" />' +
                    '</span><span> Likes: ' + result.threads[i].likes + '</span></div><div class="threadContent">';
                if (result.threads[i].image != null) {
                    newsFeedItem += '<img src="/Profile/RenderThreadImage/' + result.threads[i].threadID + '" alt="Photo change" width="100" height="100">';
                }
                newsFeedItem +='<span id="textContent">' + result.threads[i].mainContent + '</span></div><span class="DetailView">' +
                '<a href="/ThreadDetail/ThreadDetail?threadID=' + result.threads[i].threadID + '">Go to comments</a> </span></div>';
                $('.mainNewsFeed').append(newsFeedItem);
            }
            $("#threadsOnPage").val("5");
            $("#loader1").hide();
        });
    });

    $('#loadMoreNews').click(function () {
        $('#loadMoreNews').attr('disabled', 'disabled');
        $("#loader2").show();
            var order = $("#orderBy option:selected").val();
            var filter = $("#filterBy option:selected").val();
            var profileID = $("#profileID").val();
            var threadsShown = $("#threadsOnPage").val();
            var returnstring = { 'orderBy': order, 'filterBy': filter, 'threadsShown': threadsShown, 'profileID': profileID };
            var posting = $.post('/Stoker/GetMoreNews/', returnstring);
            posting.done(function (result) {
                var currentValue = parseInt($("#threadsOnPage").val(),10);
                currentValue += result.threads.length;
                $("#threadsOnPage").val(currentValue.toString());
                for (var i = 0; i < result.threads.length; i++) {
                    var date = new Date(parseInt(result.threads[i].dateCreated.substr(6))).toUTCString();
                    var newsFeedItem = '<div class="threadItem well well-lg" > <div class="threadTop"> <span class="threadTitle">' +
                        result.threads[i].title + '</span><span class="threadUsername">Posted by: <a href="/Profile/FriendIndex?userID=' +
                        result.threads[i].originalPoster.Id + '">' + result.threads[i].originalPoster.firstName + ' ' + result.threads[i].originalPoster.lastName +
                        '</a> </span> <span>' + date + '</span> <span id="UserLike"><input type="button" id="likebutton" name="likeButton" />' +
                        '</span><span> Likes: ' + result.threads[i].likes + '</span></div><div class="threadContent">';
                    if (result.threads[i].image != null) {
                        newsFeedItem += '<img src="/Profile/RenderThreadImage/' + result.threads[i].threadID + '" alt="Photo change" width="100" height="100">';
                    }
                    newsFeedItem += '<span id="textContent">' + result.threads[i].mainContent + '</span></div><span class="DetailView">' +
                    '<a href="/ThreadDetail/ThreadDetail?threadID=' + result.threads[i].threadID + '">Go to comments</a> </span></div>';
                    $('.mainNewsFeed').append(newsFeedItem);
                }
                $("#loader2").hide();
                $('#loadMoreNews').removeAttr('disabled');
            });
    });
});