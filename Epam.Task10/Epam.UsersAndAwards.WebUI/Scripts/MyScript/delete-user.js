/// <reference path="../jquery-3.3.1.js" />
(function () {
    var $userList = $('#userList');

    $userList.on('click', '.delete-user', function (e) {
        var $target = $(e.target),
            $listItem = $target.closest('li'),
            id = $listItem.data('id');

        $.ajax({
            url: '/Users/delete',
            type: 'post',
            data: { id: id }
        })
            .done(function () {
                $listItem.remove();
            })
            .fail(function () {
                alert("Cannot delete");
            })
    });
})();