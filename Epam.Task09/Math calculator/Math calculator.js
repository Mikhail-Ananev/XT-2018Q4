function calculate(str) {
    var result,
        mathregex = /\s*(\d+\.?\d*)\s*/,
        mathsimbols = str.split(mathregex),
        i,
        j;
 /*   for(let i = 0; i < mathsimbols.length; i++) {
        if(mathsimbols[i] == " "){
            delete mathsimbols[i];
        }
    }*/
alert(mathsimbols);
    alert(mathsimbols.length);
    if (mathsimbols[0] === "-"){
        result = -mathsimbols[1];
    };

    if(mathsimbols[0] === "+"){
        result = +mathsimbols[1];
    };

    if(mathsimbols[0] === ""){
        result = +mathsimbols[1];
    };

    if(mathsimbols[mathsimbols.length - 1] !== "=")
    {
        return NaN;
    };

    if(typeof(+mathsimbols[mathsimbols.length - 2]) !== "number")
    {
        return NaN;
    };

    for(let i = 2; i < mathsimbols.length - 2; i+=2) {
        switch(mathsimbols[i]) {
            case '+':{
                result += +mathsimbols[i+1];
                break;
            }
            case '-':{
                result -= +mathsimbols[i+1];
                break;
            }
            case '*':{
                result *= +mathsimbols[i+1];
                break;
            }
            case '/':{
                result /= +mathsimbols[i+1];
                break;
            }
            case '=':{
                break;
            }
            default:{
                result = NaN;
                break;
            }
        }
    }

    return result.toFixed(2);
}

var input = document.getElementById("Input"),
    processBtn = document.getElementById("Process"),
    output = document.getElementById("Output");

processBtn.onclick = function() {
    output.value = calculate(input.value);
}