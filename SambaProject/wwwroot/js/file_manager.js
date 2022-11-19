function onCreated(args) {
    var fileObject = document.getElementById("filemanager").ej2_instances[0];
    document.getElementById('filemanager_tb_upload').onclick = function (args) {
        args.stopPropagation();
    };
    var items = [{ text: 'Folder' }, { text: 'Files' }];
    var drpDownBtn = new ej.splitbuttons.DropDownButton(
        {
            items: items,
            select: (args) => {
                if (args.item.text === 'Folder') {
                    fileObject.uploadSettings.directoryUpload = true;
                } else {
                    fileObject.uploadSettings.directoryUpload = false;
                }
                setTimeout(function () {
                    let uploadBtn = document.querySelector('.e-file-select-wrap button');
                    uploadBtn.click();
                }, 100);
            }
        },
        '#filemanager_tb_upload'
    );
}