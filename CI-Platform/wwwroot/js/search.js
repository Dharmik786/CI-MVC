var card = document.getElementsByClassName("missioncard")
var cardimg = document.getElementsByClassName("card-img");
var carddisplay = document.getElementsByClassName("card");
var theme = document.getElementsByClassName("theme-btn");
var search = "";
var sortValue = ""


function list() {
    localStorage.setItem("view", "list");
    //for (i = 0; i < card.length; i++) {
        //card[i].style.width = "100%";
        //card[i].style.marginTop = "1%";
        //carddisplay[i].style.display = "flex";
        //carddisplay[i].style.flexDirection = "row";
        //cardimg[i].style.height = "48vh";
        //theme[i].style.top = "95%";
        // theme[i].style.left = "34%";
    // }
    var grid1 = document.getElementById('myItem');
    var list1 = document.getElementById('list-view');
    grid1.style.display = "none";
    list1.style.display = "block";
    document.getElementById('grid').style.backgroundColor = "lightgrey";
    document.getElementById('list').style.backgroundColor = "white";
}


function grid() {

    localStorage.setItem("view", "grid");
    var grid1 = document.getElementById('myItem');
    var list1 = document.getElementById('list-view');
    list1.style.display = "none";
    grid1.style.display = "flex";
    document.getElementById('grid').style.backgroundColor = "white";
    document.getElementById('list').style.backgroundColor = "lightgrey";
    //for (i = 0; i < card.length; i++) {

    //    if (screen.width > 1023) {
    //        card[i].style.width = "33%";
    //        carddisplay[i].style.flexDirection = "column";
    //        cardimg[i].style.width = "100%";
    //        theme[i].style.top = "95%";
    //        theme[i].style.left = "35%";
    //    }
    //    else {
    //        card[i].style.width = "50%";
    //        carddisplay[i].style.flexDirection = "column";
    //        cardimg[i].style.width = "100%";
    //        theme[i].style.top = "95%";
    //        theme[i].style.left = "35%";

    //    }
    //}
}




$(document).ready(function () {
    mySearch();
   /* LoadMission(sortValue);
    myCountry();*/
});


//search
function mySearch(jpg) {
    check();
    
    console.log(jpg);

    var id = $(".sortSelect").val();
    console.log(id);

    var Search = $("input[name='searchQuery']").val();

    var country = [];

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

    var skill = [];
    $("input[type='checkbox'][name='skill']:checked").each(function () {
        skill.push($(this).val());
    })
    console.log(skill)

    $.ajax({
        url: "/User/Landingpage/_Missions",
        type: "POST",
        data: { 'search': Search, 'sortValue': id, 'country': country, 'city': city, 'theme': theme, 'skill': skill, 'jpg': jpg },

        success: function (res) {
            $("#missions").html('');
            $("#missions").html(res);
            $("#countMission1").html($("#countMission2").html());
            $("#countMission2").hide();

           
        },
        error: function () {
            alert("some Error");
        }
        
    })
}


function pagination(jpg) {
    console.log(jpg);
    $.ajax({
        url: '/User/Landingpage/_Missions',
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
    console.log("Pagination");
});



