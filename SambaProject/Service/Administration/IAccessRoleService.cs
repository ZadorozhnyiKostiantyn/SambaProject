﻿using SambaProject.Data.Models;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.Administration
{
    public interface IAccessRoleService
    {
        public AccessRole? GetRoleById(int id);
        public List<AccessRole> GetAllRoles();
        public AccessDetails GetAccessDetails();
    }
}