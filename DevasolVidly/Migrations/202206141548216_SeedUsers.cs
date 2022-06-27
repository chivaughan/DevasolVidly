namespace DevasolVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'118d2a4c-9f3e-4ab8-bda6-f83b010e132f', N'admin@devasol.com', 0, N'ANcCR/DaodnX9cFrME5BL4aNR+TGyADfYH9yGiAZ6cra4ahJwHNYJ6wNkzBVO9RAAQ==', N'd88a5bd4-2eff-4773-9023-f0fd15dd59f8', NULL, 0, 0, NULL, 1, 0, N'admin@devasol.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'15197ddc-ba8c-4efd-b098-13b85364e69d', N'chivaughan007@gmail.com', 0, N'APETttJiaXOrV/z0ASBSn8Ux5KYULAnMxBupvpBoUi0vHCjBV1S+AjAvT91efkDk5g==', N'29e77122-8ef9-4c59-99cc-f54009a2eb62', NULL, 0, 0, NULL, 1, 0, N'chivaughan007@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a6ea0309-b8fc-4126-ac18-09c2b3672a99', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'118d2a4c-9f3e-4ab8-bda6-f83b010e132f', N'a6ea0309-b8fc-4126-ac18-09c2b3672a99')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
