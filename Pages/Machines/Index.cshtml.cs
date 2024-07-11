using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Machines
{
    public class IndexModel : PageModel
    {
        public List<MachinesInfo> listMachines = new List<MachinesInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM machines"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MachinesInfo machinesInfo = new MachinesInfo();
                                machinesInfo.id = "" + reader.GetInt32(0);
                                machinesInfo.name = reader.GetString(1);
                                machinesInfo.units = reader.GetString(2);

                                listMachines.Add(machinesInfo);
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

    public class MachinesInfo
    {
        public String id;
        public String name;
        public String units;
    }
}
