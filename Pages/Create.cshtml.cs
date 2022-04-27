
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DrinkLogger.Models;
using Microsoft.Data.Sqlite;

namespace DrinkLogger.Pages
{
    public class Create : PageModel
    {
        private readonly IConfiguration _configuration;

        public Create(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DrinkLog DrinkLog { get; set; } 

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
                command.CommandText = $@"INSERT INTO DrinkLog(Date, Quantity)
                                        VALUES(
                                                '{DrinkLog.Date.ToString("d")}',
                                                '{DrinkLog.Quantity}')";
                command.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }
    }
}