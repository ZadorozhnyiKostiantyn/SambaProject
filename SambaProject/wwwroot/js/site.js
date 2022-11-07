function changeTdToInput(id, value) {
    $(id).on('click', function () {
        var $this = $(this);
        var $input = $('<input>', {
            value: value,
            type: 'text',
            blur: function () {
                $this.text(this.value);
            },
            keyup: function (e) {
                if (e.which === 10) $input.blur();
            }
        }).appendTo($this.empty()).focus();
    });
}

function changeTdToSelect(id) {
    $(id).on('click', function (e) {
        e.stopPropagation();
        var $td = $(this);

        if ($td.hasClass('active'))
            return;

        var value = $td.text().trim();
        $td.empty();
        var $select = $('<select>' +
            '<option name="Owner">Owner</option>' +
            '<option value="Admin">Admin</option>' +
            '<option value="Write">Write</option>' +
            '<option value="Read">Read</option>' +
            '</select>').val(value).appendTo($td).focus();
        $td.addClass('active');
    }).on('blur', 'select', function () {
        var $select = $(this);
        var $td = $select.closest('td');
        $td.html($select.val()).removeClass('active');
    });
}

// ------------------------------------------------------AJAX-------------------------------------------------------------------

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'DELETE',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    location.reload(true);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}

$(function () {
    $("#search-username").keyup(function () {
        GetUsers();
    });
});
function GetUsers() {
    var username = $.trim($("#search-username").val());
    var table = $("#tblUsers");
    table.html("");
    console.log(username)
    $.ajax({
        type: "POST",
        url: "/Administration/SearchUsers?username=" + username,
        data: "{username:'" + username + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (users) {
            console.log(users)
            
            $.each(users, function (i, user) {
                let accessRole = ''
                switch (user.userId) {
                    case 1:
                        accessRole = `<td id="drop-down-user-` + user.userId + `" onclick='changeTdToSelect("#drop-down-user-` + user.userId + `")'>Owner</td>`
                        break;
                    case 2:
                        accessRole = `<td id="drop-down-user-` + user.userId + `" onclick='changeTdToSelect("#drop-down-user-` + user.userId + `")'>Admin</td>`
                        break;
                    case 3:
                        accessRole = `<td id="drop-down-user-` + user.userId + `" onclick='changeTdToSelect("#drop-down-user-` + user.userId + `")'>Write</td>`
                        break;
                    default:
                        accessRole = `<td id="drop-down-user-` + user.userId + `" onclick='changeTdToSelect("#drop-down-user-` + user.userId + `")'>Read</td>`
                }

                var data = "<tr>" +
                    "<td>" +
                    '<span class="custom-checkbox">' +
                    '<input type="checkbox" id="checkbox1" name="options[]" value="1">' +
                    '<label for="checkbox1"></label>' +
                    '</span>' +
                    '</td>' +
                    `<td id="user-` + user.userId + `" onclick='changeTdToInput("#user-` + user.userId + `", "` + user.username + `")' >` + user.username + `</td >` +
                    accessRole + 
                    "<td>" + 
                    `<a href="#editEmployeeModal" class="edit" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Edit"><iconify-icon icon="eva:edit-outline" width="27" height="27"></iconify-icon></i></a>` +
                    `<form asp-action="DeleteUser" asp-route-id=` + user.userId + `onsubmit="return jQueryAjaxDelete(this)">` + 
                            `<button type="submit" class="delete" data-toggle="modal">
                                <i class="material-icons" data-toggle="tooltip" title="Delete">
                                    <iconify-icon icon="mingcute:delete-2-line" width="25" height="25"></iconify-icon>
                                </i>
                            </button>
                        </form>
                    </td>` + 
                    "</tr>";

                table.append(data);

            });
        }
    });
}