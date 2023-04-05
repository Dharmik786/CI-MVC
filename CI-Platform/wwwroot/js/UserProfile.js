$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

function ChangePsw() {
    var oldPsw = document.getElementById('inputPassword1').value;
    var NewPsw = document.getElementById('inputPassword2').value;
    var CnfPsw = document.getElementById('inputPassword3').value;
   
    $.ajax({
        type: "POST",
        url: "/Home/ChangePassword",
        data: { 'oldPsw': oldPsw, 'NewPsw': NewPsw, 'CnfPsw': CnfPsw },
        success: function (result) {
            if (result == true) {
                alert("Password Change SuccessFully");
                document.getElementById("PswClose").click();
            }
            else if (result == false) {
                alert("Your Old Passoword is Incorrect")
            }
        },
        error: function () {
            console.log("Erroor");
        }
    });
   
}

