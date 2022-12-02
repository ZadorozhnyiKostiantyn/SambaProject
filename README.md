# SambaProject

SambaProject is a web interface for samba service. It offers sharing files with people and getting access to them anytime from anywhere.
## Installation and Configuration on Samba

[Install and configure Samba](https://ubuntu.com/tutorials/install-and-configure-samba#1-overview)

In appsettings.Development.json change these settings
```json
"NetworkSettings": {
    "NetworkPath": "\\\\YOUR IP\\YOUR FOLDER NAME",
    "Username": "USERNAME",
    "Password": "YOUR PASSWORD"
  }
```

## Database Configuration

Change the database connection string settings in appsettings.Development.json

```json
"ConnectionStrings": {
    "MySqlDatabase": "Server=YOUR SERVER IP;Database=DATABASE NAME;User=USERNAME;Password=YOUR PASSWORD;"
  },
```
To create initial users, change the following code in DbInitializer.cs to your credentials

```python
if (!context.Users.Any())
{
    IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
    context.Users.AddRange(new List<User>
    {
        new User()
        {
            Username = "YOURNAME",
            Password = passwordHasher.HashPassword(
            new User()
            {
                Username = "YOURNAME",
                AccessRoleId = 0
            },
            "YOURPASSWORD"),
            AccessRoleId = 1

        }
    });
    context.SaveChanges();
}
```