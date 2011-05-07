using System;
using FluentMigrator;

namespace SocialFreeks.Database.Migrations
{
    [MigrationTime(0, 0, 0, 0, 0, 0)]
    public class Init : Migration
    {
        public override void Up()
        {
            //Execute.ResourceScript(GetType().Assembly, "SocialFreeks.Database.init.sql");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }

    [MigrationTime(2011, 5, 7, 13, 16, 0)]
    public class Add_User_Table : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsAnsiString(255).NotNullable()
                .WithColumn("LastName").AsAnsiString(255).NotNullable()
                .WithColumn("UserName").AsAnsiString(255).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
