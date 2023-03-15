
//search
function mySearch() {
    var Search = $("input[name='searchQuery']").val();
    console.log(Search)
    $.ajax({
        url: "/landingpage/landingpage",
        type: "GET",
        data: { 'search': Search },
        
        success: function (res) {
            $("#myItem").html(res);
        },
        error: function () {
            alert("some Error");
        }
    })
   
}

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


