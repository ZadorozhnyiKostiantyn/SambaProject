using Microsoft.AspNetCore.Identity;
using SambaProject.Data.Models;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Data
{
    public static class DbInitializer
    {
        public static WebApplication DbInitialize(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
                try
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    DbInitializer.Initializer(scopedContext);
                }
                catch
                {
                    throw;
                }

            return app;
        }

        public static void Initializer(
            ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.AccessRoles.Any())
            {
                context.AccessRoles.AddRange(new List<AccessRole>()
                    {
                        new AccessRole()
                        {
                            Role = "Owner"
                        },
                        new AccessRole()
                        {
                            Role = "Admin"
                        },
                        new AccessRole()
                        {
                            Role = "Editor"
                        },
                        new AccessRole()
                        {
                            Role = "Viewer"
                        }
                    });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                context.Users.AddRange(new List<User>
                {
                    new User()
                    {
                        Username = "Kostya",
                        Password = passwordHasher.HashPassword(
                            new User()
                            {
                                Username = "Kostya",
                                AccessRoleId = 0
                            },
                            "root"),
                        AccessRoleId = 1

                    }
                });
                context.SaveChanges();
            }

            if (!context.AccessRules.Any())
            {
                context.AccessRules.AddRange(new List<AccessRuleRoles>()
                    {
                        // Owner
                        // Folder Rule
                        new AccessRuleRoles()
                        {
                            Path = "*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = false,
                            AccessRoleId = 1
                        },
                        // File Rule
                        new AccessRuleRoles()
                        {
                            Path = "/",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = true,
                            AccessRoleId = 1
                        },

                        // Admin
                        // Folder Rule
                        new AccessRuleRoles()
                        {
                            Path = "*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = false,
                            AccessRoleId = 2
                        },

                        // File Rule
                        new AccessRuleRoles()
                        {
                            Path = "/",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = true,
                            AccessRoleId = 1
                        },

                        // Editor
                        // Folder rule
                        new AccessRuleRoles()
                        {
                            Path = "*",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Deny,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 3
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 3
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/User",
                            Copy = Permission.Deny,
                            Download = Permission.Allow,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = false,
                            AccessRoleId = 3
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/User/*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = false,
                            AccessRoleId = 3
                        },

                        // File Rule
                        new AccessRuleRoles()
                        {
                            Path = "*.*",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Deny,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = true,
                            AccessRoleId = 3
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/User/*.*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Allow,
                            Read = Permission.Allow,
                            WriteContents = Permission.Allow,
                            Upload = Permission.Allow,
                            IsFile = true,
                            AccessRoleId = 3
                        },

                        // Viewer
                        // Folder rule
                        new AccessRuleRoles()
                        {
                            Path = "*",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Deny,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 4
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 4
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/User",
                            Copy = Permission.Deny,
                            Download = Permission.Deny,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 4
                        },
                        new AccessRuleRoles()
                        {
                            Path = "/User/*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = false,
                            AccessRoleId = 4
                        },

                        // File Rule
                        new AccessRuleRoles()
                        {
                            Path = "/User/*.*",
                            Copy = Permission.Allow,
                            Download = Permission.Allow,
                            Write = Permission.Deny,
                            Read = Permission.Allow,
                            WriteContents = Permission.Deny,
                            Upload = Permission.Deny,
                            IsFile = true,
                            AccessRoleId = 4
                        },
                    });
                context.SaveChanges();
            }
        }
    }
    
}
