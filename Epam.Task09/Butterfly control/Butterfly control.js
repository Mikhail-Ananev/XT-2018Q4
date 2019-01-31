var forms = document.getElementsByClassName("butterflyControl"),
    i,
    availableOption = [],
    selectedOption = [],
    allRightButton = [],
    rightButton = [],
    leftButton = [],
    allLeftButton = [];

for(i=0; i<forms.length; i++){
    availableOption[availableOption.length] = forms[i].getElementsByClassName('Available')[0];
    selectedOption[selectedOption.length] = forms[i].getElementsByClassName('Selected')[0];
    allRightButton[allRightButton.length] = forms[i].getElementsByClassName('AllRight')[0];
    rightButton[rightButton.length] = forms[i].getElementsByClassName('Right')[0];
    leftButton[leftButton.length] = forms[i].getElementsByClassName('Left')[0];
    allLeftButton[allLeftButton.length] = forms[i].getElementsByClassName('AllLeft')[0];
}

function binding(i){

    rightButton[i].classList.add("useless");

    leftButton[i].classList.add("useless");


    forms[i].onclick = function(myevent) {
        if(myevent.srcElement.parentElement.name === "Available"){
            rightButton[i].classList.remove("useless");
        }
        if(myevent.srcElement.parentElement.name === "Selected"){
            leftButton[i].classList.remove("useless");
        }
        if(myevent.srcElement.name === "AllRight"){
            while(availableOption[i].options.length !== 0){
                selectedOption[i].options[selectedOption[i].options.length] = availableOption[i].options[0];
            }

            rightButton[i].classList.add("useless");
        }
        if(myevent.srcElement.name === "Right"){
            if(availableOption[i].selectedIndex === -1){
                alert("You select nothing");
                return;
            }
            selectedOption[i].options[selectedOption[i].options.length] = availableOption[i].options[availableOption[i].selectedIndex];

            rightButton[i].classList.add("useless");

            leftButton[i].classList.remove("useless");
        }
        if(myevent.srcElement.name === "Left"){
            if(selectedOption[i].selectedIndex === -1){
                alert("You select nothing");
                return;
            }
            availableOption[i].options[availableOption[i].options.length] = selectedOption[i].options[selectedOption[i].selectedIndex];

            leftButton[i].classList.add("useless");

            rightButton[i].classList.remove("useless");
        }
        if(myevent.srcElement.name === "AllLeft"){
            while(selectedOption[i].options.length !== 0){
                availableOption[i].options[availableOption[i].options.length] = selectedOption[i].options[0];
            }

            leftButton[i].classList.add("useless");
        }

    }
};

for(i=0; i<forms.length; i++){
    binding(i);
}
