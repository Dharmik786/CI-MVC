CKEDITOR.replace('editor1');


function loginalert() {
    alert("login first");
}

var loader = document.getElementById('preloader');
window.addEventListener("load", function () {
    loader.style.display = "none";
})
//============================= Image Preview ================
const inputDiv = document.querySelector(".input-div")
const input = document.querySelector("#imageupload")
const output = document.querySelector("#preview")
let imagesArray = [];
let FilesToUpload = [];
let FilesNameToUpload = [];
let DelImg = [];
$("#preview").html("");

$("#imageupload").on("change", () => {

    const files = input.files
    for (let i = 0; i < files.length; i++) {

        FilesNameToUpload.push(files[i].name)
        var file = files[i]
        var reader = new FileReader();
        reader.onload = function (event) {
            FilesToUpload.push(event.target.result);
        }

        reader.readAsDataURL(file);

    }

    displayImages()
})




function displayImages() {
    $("#preview").html("");

    setTimeout(function () {
        for (let i = 0; i < FilesToUpload.length; i++) {
            $("#preview").append(`<div class="image"><img src="${FilesToUpload[i]}" alt="image">
                                                             <button type="button" class="position-absolute btn image-close-btn" onclick="deleteImage(${i})" style="right:-3px;font-size:15px">&#x2716</button>
                                                           </div>`)
        }

    }, 500)
}

function deleteImage(i) {
    FilesToUpload.splice(i, 1);
    FilesNameToUpload.splice(i, 1);

    displayImages()
}