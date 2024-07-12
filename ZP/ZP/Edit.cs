using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ZP
{
    public partial class Edit : Form
    {
        string id;
        string table;
        new string Name;
        string start;
        string end;
        string phone;
        string mail;
        string work_date;
        string dolj;
        string name1;
        string name2;

        public Edit(string id, string table)
        {
            InitializeComponent();
            this.id = id;
            this.table = table;
        }
        SqlConnection connect = new SqlConnection();
        public string dwg_id;

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

        private void Edit_Load(object sender, EventArgs e) 
        {
            MaximizeBox = false;
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
            int count;
            switch (table)
            {
                case "butn1":
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
                    connect.Close() ;
                    this.Size = new Size(465, 305);
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    comboBox1.Visible = true;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;

                    panel3.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    panel4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;

                    sql = "select Сотрудник from Vremya_rab where id = '" + id + "'";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        connect.Open();
                        Name = (string)cmdCount.ExecuteScalar();
                    }
                    sql = "select Время_начала_работы from Vremya_rab where id = '" + id + "'";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        start = cmdCount.ExecuteScalar().ToString();
                    }
                    sql = "select Время_окончания_работы from Vremya_rab where id = '" + id + "'";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        end = cmdCount.ExecuteScalar().ToString();
                    }
                    connect.Close();

                    comboBox1.Text = Name;
                    textBox1.Text = start.Remove(13).Remove(0,11);  
                    textBox2.Text = start.Remove(16).Remove(0, 14);
                    textBox3.Text = end.Remove(13).Remove(0, 11);
                    textBox4.Text = end.Remove(16).Remove(0, 14);
                    dateTimePicker1.Value = DateTime.Parse(start.Remove(10));
                    dateTimePicker2.Value = DateTime.Parse(end.Remove(10));
                    break;

                case "butn2":
                    connect.Open();
                    sql = "select count(*) from Doljnost";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        count = Int32.Parse(cmdCount.ExecuteScalar().ToString());
                    }
                    connect.Close();
                    for (int i = 0; count != i; i++)
                    {
                        connect.Open();
                        sql = "select Должность from Doljnost where id = '" + i + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            comboBox2.Items.Add(cmdCount.ExecuteScalar().ToString());
                            comboBox2.Text = cmdCount.ExecuteScalar().ToString();
                        }
                        connect.Close();
                    }
                    connect.Close();
                    this.Size = new Size(515, 429);
                    panel3.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;

                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                    comboBox1.Visible = false;
                    panel1.Visible = false;
                    panel2.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    panel4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;

                    sql = "select ФИО from Sotr where id = '" + id + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            connect.Open();
                            Name = (string)cmdCount.ExecuteScalar();
                        }
                    sql = "select Телефон from Sotr where id = '" + id + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            phone = cmdCount.ExecuteScalar().ToString();
                        }
                    sql = "select Электронная_почта from Sotr where id = '" + id + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            mail = cmdCount.ExecuteScalar().ToString();
                        }
                    sql = "select Дата_приёма_на_работу from Sotr where id = '" + id + "'"; 
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            work_date = cmdCount.ExecuteScalar().ToString();
                        }
                    sql = "select Должность from Sotr where id = '" + id + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            dolj = cmdCount.ExecuteScalar().ToString();
                        }
                    connect.Close();

                    textBox6.Text = Name;
                    dateTimePicker3.Value = DateTime.Parse(work_date.Remove(10));
                    maskedTextBox1.Text = phone;
                    textBox5.Text = mail;
                    comboBox2.Text = dolj;
                    try
                    {
                        sql = " select Заработная_плата from Doljnost where Должность = '" + comboBox2.Text + "'";
                        string zp;
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            connect.Open();
                            zp = cmdCount.ExecuteScalar().ToString();
                        }
                        connect.Close();
                        label10.Text = zp.ToString();
                    }
                    catch { }
                    name2 = textBox6.Text;
                    break;

                case "butn3":
                    connect.Open();
                    sql = "select count(*) from Vremya_rab";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        count = Int32.Parse(cmdCount.ExecuteScalar().ToString());
                    }
                    for (int i = 0; count != i; i++)
                    {
                        sql = "select Название from Vremya_rab where id = '" + i + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            comboBox3.Items.Add(cmdCount.ExecuteScalar().ToString());
                            comboBox3.Text = cmdCount.ExecuteScalar().ToString();
                        }
                    }
                    connect.Close();
                    this.Size = new Size(572, 218);
                    panel4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;

                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                    comboBox1.Visible = false;
                    panel1.Visible = false;
                    panel2.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    panel3.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;

                    sql = "select Название from Vremya_rab where id = '" + id + "'";
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        connect.Open();
                        name1 = (string)cmdCount.ExecuteScalar();
                    }
                    connect.Close();
                    Money money = new Money(); 
                    label17.Text = Double.Parse(money.MainMethod(name1)).ToString("0.##") + "₽";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" &&  textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
                TimeSpan ts1 = new TimeSpan(0, 0, 0, 0, 0);
                ts1 = dateTimePicker2.Value - dateTimePicker1.Value;
                TimeSpan ts2 = new TimeSpan(0, 0, 0, 0, 0);
                string start = dateTimePicker1.Value.ToString().Remove(10) + " " + textBox1.Text + ":" + textBox2.Text;
                string finish = dateTimePicker2.Value.ToString().Remove(10) + " " + textBox3.Text + ":" + textBox4.Text;
                string name = comboBox1.Text + " (" + start + " - " + finish + ")";
                Name = comboBox1.Text;

                if (ts1 >= ts2)
                {
                    sql = "update Vremya_rab set Сотрудник = '"+Name+"' where id = '"+id+ "' \r\n" +
                        "update Vremya_rab set Время_начала_работы = '"+start+"' where id = '"+id+ "' \r\n" +
                        "update Vremya_rab set Время_окончания_работы = '"+finish+"' where id = '"+id+ "' \r\n" +  //возможно здесь будет ошибка
                        "update Vremya_rab set Название = '"+name+"' where id = '"+id+"'";
                    ToServer();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Дата окончания работы раньше чем начало работы") ;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connect.Close();
                sql = " select Заработная_плата from Doljnost where Должность = '" + comboBox2.Text + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();
                label10.Text = zp.ToString();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" &&  textBox6.Text != ""&& comboBox2.Text != "" && maskedTextBox1.Text != "")
            {
                sql = "update Sotr set ФИО = '"+textBox6.Text+"' where id = '"+id+"'\r\n" +
                    "update Sotr set Дата_приёма_на_работу = '"+dateTimePicker3.Value+"' where id = '"+id+"'\r\n" +
                    "update Sotr set Телефон = '"+maskedTextBox1.Text+"' where id = '"+id+"'\r\n" +
                    "update Sotr set Электронная_почта = '"+textBox5.Text+"' where id = '"+id+"'\r\n" +
                    "update Sotr set Должность = '"+comboBox2.Text+"' where id = '"+id+"'\r\n"+
                    "update Vremya_rab set Сотрудник = '"+textBox6.Text+"' where Сотрудник = '"+name2+"'\r\n"+
                    "update Raschet set Сотрудник = '"+textBox6.Text+"' where Сотрудник = '"+name2+"' ";
                ToServer();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните пустые ячейки");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                name1 = comboBox3.Text;
                Money money = new Money();
                label17.Text = Double.Parse(money.MainMethod(name1)).ToString("0.##") + "₽";
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "select Сотрудник from Vremya_rab where Название = '" + comboBox3.Text + "'";
                string sotr;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    sotr = cmdCount.ExecuteScalar().ToString();
                }

                sql = "select Vremya_rab.Время_начала_работы from Vremya_rab where Vremya_rab.Название = '" + comboBox3.Text + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    zp = cmdCount.ExecuteScalar().ToString();
                }

                sql = "select Vremya_rab.Время_окончания_работы from Vremya_rab where Vremya_rab.Название = '" + comboBox3.Text + "'";
                string zp1;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    zp1 = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();

                DateTime d1 = new DateTime();
                DateTime d2 = new DateTime();

                d1 = DateTime.Parse(zp);
                d2 = DateTime.Parse(zp1);

                double d = Double.Parse(d2.Subtract(d1).TotalHours.ToString());

                string pay = label17.Text.Remove(label17.Text.Length-1).Replace(",",".");
                sql = "update Raschet set Сотрудник = '"+ sotr + "' where id = '"+id+"'\r\n" +
                    "update Raschet set Часы_работы = '"+d.ToString("0.##").Replace(",",".")+"' where id = '"+id+"'\r\n" +
                    "update Raschet set Выплата_сотруднику = '"+pay+"' where id = '"+id+"'";
                ToServer();
                this.Close();
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
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
