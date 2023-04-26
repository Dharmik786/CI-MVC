﻿$(document).ready(function () {
    $('#example').DataTable();
    /* $('.user').click();*/
    User();
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


    $.ajax({
        method: "GET",
        url: "/Admin/Admin/GetBanner",
        success: function (res) {
            $("#Admin").html(" ");
            $("#Admin").html(res)
        },
        error: function () {
            alert("banner Error")
        }
    });
}


/*-----------------------------------------------------------------------------------------Theme--------------------------------------------------- */
function DeleteTheme(ThemeId) {



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
                method: "POST",
                url: "/Admin/Admin/DeleteTheme",
                data: { ThemeId: ThemeId },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'Theme has been deleted.',
                        'success'
                    )
                    Theme();

                },
                error: function () {
                    alert("theme Error")
                }
            });


        }
    })

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
function themeKey() {
    $(".theme-validation").addClass("d-none");
}
function UpdateTheme() {

    var theme = document.getElementById("mtheme").value;
    var themmeid = document.getElementById("mthemeid").value;

    if (theme == "" || theme == null) {
        $(".theme-validation").removeClass("d-none");
    }
    else {
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

}
function Addtheme() {

    var theme = document.getElementById("missionTheme").value;

    if (theme == "" || theme == null) {
        $(".theme-validation").removeClass("d-none");
    }
    else {
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


}
/*---------------------------------------------------------------------------------------- -Skill --------------------------------------------------- */

function DeleteSkill(skillid) {

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
                method: "POST",
                url: "/Admin/Admin/DeleteSkill",
                data: { skillid: skillid },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'Skill has been deleted.',
                        'success'
                    )

                    Skills();
                },
                error: function () {
                    alert("Skill Error")
                }
            });

        }
    })
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

    if (skill == "" || skill == null) {
        $(".skill-validation").removeClass("d-none");
    }
    else {
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

}
function AddSkill() {
    var skill = document.getElementById("missionSkill").value;
    if (skill == "" || skill == null) {
        $(".skill-validation").removeClass("d-none");
    }
    else {
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

}
function skillPress() {
    $(".skill-validation").addClass("d-none");

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
//--------------------------------------------------------User--------------------------------------------------
function changeUser() {

    var file = document.getElementById("InputImg").files[0];

    const reader = new FileReader();
    reader.readAsDataURL(file)
    reader.onload = function (e) {
        document.getElementById('Img').src = reader.result;
    }
    reader.readAsDataURL(e.target.files[0]);
}


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
function Fnamekey() {
    $("#Fname-val").addClass("d-none");
}
function Lnamekey() {
    $("#Lname-val").addClass("d-none");
}
function Emailkey() {
    $("#Email-val").addClass("d-none");
}
function Passkey() {
    $("#Pass-val").addClass("d-none");
}
function Depkey() {
    $("#dep-val").addClass("d-none");
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


    if (Fname == null || Fname == "") {
        $("#Fname-val").removeClass("d-none");
    }
    else if (Lname == null || Lname == "") {
        $("#Lname-val").removeClass("d-none")
    }
    else if (Email == null || Email == "") {
        $("#Email-val").removeClass("d-none")
    }
    else if (Password == null || Password == "") {
        $("#Pass-val").removeClass("d-none")
    }
    else if (Department == null || Department == "") {
        $("#dep-val").removeClass("d-none")
    }
    else {
        if (file != undefined) {
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
                        $(".btn-close").click();
                        User();
                    },
                    error: function () {
                        alert("user Edit  img error")
                    }

                })
            }
        }
        else {
            $.ajax({

                url: "/Admin/Admin/EditUser",
                method: "POST",
                data:
                {
                    'Id': Id, 'Fname': Fname, 'Lname': Lname, 'Email': Email, 'Password': Password, 'Employeeid': Employeeid, 'Department': Department,
                    'Profiletext': Profiletext, 'status': status, 'Country': Country, 'City': City
                },
                success: function (res) {
                    $(".btn-close").click();
                    User();
                },
                error: function () {
                    alert("user Edit error")
                }

            })
        }
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
//----------------------------------------------------------banner -------------------------------------------

function BannerChange() {
    document.getElementById("imginput").click();
}

function change() {

    var file = document.getElementById("imginput").files[0];

    const reader = new FileReader();
    reader.readAsDataURL(file)
    reader.onload = function (e) {
        document.getElementById('imgTag').src = reader.result;
    }
    reader.readAsDataURL(e.target.files[0]);
}

function AddUpdateBanner() {


    //var img = document.getElementById("imginput").files[0];
    var desc = document.getElementById("discrption").value;
    var sort = document.getElementById("sortorder").value;
    var id = document.getElementById("BannerId").value;

    var file = document.getElementById("imginput").files[0];

    if (file != undefined) {
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {

            document.getElementById('imgTag').src = reader.result;
            var base64Image = e.target.result;
            console.log(base64Image);

            $.ajax({

                url: "/Admin/Admin/AddBanner",
                method: "POST",
                data: { 'Id': id, 'image': base64Image, 'desc': desc, 'sort': sort },
                success: function (res) {
                    $(".btn-close").click();
                    Management();
                },
                error: function () {
                    alert("banner Edit error")
                }


            })
        }
    }
    else {
        $.ajax({

            url: "/Admin/Admin/AddBanner",
            method: "POST",
            data: { 'Id': id, 'desc': desc, 'sort': sort },
            success: function (res) {
                $(".btn-close").click();
                Management();
            },
            error: function () {
                alert("banner Edit error")
            }


        })
    }


}


function EditBanner(id) {

    $.ajax({

        url: "/Admin/Admin/GetBannerById",
        method: "GET",
        data: { 'Id': id },
        success: function (res) {
            console.log(res);
            var i = document.getElementById("BannerId").value = res.bannerId;
            document.getElementById("imgTag").src = res.image;
            document.getElementById("discrption").value = res.text;
            document.getElementById("sortorder").value = res.sortOrder;
        },
        error: function () {
            alert("banner Edit error")
        }


    })
}
function Addbanner() {

    document.getElementById("BannerId").value = 0;
    document.getElementById("imgTag").src = "/image/blank.jpeg";
    document.getElementById("discrption").value = null;
    document.getElementById("sortorder").value = null;
}

function deletebanner(id) {
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
                url: "/Admin/Admin/DeleteBanner",
                method: "POST",
                data: { 'Id': id },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'Banner has been deleted.',
                        'success'
                    )
                    Management()
                },
                error: function () {
                    alert("banner delete error")
                }


            });

        }
    })
}
//----------------------------------------------------------Misison-------------------------------------------

//function EditMission(id) {
//    alert(id);

//    $.ajax({
//        url: "/Admin/Admin/EditMission",
//        method: "GET",
//        data: { id: id },
//        success: function (res) {
//            console.log(res)

//            document.getElementById('Mid').value = res.mission.missionId;
//            document.getElementById('Title').value = res.mission.title;
//            document.getElementById('ShortDescription').value = res.mission.shortDescription;
//            document.getElementById('Description').value = res.mission.description;
//            document.getElementById('Country').value = res.mission.countryId;
//            document.getElementById('City').value = res.mission.cityId;
//            document.getElementById('OrName').value = res.mission.organizationName;
//            document.getElementById('OrDetail').value = res.mission.organizationDetail;
//            document.getElementById('Type').value = res.mission.missionType;
//            document.getElementById('Sdate').value = res.mission.startDate;
//            document.getElementById('Edate').value = res.mission.endDate;
//            document.getElementById('seats').value = res.mission.seats;
//            document.getElementById('Deadline').value = res.mission.deadline;
//            document.getElementById('Theme').value = res.mission.themeId;
//            document.getElementById('Skill').value = res.mission.skillId;

//            //document.getElementById('Img').src = res.Mission.avatar;

//        },
//        error: function () {
//            alert("Get user details error")
//        }

//    })
//}

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

//const input = document.querySelector(".file")
//let imagesArray = []

function Cancle() {
    Mission()
}

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
        data: { id: id },
        success: function (res) {
            $("#Admin").html(res);
        },
        error: function () {
            alert("Mission edit error")
        }


    })
}

function deletemission(missionId) {


    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn-success',
            cancelButton: 'btn-danger'
        },
        buttonsStyling: true
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this goalsheet!",
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
                url: '/Admin/Admin/DeleteMission',
                type: 'POST',
                data: { missionId: missionId },

                success: function (response) {

                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your goalsheet has been deleted.',
                        'success'
                    )
                    /* $('#example').html($(response).find('#example').html());*/

                    Mission();

                },
                error: function () {
                    alert("could not comment");
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

//const inputDiv = document.querySelector(".input-div")
//const input = document.querySelector("#imageupload")
//const output = document.querySelector("#preview")
//let imagesArray = [];
//let FilesToUpload = [];
//let FilesNameToUpload = [];
//let DelImg = [];
//$(output).html("");

//input.addEventListener("change", () => {

//    const files = input.files
//    for (let i = 0; i < files.length; i++) {
//        imagesArray.push(files[i])
//        FilesNameToUpload.push(files[i].name)
//    }

//    displayImages()
//})

function dk1() {

    const input = document.querySelector("#imageupload")

    const files = input.files
    for (let i = 0; i < files.length; i++) {
        imagesArray.push(files[i])
        FilesNameToUpload.push(files[i].name)
    }
    console.log(imagesArray)
    displayImages()


}

//inputDiv.addEventListener("drop", () => {
//    e.preventDefault()
//    const files = e.dataTransfer.files
//    for (let i = 0; i < files.length; i++) {
//        if (!files[i].type.match("image")) continue;

//        if (imagesArray.every(image => image.name !== files[i].name))
//            imagesArray.push(files[i])
//    }
//    displayImages()
//})


//function dk2() {
//    e.preventDefault()
//    const files = e.dataTransfer.files
//    for (let i = 0; i < files.length; i++) {
//        if (!files[i].type.match("image")) continue;

//        if (imagesArray.every(image => image.name !== files[i].name))
//            imagesArray.push(files[i])
//    }
//    displayImages()

//}


//function displayImages() {
//    $(output).html("");
//    FilesToUpload.length = 0;

//    if (imagesArray != null) {
//        imagesArray.forEach((image, i) => {
//            var file = imagesArray[i];
//            console.log(file)
//            var reader = new FileReader();
//            reader.onload = function (event) {
//                $(output).append(`<div class="image">
//                                                                 <img src="${event.target.result}" alt="image">
//                                                                 <button type="button" class="position-absolute btn image-close-btn" onclick="deleteImage(${i})" style="right:-3px;font-size:15px">&#x2716</button>
//                                                               </div>`)
//                FilesToUpload.push(event.target.result);
//            }

//            reader.readAsDataURL(file);


//        })

//        console.log("aiufh", FilesToUpload);
//    }
//}

//function deleteImage(i) {
//    console.log($(this).parent())
//    console.log(imagesArray[i])

//    imagesArray.splice(i, 1);
//    DelImg.push(imagesArray[i]);
//    console.log("del", DelImg)

//    console.log("hihjdjd", imagesArray);
//    displayImages()
//}