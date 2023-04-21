$(document).ready(function () {
    $('#example').DataTable();
    /* $('.user').click();*/
    Mission();
    currentTime();
});


function currentTime() {
    var dashboadtime = document.getElementById("dashboadtime");
    var time = new Date();
    var dateString = time.toDateString();
    var timeString = time.toLocaleTimeString();
    dashboadtime.innerHTML = dateString + ' ' + timeString;
    let t = setTimeout(function () { currentTime() }, 1000);
}



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
        data: { theme: theme },
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

function SkillStatus(id) {
    $.ajax({
        method: "POST",
        url: "/Admin/Admin/SkillStatus",
        data: { id: id },
        success: function (result) {
            Skills();

        },
        error: function () {
            alert(" skill status Error")
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
        data: { skill: skill },
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
function editAdminUser(id) {

    $.ajax({
        url: "/Admin/Admin/GetUserDetails",
        method: "GET",
        data: { id: id },

        success: function (res) {
            console.log(res)

            document.getElementById('userId').value = res.user.userId;
            document.getElementById('Fname').value = res.user.firstName;
            document.getElementById('Lname').value = res.user.lastName;
            document.getElementById('Email').value = res.user.email;
            document.getElementById('Password').value = res.user.password;
            document.getElementById('Employeeid').value = res.user.employeeId;
            document.getElementById('Department').value = res.user.department;
            document.getElementById('Profiletext').value = res.user.profileText;


            document.getElementById('Status').value = res.user.status;
            document.getElementById('City').value = res.user.cityId;
            document.getElementById('Country').value = res.user.countryId;

            document.getElementById('Img').src = res.user.avatar; 

        },
        error: function () {
            alert("Get user details error")
        }

    })
}

function ImgDiv() {

    $("#InputImg").click();
}

function UpdateUser() {


    var Id = document.getElementById('userId').value;

    var Fname = document.getElementById('Fname').value;
    var Lname = document.getElementById('Lname').value;
    var Email = document.getElementById('Email').value;
    var Password = document.getElementById('Password').value;
    var Employeeid = document.getElementById('Employeeid').value;
    var Department = document.getElementById('Department').value;
    var Profiletext = document.getElementById('Profiletext').value;
    var status = document.getElementById('Status').value;
    var City = document.getElementById('City').value;
    var Country = document.getElementById('Country').value;

    var file = document.getElementById("InputImg").files[0];
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function (e) {

        var base64Image = e.target.result;
        console.log(base64Image);

        $.ajax({

            url: "/Admin/Admin/EditUser",
            method: "POST",
            data:
            {
                'Image': base64Image, 'Id': Id, 'Fname': Fname, 'Lname': Lname, 'Email': Email, 'Password': Password, 'Employeeid': Employeeid, 'Department': Department,
                'Profiletext': Profiletext, 'status': status, 'Country': Country, 'City': City
            },
            success: function (res) {

                User();
            },
            error: function () {
                alert("user Edit error")
            }

        })
    }
}

function AddUser() {

    document.getElementById('userId').value = 0;
    document.getElementById('Fname').value = null;
    document.getElementById('Lname').value = null;
    document.getElementById('Email').value = null;
    document.getElementById('Password').value = null;
    document.getElementById('Employeeid').value = null;
    document.getElementById('Department').value = null;
    document.getElementById('Profiletext').value = null;
    document.getElementById('Status').value = null;
    document.getElementById('City').value = null;
    document.getElementById('Country').value = null;
    document.getElementById('Img').src = null;
}

function DeleteUser(id) {



    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {



            $.ajax({

                url: "/Admin/Admin/DeleteUser",
                method: "POST",
                data: { 'Id': id },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'User has been deleted.',
                        'success'
                    )
                    User();
                },
                error: function () {
                    alert("user Edit error")
                }


            })




        }
    })

}
//----------------------------------------------------------Misison-------------------------------------------

function EditMission(id) {
    alert(id);

    $.ajax({
        url: "/Admin/Admin/EditMission",
        method: "GET",
        data: { id: id },
        success: function (res) {
            console.log(res)

            document.getElementById('Mid').value = res.mission.missionId;
            document.getElementById('Title').value = res.mission.title;
            document.getElementById('ShortDescription').value = res.mission.shortDescription;
            document.getElementById('Description').value = res.mission.description;
            document.getElementById('Country').value = res.mission.countryId;
            document.getElementById('City').value = res.mission.cityId;
            document.getElementById('OrName').value = res.mission.organizationName;
            document.getElementById('OrDetail').value = res.mission.organizationDetail;
            document.getElementById('Type').value = res.mission.missionType;
            document.getElementById('Sdate').value = res.mission.startDate;
            document.getElementById('Edate').value = res.mission.endDate;
            document.getElementById('seats').value = res.mission.seats;
            document.getElementById('Deadline').value = res.mission.deadline;
            document.getElementById('Theme').value = res.mission.themeId;
            document.getElementById('Skill').value = res.mission.skillId;

            //document.getElementById('Img').src = res.Mission.avatar;

        },
        error: function () {
            alert("Get user details error")
        }

    })
}

//const fileInput = document.getElementById('fileInput');

//fileInput.addEventListener('change', (e) => {
//    const files = e.target.files;

//    for (let i = 0; i < files.length; i++) {
//        const file = files[i];
//        console.log(file)
//        const reader = new FileReader();
//       // reader.addEventListener('load')
//    }
//})

//const fileInput = document.getElementById('fileInput');

//fileInput.addEventListener('change', (event) => {
//    const files = event.target.files; // get the selected files
//    for (let i = 0; i < files.length; i++) {
//        const file = files[i];

//        console.log(file)

//        const reader = new FileReader();
//        reader.addEventListener('load', (event) => {
//            const contents = event.target.result; // get the contents of the file
//            console.log(`File ${i + 1}: ${file.name}\nContents:\n${contents}`);
//        });
//        reader.readAsText(file); // read the contents of the file
//    }
//});

const input = document.querySelector(".file")
let imagesArray = []

function SaveMission() {
    var files = document.getElementById("fileInput").files;
    console.log(files);

    const imageFiles = [];
    const pdfFiles = [];

    var reader = new FileReader();

    for (let i = 0; i <= files.length; i++) {
        const file = files[i];
        if (file.type.startsWith('image/')) {
            console.log("imgae 6")
            imageFiles.push(files[i]);




            const reader = new FileReader();
            reader.addEventListener('load', (event) => {
                const contents = event.target.result; // get the contents of the file
                imageFiles.push(contents); // push the contents to the imageFiles array
            });
            reader.readAsDataURL(file); 




        }
        else if (file.type === 'application/pdf') {
            console.log("file 6")
            pdfFiles.push(files[i]);
        }
        console.log("Img:", imageFiles)
        console.log("pdf:", pdfFiles)
    };
   
}

function AddMission(id) {

    $.ajax({

        url: "/Admin/Admin/AddMission",
        method: "GET",
        data: {id:id},
        success: function (res) {
            $("#Admin").html(res);
        },
        error: function () {
            alert("Mission edit error")
        }


    })
}