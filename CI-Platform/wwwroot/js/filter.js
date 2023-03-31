
var checkboxes = document.querySelectorAll(".checkbox");

let filtersSection = document.querySelector(".filters-section");

var listArray = [];

var filterList = document.querySelector(".filter-list");

var len = listArray.length;

for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function () {
        if (this.checked == true) {
            addElement(this, this.value);
        }
        else {
            removeElement(this.value);
            console.log("unchecked");
        }
    })
}

function addElement(current, value) {
    let filtersSection = document.querySelector(".filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    //createdTag.classList.add('border border-dark');
    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    createdTag.setAttribute('name', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    let cross = '&times';

    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        elementToBeRemoved.remove();
        console.log(current);
        current.checked = false;
        mySearch();
    })

    crossButton.innerHTML = cross;

    // let crossButton = '&times;'

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);
    mySearch();

}

function removeElement(value) {

    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);
    mySearch();
}

//function myFunction() {

//    var input, filter, cards, cardContainer, title, i;
//    input = document.getElementById("myFilter");
//    filter = input.value.toUpperCase();
//    cardContainer = document.getElementById("myItems");
//    cards = cardContainer.getElementsByClassName("card");

//    for (i = 0; i < cards.length; i++) {
//        title = cards[i].querySelector(".card-body h5.card-title");
//        if (title.innerText.toUpperCase().indexOf(filter) > -1) {
//            cards[i].style.display = "";
//        } else {
//            cards[i].style.display = "none";
//        }
//    }
//}


//var debounceTimer;

//function debounce(func, delay)
//{
//    clearTimeout(debounceTimer);
//    debounceTimer = setTimeout(func, delay);
//}


//document.getElementById("search-bar").addEventListener("input", function () {
//    debounce(function () {
//        search(document.getElementById("search-bar").value);
//    }, 1500); // adjust the delay time as needed
//});

//function search(query) {
//    // Get the current URL
//    let url = window.location.href;

//    let separator = url.indexOf('?') !== -1 ? '&' : '?';

//    // Check if the searchQuery parameter already exists in the URL
//    if (url.includes('searchQuery=')) {
//        // Replace the value of the searchQuery parameter
//        url = url.replace(/searchQuery=([^&]*)/, 'searchQuery=' + query);
//    } else {
//        // Append the parameter to the URL
//        url += separator + 'searchQuery=' + query;
//    }

//    // Navigate to the updated URL
//    window.location.href = url;


//}


