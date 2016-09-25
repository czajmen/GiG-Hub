

var FollowArtistService = function () {

    var isFollowing = function (artistId, done, fail) {

        $.post("/api/follow/check/", { ArtistId: artistId })
        .done(done)
        .fail();
    };

    var delFollowing = function(artistId, done, fail) {

        $.post("/api/unfollow/", { ArtistId: artistId })
            .done(done)
            .fail(fail);

    };

    var followArtist = function (artistId, done, fail) {

        $.post("/api/follow/", { ArtistId: artistId })
            .done(done)
            .fail(fail);
    }


    return {     
        follow: followArtist,
        unfollow: delFollowing,
        isfollowing: isFollowing
    }


}();