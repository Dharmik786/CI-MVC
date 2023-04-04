function Delete(id) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Home/DeleteTimeSheet",
        data: { 'id': id },
        success: function (result) {
            $('.tbl').html($(result).find('.tbl').html());
        },
        error: function () {
            console.log("Erroor");
        }
    });
}

function EditTimeSheet(id) {
    $.ajax({
        type: "POST",
        url: "/Home/EditTimeSheet",
        data: { id: id },
        success: function (result) {

            var mission = document.getElementById('mission');
            mission.value = result.timesheet.missionId;

            var action = document.getElementById('Gaction');
            action.value = result.timesheet.action; 

            var dateVolunteered = document.getElementById('dateVolunteered');
            dateVolunteered.value = result.timesheet.dateVolunteered;

            var notes = document.getElementById('notes');
            notes.value = result.timesheet.notes;

        },
        error: function () {
            console.log("Erroor");
        }
    });
}


