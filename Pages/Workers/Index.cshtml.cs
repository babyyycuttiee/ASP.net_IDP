using Job_Distribuition.Pages.Historical_Job_Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Workers
{
    public class IndexModel : PageModel
    {
        public List<WorkersInfo> listWorkers = new List<WorkersInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM workers"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                WorkersInfo workersInfo = new WorkersInfo();
                                workersInfo.id = reader.GetInt32(0).ToString();
                                workersInfo.workers_id = reader.GetString(1);
                                workersInfo.name = reader.GetString(2);
                                workersInfo.email = reader.GetString(3);
                                workersInfo.phone = reader.GetString(4);
                                workersInfo.nationality = reader.GetString(5);
                                workersInfo.created_at = reader.GetDateTime(6).ToString();

                                listWorkers.Add(workersInfo);
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

    public class WorkersInfo
    {
        public String id;
        public String workers_id;
        public String name;
        public String email;
        public String phone;
        public String nationality;
        public String created_at;
    }
}
