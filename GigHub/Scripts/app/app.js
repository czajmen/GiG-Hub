
function initGigs() {

    $(".js-toggle-attendance")
    .click(function (e) {
        var button = $(e.target);

        if (button.hasClass("btn-default")) {

            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(function () {
                    button.removeClass("btn-default")
                        .addClass("btn-info")
                        .text("Ide");
                })
                .fail(function () {
                    alert("Coś poszło nie tak :(");
                });

        } else {

            $.ajax({
                url: "/api/attendances/",
                method: "DELETE",
                data: { GigId: button.attr("data-gig-id") }

            })
                .done(function () {
                    button.removeClass("btn-info").addClass("btn-default").text("Idziesz?");

                })
                .fail(function () {

                    alert("błąd");

                });

        }
    });

}