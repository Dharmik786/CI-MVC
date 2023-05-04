function TimeDelete(id) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: true
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this timesheet!",
        icon: 'warning',
        width: '300',
        height: '100',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/User/Home/DeleteTimeSheet",
                data: { 'id': id },
                success: function (result) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success')
                    $('.tbl1').html($(result).find('.tbl1').html());
                },
                error: function () {
                    console.log("Erroor");
                }
            });

        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',

            )
        }
    })
}

function GoalDelete(id) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: true
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this timesheet!",
        icon: 'warning',
        width: '300',
        height: '100',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/User/Home/DeleteTimeSheet",
                data: { 'id': id },
                success: function (result) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success')
                    $('.tbl').html($(result).find('.tbl').html());
                },
                error: function () {
                    console.log("Erroor");
                }
            });

        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',

            )
        }
    })
}

function EditTimeSheet(id) {
    $.ajax({
        type: "POST",
        url: "/User/Home/EditGoalTimeSheet",
        data: { id: id },
        success: function (result) {

            var mission = document.getElementById('TimemissionId');
            mission.value = result.timesheet.missionId;

            var date1 = document.getElementById('date1');
            date1.value = result.timesheet.dateVolunteered;

            var hour1 = document.getElementById('hour1');
            hour1.value = String(result.timesheet.timesheetTime).split(":")[0];

            var min1 = document.getElementById('min1');
            min1.value = String(result.timesheet.timesheetTime).split(":")[1];

            var notes = document.getElementById('notes1');
            notes.value = result.timesheet.notes;

            var Hidden = document.getElementById('TimeSheetId1');
            Hidden.value = result.timesheet.timesheetId;    

        },
        error: function () {
            console.log("Erroor");
        }
    });
}


function EditGoalSheet(id) {
    $.ajax({
        type: "POST",
        url: "/User/Home/EditGoalTimeSheet",
        data: { id: id },
        success: function (result) {
            console.log(result)
            var mission = document.getElementById('mission');
            mission.value = result.timesheet.missionId;

            document.getElementById('mission').setAttribute('disabled', 'disabled');

            document.getElementById('m2').value = result.timesheet.missionId;

            var action = document.getElementById('Gaction');
            action.value = result.timesheet.action; 

            var dateVolunteered = document.getElementById('dateVolunteered');
            dateVolunteered.value = result.timesheet.dateVolunteered;

            var notes = document.getElementById('notes');
            notes.value = result.timesheet.notes;

            var Hidden = document.getElementById('TimeSheetId2');
            Hidden.value = result.timesheet.timesheetId; 

            CheckGoalValue(result.timesheet.missionId)
        },
        error: function () {
            console.log("Erroor");
        }
    });
}

function CheckGoalValue(missionid) {
    if (missionid == null) {

        var id = document.getElementById("AddGoalSelect").value;
    }
    else {
        id = missionid;
    }

    alert(id)
    $.ajax({
        type:"GET",
        url:"/User/Home/GetGoalValue",
        data: { id: id },
        success: function (res) {
            console.log(res);
            document.getElementById("GoalValue").max = res.remGoalValue;
            document.getElementById("Gaction").max = res.remGoalValue;
        },
        error: function () {
            console.log("check Goal Value Error")
        }
    })

}