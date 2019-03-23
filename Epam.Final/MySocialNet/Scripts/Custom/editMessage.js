var modal = document.getElementById('myModal');
var btn = document.getElementById("myBtn");
var span = document.getElementsByClassName("close")[0];

//btn.onclick = function () {
//    modal.style.display = "block";
//}


//span.onclick = function () {
//    modal.style.display = "none";
//}

function onDoneHandler() {
    modal.style.display = "none";

    var $editText = $('#edit'),
     text = $editText[0].value;
    id = $editText[0].customId;

    var $messageList = $('#message-list');

    var $message = $('#' + id);



    var result = $message.children('.message').children('.main-message');
    result[0].innerText = text;

}

function onFailHandler() {
    modal.style.display = "none";

    alert('БЯДА');
}



function sendMessageHandler() {
    var $editText = $('#edit'),
        text = $editText[0].value;

    id = $editText[0].customId;

    $.ajax({
        url: '/Message/EditMessage',
        type: 'post',
        data: {
            id: id,
            message: text,
        }
    })
        .done(onDoneHandler)
        .fail(onFailHandler)
}



    /// <reference path="../jquery-3.3.1.js" />
    (function () {
        var $messageList = $('#message-list');

        $messageList.on('click', '.editMessage', function (e) {
            var $target = $(e.target),
                $message = $target.closest('li'),
                id = $message.data('id');
            //$("div").children(".bigBlock")
            //var $messageText = $('');
            var result = $message.children('.message').children('.main-message');
            var text = result[0].innerText;

            modal.style.display = "block";

            var $editText = $('#edit');
            $editText[0].value = text;
            $editText[0].customId = id;


            var $sendEditMessage = $('#sendEditMessage');
            $sendEditMessage.on('click', sendMessageHandler)

           
        });
    })();