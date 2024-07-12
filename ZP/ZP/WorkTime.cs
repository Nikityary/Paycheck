using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ZP
{
    public partial class WorkTime : Form
    {
        public WorkTime()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection();
        string sql;
        string public_name;
        private void Form3_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
            int count;
            connect.Open();
            sql = "select count(*) from Sotr";
            using (SqlCommand cmdCount = new SqlCommand(sql, connect))
            {
                count = Int32.Parse(cmdCount.ExecuteScalar().ToString());
            }
            for (int i = 0; count != i; i++)
            {
                sql = "select ФИО from Sotr where id = '" + i + "'";
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    comboBox1.Items.Add(cmdCount.ExecuteScalar().ToString());
                    comboBox1.Text = cmdCount.ExecuteScalar().ToString();
                }
            }
            connect.Close();
        }

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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox1.Text);
                    if (a > 23)
                    {
                        textBox1.Text = "23";
                    }
                    else if (a < 0)
                    {
                        textBox1.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox1.Text = "";
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox2.Text);
                    if (a > 59)
                    {
                        textBox2.Text = "59";
                    }
                    else if (a < 0)
                    {
                        textBox2.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox2.Text = "";
                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox3.Text);
                    if (a > 23)
                    {
                        textBox3.Text = "23";
                    }
                    else if (a < 0)
                    {
                        textBox3.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox3.Text = "";
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox4.Text);
                    if (a > 59)
                    {
                        textBox4.Text = "59";
                    }
                    else if (a < 0)
                    {
                        textBox4.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox4.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                string start = dateTimePicker1.Value.ToString().Remove(10) + " " + textBox1.Text + ":" + textBox2.Text;
                string finish = dateTimePicker2.Value.ToString().Remove(10) + " " + textBox3.Text + ":" + textBox4.Text;
                string name = comboBox1.Text + " (" + start + " - " + finish + ")";

                sql = "insert Vremya_rab values('" + row_count("Vremya_rab") + "', '" + comboBox1.Text + "', '" + start + "', '" + finish + "', '" + name + "')";
                ToServer();

                public_name = name;
                MainMethod();
                this.Close();
            }
            else
                MessageBox.Show("Есть незаполненные поля");
        }

        public void MainMethod()
        {
            try
            {
                sql = "select Vremya_rab.Время_начала_работы from Vremya_rab where Vremya_rab.Название = '" + public_name + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();

                sql = "select Vremya_rab.Время_окончания_работы from Vremya_rab where Vremya_rab.Название = '" + public_name + "'";
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

                sql = "select Сотрудник from Vremya_rab where Vremya_rab.Название = '" + public_name + "'";
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
                double main_zp = Double.Parse(d) * pay;

                sql = "insert Raschet values ('"+row_count("Raschet") +"', '"+comboBox1.Text+"', '"+ 
                    Math.Round(Double.Parse(d),2).ToString().Replace(",",".")+"', '"+Math.Round(main_zp,2).ToString().Replace(",", ".") + "')";
                ToServer();
            }
            catch { }
        }

        public int row_count(string table)
        {
            sql = "select count(*) FROM "+table;
            int count = 0;
            using (SqlCommand cmdCount = new SqlCommand(sql, connect))
            {
                connect.Open();
                count = (int)cmdCount.ExecuteScalar();
            }
            connect.Close();
            return count;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox1.Text);
                    if (a > 23)
                    {
                        textBox1.Text = "23";
                    }
                    else if (a < 0)
                    {
                        textBox1.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox1.Text = "";
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox2.Text);
                    if (a > 59)
                    {
                        textBox2.Text = "59";
                    }
                    else if (a < 0)
                    {
                        textBox2.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox2.Text = "";
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox3.Text);
                    if (a > 23)
                    {
                        textBox3.Text = "23";
                    }
                    else if (a < 0)
                    {
                        textBox3.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox3.Text = "";
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                try
                {
                    int a = Int32.Parse(textBox4.Text);
                    if (a > 59)
                    {
                        textBox4.Text = "59";
                    }
                    else if (a < 0)
                    {
                        textBox4.Text = "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Ввод букв недоступен");
                    textBox4.Text = "";
                }
            }
        }
    }
}
