


var FollowingController = function(followService) {

    var followButton = $(".js-follow-artist");

    var init = function () {

        var artistId = followButton.attr("data-artist-id");
        followService.isfollowing(artistId, done, fail);

        $(".js-follow-artist").click(followingManager);
    };

    var followingManager = function(e) {

        followButton = $(e.target);
        var artistId = followButton.attr("data-artist-id");

        if (followButton.hasClass("btn-primary")) {
            followService.follow(artistId, done, fail)
        } else {
            followService.unfollow(artistId, done, fail);
        }
    }

    var done = function() {
        var text = (followButton.text() == "Obserwuj") ? "Obserwuje" : "Obserwuj";
        followButton.toggleClass("btn-primary").toggleClass("btn-info").text(text);
    };
    var fail = function() {
        alert("byk");
    };


    return {
        init : init
    }

}(FollowArtistService);
