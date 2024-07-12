using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            sql = "select Vremya_rab.ID, Sotr.ФИО, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник";
            ToServer();
            button1.BackColor = Color.Gray;
        }

        /*SqlConnection connect = new SqlConnection(
           "Data Source=MS-sql\\SQLEXPRESS;Initial Catalog=ZP;Integrated Security=True");*/
        SqlConnection connect = new SqlConnection(
          "Data Source=KOMPUTER\\MSSQLSERVER01;Initial Catalog=ZP;Integrated Security=True");
        public string dwg_id;

        string sql;

        public void ToServer()
        {
            try
            {
                dataGridView1.DefaultCellStyle.Font = new Font("Comic Sans MS", 8);
                connect.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, connect);
                SqlCommandBuilder sdc = new SqlCommandBuilder(sda);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
            }
            catch
            {
                connect.Close();
            }
            dataGridView1.ReadOnly = true;
            connect.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = "select Vremya_rab.ID, Sotr.ФИО, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник";
            ToServer();
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql = "select Sotr.ID, Sotr.ФИО, Format(Sotr.Дата_приёма_на_работу,'dd.MM.yyyy') as Дата_приёма_на_работу, " +
                "Sotr.Телефон, Sotr.Электронная_почта, Doljnost.Должность, Doljnost.Заработная_плата from Sotr " +
                "left join Doljnost on Doljnost.Должность = Sotr.Должность";
            ToServer();
            button1.BackColor = Color.White;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sql = "select Raschet.ID, Sotr.ФИО,Raschet.Часы_работы,Doljnost.Заработная_плата,Raschet.Выплата_сотруднику from Raschet " +
                "left join Sotr on Sotr.ФИО = Raschet.Сотрудник left join Doljnost on Doljnost.Должность = Sotr.Должность";
            ToServer();
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.Gray;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Form3 f3 = new Form3();
                f3.Show();
            }
            else if (button2.BackColor == Color.Gray)
            {

            }
            else 
            { 
                
            }
        }
    }
}
