/// <reference path="../jquery-3.3.1.js" />

var $target,
    $listItem,
    id;


function removeItem() {

    $listItem.remove();
}

function totalFail() {
    alert('Cannot delete');

}


function failRemoveItem() {
    var modalWindow = $('#modalWindow'),
        coverDiv = $('#cover-div');

    coverDiv.style.zIndex = 90;
    modalWindow.style.opacity = 1;
}



function modalWindowOnOkHandler() {
    var modalWindow = $('#modalWindow'),
        coverDiv = $('#cover-div');
    modalWindow.style.opacity = 0;
    coverDiv.style.zIndex = -90;

    $.ajax({
        url: '/Users/deleteallaward',
        type: 'post',
        data: { id: id }
    })
        .done(removeItem)
        .fail(totalFail)
}

(function () {
    var $awardList = $('#awardList'),
        $deleteAwardButton = $('#deleteAward'),
        $stayAwardButton = $('#stayAward');
    var modalWindow = $('#modalWindow'),
        coverDiv = $('#cover-div');


    $awardList.on('click', '.delete-award', function (e) {
        $target = $(e.target),
            $listItem = $target.closest('li'),
            id = $listItem.data('id');

        $.ajax({
            url: '/Users/delete-award',
            type: 'post',
            data: { id: id }
        })
            .done(removeItem)
            .fail(failRemoveItem)
    });

    $deleteAwardButton.on('click', modalWindowOnOkHandler);
    $stayAwardButton.on('click', function (e) {
        modalWindow.style.opacity = 0;
        coverDiv.style.zIndex = -90;

    })
})();