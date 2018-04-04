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
    public partial class Доктор : Form
    {
        public OracleConnection connectDB;
        public int id_doc1;
        public Доктор(string login1, string pass1)
        {
            InitializeComponent();
            
            string connectStr = "Data Source=orcl;" + " User ID = " + login1 + ";" + "Password=" + pass1 + ";";
            connectDB = new OracleConnection(connectStr);
            connectDB.Open();

            string zapros1 = string.Format("select id_doctor,login_d,fam_d,name_d,otch_d from doctor where login_d ='{0}'", login1);
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                id_doc1 = Convert.ToInt32(reader1.GetValue(0));
                label1.Text = "Доктор: " + reader1.GetValue(2).ToString().Trim() + " " + reader1.GetValue(3).ToString().Trim() + " " + reader1.GetValue(4).ToString().Trim();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form d = Application.OpenForms[0];
            d.Show();
            this.Close();
        }

        private void Доктор_Load(object sender, EventArgs e)
        {
            try
            {
                allrec.Rows.Clear();
                allrec.ColumnCount = 6;
                allrec.Columns[0].HeaderText = "ID";
                allrec.Columns[1].HeaderText = "ФИО";
                allrec.Columns[2].HeaderText = "Дата рождения";
                allrec.Columns[3].HeaderText = "Номер полиса";
                allrec.Columns[4].HeaderText = "Номер СНИЛСа";
                allrec.Columns[5].HeaderText = "Дата, время записи";

                string zapros0 = string.Format("select id_reg,fam_p,name_p,otch_p,to_char(date_r_p,'dd.mm.yyyy'),num_policy,num_snils,to_char(date_vrem_pr,'dd.mm.yyyy, HH24:MI') from registration r,doctor d,patient p,policlinic pol where d.id_policlinic=pol.id_policlinic and p.id_pacient=r.id_pacient and r.id_doctor=d.id_doctor and d.id_doctor='{0}'", id_doc1);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                OracleDataReader reader0 = command0.ExecuteReader();

                while (reader0.Read())
                {
                    allrec.Rows.Add(reader0.GetValue(0).ToString(), reader0.GetValue(1).ToString().Trim()+" " + reader0.GetValue(2).ToString().Trim() + " " + reader0.GetValue(3), reader0.GetValue(4).ToString(), reader0.GetValue(5).ToString(), reader0.GetValue(6).ToString(), reader0.GetValue(7).ToString(),"Не был");          
                }
                allrec.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                allrec.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void allrec_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Form ad = new Назначение(Convert.ToInt32(allrec[0, allrec.CurrentCell.RowIndex].Value.ToString().Trim()), connectDB);
            ad.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                allrec.Rows.Clear();
                allrec.ColumnCount = 6;
                allrec.Columns[0].HeaderText = "ID";
                allrec.Columns[1].HeaderText = "ФИО";
                allrec.Columns[2].HeaderText = "Дата рождения";
                allrec.Columns[3].HeaderText = "Номер полиса";
                allrec.Columns[4].HeaderText = "Номер СНИЛСа";
                allrec.Columns[5].HeaderText = "Дата, время записи";
                string date_pr;
                if (dateTimePicker3.Checked == true)
                    date_pr = dateTimePicker3.Value.ToString("dd.MM.yy");
                else
                    date_pr = "%";

                string zapros0 = string.Format("select id_reg,fam_p,name_p,otch_p,to_char(date_r_p,'dd.mm.yyyy'),num_policy,num_snils,to_char(date_vrem_pr,'dd.mm.yyyy, HH24:MI') from registration r,doctor d,patient p,policlinic pol where d.id_policlinic=pol.id_policlinic and p.id_pacient=r.id_pacient and r.id_doctor=d.id_doctor and d.id_doctor='{0}' and (fam_p like '{1}%' and name_p like '{2}%' and otch_p like '{3}%' and date_vrem_pr like '{4}')", id_doc1, fam_txt.Text, im_txt.Text, otch_txt.Text, date_pr);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                OracleDataReader reader0 = command0.ExecuteReader();

                while (reader0.Read())
                {
                    allrec.Rows.Add(reader0.GetValue(0).ToString(), reader0.GetValue(1).ToString().Trim() + " " + reader0.GetValue(2).ToString().Trim() + " " + reader0.GetValue(3), reader0.GetValue(4).ToString(), reader0.GetValue(5).ToString(), reader0.GetValue(6).ToString(), reader0.GetValue(7).ToString(), "Не был");
                }
                allrec.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                allrec.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void количествоПользователейПоДатеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(количествоПользователейПоДатеToolStripMenuItem.Checked==true)
                this.Height = 464;
            else 
                this.Height = 424;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string zapros1 = string.Format("select SUMPAC({0},'{1}','{2}') from dual", id_doc1, dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString());
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                MessageBox.Show("Количество пациентов с " + dateTimePicker1.Value.ToShortDateString() + " по "+ dateTimePicker2.Value.ToShortDateString() + " у вас составляет " + reader1.GetValue(0).ToString().Trim());
            }
        }
    }
}
