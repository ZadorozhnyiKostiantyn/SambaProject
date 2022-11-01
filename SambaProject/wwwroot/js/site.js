//$(document).ready(function () {
//	// Activate tooltip
//	$('[data-toggle="tooltip"]').tooltip();

//	// Select/Deselect checkboxes
//	var checkbox = $('table tbody input[type="checkbox"]');
//	$("#selectAll").click(function () {
//		if (this.checked) {
//			checkbox.each(function () {
//				this.checked = true;
//			});
//		} else {
//			checkbox.each(function () {
//				this.checked = false;
//			});
//		}
//	});
//	checkbox.click(function () {
//		if (!this.checked) {
//			$("#selectAll").prop("checked", false);
//		}
//	});
//});

//function onCreated(args) {
//    var fileObject = document.getElementById("filemanager").ej2_instances[0];
//    document.getElementById('filemanager_tb_upload').onclick = function (args) {
//        args.stopPropagation();
//    };
//    var items = [{ text: 'Folder' }, { text: 'Files' }];
//    var drpDownBtn = new ej.splitbuttons.DropDownButton(
//        {
//            items: items,
//            select: (args) => {
//                if (args.item.text === 'Folder') {
//                    fileObject.uploadSettings.directoryUpload = true;
//                } else {
//                    fileObject.uploadSettings.directoryUpload = false;
//                }
//                setTimeout(function () {
//                    let uploadBtn = document.querySelector('.e-file-select-wrap button');
//                    uploadBtn.click();
//                }, 100);
//            }
//        },
//        '#filemanager_tb_upload'
//    );
//}

