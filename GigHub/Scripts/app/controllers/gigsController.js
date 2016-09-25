
var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {

        $(container).on("click", "..js-toggle-attendance", toggleAttendance);

    };

    var toggleAttendance = function (e) {

        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, done, fail);
        } else {
            attendanceService.deleteAttendance(gigId, done, fail);
        }
    };


    var done = function () {
        var text = (button.text() == "Ide!") ? "Idziesz?" : "Ide!";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function () {
        alert("błąd");
    }

    return {
        init: init
    };

}(AttendanceService);
