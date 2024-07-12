using System.Data.SqlClient;

namespace ZP
{
    internal class GetConnectName
    {
        public SqlConnection get(string a)
        {
            SqlConnection con = new SqlConnection();
            if (a == "DESKTOP-ARLDGH1\\Nikityara")
            {
                con = new SqlConnection("Data Source=DESKTOP-ARLDGH1\\MSSQLSERVER2;" +
                    "Initial Catalog=ZP;Integrated Security=True");
                return con;
            }
            con = new SqlConnection("Data Source=DESKTOP-99GNU2G\\" +
                "SQLEXPRESS;Initial Catalog=ZP;Integrated Security=True");
            return con;
        }
    }
}
