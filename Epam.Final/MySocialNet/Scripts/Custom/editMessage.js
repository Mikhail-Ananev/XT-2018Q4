// <reference path="../jquery-3.3.1.js" />
var modal = document.getElementById('myModal');
var errorModal = document.getElementById('errorModal');
var btn = document.getElementById('myBtn');
var span = document.getElementsByClassName('close')[0];

span.onclick = function () {
    modal.style.display = "none";
}

function onDoneHandler() {
    var $editText = $('#edit'),
        text = $('#edit')[0].value,
        $message = $('[data-id="' + id + '"]'),
        result = $message.find('.main-message');

    id = $editText[0].customId;
    result[0].innerText = text;
    modal.style.display = "none";
}

function onFailHandler() {
    modal.style.display = "none";

    errorModal.style.display = "block";
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

(function () {
    var $messageList = $('#message-list');

    $messageList.on('click', '.editMessage', function (e) {
        var $target = $(e.target),
            $message = $target.closest('li'),
            id = $message.data('id'),
            text = $message.find('.main-message').text().trim(),
            $editText = $('#edit'),
            $sendEditMessage = $('#sendEditMessage');

        $editText.text(text);
        $editText[0].customId = id;
        modal.style.display = "block";
        $sendEditMessage.on('click', sendMessageHandler)
    });
})();