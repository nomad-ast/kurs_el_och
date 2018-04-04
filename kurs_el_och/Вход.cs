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
    public partial class Вход : Form
    {
        OracleConnection connectDB;
        string login = "";
        АдминПанель a;
        Пациент p;
        Доктор d;
        public Вход()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
               
            }

            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = login_txt.Text;
            string pass = pass_txt.Text;
            bool isadmin=false;
            bool isdoctor = false;
            bool ispacient = false;
            try
            {
                string connectStr = "Data Source=orcl;" + " User ID = "+ login + ";" + "Password=" + pass + ";";  
                connectDB = new OracleConnection(connectStr);
                connectDB.Open();                                                                                       //connect к бд

                string zapros11 = "DELETE FROM REGISTRATION WHERE DATE_VREM_PR<sysdate-10";    //удаление из регистрации записей старше 10 дней
                OracleCommand command11 = new OracleCommand(zapros11, connectDB);
                command11.ExecuteReader();

                string zapros = string.Format("select * from session_roles");
                OracleCommand command1 = new OracleCommand(zapros, connectDB);
                OracleDataReader reader1 = command1.ExecuteReader(); bool pac_r = false; bool doc_r= false;
               
                while (reader1.Read())
                {
                    if (reader1.GetString(0) == "PATIENT_ROLE")
                    {
                        pac_r = true;
                    }

                    if (reader1.GetString(0) == "DOCTOR_ROLE")
                    {
                        doc_r = true;
                    }

                }


                string zapros_admin = string.Format("select user from dual");           //запишем запрос в переменную 
                OracleCommand command2 = new OracleCommand(zapros_admin, connectDB);    //выолним запрос
                OracleDataReader reader2 = command2.ExecuteReader();                    //запишем результат запроса в перемменную
                while (reader2.Read())
                {
                    if (reader2.GetString(0) == "ADMIN_SYS")                            //Если вход под админом
                    {
                        isadmin = true;
                    }

                }


                if (isadmin)
                {
                    login = login_txt.Text;
                    a = new АдминПанель(login,pass); this.Hide();
                    a.ShowDialog();
                }

                if(pac_r &&!isadmin&&!doc_r)
                {
                    login = login_txt.Text;
                    p = new Пациент(login, pass); this.Hide();
                    p.ShowDialog();
                }

                if(!pac_r && !isadmin && doc_r)
                {
                    login = login_txt.Text;
                    d = new Доктор(login, pass); this.Hide();
                    d.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                ex.GetHashCode();
                MessageBox.Show("Ошибка!" + ex.Message, "Ошибка!");
            }
        }

        private void Вход_VisibleChanged(object sender, EventArgs e)
        {
            login_txt.Text = null;
            pass_txt.Text = null;
        }
    }
}
