namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGeneresTable : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO Genres (Id,Name) Values (1 , 'Jazz')");
            Sql("INSERT INTO Genres (Id,Name) Values (2 , 'Blues')");
            Sql("INSERT INTO Genres (Id,Name) Values (3 , 'Rock')");
            Sql("INSERT INTO Genres (Id,Name) Values (4 , 'Country')");
            Sql("INSERT INTO Genres (Id,Name) Values (5 , 'Pop')");
            Sql("INSERT INTO Genres (Id,Name) Values (6 , 'Disco Polo')");


        }

        public override void Down()
        {

            Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4,5,6)");

        }
    }
}
