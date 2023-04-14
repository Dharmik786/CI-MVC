$(document).ready(function () {
    $('#example').DataTable();
});

function User() {
    alert("User")
    $(".user").addClass("active");
    $(".page").removeClass("active")

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetUsers",
        success: function (res) {
            $("#Admin").html(res)
        },
        error: function () {
            alert("User Error")
        }
    });

}

function Page() {
    alert("Page")
    $(".page").addClass("active")
    $(".user").removeClass("active")
}

