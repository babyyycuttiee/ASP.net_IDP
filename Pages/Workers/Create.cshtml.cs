using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Workers
{
    public class CreateModel : PageModel
    {
        public WorkersInfo workersInfo = new WorkersInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            workersInfo.name= Request.Form["name"];
            workersInfo.email = Request.Form["email"];
            workersInfo.phone = Request.Form["phone"];
            workersInfo.nationality = Request.Form["nationality"];

            if(workersInfo.name.Length == 0 || workersInfo.email.Length == 0 ||
               workersInfo.phone.Length == 0 || workersInfo.nationality.Length == 0) 
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new workers into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO workers " +
                                 "(name, email, phone, nationality) VALUES " +
                                 "(@name, @email, @phone, @nationality);";

                    using(SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@name", workersInfo.name);
                        command.Parameters.AddWithValue("@email", workersInfo.email);
                        command.Parameters.AddWithValue("@phone", workersInfo.phone);
                        command.Parameters.AddWithValue("@nationality", workersInfo.nationality);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
            
            workersInfo.name = ""; workersInfo.email = ""; workersInfo.phone = ""; workersInfo.nationality = "";
            successMessage = "New Workers Added Successfully";

            Response.Redirect("/Workers/Index");
        }
    }
}
