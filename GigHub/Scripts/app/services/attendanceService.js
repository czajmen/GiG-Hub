
var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {

        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function (gigId, done, fail) {

        $.ajax({
            url: "/api/attendances/",
            method: "DELETE",
            data: { GigId: gigId }
        })
         .done(done)
         .fail(fail);
    };


    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}
();