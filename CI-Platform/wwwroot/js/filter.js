
var checkboxes = document.querySelectorAll(".checkbox");

let filtersSection = document.querySelector(".filters-section");

var listArray = [];

var filterList = document.querySelector(".filter-list");

var len = listArray.length;

for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function () {
        if (this.checked == true) {
            clear()
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

function clear() {

    $(".clear-all").remove();

    let filtersSection = document.querySelector(".filters-section");

    let clearAll = document.createElement('span');
    clearAll.classList.add('clear-all');
    clearAll.classList.add('px-2');
    clearAll.classList.add('me-2');
    clearAll.innerHTML = "Clear All";
    clearAll.style.cursor = 'pointer';
    clearAll.style.border = '2px solid';
    clearAll.style.borderRadius = '22px';
    clearAll.style.color = '#f88634';
    clearAll.setAttribute("onclick", "ClearAll()")

    filtersSection.appendChild(clearAll);
}

function check() {
    var numberOfChecked = $('input:checkbox:checked').length;
    if (numberOfChecked <= 0) {
        $(".clear-all").remove();
    }
}


function ClearAll() {
    $(".filter-close-button").click()
    $(".clear-all").remove();
}

function removeElement(value) {
    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);
    mySearch();

}


