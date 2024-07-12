using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ZP
{
    public partial class Sotr : Form
    {
        public Sotr()
        {
            InitializeComponent();
        }
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

        public int row_count()
        {
            sql = "select count(*) FROM Sotr";
            int count = 0;
            using (SqlCommand cmdCount = new SqlCommand(sql, connect))
            {
                connect.Open();
                count = (int)cmdCount.ExecuteScalar();
            }
            connect.Close();
            return count;
        }

        private void Sotr_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
            sql = "select count(*) from Doljnost";
            int count;
            using (SqlCommand cmdCount = new SqlCommand(sql, connect))
            {
                connect.Open();
                count = Int32.Parse(cmdCount.ExecuteScalar().ToString());
            }
            for (int i = 0;count!=i; i++)
            {
                sql = "select Должность from Doljnost where id = '"+i+"'";
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    comboBox1.Items.Add(cmdCount.ExecuteScalar().ToString());
                    comboBox1.Text = cmdCount.ExecuteScalar().ToString();
                }
            }
            connect.Close();

            try
            {
                sql = " select Заработная_плата from Doljnost where Должность = '" + comboBox1.Text + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();
                label7.Text = zp.ToString();
            }
            catch { }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sql = " select Заработная_плата from Doljnost where Должность = '" + comboBox1.Text + "'";
                string zp;
                using (SqlCommand cmdCount = new SqlCommand(sql, connect))
                {
                    connect.Open();
                    zp = cmdCount.ExecuteScalar().ToString();
                }
                connect.Close();
                label7.Text = zp.ToString();
            }
            catch{}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && maskedTextBox1.MaskFull)
            {
                sql = "insert Sotr values ('"+row_count()+"', '"+textBox1.Text+"', '"+maskedTextBox1.Text+"', '"+
                    textBox2.Text + "', '" + dateTimePicker1.Value + "', '" + comboBox1.Text + "')";
                ToServer();
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля");
        }
    }
}
