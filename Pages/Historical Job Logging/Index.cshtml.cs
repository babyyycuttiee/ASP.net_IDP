using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Historical_Job_Logging
{
    public class IndexModel : PageModel
    {
        public List<Historical_Job_LoggingInfo> listHistorical_Job_Logging = new List<Historical_Job_LoggingInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM historical_job_logging"; //allow to read all rows from historical_job_logging table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Historical_Job_LoggingInfo historical_job_loggingInfo = new Historical_Job_LoggingInfo();
                                historical_job_loggingInfo.id = reader.GetInt32(0).ToString();
                                historical_job_loggingInfo.task_id = reader.GetString(1);
                                historical_job_loggingInfo.request_date = reader.GetDateTime(2).ToString("d/M/yyyy");
                                historical_job_loggingInfo.submission_date = reader.GetDateTime(3).ToString("d/M/yyyy");
                                historical_job_loggingInfo.start_date = reader.GetDateTime(4).ToString("d/M/yyyy");
                                historical_job_loggingInfo.finish_date = reader.GetDateTime(5).ToString("d/M/yyyy");
                                historical_job_loggingInfo.machine_used = reader.GetString(6);
                                historical_job_loggingInfo.material_used = reader.GetString(7);
                                historical_job_loggingInfo.workers_id = reader.GetString(8);

                                listHistorical_Job_Logging.Add(historical_job_loggingInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class Historical_Job_LoggingInfo
    {
        public String id;
        public String task_id;
        public String request_date;
        public String submission_date;
        public String start_date;
        public String finish_date;
        public String machine_used;
        public String material_used;
        public String workers_id;
    }

}
