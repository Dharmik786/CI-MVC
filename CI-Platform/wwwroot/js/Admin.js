$(document).ready(function () {
    $('#example').DataTable();
    $('.user').click();
});

function User() {
    $(".user").addClass("active");
    $(".page,.mission,.theme,.skills,.application,.story,.management").removeClass("active");

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
    
    $(".page").addClass("active");
    $(".user,.mission,.theme,.skills,.application,.story,.management").removeClass("active");


}

function Mission() {

    $(".mission").addClass("active");
    $(".user,.page,.theme,.skills,.application,.story,.management").removeClass("active");

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetMission",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("User Error")
        }
    });
}

function Theme() {

    $(".theme").addClass("active");
    $(".user,.page,.mission,.skills,.application,.story,.management").removeClass("active");

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetThemes",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("Application Error")
        }
    });
}

function Skills() {

    $(".skills").addClass("active");
    $(".user,.page,.mission,.theme,.application,.story,.management").removeClass("active");

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetSkills",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("User Error")
        }
    });
}

function Application() {

    $(".application").addClass("active");
    $(".user,.page,.mission,.skills,.theme,.story,.management").removeClass("active");

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetApplication",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("Application Error")
        }
    });
}

function Story() {

    $(".story").addClass("active");
    $(".user,.page,.mission,.skills,.theme,.application,.management").removeClass("active");

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetStory",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("Application Error")
        }
    });
}

function Management() {
    $(".management").addClass("active");
    $(".user,.page,.mission,.skills,.theme,.application,.story").removeClass("active");
}

function DeleteTheme(ThemeId) {
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/DeleteTheme",
        data: { ThemeId: ThemeId },
        success: function (res) {
            debugger
             $('#Themes').html($(res).find('#Themes').html());
            /*$("#Themes").load(location.href + " #Themes>*", "");*/
           
        },
        error: function () {
            alert("User Error")
        }
    });
}

function EditTheme(ThemeId) {
    $.ajax({
        method: "GET",
        url: "/Admin/Admin/EditMissionTheme",
        data: { ThemeId: ThemeId },
        success: function (result) {
            console.log(result)

            document.getElementById("mtheme").value = result.missionTheme.title;
            document.getElementById("mthemeid").value = result.missionTheme.missionThemeId;

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}

function DeleteSkill(skillid) {
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/DeleteSkill",
        data: { skillid: skillid },
        success: function (res) {
            $('.Themes').html($(res).find('.Themes').html());
        },
        error: function () {
            alert("User Error")
        }
    });
}
function EditSkill(SkillId) {
    $.ajax({
        method: "GET",
        url: "/Admin/Admin/EditSkill",
        data: { SkillId: SkillId },
        success: function (result) {
            console.log(result)

            document.getElementById("mskill").value = result.skill.skillName;
            document.getElementById("mskillid").value = result.skill.skillId;

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}
