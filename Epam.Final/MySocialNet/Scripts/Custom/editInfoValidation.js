(function () {
    if (window.addEventListener) {
        window.addEventListener("load", init, false);
    } else if (window.attachEvent) {
        window.attachEvent("onload", init);
    }

    function init() {
        for (var i = 0; i < document.forms.length; i++) {
            var f = document.forms[i];
            var needsValidation = false;

            for (var j = 0; j < f.elements.length; j++) {
                var e = f.elements[j];


                if (e.type != "text") {
                    continue;
                }

                var pattern = e.getAttribute("pattern");
                var required = e.getAttribute("required") != null;

                if (required && !pattern) {
                    pattern = "\\S";
                    e.setAttribute("pattern", pattern);
                }

                if (pattern) {
                    e.onchange = validateOnChange;
                    needsValidation = true;
                }


            }

            if (needsValidation) {
                f.onsubmit = validateOnSubmit;
            }
        }
    }

    function validateOnChange() {
        var textField = this;
        var pattern = textField.getAttribute("pattern");
        var value = textField.value;

        if (value.search(pattern) == -1) {
            textField.classList.add("invalid");
            textField.classList.remove("valid");
        } else {
            textField.classList.remove("invalid");
            textField.classList.add("valid");
        }
    }

    function validateOnSubmit() {
        var invalid = false;

        for (var i = 0; i < this.elements.length; i++) {
            var e = this.elements[i];

            if (e.type == "text" && e.onchange == validateOnChange) {
                e.onchange();

                if (e.className == "invalid") {
                    invalid = true;
                }
            }
        }

        if (invalid) {
            alert("Форма заполнена некорректно");
            return false;
        } else {
            return true; //проверить при отсутствии true
        }
    }
})();