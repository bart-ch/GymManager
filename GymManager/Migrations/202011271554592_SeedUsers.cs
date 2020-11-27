namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Surname], [JobTitle]) VALUES (N'3b51c28f-83b7-4365-9df9-943fedc50fa6', N'guest@test.com', 0, N'AJuq4yysMJwIFIexRLs0vaIMJ+SQAA8yPGFnW/Zq4MHdvHX3yezL/98gvj6ERGhsbA==', N'e3553465-a677-4223-bd66-96d57139eda5', NULL, 0, 0, NULL, 1, 0, N'guest@test.com', N'Guest', N'User', N'Customer Service')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Surname], [JobTitle]) VALUES (N'6993a36f-58eb-4fd6-8443-9d908723495d', N'admin@test.com', 0, N'AD/ISFGA4NZjWh1FVBeQPY2IbBcVcXVyAJPFaeOe/I9FN7VAwMUhnhwnBYdUra0w5Q==', N'ba8b2707-f4ff-4e97-b35f-7aa6c372cb5b', NULL, 0, 0, NULL, 1, 0, N'admin@test.com', N'Admin', N'User', N'Gym Manager')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'921961a9-77c7-4749-87f9-a9d7f8bf5e91', N'CanManageEmployees')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6993a36f-58eb-4fd6-8443-9d908723495d', N'921961a9-77c7-4749-87f9-a9d7f8bf5e91')

");
        }
        
        public override void Down()
        {
        }
    }
}
