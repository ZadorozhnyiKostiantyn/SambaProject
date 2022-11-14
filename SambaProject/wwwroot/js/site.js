function changeTdToInputAndSelect(inputId, valueInput, selectId, tdButtonsId) {
    let id = inputId;
    console.log(inputId);
    id = id.replace("#user-", "");

    var $input = $('<input>', {
        value: valueInput,
        type: 'text',
        id: `input-edit-user-${id}`
    });

    var $select = $(`<select id="select-role-${id}">` +
        '<option value="1" name="Owner">Owner</option>' +
        '<option value="2" name="Admin">Admin</option>' +
        '<option value="3" name="Editor">Editor</option>' +
        '<option value="4" name="Viewer">Viewer</option>' +
        '</select>'
    );


    var $newButton = $('<button>', {
        id: "update-btn-" + id,
        name: "Update",
        onclick: `editUser("#input-edit-user-${id}", "#select-role-${id}")`
    });

    var $i = document.createElement("i");
    $i.innerHTML = '<iconify-icon icon="ic:baseline-check-circle-outline" style="color: #00a300;" width="27" height="27"></iconify-icon>'
    $newButton.append($i);

    var $tdInput = $(inputId);
    var $tdSelect = $(selectId);
    var $tdButtons = $(tdButtonsId);

    $input.appendTo($tdInput.empty());
    $select.appendTo($tdSelect.empty());
    $newButton.appendTo($tdButtons.empty());
}

// ------------------------------------------------------AJAX-------------------------------------------------------------------

function editUser(inputId, selectId) {
    var id = inputId;
    id = id.replace("#input-edit-user-", "");
    var formData = new FormData();
    console.log(id);
    console.log(document.querySelector(inputId).value);
    formData.append("id", id);
    formData.append("username", document.querySelector(inputId).value);
    formData.append("accessRoleId", $(selectId + " option:selected").val());

    $.ajax({
        type: 'PUT',
        url: 'Administration/EditUser',
        contentType: false,
        processData: false,
        cache: false,
        data: formData,
        success: function (res) {
            location.reload(true);
        }
    });
}

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
            });
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
    $.ajax({
        type: "POST",
        url: "/Administration/SearchUsers?username=" + username,
        data: "{username:'" + username + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (users) {
            console.log(users)
            
            $.each(users, function (i, user) {
                let accessRole = '';
                switch (user.accessRoleId) {
                    case 1:
                        accessRole = `<td id="drop-down-user-` + user.userId + `">Owner</td>`;
                        break;
                    case 2:
                        accessRole = `<td id="drop-down-user-` + user.userId + `">Admin</td>`;
                        break;
                    case 3:
                        accessRole = `<td id="drop-down-user-` + user.userId + `">Write</td>`;
                        break;
                    default:
                        accessRole = `<td id="drop-down-user-` + user.userId + `">Read</td>`;
                }

                var data = "<tr>" +
                    "<td>" +
                    '<span class="custom-checkbox">' +
                    '<input type="checkbox" id="checkbox1" name="options[]" value="1">' +
                    '<label for="checkbox1"></label>' +
                    '</span>' +
                    '</td>' +
                    `<td id="user-` + user.userId + `">` + user.username + `</td >` +
                    accessRole +
                    `<td id="buttons-` + user.userId + `" >` +
                    `<a id="edit-user-` + user.userId + `" onclick="changeTdToInputAndSelect('#user-` + user.userId + `', '` + user.username + `', '#drop-down-user-` + user.userId + `', '#buttons-` + user.userId + `')" class="edit" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Edit"><iconify-icon icon="eva:edit-outline" width="27" height="27"></iconify-icon></i></a>` +
                    `<form asp-action="DeleteUser" asp-route-id=` + user.userId + `onsubmit="return jQueryAjaxDelete(this)">` +
                    `<button type="submit" class="delete" data-toggle="modal">
                                <i class="material-icons" data-toggle="tooltip" title="Delete">
                                    <iconify-icon icon="mingcute:delete-2-line" width="25" height="25"></iconify-icon>
                                </i>
                            </button>
                        </form>
                    </td>` +
                    "</tr>";

                table.append(data);;
            });
        }
    });
}