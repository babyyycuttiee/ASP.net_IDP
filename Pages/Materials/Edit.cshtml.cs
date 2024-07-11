using Job_Distribuition.Pages.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Materials
{
    public class EditModel : PageModel
    {
        public MaterialsInfo materialsInfo = new MaterialsInfo();
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
                    String sql = "SELECT * FROM materials WHERE id=@id"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                materialsInfo.id = "" + reader.GetInt32(0);
                                materialsInfo.name = reader.GetString(1);
                                materialsInfo.units = reader.GetString(2);
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
            materialsInfo.id = Request.Form["id"];
            materialsInfo.name = Request.Form["name"];
            materialsInfo.units = Request.Form["units"];

            //if (materialsInfo.id.Length == 0 || materialsInfo.name.Length == 0 || materialsInfo.units.Length == 0)
            //{
            //    errorMessage = "All the fields are required";
            //    return;
            //}

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE materials " +
                                 "SET units=@units " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //command.Parameters.AddWithValue("@name", materialsInfo.name);
                        command.Parameters.AddWithValue("@units", materialsInfo.units);
                        command.Parameters.AddWithValue("@id", materialsInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Materials/Index");
        }
    }
}
