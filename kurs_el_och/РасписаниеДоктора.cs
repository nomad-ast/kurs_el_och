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
    public partial class РасписаниеДоктора : Form
    {
        OracleConnection connectDB;
        int doc;
        
        public РасписаниеДоктора(int tdoc, OracleConnection condb)
        {
            doc = tdoc;
            InitializeComponent();
            connectDB = condb;
            string zapros1 = string.Format("select id_doctor,login_d,fam_d,name_d,otch_d from doctor where id_doctor ='{0}'", tdoc);
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                label1.Text = "Расписание доктора: " + reader1.GetValue(2).ToString().Trim() + " " + reader1.GetValue(3).ToString().Trim() + " " + reader1.GetValue(4).ToString().Trim();
            }
            vivod();
            /*Добавить триггер который ставит - в таблице если 0*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string doctor = doc.ToString();
            string pn = pn_txt.Text;
            string vt = vt_txt.Text;
            string sr = sr_txt.Text;
            string cht = cht_txt.Text;
            string pt = pt_txt.Text;
            string sb = sb_txt.Text;
            try
            {
                string zapros2 = string.Format("DELETE FROM shedule where id_doctor={0}", doctor);
                 OracleCommand command2 = new OracleCommand(zapros2, connectDB);
                 OracleDataReader reader2 = command2.ExecuteReader();

                string zapros1 = string.Format("insert into shedule values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    doctor, pn, vt, sr, cht, pt, sb);
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                
               
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            this.Close();
        }

        void vivod()
        {
            try
            {
                pn_txt.Text = "00000000";
                vt_txt.Text = "00000000";
                sr_txt.Text = "00000000";
                cht_txt.Text = "00000000";
                pt_txt.Text = "00000000";
                sb_txt.Text = "00000000";
                string zapros1 = string.Format("select id_doctor,mon,tue,wed,thur,fri,sat from shedule");
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    if (doc.ToString() == reader1["id_doctor"].ToString())
                    {
                        pn_txt.Text = reader1["mon"].ToString().Trim();
                        vt_txt.Text = reader1["tue"].ToString().Trim();
                        sr_txt.Text = reader1["wed"].ToString().Trim();
                        cht_txt.Text = reader1["thur"].ToString().Trim();
                        pt_txt.Text = reader1["fri"].ToString().Trim();
                        sb_txt.Text = reader1["sat"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void РасписаниеДоктора_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            if(pn_txt.Text== "00:00-00:00")
                t.SetToolTip(pn_txt, "У доктора выходной");
            if (vt_txt.Text == "00:00-00:00")
                t.SetToolTip(vt_txt, "У доктора выходной");
            if (sr_txt.Text == "00:00-00:00")
                t.SetToolTip(sr_txt, "У доктора выходной");
            if (cht_txt.Text == "00:00-00:00")
                t.SetToolTip(cht_txt, "У доктора выходной");
            if (pt_txt.Text == "00:00-00:00")
                t.SetToolTip(pt_txt, "У доктора выходной");
            if (sb_txt.Text == "00:00-00:00")
                t.SetToolTip(sb_txt, "У доктора выходной");
        }
    }
}
