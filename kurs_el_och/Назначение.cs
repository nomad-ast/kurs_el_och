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
    public partial class Назначение : Form
    {
        int idreg=-1;
        OracleConnection connectDB;
        public Назначение(int a, OracleConnection condb)
        {
            idreg = a;
            InitializeComponent();
            connectDB = condb;
        }

        private void Назначение_Load(object sender, EventArgs e)
        {
            string zapros0 = string.Format("select OTM_O_POSESH,SERVECE from registration where ID_REG='{0}'", idreg);
            OracleCommand command0 = new OracleCommand(zapros0, connectDB);
            OracleDataReader reader0 = command0.ExecuteReader();
            while (reader0.Read())
            {
                if (reader0.GetValue(0).ToString().Trim() == "н")
                { comboBox1.Text = "Не был"; Naznach_txt.Enabled = false; }
                else
                { comboBox1.Text = "Был"; Naznach_txt.Enabled = true; }

                Naznach_txt.Text = System.Text.RegularExpressions.Regex.Replace(reader0.GetValue(1).ToString(), @"\s+", " ");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pos = "";
            string pn = Naznach_txt.Text;
            if(comboBox1.Text=="Не был")
                pos = "н";
            else
                pos = "д";
            string zapros1 = string.Format("update registration set otm_o_posesh='{0}',servece='{1}' where id_reg='{2}'", pos, pn, idreg);
            OracleCommand command1 = new OracleCommand(zapros1, connectDB);
            command1.ExecuteReader();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Не был")
            {  Naznach_txt.Enabled = false; Naznach_txt.Text = "-"; }
            else
            {  Naznach_txt.Enabled = true; }
        }
    }
}
