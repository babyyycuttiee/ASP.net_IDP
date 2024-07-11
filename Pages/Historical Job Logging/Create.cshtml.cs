using Job_Distribuition.Pages.Historical_Job_Logging;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Historical_Job_Logging
{
    public class CreateModel : PageModel
    {
        public Historical_Job_LoggingInfo historical_job_loggingInfo = new Historical_Job_LoggingInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            historical_job_loggingInfo.request_date = Request.Form["request_date"];
            historical_job_loggingInfo.submission_date = Request.Form["submission_date"];
            historical_job_loggingInfo.machine_used = Request.Form["machine_used"];

            if (string.IsNullOrEmpty(historical_job_loggingInfo.request_date) || string.IsNullOrEmpty(historical_job_loggingInfo.submission_date) || 
                string.IsNullOrEmpty(historical_job_loggingInfo.machine_used))
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO historical_job_logging " +
                                 "(request_date, submission_date, machine_used, start_date, finish_date, workers_id, material_used) VALUES " +
                                 "(@request_date, @submission_date, @machine_used, @start_date, @finish_date, @workers_id, @material_used);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@request_date", Convert.ToDateTime(historical_job_loggingInfo.request_date));
                        command.Parameters.AddWithValue("@submission_date", Convert.ToDateTime(historical_job_loggingInfo.submission_date));
                        command.Parameters.AddWithValue("@machine_used", historical_job_loggingInfo.machine_used);
                        command.Parameters.AddWithValue("@start_date", DateTime.Parse("1/1/1900"));
                        command.Parameters.AddWithValue("@finish_date", DateTime.Parse("1/1/1900"));
                        command.Parameters.AddWithValue("@material_used", "");
                        command.Parameters.AddWithValue("@workers_id", "");


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            historical_job_loggingInfo.request_date = "";
            historical_job_loggingInfo.submission_date = "";
            historical_job_loggingInfo.machine_used = "";
            successMessage = "New Task Added Successfully";

            RedirectToPage("/Historical_Job_Logging/Index");
        }
    }
}
