using Job_Distribuition.Pages.Historical_Job_Logging;
using Job_Distribuition.Pages.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Historical_Job_Logging
{
    public class EditModel : PageModel
    {
        public Historical_Job_LoggingInfo historical_job_loggingInfo = new Historical_Job_LoggingInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM historical_job_logging WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                historical_job_loggingInfo.id = reader.GetInt32(0).ToString();
                                historical_job_loggingInfo.request_date = reader.GetDateTime(2).ToString("d/M/yyyy");
                                historical_job_loggingInfo.submission_date = reader.GetDateTime(3).ToString("d/M/yyyy");
                                historical_job_loggingInfo.start_date = reader.GetDateTime(4).ToString("d/M/yyyy");
                                historical_job_loggingInfo.finish_date = reader.GetDateTime(5).ToString("d/M/yyyy");
                                historical_job_loggingInfo.material_used = reader.GetString(7);
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
            historical_job_loggingInfo.id = Request.Form["id"];
            historical_job_loggingInfo.start_date = Request.Form["start_date"];
            historical_job_loggingInfo.finish_date = Request.Form["finish_date"];
            historical_job_loggingInfo.material_used = Request.Form["material_used"];

            DateTime startDate;
            DateTime finishDate;

            if (!DateTime.TryParseExact(historical_job_loggingInfo.start_date, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                errorMessage = "Invalid start date format";
                return;
            }

            if (!DateTime.TryParseExact(historical_job_loggingInfo.finish_date, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out finishDate))
            {
                errorMessage = "Invalid finish date format";
                return;
            }

            if (finishDate <= startDate)
            {
                errorMessage = "Finish date must be after start date";
                return;
            }


            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE historical_job_logging " +
                                 "SET start_date = @start_date, finish_date = @finish_date, material_used = @material_used " +
                                 "WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@start_date", startDate);
                        command.Parameters.AddWithValue("@finish_date", finishDate);
                        command.Parameters.AddWithValue("@material_used", historical_job_loggingInfo.material_used);
                        command.Parameters.AddWithValue("@id", historical_job_loggingInfo.id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            successMessage = "Task information updated successfully.";
                        }
                        else
                        {
                            errorMessage = "No task found with the provided ID.";
                        }
                    }
                }
                successMessage = "Job details updated successfully";
            }
            catch (Exception ex)
            {
                errorMessage = "Error updating worker information: " + ex.Message;
                return;
            }

            Response.Redirect("/Historical_Job_Logging/Index");
        }
    }
}
