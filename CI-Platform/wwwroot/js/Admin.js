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
    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetCmsPage",
        success: function (res) {
            $("#Admin").html(res)
        },
        error: function () {
            alert("Cms Error")
        }
    });

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


/*-----------------------------------------------------------------------------------------Theme--------------------------------------------------- */
function DeleteTheme(ThemeId) {
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/DeleteTheme",
        data: { ThemeId: ThemeId },
        success: function (res) {
           
            Theme();
            
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


function UpdateTheme() {

    var theme = document.getElementById("mtheme").value;
    var themmeid = document.getElementById("mthemeid").value;
   

    $.ajax({
        method: "POST",
        url: "/Admin/Admin/UpdateMissionTheme",
        data: { theme: theme, themmeid: themmeid },
        success: function (result) {
            $(".btn-close").click();
            Theme();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}
function Addtheme() {

    var theme = document.getElementById("missionTheme").value;
   
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/AddMissionTheme",
        data: { theme: theme},
        success: function (result) {
            $(".btn-close").click();
            Theme();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}
/*---------------------------------------------------------------------------------------- -Skill --------------------------------------------------- */

function DeleteSkill(skillid) {
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/DeleteSkill",
        data: { skillid: skillid },
        success: function (res) {
            Skills();
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

function UpdateSkill() {
    var skill = document.getElementById("mskill").value;
    var skillid = document.getElementById("mskillid").value;


    $.ajax({
        method: "POST",
        url: "/Admin/Admin/UpdateSkill",
        data: { skill: skill, skillid: skillid },
        success: function (result) {
            $(".btn-close").click();
            Skills();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}
function AddSkill() {
    var skill = document.getElementById("missionSkill").value;


    $.ajax({
        method: "POST",
        url: "/Admin/Admin/AddSkill",
        data: { skill: skill},
        success: function (result) {
            $(".btn-close").click();
            Skills();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    });
}

//---------------------------------------------------Application--------------------------------------
function approve(id) {

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/ApproveApplication",
        data: { id: id },
        success: function (result) {
            Application();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    })
}

function reject(id) {

    $.ajax({
        method: "GET",
        url: "/Admin/Admin/RejectApplication",
        data: { id: id },
        success: function (result) {
            Application();

        },
        error: function () {
            alert("Editt Theme Error")
        }
    })
}
//--------------------------------------------------------Admin--------------------------------------------------
