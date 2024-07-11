using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Workers
{
    public class EditModel : PageModel
    {
        public WorkersInfo workersInfo = new WorkersInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM workers WHERE id=@id"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                workersInfo.id = reader.GetInt32(0).ToString();
                                workersInfo.workers_id = reader.GetString(1);
                                workersInfo.name = reader.GetString(2);
                                workersInfo.email = reader.GetString(3);
                                workersInfo.phone = reader.GetString(4);
                                workersInfo.nationality = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            workersInfo.id = Request.Form["id"];
            workersInfo.name = Request.Form["name"];
            workersInfo.email = Request.Form["email"];
            workersInfo.phone = Request.Form["phone"];
            workersInfo.nationality = Request.Form["nationality"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE workers " +
                                 "SET name=@name, email=@email, phone=@phone, nationality=@nationality " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", workersInfo.name);
                        command.Parameters.AddWithValue("@email", workersInfo.email);
                        command.Parameters.AddWithValue("@phone", workersInfo.phone);
                        command.Parameters.AddWithValue("@nationality", workersInfo.nationality);
                        command.Parameters.AddWithValue("@id", workersInfo.id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            successMessage = "Worker information updated successfully.";
                        }
                        else
                        {
                            errorMessage = "No worker found with the provided ID.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error updating worker information: " + ex.Message;
                return;
            }

            Response.Redirect("/Workers/Index");
        }
    }
}
