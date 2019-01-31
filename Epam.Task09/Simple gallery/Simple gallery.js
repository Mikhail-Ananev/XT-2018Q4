function pageRoutine() {
    var cntstr = document.getElementById("timeCounter"),
        numregexp = /(\d+)(\.html)/,
        urlpart = document.location.href.split(numregexp),
        currentpagenum = + urlpart[1],
        nextpagenum = currentpagenum + 1,
        previouspagenum = currentpagenum - 1,
        nextpage = urlpart[0] + nextpagenum + urlpart[2],
        firspage = urlpart[0] + 1 + urlpart[2],
        previouspage = urlpart[0] + previouspagenum + urlpart[2],
        numofpage = 4,
        timerrun = true,
        counter = 10;

    if (document.location.href.indexOf('index') + 1) {
        window.open(document.location.href.replace(/index/g,"Simple gallery1"), '_gallery');
        return;
    }

    run.disabled = "disabled";

    if (currentpagenum === 1){
        back.disabled = "disabled";
    }

    if (currentpagenum === 4){
        next.disabled = "disabled";
    }

    cntstr.innerHTML = counter;

    setTimeout(func, 1000);

    function func() {
        counter--;
        cntstr.innerHTML = counter;
        if ((counter > 0) & (timerrun)){
            setTimeout(func, 1000);
        }
        if (counter === 0){
            if (currentpagenum === numofpage) {
                if(confirm("Repeat?")) {
                    document.location.replace(firspage);
                    return;
                }
                else{
                    self.close();
                    return;
                }
            }
            document.location.replace(nextpage);
        }
    }

    back.onclick = function() {
        document.location.replace(previouspage);
    };

    next.onclick = function() {
        document.location.replace(nextpage);
    };

    pause.onclick = function() {
        pause.disabled = "disabled";
        run.disabled = "";
        timerrun = false;
        counter++;
    };

    run.onclick = function() {
        run.disabled = "disabled";
        pause.disabled = "";
        timerrun = true;
        setTimeout(func, 1000);
    };

    return;
}

pageRoutine();