var card = document.getElementsByClassName("missioncard")
var cardimg = document.getElementsByClassName("card-img");
var carddisplay = document.getElementsByClassName("card");
var theme = document.getElementsByClassName("theme-btn");
var search = "";
var sortValue = ""



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
   /* LoadMission(sortValue);
    myCountry();*/
});


//search
function mySearch(sortValue) {

    var Search = $("input[name='searchQuery']").val();
    //if (Search == '')
    //    Search = '';
    console.log(Search)


    var country = [];

    //$('#countryDropdown').find("input:checked").each(function (i, ob) {
    //    country.push($(ob).val());
    //});
    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
    });
    console.log(country)


    var city = [];
    $("input[type='checkbox'][name='city']:checked").each(function () {
        city.push($(this).val());
    });
    console.log(city)

    var theme = [];
    $("input[type='checkbox'][name='theme']:checked").each(function () {
        theme.push($(this).val());
    });
    console.log(theme)

    $.ajax({
        url: "/Landingpage/_Missions",
        type: "POST",
        data: { 'search': Search, 'sortValue': sortValue, 'country': country, 'city': city, 'theme': theme },

        success: function (res) {
            $("#missions").html('');
            $("#missions").html(res);
           
        },
        error: function () {
            alert("some Error");
        }
        
    })
}


function pagination(jpg) {
    console.log(jpg);
    $.ajax({
        url: '/Landingpage/_Missions',
        type: 'GET',
        data: { 'jpg': jpg },
        success: function (res) {
            $("#missions").html(res);
        },

        error: function (res) {
            alert("error");
        }
    });
}

$(document).ready(function () {
    pagination(jpg = 1);
    console.log("hello shubh");
});



