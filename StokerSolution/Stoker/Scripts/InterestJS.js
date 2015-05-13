$(document).ready(function(){
    $("#interestButton").click(function () {
        if ($(this).val() == "Follow") {
            $(this).attr('value', 'Unfollow');
            var returnString = { 'id': $('#idInterest').val() };
            var posting = $.post('/Interest/FollowInterest/', returnString);
        } else {
            $(this).attr('value', 'Follow');
            var returnString = { 'id': $('.idInterest').val() };
            var posting = $.post('/Interest/UnFollowInterest/', returnString);
        }
    })
});