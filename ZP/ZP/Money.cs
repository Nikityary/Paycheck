using System;
using System.Data.SqlClient;
using System.Data;

namespace ZP
{
    internal class Money
    {
        SqlConnection connect = new SqlConnection();

        string sql;

        public void ToServer()
        {
            try
            {
                connect.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, connect);
                SqlCommandBuilder sdc = new SqlCommandBuilder(sda);
                DataTable data = new DataTable();
                sda.Fill(data);
            }
            catch
            {
                connect.Close();
            }
            connect.Close();
        }

        public string MainMethod(string name1)
        {
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
            try
            {
                sql = "select Vremya_rab.Время_начала_работы from Vremya_rab where Vremya_rab.Название = '" + name1 + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();

                sql = "select Vremya_rab.Время_окончания_работы from Vremya_rab where Vremya_rab.Название = '" + name1 + "'";
                string zp1;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp1 = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();

                DateTime d1 = new DateTime();
                DateTime d2 = new DateTime();

                d1 = DateTime.Parse(zp);
                d2 = DateTime.Parse(zp1);

                string d = d2.Subtract(d1).TotalHours.ToString();

                sql = "select Сотрудник from Vremya_rab where Vremya_rab.Название = '" + name1 + "'";
                double pay;
                using (SqlCommand cmdString = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    sql = "select Должность from Sotr where ФИО = '" + cmdString.ExecuteScalar().ToString() + "'";
                    using (SqlCommand cmdString1 = new SqlCommand(sql, connect))
                    {
                        sql = "select Заработная_плата from Doljnost where Должность = '" + cmdString1.ExecuteScalar().ToString() + "'";
                        using (SqlCommand cmdString2 = new SqlCommand(sql, connect))
                        {
                            pay = Double.Parse(cmdString2.ExecuteScalar().ToString());
                        }
                    }
                }
                connect.Close();

                string reward = (Double.Parse(d) * pay).ToString();
                return reward;
            }
            catch { return null; }
        }
    }
}
