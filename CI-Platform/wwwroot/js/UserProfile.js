﻿$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})
var loader = document.getElementById('preloader');
window.addEventListener("load", function () {
    loader.style.display = "none";
})

function validatePassword(password) {
    // password must be at least 8 characters long
    if (password.length < 8) {
        return false;
    }

    // password must contain at least one uppercase letter
    if (!/[A-Z]/.test(password)) {
        return false;
    }

    // password must contain at least one lowercase letter
    if (!/[a-z]/.test(password)) {
        return false;
    }

    // password must contain at least one special character
    if (!/[\W_]/.test(password)) {
        return false;
    }

    // password is valid
    return true;
}


function oldp() {
    $('#old').addClass('d-none');
}
function ChangePsw() {
    var oldPsw = document.getElementById('inputPassword1').value;
    var NewPsw = document.getElementById('inputPassword2').value;
    var CnfPsw = document.getElementById('inputPassword3').value;

    const isValid = validatePassword(NewPsw)

    if (oldPsw == "") {
        $('#old').removeClass('d-none');
        $('#old').addClass('d-inline!important');
    }

    else if (NewPsw == "") {
        Swal.fire('Please Enter New Password')
    }
    else if (CnfPsw == "") {
        Swal.fire('Please Enter Confrim Password')
    }
    else if (NewPsw != CnfPsw) {
        Swal.fire('Passowrd and Confrim Password Does not Match')

    }
    else if (isValid == false) {
        Swal.fire('Password is not Valid')
    }

    else {
        $.ajax({
            type: "POST",
            url: "/User/Home/ChangePassword",
            data: { 'oldPsw': oldPsw, 'NewPsw': NewPsw, 'CnfPsw': CnfPsw },
            success: function (result) {
      
                if (result == true) {
                    document.getElementById("PswClose").click();
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Password Change SuccessFully',
                        showConfirmButton: false,
                        timer: 2500
                    })
                }
                else if (result == false) {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'error',
                        title: 'Old Password is incorrect',
                      
                    })
                }
            },
            error: function () {
                console.log("Erroor");
            }
        });
    }



}

document.getElementById('imgDiv').addEventListener("click", e => {
    document.getElementById('inputImg').click();
});

document.getElementById('inputImg').addEventListener("change", e => {
    const reader = new FileReader(); // Create a new FileReader object
    reader.onload = function () {
        document.getElementById('imgDiv').src = reader.result; // Set the source of the image tag to the selected image
    }
    reader.readAsDataURL(e.target.files[0]); // Read the selected file as a data URL
});



function ved1() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");

    for (var i = 0; i < a.length; i++) {
        if (a.options[i].selected == true) {
            a.options[i].selected = false
            c.add(a.options[i])

            ved1()
        }

    }
}
function ved2() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");

    for (var i = 0; i < c.length; i++) {
        if (c.options[i].selected == true) {
            c.options[i].selected = false
            a.add(c.options[i])
            ved2()
        }
    }
}
function ved3() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < a.length;) {
        c.add(a.options[c, i])
    }
}
function ved4() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < c.length;) {
        a.add(c.options[a, i])
    }
}
document.getElementById('skillSave').addEventListener("click", e => {
    var selectedSkills = [];
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
    }

    console.log(selectedSkills);
    $.ajax({
        url: '/User/Home/SaveUserSkills',
        type: 'POST',
        data: { selectedSkills: selectedSkills },

        success: function (response) {

            $('#userskilldiv').html($(response).find('#userskilldiv').html());
            document.getElementById('close').click();

        },
        error: function () {
            alert("could not comment");
        }
    });
});