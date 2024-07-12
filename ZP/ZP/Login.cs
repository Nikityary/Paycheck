using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ZP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection();
        public string dwg_id;
        string sql = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Введите логин");
            }
            else
            {
                if (textBox2.Text.Equals(""))
                {
                    MessageBox.Show("Введите пароль");
                }
                else
                {
                    sql = "select count(*) from Users where Логин = '" + textBox1.Text + "'";
                    int a = 0;
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        connect.Open();
                        a = (int)cmdCount.ExecuteScalar();
                        connect.Close();
                    }
                    if (a == 0)
                    {
                        textBox1.BackColor = Color.Red;
                        MessageBox.Show("Логин неверен");
                    }
                    else
                    {
                        sql = sql = "select count(*) from Users where Логин = '" + textBox1.Text + "' and Пароль = '" + textBox2.Text + "'";
                        using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                        {
                            connect.Open();
                            a = (int)cmdCount.ExecuteScalar();
                            connect.Close();
                        }
                        if (a == 0)
                        {
                            textBox2.BackColor = Color.Red;
                            MessageBox.Show("Пароль неверен");
                        }
                        else
                        {
                            MainForm f2 = new MainForm(textBox1.Text);
                            f2.Show();
                            this.Hide();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals(""))
            {
                textBox3.BackColor = Color.Red;
                MessageBox.Show("Введите логин");
            }
            else
            {
                if (textBox4.Text.Equals(""))
                {
                    textBox4.BackColor = Color.Red;
                    MessageBox.Show("Введите пароль");
                }
                else
                {
                    sql = "select count(*) from Users where Логин = '" + textBox3.Text + "'";
                    int a = 0;
                    using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                    {
                        connect.Open();
                        a = (int)cmdCount.ExecuteScalar();
                        connect.Close();
                    }
                    if (a == 0)
                    {
                        int count = row_count();
                        sql = "insert Users values ('" + count + "', '" + textBox3.Text + "', '" + textBox4.Text + "')";
                        connect.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sql, connect);
                        DataTable data = new DataTable();
                        sda.Fill(data);
                        connect.Close();
                        textBox3.Text = "";
                        textBox4.Text = "";
                        MessageBox.Show("Пользователь создан");
                    }
                    else
                    {
                        textBox3.BackColor = Color.Red;
                        MessageBox.Show("Логин не уникален");
                    }
                }
            }
        }

        public int row_count()
        {
            sql = "select count(*) FROM Users";
            int count = 0;
            using (SqlCommand cmdCount = new SqlCommand(sql, connect))
            {
                connect.Open();
                count = (int)cmdCount.ExecuteScalar();
            }
            connect.Close();
            return count;
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }
        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
        }

        private void textBox4_MouseEnter(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
        }
    }
}
