var card = document.getElementsByClassName("missioncard")
var cardimg = document.getElementsByClassName("card-img");
var carddisplay = document.getElementsByClassName("card");
var theme = document.getElementsByClassName("theme-btn");
var search = "";
var sortValue = ""
if (localStorage.getItem("view") === "list") {
    list();
}


function list() {
    localStorage.setItem("view", "list");
    for (i = 0; i < card.length; i++) {
        card[i].style.width = "100%";
        card[i].style.marginTop = "1%";
        carddisplay[i].style.display = "flex";
        carddisplay[i].style.flexDirection = "row";
        cardimg[i].style.height = "48vh";
        theme[i].style.top = "95%";
        // theme[i].style.left = "34%";
    }
}


function grid() {

    localStorage.setItem("view", "grid");
    for (i = 0; i < card.length; i++) {

        if (screen.width > 1023) {
            card[i].style.width = "33%";
            carddisplay[i].style.flexDirection = "column";
            cardimg[i].style.width = "100%";
            theme[i].style.top = "95%";
            theme[i].style.left = "35%";
        }
        else {
            card[i].style.width = "50%";
            carddisplay[i].style.flexDirection = "column";
            cardimg[i].style.width = "100%";
            theme[i].style.top = "95%";
            theme[i].style.left = "35%";

        }
    }
}








$(document).ready(function () {
    mySearch();
    LoadMission();
});


//search
function mySearch()
{
    var Search = $("input[name='searchQuery']").val();
    if (Search == '')
        Search = '';
    $.ajax({
        url: "/Landingpage/_Missions",
        type: "GET",
        data: { 'search': Search },
        
        success: function (res) {
            $("#missions").html('');
            $("#missions").html(res);
        },
        error: function () {
            alert("some Error");
        }
    })
}

function LoadMission(sortValue) {
    console.log(sortValue)

    @
}










//sort

//search
function myCountry() {

    var country = [];
    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
    });

   
    $.ajax({
        url: "/landingpage/landingpage",
        type: "GET",
        data: { 'countryId': country.toString() },
    })
    console.log(country)
}

//Fiters
//function updateUrlForCountry() {

//    const country = Array.from(document.querySelectorAll('input[name="country"]:checked')).map(el => el.id);
//    let url = window.location.href;
//    let separator = url.indexOf('?') !== -1 ? '&' : '?';
//    url += separator + 'ACountries=' + country;
//    window.location.href = url;
//}


function countryBtn() {
    var c = document.getElementById("co");
    c.style.display = "none";
    let url = window.location.href;
    c = "";
    url = url.replace(/ACountries=([^&]*)/, 'ACountries=' + c);
    window.location.href = url;
}

function updateUrlForCity() {

    const city = Array.from(Document.querySelectorAll('input[name="city"]:checked')).map(el => el.id);
    let url = window.location.href;
    let separator = url.indexOf('?') !== -1 ? '&' : '?';
    url += separator + 'ACities=' + city;
    window.location.href = url;
}

function cityBtn() {
    var c = document.getElementById("ci");
    c.style.display = "none";
    let url = window.location.href;
    c = "";
    url = url.replace(/ACity=([^&]*)/, 'ACity=' + c);
    window.location.href = url;
}


