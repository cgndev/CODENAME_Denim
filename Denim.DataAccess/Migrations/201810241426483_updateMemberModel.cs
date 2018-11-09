namespace Denim.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMemberModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "IsAllowMotion", c => c.Boolean());
            AddColumn("dbo.Member", "IsAllowTypo", c => c.Boolean());
            AddColumn("dbo.Member", "IsAllowLogo", c => c.Boolean());
            AddColumn("dbo.Member", "IsAllowBrand", c => c.Boolean());
            AddColumn("dbo.Member", "IsAllowVFX", c => c.Boolean());
            DropColumn("dbo.Member", "AllowMotion");
            DropColumn("dbo.Member", "AllowTypo");
            DropColumn("dbo.Member", "AllowLogo");
            DropColumn("dbo.Member", "AllowBrand");
            DropColumn("dbo.Member", "AllowVFX");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Member", "AllowVFX", c => c.Boolean());
            AddColumn("dbo.Member", "AllowBrand", c => c.Boolean());
            AddColumn("dbo.Member", "AllowLogo", c => c.Boolean());
            AddColumn("dbo.Member", "AllowTypo", c => c.Boolean());
            AddColumn("dbo.Member", "AllowMotion", c => c.Boolean());
            DropColumn("dbo.Member", "IsAllowVFX");
            DropColumn("dbo.Member", "IsAllowBrand");
            DropColumn("dbo.Member", "IsAllowLogo");
            DropColumn("dbo.Member", "IsAllowTypo");
            DropColumn("dbo.Member", "IsAllowMotion");
        }
    }
}
