using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form4 : Form
    {

        public Form1 frm1;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm1.baglanti.Open();
            string kayit = "insert into Gelir (KisiID,Tutar,Aciklama) values ('" +frm1.profil() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "')";

            frm1.komut.Connection = frm1.baglanti;
            frm1.komut.CommandText = kayit;
     
            frm1.komut.ExecuteNonQuery();
          
            
           
            frm1.komut.Dispose();
            frm1.baglanti.Close();
      
            textBox2.Clear();
            textBox3.Clear();

            frm1.TabloGelir();
            this.Close();
        }
    }
}
