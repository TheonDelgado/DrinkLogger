using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DrinkLogger.Models;
using Microsoft.Data.Sqlite;
using System;

namespace DrinkLogger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<DrinkLog> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {   
        CreateDataBase();
        Records = GetAllRecords();
    }

    private List<DrinkLog> GetAllRecords() 
    {
        using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM DrinkLog";

            var data = new List<DrinkLog>();
            SqliteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                data.Add(
                    new DrinkLog {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1)),
                        Quantity = reader.GetInt32(2)
                    }
                );
            }
            return data;
        }
    }

    private void CreateDataBase()
    {
        using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $@"CREATE TABLE IF NOT EXISTS 'DrinkLog' (
	                                'Id'	INTEGER,
	                                'Date'	TEXT,
	                                'Quantity'	INTEGER,
	                                PRIMARY KEY('Id' AUTOINCREMENT)
                                    );";

            command.ExecuteNonQuery();
        }
    }
}
