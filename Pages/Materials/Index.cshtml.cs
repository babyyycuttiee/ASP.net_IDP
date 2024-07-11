using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Job_Distribuition.Pages.Materials
{
    public class IndexModel : PageModel
    {
        public List<MaterialsInfo> listMaterials = new List<MaterialsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Job_Distribuition;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM materials"; //allow to read all rows from workers table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allow to execute sql
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MaterialsInfo materialsInfo = new MaterialsInfo();
                                materialsInfo.id = "" + reader.GetInt32(0);
                                materialsInfo.name = reader.GetString(1);
                                materialsInfo.units = reader.GetString(2);

                                listMaterials.Add(materialsInfo);
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

    public class MaterialsInfo
    {
        public String id;
        public String name;
        public String units;
    }
}
