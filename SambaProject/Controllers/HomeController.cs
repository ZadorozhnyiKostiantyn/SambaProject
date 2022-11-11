using Microsoft.AspNetCore.Mvc;
using SambaProject.Models;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using System.Diagnostics;
using Newtonsoft.Json;
using Syncfusion.EJ2.FileManager.Base;
using SambaProject.Service.Connection;
using Microsoft.AspNetCore.Http.Features;
using SambaProject.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using SambaProject.Service.Authentication;
using System.Net;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SambaProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PhysicalFileProvider operation;
        private readonly NetworkSettings _networkSettings;
        private readonly IJwtValidatorService _jwtValidator;

        public HomeController(
            IAccessRoleRepository accessRoleRepository,
            IAccessRuleRepository accessRuleRepository,
            IConfiguration configuration,
            IJwtValidatorService jwtValidator,
            NetworkSettings networkSettings)
        {

            _jwtValidator = jwtValidator;
            _networkSettings = networkSettings;
            this.operation = new PhysicalFileProvider();
            this.operation.RootFolder(_networkSettings.NetworkPath);
        }

        public object FileOperations([FromBody] FileManagerDirectoryContent args)
        {
            using(new ConnectToSharedFolder(
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

                        return this.operation.ToCamelCase(response);
                    }
                }
                switch (args.Action)
                {
                    case "read":
                        // reads the file(s) or folder(s) from the given path.
                        return this.operation.ToCamelCase(this.operation.GetFiles(args.Path, args.ShowHiddenItems));
                    case "delete":
                        // deletes the selected file(s) or folder(s) from the given path.
                        return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
                    case "copy":
                        // copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return this.operation.ToCamelCase(
                            this.operation.Copy(
                                args.Path,
                                args.TargetPath,
                                args.Names,
                                args.RenameFiles,
                                args.TargetData
                        ));
                    case "move":
                        // cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
                        return this.operation.ToCamelCase(
                            this.operation.Move(
                                args.Path,
                                args.TargetPath,
                                args.Names,
                                args.RenameFiles,
                                args.TargetData
                        ));
                    case "details":
                        // gets the details of the selected file(s) or folder(s).
                        return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names, args.Data));
                    case "create":
                        // creates a new folder in a given path.
                        return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
                    case "search":
                        // gets the list of file(s) or folder(s) from a given path based on the searched key string.
                        return this.operation.ToCamelCase(
                            this.operation.Search(
                                args.Path,
                                args.SearchString,
                                args.ShowHiddenItems,
                                args.CaseSensitive
                        ));
                    case "rename":
                        // renames a file or folder.
                        return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));
                }
                return null;
            }
        }

        // uploads the file(s) into a specified path
        public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
        {
            using (new ConnectToSharedFolder(
                _networkSettings.NetworkPath,
                new NetworkCredential(_networkSettings.Username, _networkSettings.Password)))
            {
                FileManagerResponse uploadResponse;
                uploadResponse = operation.Upload(path, uploadFiles, action, null);
                if (uploadResponse.Error != null)
                {
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
                    Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
                }
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
                return this.operation.GetImage(args.Path, args.Id, false, null, null);
            }
        }


        [HttpGet]
        [Route("ShareFolder")]
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            if (!_jwtValidator.ValidateToken(token))
            {
                return RedirectToAction("Login", "Authentication");
            }

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