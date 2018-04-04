using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;


namespace kurs_el_och
{
    public partial class АдминПанель : Form
    {
        OracleConnection connectDB;
        int id_polic;  //id поликлиники упоменается в запросе
        
        

        public АдминПанель(string login1, string pass1)
        {
            InitializeComponent();
            string connectStr = "Data Source=orcl;" + " User ID = " + login1 + ";" + "Password=" + pass1 + ";";
            connectDB = new OracleConnection(connectStr);
            connectDB.Open();

        }

        public string GetNamePolic(string k12)      //получение названия поликлиники по айди
        {
            string zapros1 = string.Format("select getpolic({0}) from dual", k12);
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();
            string a=null;
            id_polic = Convert.ToInt32(k12);
            while (reader1.Read())
            {
                a = reader1.GetValue(0).ToString().Trim();
            }
            return a;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = login_p_txt.Text;
            string pass = pass_p_txt.Text;
            string fam = fam_p_txt.Text;
            string name = im_p_txt.Text;
            string otch = otch_p_txt.Text;
            string dr = dr_p_txt.Text;
            string np = np_p_txt.Text;
            string ns = ns_p_txt.Text;
            string addr = add_p_txt.Text;
            string tel = tel_p_txt.Text;
          
            try
            {
                string zapros1 = "create user " + login + " identified by " + pass;
                string zapros2 = string.Format("insert into patient values (seq_p.nextval,'{0}','{1}','{2}','{3}',to_date('{4}','DD.MM.YYYY'),'{5}','{6}','{7}','{8}')", 
                    login, fam, name, otch, dr, np, ns, addr, tel);
                string zapros3 = string.Format("grant patient_role to " + login);

                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleCommand command2 = new OracleCommand(zapros2, connectDB);
                OracleCommand command3 = new OracleCommand(zapros3, connectDB);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                MessageBox.Show("Новый пациент " + login + " создан.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            пациентовToolStripMenuItem_Click(sender, e);
        }                   //добавление пациента
       
        private void login_p_txt_TextChanged(object sender, EventArgs e)
        {
            string log = login_p_txt.Text;
            string querry = "select login_p from patient";
           // login_p_txt.BackColor = Color.LawnGreen;
            OracleCommand command1 = new OracleCommand(querry, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();
            bool fl = false;

            while (reader1.Read())
            {
                if (log == Convert.ToString(reader1.GetValue(0)).Trim()) fl = true; //нужно убрать пробелы!!!!!
            }
            //if (fl == true)
            //{
            //    toolTip1.Show("Оператор с таким логином уже существует", login_p_txt, login_p_txt.Location, 3000);
            //    login_p_txt.BackColor = Color.Red;
            //}
        }

        private void login_p_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
            {
                e.KeyChar = '\0';
                toolTip1.Show("Пробелы не допускаются", login_p_txt, login_p_txt.Location, 3000);
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = Application.OpenForms[0];
            a.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)                      //добавление доктора
        {
            string login = login_d_txt.Text;
            string pass = pass_d_txt.Text;
            string fam = fam_d_txt.Text;
            string name = im_d_txt.Text;
            string otch = otch_d_txt.Text;
            string spec = spec_d_txt.Text;
            int kab = (int)num_kab.Value;
            

            try
            {
                
                string zapros1 = "create user " + login + " identified by " + pass;
                string zapros2 = string.Format("insert into doctor values (seq_d.nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    login, fam, name, otch, spec, kab, id_polic);
                string zapros3 = string.Format("grant doctor_role to " + login);

                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleCommand command2 = new OracleCommand(zapros2, connectDB);
                OracleCommand command3 = new OracleCommand(zapros3, connectDB);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                MessageBox.Show("Новый доктор " + login + " создан.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            докторовToolStripMenuItem_Click(sender,e);

        }

        private void button3_Click(object sender, EventArgs e)                  //добавление поликлиники
        {
            string name = name_pol_txt.Text;
            string tel = tel_pol_txt.Text;
            string zav = zav_pol_txt.Text;
            string add = add_pol_txt.Text;
            try
            {
                string zapros1 = string.Format("insert into policlinic values (seq_pol.nextval,'{0}','{1}','{2}','{3}')",
                    name, tel, zav, add);

                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                
                command1.ExecuteNonQuery();
                
                MessageBox.Show("Новая поликлиника создана", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            поликлинникToolStripMenuItem_Click(sender, e);
        }

        private void polic_d_Click(object sender, EventArgs e)                  //обновление данных о поликлиниках в окне
        {
            polic_d.Items.Clear();
            try
            {
                string zapros1 = "select name_pol from policlinic";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    polic_d.Items.Add(Convert.ToString(reader1.GetValue(0)).Trim());
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }                       

        private void polic_d_SelectionChangeCommitted(object sender, EventArgs e)  //срабатывает при выборе элемента из выпадающего списка
        {
            
            try
            {
                string zapros1 = "select id_policlinic,name_pol from policlinic";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    if (reader1.GetValue(1).ToString().Trim() == polic_d.SelectedItem.ToString())
                    {
                        id_polic = Convert.ToInt32(reader1["id_policlinic"]);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }  //поиск id поликлиники

        private void пациентовToolStripMenuItem_Click(object sender, EventArgs e)    //вывод таблицы пациентов
        {
            label13.Text = "Таблица пациентов";
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            login_p_txt.Enabled = true;
            pass_p_txt.Enabled = true;
            button1.Enabled = true;
            button9.Enabled = false;
            button8.Enabled = false;                 //конопочки изменить, расписание, удалить

            login_p_txt.Text = null;
            pass_d_txt.Text = null;
            fam_p_txt.Text = null;
            im_p_txt.Text = null;
            otch_p_txt.Text = null;
            dr_p_txt.Text = null;
            np_p_txt.Text = null;
            ns_p_txt.Text = null;
            add_p_txt.Text = null;
            tel_p_txt.Text = null;

            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 10; int i = 0;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Логин";
            dataGridView1.Columns[2].Name = "Фамилия";
            dataGridView1.Columns[3].Name = "Имя";
            dataGridView1.Columns[4].Name = "Отчество";
            dataGridView1.Columns[5].Name = "Дата рождения";
            dataGridView1.Columns[6].Name = "Номер полиса";
            dataGridView1.Columns[7].Name = "Номер снилса";
            dataGridView1.Columns[8].Name = "Адрес";
            dataGridView1.Columns[9].Name = "Номер";
            try
            {
                string zapros1 = "select id_pacient,login_p,fam_p,name_p,otch_p,to_char(date_r_p,'dd:mm:yyyy'),num_policy,num_snils,address_p,phone_p from patient";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
             
                while (reader1.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = reader1["id_pacient"].ToString();
                    dataGridView1[1, i].Value = reader1["login_p"].ToString();
                    dataGridView1[2, i].Value = reader1["fam_p"].ToString();
                    dataGridView1[3, i].Value = reader1["name_p"].ToString();
                    dataGridView1[4, i].Value = reader1["otch_p"].ToString();
                    dataGridView1[5, i].Value = reader1["to_char(date_r_p,'dd:mm:yyyy')"].ToString();
                    dataGridView1[6, i].Value = reader1["num_policy"].ToString();
                    dataGridView1[7, i].Value = reader1["num_snils"].ToString();
                    dataGridView1[8, i].Value = reader1["address_p"].ToString();
                    dataGridView1[9, i].Value = reader1["phone_p"].ToString();
                    i++;
                }
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoResizeColumns();

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
               
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void докторовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label13.Text = "Таблица докторов";
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            groupBox1.Visible = false;

            button2.Enabled = true;
            button4.Enabled = false; 
            button10.Enabled = false;
            button5.Enabled = false;                 //конопочки изменить, расписание, удалить
            login_d_txt.Enabled = true;
            pass_d_txt.Enabled = true;

            login_d_txt.Text = null;
            pass_d_txt.Text= null;
            fam_d_txt.Text = null;
            im_d_txt.Text = null;
            otch_d_txt.Text = null;
            spec_d_txt.Text = null;
            num_kab.Text = null;
            polic_d.SelectedItem = null;


            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 8; int i = 0;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Логин";
            dataGridView1.Columns[2].Name = "Фамилия";
            dataGridView1.Columns[3].Name = "Имя";
            dataGridView1.Columns[4].Name = "Отчество";
            dataGridView1.Columns[5].Name = "Специальность";
            dataGridView1.Columns[6].Name = "Номер кабинета";
            dataGridView1.Columns[7].Name = "ID поликлиники";
            
            try
            {
                string zapros1 = "select id_doctor,login_d,fam_d,name_d,otch_d,spec_d,num_kab_d,id_policlinic from doctor";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = reader1["id_doctor"].ToString();
                    dataGridView1[1, i].Value = reader1["login_d"].ToString();
                    dataGridView1[2, i].Value = reader1["fam_d"].ToString();
                    dataGridView1[3, i].Value = reader1["name_d"].ToString();
                    dataGridView1[4, i].Value = reader1["otch_d"].ToString();
                    dataGridView1[5, i].Value = reader1["spec_d"].ToString();
                    dataGridView1[6, i].Value = reader1["num_kab_d"].ToString();
                    dataGridView1[7, i].Value = reader1["id_policlinic"].ToString();
                    i++;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoResizeColumns();

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }   //вывод таблицы докторов

        private void поликлинникToolStripMenuItem_Click(object sender, EventArgs e)     //отображение поликлиник
        {
            label13.Text = "Таблица поликлиник";
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            button3.Enabled = true;
            button7.Enabled = false;
            button6.Enabled = false;

            name_pol_txt.Text = null;
            tel_pol_txt.Text = null;
            zav_pol_txt.Text = null;
            add_pol_txt.Text=null;

            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 5; int i = 0;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Название";
            dataGridView1.Columns[2].Name = "Телефон";
            dataGridView1.Columns[3].Name = "Заведующий";
            dataGridView1.Columns[4].Name = "Адрес";
            

            try
            {
                string zapros1 = "select id_policlinic,name_pol,phone_pol,zav_pol,address_pol from policlinic";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = reader1["id_policlinic"].ToString();
                    dataGridView1[1, i].Value = reader1["name_pol"].ToString();
                    dataGridView1[2, i].Value = reader1["phone_pol"].ToString();
                    dataGridView1[3, i].Value = reader1["zav_pol"].ToString();
                    dataGridView1[4, i].Value = reader1["address_pol"].ToString();
                    i++;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoResizeColumns();

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }  //вывод таблицы поликлиник

        

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(groupBox1.Visible == true)       //паза пациентов
            {
                login_p_txt.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                fam_p_txt.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                im_p_txt.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                otch_p_txt.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                dr_p_txt.Text = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                np_p_txt.Text = dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                ns_p_txt.Text = dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                add_p_txt.Text = dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                tel_p_txt.Text = dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                login_p_txt.Enabled= false;
                pass_p_txt.Enabled = false;
                button1.Enabled = false;
                button9.Enabled = true;
                button8.Enabled = true;
            }
            if (groupBox2.Visible == true)       //база врачей
            {

                login_d_txt.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                fam_d_txt.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                im_d_txt.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                otch_d_txt.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                spec_d_txt.Text = dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                num_kab.Text = dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                polic_d.Text = GetNamePolic(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim());
                login_d_txt.Enabled = false;
                pass_d_txt.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = true;
                button10.Enabled = true;
                button5.Enabled = true;
            }
            if (groupBox3.Visible == true)      //база поликлиник
            {
                name_pol_txt.Text = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                tel_pol_txt.Text = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                zav_pol_txt.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                add_pol_txt.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
                button7.Enabled = true;
                button6.Enabled = true;
                button3.Enabled = false;
            }
        }       //двойной щелчек по строчке для изменения

        private void button9_Click(object sender, EventArgs e)                                 // измененин пациента
        {
            string fam = fam_p_txt.Text;
            string name = im_p_txt.Text;
            string otch = otch_p_txt.Text;
            string dr = dr_p_txt.Text;
            string np = np_p_txt.Text;
            string ns = ns_p_txt.Text;
            string addr = add_p_txt.Text;
            string tel = tel_p_txt.Text;

            try
            {
                string zapros1 = string.Format("UPDATE patient SET fam_p='{0}',name_p='{1}',otch_p='{2}',date_r_p=to_date('{3}','DD.MM.YYYY'),num_policy='{4}',num_snils='{5}',address_p='{6}',phone_p='{7}' where trim(' ' from login_p)='{8}'",
                    fam, name, otch, dr, np, ns, addr, tel, login_p_txt.Text);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);                
                command1.ExecuteNonQuery();                
                MessageBox.Show("Пациент изменен.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                пациентовToolStripMenuItem_Click(sender, e);

            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            

        }

        private void button8_Click(object sender, EventArgs e)                      //удалить пациента
        {
            try
            {
                string zapros0 = string.Format("DELETE FROM patient where login_p='{0}'", login_p_txt.Text);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                command0.ExecuteNonQuery();

                string zapros1 = string.Format("drop user {0} CASCADE", login_p_txt.Text);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();
                MessageBox.Show("Пациент удален.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            пациентовToolStripMenuItem_Click(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)                 //отобразить расписание
        {
            
            Form a = new РасписаниеДоктора(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim()), connectDB);
            a.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)      //изменить доктора
        {
         

            string fam = fam_d_txt.Text;
            string name = im_d_txt.Text;
            string otch = otch_d_txt.Text;
            string spec = spec_d_txt.Text;
            int kab = (int)num_kab.Value;

            try
            {
                string zapros1 = string.Format("UPDATE doctor SET fam_d='{0}',name_d='{1}',otch_d='{2}',spec_d='{3}',num_kab_d='{4}',id_policlinic='{5}'where trim(' ' from login_d)='{6}'",
                    fam, name, otch, spec, kab, id_polic, login_d_txt.Text);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();
                MessageBox.Show("Доктор изменен.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            докторовToolStripMenuItem_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)              //удалить доктора
        {
            try
            {
                string zapros0 = string.Format("DELETE FROM doctor where login_d='{0}'", login_d_txt.Text);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                command0.ExecuteNonQuery();

                string zapros1 = string.Format("drop user {0} CASCADE", login_d_txt.Text);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();
                MessageBox.Show("Доктор удален.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                докторовToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            докторовToolStripMenuItem_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)   //изменить поликлинику
        {
            string name = name_pol_txt.Text;
            string tel = tel_pol_txt.Text;
            string zav = zav_pol_txt.Text;
            string add = add_pol_txt.Text;
            
            try
            {
                string zapros1 = string.Format("UPDATE policlinic SET name_pol='{0}',phone_pol='{1}',zav_pol='{2}',address_pol='{3}' where trim(' ' from id_policlinic)='{4}'",
                    name, tel, zav, add, Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim()));
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();
                MessageBox.Show("Поликлиника изменен.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            поликлинникToolStripMenuItem_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)   //удалить поликлинику
        {
            try
            {
                string zapros1 = string.Format("begin DropPolic('{0}'); end;", name_pol_txt.Text);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();
                MessageBox.Show("Поликлиника удалена удален.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            поликлинникToolStripMenuItem_Click(sender, e);
        }
    }
}
