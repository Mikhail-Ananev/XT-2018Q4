function onclickHandler() {

    var form = document.registration;

    if (!isLoginValid(form.login.value)) {
        return false;
    }

    if (form.password.value != form.password_again.value) {
        return false;
    }

    if (!isNameValid(form.firstName.value)) {
        return false;
    }

    if (!isNameValid(form.lastName.value)) {
        return false;
    }

    if (!isDateValid(form.date.value)) {
        return false;
    }

    return true;
}

function isLoginValid(eMail) {
    var pattern = /[A-Za-z0-9]([A-Za-z0-9.\-_]*[A-Za-z0-9])*@([A-Za-z0-9]([A-Za-z0-9\-]*[A-Za-z0-9])*\.)+[A-Za-z]{2,6}/;
    return pattern.test(eMail);
}

function isNameValid(name) {

    if (isEmpty(name)) {
        return false;
    }

    if (!isFirstUpper(name[0])) {
        return false;
    }

    return true;
}

function isEmpty(str) {
    if (str.trim() == '' || str == null)
        return true;

    return false;
}

function isFirstUpper(symbol) {

    if (symbol.toUpperCase() == symbol) {
        return true;
    }
    return false;
}

function isDateValid(date) {

    if (!isInputDateValid(date)) {
        return false;
    }

    var age = getAge(date);

    if (age < 4 || age > 125) {
        return false;
    }

    return true;
}

function isInputDateValid(date) {
    //var pattern = /((31-(0[13578]|1[02])|30-(0[13-9]|1[012])|(0[1-9]|1\d|2[0-8])-(0[1-9]|1[012]))-\d{4})|(29-02-\d\d([02468][048])|([13579][26]))/;
    var pattern = /(\d{4}-((0[13578]|1[02])-31|(0[13-9]|1[012])-30|(0[1-9]|1[012])-(0[1-9]|1\d|2[0-8])))|(\d\d([02468][048])|([13579][26])-02-29)/;
    return pattern.test(date);
}

function getAge(date) {
    var nowDate = new Date;
    var arrDate = date.split("-");
    arrDate[1]--;
    var inputDate = new Date(arrDate[0], arrDate[1], arrDate[2]);

    var year = nowDate.getFullYear() - inputDate.getFullYear();
    var month = nowDate.getMonth() - inputDate.getMonth();
    var day = nowDate.getDate() - inputDate.getDate();

    if (month < 0) {
        year--;
    } else if (month == 0 && day < 0) {
        year--;
    }

    return year;
}

window.onload = function () {
    var submitButton = document.getElementById('reg');
    submitButton.onclick = onclickHandler;
}