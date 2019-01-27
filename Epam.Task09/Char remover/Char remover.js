function proceed() {
    function getMultiple(str) {
        var dict = {};
        str.split('').forEach((c) => dict[c] = dict[c] === undefined ? 1 : dict[c] + 1);

        return Object.keys(dict).filter((key) => dict[key] > 1)
    }

    function getMultipleInWords(words) {
        var r = new Set();
        words.forEach((word) => getMultiple(word).forEach((v) => r.add(v)));
        return r;
    }

    function splitWords(str, splitters) {
        var result = [];
        var word = "";

        for (var i = 0; i < str.length; i++) {
            let c = s.charAt(i);
            if (splitters.includes(c)) {
                if (word.length > 0) {
                    result.push(word);
                    word = "";
                }
            }
            else {
                word += c;
            }
        }

        if (word.length > 0) {
            result.push(word);
        }

        return result;
    }

    const splitters = [' ', '\t', '?', '!', ':', ';', ',', '.'];
    var inputTxt =  document.getElementById("inputText");
    var outputTxt = document.getElementById("resultText");

    var s = inputTxt.value;

    var words = splitWords(s, splitters);
    var set = getMultipleInWords(words);

    var r = "";
    for (var i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (!set.has(c) || splitters.includes(c))
            r += c;
    }

    outputTxt.value = r;
}

window.onload = function () {
    document.getElementById("runButton").onclick = proceed;
}