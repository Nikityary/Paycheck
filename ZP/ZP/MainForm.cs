using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace ZP
{
    public partial class MainForm : Form
    {
        string login;
        public MainForm(string login)
        {
            InitializeComponent();
            
            this.login = login;
        }
        SqlConnection connect = new SqlConnection();
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
            sql = "select Vremya_rab.ID, Vremya_rab.Сотрудник, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник";
            ToServer();
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.Visible = true;
            checkBox1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            dateTimePicker1.Visible =  true;
            dateTimePicker2.Visible = true;
            comboBox2.Visible = false;
            button6.Visible = false;
            comboboxFill("Vremya_rab");
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
            button4.Visible = true;
            checkBox1.Visible = true;
            label2.Visible = true;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            comboBox2.Visible = true;
            button6.Visible = false;
            comboboxFill("Sotr");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sql = "select Raschet.ID, Raschet.Сотрудник,Raschet.Часы_работы,Doljnost.Заработная_плата,Raschet.Выплата_сотруднику from Raschet " +
                "left join Sotr on Sotr.ФИО = Raschet.Сотрудник left join Doljnost on Doljnost.Должность = Sotr.Должность";
            ToServer();
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.Gray;
            button4.Visible = false;
            checkBox1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            comboBox2.Visible = false;
            button6.Visible = true;
            comboboxFill("Raschet");
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                WorkTime f3 = new WorkTime();
                f3.Show();
            }
            else if (button2.BackColor == Color.Gray)
            {
                Sotr sotr = new Sotr();
                sotr.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            GetConnectName name = new GetConnectName();
            connect = name.get(System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());

            sql = "select Vremya_rab.ID, Sotr.ФИО, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник";
            ToServer();
            button1.BackColor = Color.Gray;
            if (login == "Админ")
            {
                button7.Visible = true;
            }
            comboboxFill("Vremya_rab");
            sql = "select count(*) from Doljnost";
            int count;
            connect.Open();
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string row = dataGridView1.CurrentCell.RowIndex.ToString();

            if (button1.BackColor == Color.Gray)
            {
                Edit edit = new Edit(row, "butn1");
                edit.Show();
            }
            else if(button2.BackColor == Color.Gray)
            {
                Edit edit = new Edit(row, "butn2");
                edit.Show();
            }
            else
            {
                Edit edit = new Edit(row, "butn3");
                edit.Show();
            }
        }

        public void Search(string table)
        {
            string col = "";
            string dop = "";
            if (table == "Vremya_rab") 
            {
                if (checkBox1.Checked)
                {
                    dop = " and Vremya_rab.Время_начала_работы >= '"+ dateTimePicker1.Value.ToString().Remove(10) + "' and Vremya_rab.Время_окончания_работы <= '"+ dateTimePicker2.Value.ToString().Remove(10) + "'";
                }
                if (comboBox1.Text == "ФИО")
                {
                    sql = "select Vremya_rab.ID, Vremya_rab.Сотрудник, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "WHERE Vremya_rab.Сотрудник LIKE '%" + textBox1.Text + "%'" + dop;
                }
                else
                {
                    sql = "select Vremya_rab.ID, Vremya_rab.Сотрудник, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, " +
                "Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы from Vremya_rab " +
                "WHERE Vremya_rab." + comboBox1.Text + " LIKE '%" + textBox1.Text + "%'" + dop;
                }
                ToServer();
            }
            else if (table == "Sotr")
            {
                switch (comboBox1.Text)
                {
                    case "ID":
                        col = "Sotr.ID";
                        break;
                    case "ФИО":
                        col = "Sotr.ФИО";
                        break;
                    case "Дата_приёма_на_работу":
                        col = "Sotr.Дата_приёма_на_работу";
                        break;
                    case "Телефон":
                        col = "Sotr.Телефон";
                        break;
                    case "Электронная_почта":
                        col = "Sotr.Электронная_почта";
                        break;
                    case "Должность":
                        col = "Doljnost.Должность";
                        break;
                    case "Заработная_плата":
                        col = "Doljnost.Заработная_плата";
                        break;
                }
                if (checkBox1.Checked)
                {
                    dop = " and Doljnost.Должность = '"+comboBox2.Text+"'";
                }
                sql = "select Sotr.ID, Sotr.ФИО, Format(Sotr.Дата_приёма_на_работу,'dd.MM.yyyy') as Дата_приёма_на_работу, " +
                "Sotr.Телефон, Sotr.Электронная_почта, Doljnost.Должность, Doljnost.Заработная_плата from Sotr " +
                "left join Doljnost on Doljnost.Должность = Sotr.Должность WHERE "+ col + " LIKE '%"+textBox1.Text+"%'" + dop;
                ToServer();
            }
            else if (table == "Raschet")
            {
                switch (comboBox1.Text)
                {
                    case "ID":
                        col = "Raschet.ID";
                        break;
                    case "ФИО":
                        col = "Raschet.Сотрудник";
                        break;
                    case "Часы_работы":
                        col = "Raschet.Часы_работы";
                        break;
                    case "Заработная_плата":
                        col = "Doljnost.Заработная_плата";
                        break;
                    case "Выплата_сотруднику":
                        col = "Raschet.Выплата_сотруднику";
                        break;
                }
                sql = "select Raschet.ID, Raschet.Сотрудник, Raschet.Часы_работы,Doljnost.Заработная_плата,Raschet.Выплата_сотруднику from Raschet " +
                "left join Sotr on Sotr.ФИО = Raschet.Сотрудник left join Doljnost on Doljnost.Должность = Sotr.Должность " +
                "WHERE "+col +" LIKE '%" + textBox1.Text + "%'";
                ToServer();
            }
        }

        public void comboboxFill(string table)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            connect.Open();
            var schemaTable2 = connect.GetSchema("Columns", new[] { null, null, table });
            foreach (DataRow row in schemaTable2.Rows)
            {
                string columnName = row["COLUMN_NAME"].ToString();
                comboBox1.Items.Add(columnName);
            }
            connect.Close();
            comboBox1.Text = (string)comboBox1.Items[1];

            if (table == "Vremya_rab")
            {
                comboBox1.Items.Remove("Название");
                comboBox1.Items[1] = "ФИО";
            }
            else if (table == "Sotr")
            {
                comboBox1.Items.Add("Заработная_плата");
            }
            else if (table == "Raschet")
            {
                comboBox1.Items.Add("Заработная_плата");
                comboBox1.Items[1] = "ФИО";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string row = dataGridView1.CurrentCell.RowIndex.ToString();
            var result = MessageBox.Show("Вы уверены что хотите удалить выбраный объект?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string table;
                if (button1.BackColor == Color.Gray)
                {
                    table = "Vremya_rab";
                }
                else if (button2.BackColor == Color.Gray)
                {
                    table = "Sotr";
                }
                else
                {
                    table = "Raschet";
                }
                sql = "delete from " + table + " where id = '" + row + "'";
                ToServer();
                connect.Open();
                string idColumnName = "ID"; 
                sql = $"SELECT MAX({idColumnName}) FROM {table}";
                int lastId;
                using (SqlCommand getMaxIdCommand = new SqlCommand(sql, connect))
                {
                    object maxIdValue = getMaxIdCommand.ExecuteScalar();
                    lastId = (maxIdValue == DBNull.Value) ? 0 : Convert.ToInt32(maxIdValue);
                }
                connect.Close();
                for (int i = Int32.Parse(row); i <= lastId; i++)
                {
                    sql = "update "+table+" set ID = '"+ i.ToString() + "' where ID = '"+ (i+1).ToString() + "'";
                    ToServer();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
            else
            {
                Search("Raschet");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sql = @"SELECT Sotr.ФИО, Raschet.Часы_работы, Doljnost.Заработная_плата, Raschet.Выплата_сотруднику
                         FROM Raschet
                         LEFT JOIN Sotr ON Sotr.ФИО = Raschet.Сотрудник
                         LEFT JOIN Doljnost ON Doljnost.Должность = Sotr.Должность";
            {
                connect.Open();
                using (SqlCommand command = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчёт");
                            for (int col = 0; col < reader.FieldCount; col++)
                            {
                                worksheet.Cells[1, col + 1].Value = reader.GetName(col);
                            }
                            int row = 2;
                            while (reader.Read())
                            {
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    worksheet.Cells[row, col + 1].Value = reader[col];
                                }
                                row++;
                            }
                            worksheet.Cells.AutoFitColumns();
                            ;
                            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString()+"\\Отчёт.xlsx"; 
                            package.SaveAs(new FileInfo(filePath));
                        }
                    }
                }
                connect.Close();
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Gray)
            {
                Search("Vremya_rab");
            }
            else if (button2.BackColor == Color.Gray)
            {
                Search("Sotr");
            }
            else
            {
                Search("Raschet");
            }
        }
    }
}
