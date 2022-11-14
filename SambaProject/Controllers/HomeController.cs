using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SambaProject.Helpers.Attribute;
using SambaProject.Models;
using SambaProject.Service.Administration;
using SambaProject.Service.Connection;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using System.Diagnostics;
using System.Net;

namespace SambaProject.Controllers
{
    [AuthorizeUser]
    public class HomeController : Controller
    {
        private readonly PhysicalFileProvider operation;
        private readonly NetworkSettings _networkSettings;
        private readonly IAccessRoleService _accessRoleService;

        public HomeController(
            NetworkSettings networkSettings,
            IAccessRoleService accessRoleService)
        {
            _networkSettings = networkSettings;
            _accessRoleService = accessRoleService;
            operation = new PhysicalFileProvider();
            operation.RootFolder(_networkSettings.NetworkPath);
            operation.SetRules(_accessRoleService.GetAccessDetails());
        }

        public object FileOperations([FromBody] FileManagerDirectoryContent args)
        {
            using (new ConnectToSharedFolder(
                _networkSettings.NetworkPath,
                new NetworkCredential(_networkSettings.Username, _networkSettings.Password)))
            {
                var fullPath = (_networkSettings.NetworkPath + args.Path).Replace("/", "\\");
                if (args.Action == "delete" || args.Action == "rename")
                {
                    if ((args.TargetPath == null) && (args.Path == ""))
                    {
                        FileManagerResponse response = new FileManagerResponse();
                        response.Error = new ErrorDetails
                        {
                            Code = "401",
                            Message = "Restricted to modify the root folder."
                        };

                        return operation.ToCamelCase(response);
                    }
                }
                switch (args.Action)
                {
                    case "read":
                        // reads the file(s) or folder(s) from the given path.
                        return operation.ToCamelCase(operation.GetFiles(args.Path, args.ShowHiddenItems));
                    case "delete":
                        // deletes the selected file(s) or folder(s) from the given path.
                        return operation.ToCamelCase(operation.Delete(args.Path, args.Names));
                    case "copy":
                        // copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return operation.ToCamelCase(
                            operation.Copy(
                                args.Path,
                                args.TargetPath,
                                args.Names,
                                args.RenameFiles,
                                args.TargetData
                        ));
                    case "move":
                        // cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return operation.ToCamelCase(
                            operation.Move(
                                args.Path,
                                args.TargetPath,
                                args.Names,
                                args.RenameFiles,
                                args.TargetData
                        ));
                    case "details":
                        // gets the details of the selected file(s) or folder(s).
                        return operation.ToCamelCase(operation.Details(args.Path, args.Names, args.Data));
                    case "create":
                        // creates a new folder in a given path.
                        return operation.ToCamelCase(operation.Create(args.Path, args.Name));
                    case "search":
                        // gets the list of file(s) or folder(s) from a given path based on the searched key string.
                        return operation.ToCamelCase(
                            operation.Search(
                                args.Path,
                                args.SearchString,
                                args.ShowHiddenItems,
                                args.CaseSensitive
                        ));
                    case "rename":
                        // renames a file or folder.
                        return operation.ToCamelCase(operation.Rename(args.Path, args.Name, args.NewName));
                }
                return null;
            }
        }

        // upload the selected file(s) or folder
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = 10_737_418_240, ValueLengthLimit = Int32.MaxValue)]
        public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            using (new ConnectToSharedFolder(
                _networkSettings.NetworkPath,
                new NetworkCredential(_networkSettings.Username, _networkSettings.Password)))
            {
                string newPath = path;
                FileManagerResponse uploadResponse;

                foreach (var file in uploadFiles)
                {
                    var folders = (file.FileName).Split('/');

                    // checking the folder upload
                    if (folders.Length > 1)
                    {
                        for (var i = 0; i < folders.Length - 1; i++)
                        {
                            string newDirectoryPath = Path.Combine(newPath, folders[i]);
                            if (!Directory.Exists(newDirectoryPath))
                            {
                                operation.ToCamelCase(operation.Create(newPath, folders[i]));
                            }
                            newPath += folders[i] + "/";
                        }
                    }

                }


                uploadResponse = operation.Upload(path, uploadFiles, action, null);
                return Content("");
            }
        }

        // downloads the selected file(s) and folder(s)
        public IActionResult Download(string downloadInput)
        {
            using (new ConnectToSharedFolder(
                _networkSettings.NetworkPath,
                new NetworkCredential(_networkSettings.Username, _networkSettings.Password)))
            {
                FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
                return operation.Download(args.Path, args.Names, args.Data);
            }
        }

        // gets the image(s) from the given path
        public IActionResult GetImage(FileManagerDirectoryContent args)
        {
            using (new ConnectToSharedFolder(
                _networkSettings.NetworkPath,
                new NetworkCredential(_networkSettings.Username, _networkSettings.Password)))
            {
                return operation.GetImage(args.Path, args.Id, false, null, null);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}