using Job_Distribuition.Pages.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Machines
{
    public class EditModel : PageModel
    {
        public MachinesInfo machinesInfo = new MachinesInfo();
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
                    String sql = "SELECT * FROM machines WHERE id=@id"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                machinesInfo.id = "" + reader.GetInt32(0);
                                machinesInfo.name = reader.GetString(1);
                                machinesInfo.units = reader.GetString(2);
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
            machinesInfo.id = Request.Form["id"];
            machinesInfo.name = Request.Form["name"];
            machinesInfo.units = Request.Form["units"];

            //if (machinesInfo.id.Length == 0 || machinesInfo.name.Length == 0 || machinesInfo.units.Length == 0)
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
                    String sql = "UPDATE machines " +
                                 "SET units=@units " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //command.Parameters.AddWithValue("@name", machinesInfo.name);
                        command.Parameters.AddWithValue("@units", machinesInfo.units);
                        command.Parameters.AddWithValue("@id", machinesInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Machines/Index");
        }
    }
}
