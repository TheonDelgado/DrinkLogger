using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DrinkLogger.Models;
using Microsoft.Data.Sqlite;

namespace DrinkLogger.Pages
{
    public class Edit : PageModel
    {
       private readonly IConfiguration _configuration;

       [BindProperty]
       public DrinkLog DrinkLog { get; set; }

       public Edit(IConfiguration configuration)
       {
           _configuration = configuration;
       }

        public IActionResult OnGet(int id)
        {
            DrinkLog = GetById(id);
            return Page();
        }

        public IActionResult OnPost() 
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $@"UPDATE DrinkLog
                                        SET Date = '{DrinkLog.Date}',
                                            Quantity = '{DrinkLog.Quantity}'
                                        WHERE Id = '{DrinkLog.Id}';";
                command.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }

        private DrinkLog GetById(int id) 
        {
            var drinksRecord = new DrinkLog();

            using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $@"SELECT * FROM DrinkLog 
                                        WHERE Id = '{id}'";
                
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    drinksRecord.Id = reader.GetInt32(0);
                    drinksRecord.Date = DateTime.Parse(reader.GetString(1));
                    drinksRecord.Quantity = reader.GetInt32(2);
                }
                return drinksRecord;
            }
        }
    }
}