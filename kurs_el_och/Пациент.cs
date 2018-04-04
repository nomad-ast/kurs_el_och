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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace kurs_el_och
{
    public partial class Пациент : Form
    {
        int id_doc1,id_pac1;
        OracleConnection connectDB;
        public Пациент(string login1, string pass1)
        {
            InitializeComponent();
            
            string connectStr = "Data Source=orcl;" + " User ID = " + login1 + ";" + "Password=" + pass1 + ";";
            connectDB = new OracleConnection(connectStr);
            connectDB.Open();
            
            string zapros1 = string.Format("select id_pacient,login_p,FAM_P,NAME_P,OTCH_P from patient where login_p ='{0}'", login1);
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            OracleDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                id_pac1 = Convert.ToInt32(reader1.GetValue(0));
                label1.Text = "Пациент: " + reader1.GetValue(2).ToString().Trim()+" " + reader1.GetValue(3).ToString().Trim() + " " + reader1.GetValue(4).ToString().Trim();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)       //нажатие на кновку выход
        {
            Form p = Application.OpenForms[0];
            p.Show();
            this.Close();
        }

        private string zap(string time11)    //заполнение по 15 минут
        {
            string[] hourmin = time11.Split(new char[] { ':' });
            int hour = Convert.ToInt32(hourmin[0]);
            int min = Convert.ToInt32(hourmin[1]);
            if (min == 45)
            {
                hour++; min = 00;
            }
            else
                min = min + 15;

            DateTime date1 = new DateTime(1999, 9, 9, hour, min,0);
            return date1.ToShortTimeString();
        }

        bool docisbusy(string time1)
        {
            try
            {
                string zapros1 = string.Format("select id_doctor,to_char(date_vrem_pr,'dd.mm.yyyy HH24:MI') from registration where id_doctor='{0}' and to_char(date_vrem_pr,'dd.mm.yyyy HH24:MI')='{1} {2}'", id_doc1, date_rasp.Value.ToString("d"), Convert.ToDateTime(time1).ToString("HH:mm"));
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
                return false;
            }
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            rasp_doc_dgv.Rows.Clear();
            string day=date_rasp.Value.ToString("dddd");
            try
            {
                string zapros1 = string.Format("select mon понедельник,tue вторник,wed среда,thur четверг,fri пятница,sat суббота,id_doctor from shedule where id_doctor=(select id_doctor from doctor where fam_d='{0}')", select_doc.SelectedItem.ToString());
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                string thistime="";
                string prevvalue = "";

                while (reader1.Read())
                {
                    id_doc1 = Convert.ToInt32(reader1.GetValue(6));
                    thistime = reader1[day].ToString().Trim();
                }
                if (thistime == "00:00-00:00")
                {
                    MessageBox.Show("У доктора выходной");
                    return;
                }
                rasp_doc_dgv.ColumnCount = 3;
                rasp_doc_dgv.RowCount = 7;
                for (int i = 0; i < 7; i++)          //заполнение расписания по 15 минут
                {
                    for (int j = 0; j < 3; j++)
                    {
                       string[] startend = thistime.Split(new char[] { '-' });
                       
                       if (i == 0 && j == 0)
                       { rasp_doc_dgv[j, i].Value = startend[0]; prevvalue = startend[0]; if (docisbusy(rasp_doc_dgv[j, i].Value.ToString()))
                                rasp_doc_dgv[j, i].Style.BackColor = Color.Red; continue; }

                       rasp_doc_dgv[j, i].Value = zap(prevvalue);
                       if (docisbusy(rasp_doc_dgv[j, i].Value.ToString()))          //покарсить ячейку если доктолр занят
                            rasp_doc_dgv[j, i].Style.BackColor= Color.Red;
                       prevvalue = rasp_doc_dgv[j, i].Value.ToString();
                       if (Convert.ToDateTime(prevvalue) == Convert.ToDateTime(startend[1]))
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
     }

        private void select_doc_Click(object sender, EventArgs e)
        {
            select_doc.Items.Clear();
            try
            {
                string zapros1 = "select id_doctor, fam_d, substr(name_d,1,1), substr(otch_d,1,1 ) from doctor";
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    select_doc.Items.Add(Convert.ToString(reader1.GetValue(1)).Trim());
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }

        }    // выбор доктора из раскрывающшего списка

        private void записатьсяКВрачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaseFont baseFont = BaseFont.CreateFont(@"C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            string name_file = string.Format("Талон_к_{0}.pdf", myRec[1, myRec.CurrentCell.RowIndex].Value.ToString().Trim());
            FileStream fs = new FileStream(name_file, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            string zapros0 = string.Format("select id_reg,fam_p,name_p,otch_p,num_policy,address_pol,name_pol,spec_d,fam_d,name_d,otch_d, to_char(date_vrem_pr,'dd.mm.yyyy, HH24:MI') from registration r,doctor d,patient p,policlinic pol where d.id_policlinic=pol.id_policlinic  and id_reg={0} and fam_d='{1}' and p.id_pacient='{2}'", myRec[0, myRec.CurrentCell.RowIndex].Value.ToString().Trim(), myRec[1, myRec.CurrentCell.RowIndex].Value.ToString().Trim(),id_pac1);
            OracleCommand command0 = new OracleCommand(zapros0, connectDB);
            OracleDataReader reader0 = command0.ExecuteReader();
            doc.Open();
            while (reader0.Read())
            {

                
                PdfContentByte cb = writer.DirectContent;
                cb.Rectangle(10f, 10f, doc.PageSize.Width - 20f, doc.PageSize.Height - 20f);
                cb.BeginText();
                cb.SetFontAndSize(baseFont, 30);
                cb.SetTextMatrix(120, 800);
                cb.ShowText("Талон на прием к врачу");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 750);
                cb.ShowText("Номер талона: "+ reader0.GetValue(0).ToString().Trim() + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 700);
                cb.ShowText("ФИО Пациента: "+ reader0.GetValue(1).ToString().Trim()+" " + reader0.GetValue(2).ToString().Trim() + " " + reader0.GetValue(3).ToString().Trim() + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 650);
                cb.ShowText("№ полиса:"+ reader0.GetValue(4).ToString().Trim() + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 600);
                cb.ShowText("Название поликлинники:"+ reader0.GetValue(6).ToString().Trim()  + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 550);
                cb.ShowText("Адрес поликлинники:"  + reader0.GetValue(5).ToString().Trim() + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 500);
                cb.ShowText("Врач:" + reader0.GetValue(7).ToString().Trim() + "      " + reader0.GetValue(8).ToString().Trim()+" "+reader0.GetValue(9).ToString().Trim() + " " + reader0.GetValue(10).ToString().Trim() + "\n");
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(baseFont, 19);
                cb.SetTextMatrix(40, 450);
                cb.ShowText("Дата и время приема:" + reader0.GetValue(11).ToString().Trim() + "\n");
                cb.EndText();

                cb.Stroke();
                
                
            }
            doc.Close();
            System.Diagnostics.Process.Start(name_file);
            MessageBox.Show("Талон сохранен");
        }

        private void моиЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            try
            {
                myRec.Rows.Clear();
                myRec.ColumnCount = 5;
                //myRec.RowCount = 7;
                myRec.Columns[0].HeaderText = "ID";
                myRec.Columns[1].HeaderText = "Фамилия доктора";
                myRec.Columns[2].HeaderText = "Специальность";
                myRec.Columns[3].HeaderText = "Дата, время";
                myRec.Columns[4].HeaderText = "Назначение";

                string zapros0 = string.Format("select id_reg,fam_d, spec_d,to_char(date_vrem_pr,'dd.mm.yyyy, HH24:MI'),servece from registration r,doctor d where d.id_doctor=r.id_doctor and id_pacient='{0}'", id_pac1);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                OracleDataReader reader0 = command0.ExecuteReader();
                while (reader0.Read())
                {
                    myRec.Rows.Add(reader0.GetValue(0).ToString(), reader0.GetValue(1), reader0.GetValue(2), reader0.GetValue(3),  System.Text.RegularExpressions.Regex.Replace(reader0.GetValue(4).ToString(), @"\s+", " "));
                }
            }
            catch (Exception ex)
            {
                ex.GetHashCode(); 
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            myRec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            myRec.AutoResizeColumns();

            myRec.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            myRec.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string zapros0 = string.Format("delete from registration where id_reg='{0}'", myRec[0, myRec.CurrentCell.RowIndex].Value.ToString().Trim(),id_pac1);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                command0.ExecuteReader();
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
            моиЗаписиToolStripMenuItem_Click(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string zapros0 = string.Format("select id_pacient,id_doctor,to_char(date_vrem_pr,'dd.mm.yyyy HH24:MI') from registration where id_pacient='{0}' and id_doctor='{1}' and date_vrem_pr>=sysdate", id_pac1, id_doc1);
                OracleCommand command0 = new OracleCommand(zapros0, connectDB);
                OracleDataReader reader0 = command0.ExecuteReader();
                while (reader0.Read())
                {
                    if (reader0.HasRows)
                    {
                        MessageBox.Show("Вы уже записаны к врачу на "+ reader0.GetValue(2), "Ошибка!");
                        return;
                    }
                }

                string zapros1 = string.Format("insert into registration values (seq_reg.nextval,{0},{1},TO_DATE('{2} {3}', 'dd.mm.YYYY HH24:MI'),'н','-')",
                    id_pac1, id_doc1, date_rasp.Value.ToShortDateString(), rasp_doc_dgv.CurrentCell.Value.ToString());
                OracleCommand command1 = new OracleCommand(zapros1, connectDB);
                command1.ExecuteNonQuery();

                MessageBox.Show("Вы успешно записались к врачу");
                comboBox1_SelectionChangeCommitted( sender,  e);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }
    }
}
