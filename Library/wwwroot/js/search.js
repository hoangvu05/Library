

var filter1 = document.querySelector(".filter.e1");
var clickable1 = document.querySelector(".clickable.e1");
var arrow1 = document.querySelector(".arrow.e1")
var turn1 = filter1.getAttribute("data-type");

var filter2 = document.querySelector(".filter.e2");
var clickable2 = document.querySelector(".clickable.e2");
var arrow2 = document.querySelector(".arrow.e2")
var turn2 = filter2.getAttribute("data-type");

var filter3 = document.querySelector(".filter.e3");
var clickable3 = document.querySelector(".clickable.e3");
var arrow3 = document.querySelector(".arrow.e3")
var turn3 = filter3.getAttribute("data-type");

var filter4 = document.querySelector(".filter.e4");
var clickable4 = document.querySelector(".clickable.e4");
var arrow4 = document.querySelector(".arrow.e4")
var turn4 = filter4.getAttribute("data-type");

function addNewChild(content, lastDigit) {
    var newInput = document.createElement("input");
    newInput.setAttribute("class", "e" + lastDigit)
    newInput.type = "checkbox";
    newInput.name = content;
    var newLabel = document.createElement("label");
    newLabel.setAttribute("class", "e" + lastDigit)
    newLabel.textContent = content;

    window["filter" + lastDigit].appendChild(newInput);
    window["filter" + lastDigit].appendChild(newLabel);
}

function openDisplay(lastDigit) {
    window["turn" + lastDigit] = "on";
    window["arrow" + lastDigit].setAttribute("src", "/picture/down-arrow.png");
}

function closeDisplay(lastDigit) {
    window["turn" + lastDigit] = "off";
    window["arrow" + lastDigit].setAttribute("src", "/picture/up-arrow.png");
    var oldInput = document.querySelectorAll("input.e" + lastDigit);
    for (var i = 0; i < oldInput.length; i++) {
        oldInput[i].style.display = 'none';
    }
    var oldLabel = document.querySelectorAll("label.e" + lastDigit);
    for (var i = 0; i < oldLabel.length; i++) {
        oldLabel[i].style.display = 'none';
    }
}


clickable1.addEventListener("click", (event) => {
    var lastDigit = "1";
    if (window["turn" + lastDigit] == "off") {
        openDisplay(lastDigit)
        addNewChild("Lãng mạn", lastDigit);
        addNewChild("Hành động", lastDigit);
    }
    else {
        closeDisplay(lastDigit);
    }
})

clickable2.addEventListener("click", (event) => {
    var lastDigit = "2";
    if (window["turn" + lastDigit] == "off") {
        openDisplay(lastDigit)
        addNewChild("Lưu Quang Vũ", lastDigit);
        addNewChild("Nguyễn Kim", lastDigit);
    }
    else {
        closeDisplay(lastDigit);
    }
})

clickable3.addEventListener("click", (event) => {
    var lastDigit = "3";
    if (window["turn" + lastDigit] == "off") {
        openDisplay(lastDigit)
        addNewChild("2015", lastDigit);
        addNewChild("2014", lastDigit);
    }
    else {
        closeDisplay(lastDigit);
    }
})

clickable4.addEventListener("click", (event) => {
    var lastDigit = "4";
    if (window["turn" + lastDigit] == "off") {
        openDisplay(lastDigit)
        addNewChild("Trẻ", lastDigit);
        addNewChild("Giáo Dục", lastDigit);
    }
    else {
        closeDisplay(lastDigit);
    }
})




