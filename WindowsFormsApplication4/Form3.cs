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
    public partial class Form3 : Form
    {
        public Form1 frm1;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm1.baglanti.Open();
           // string kayit = "insert into Gider(KisiID,Harcama_Yeri,Fiyat,Aciklama) values ('"+frm1.profil()+ "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')";
            
            frm1.komut.Connection = frm1.baglanti;
            frm1.komut.CommandText = "spGiderEkle";
            frm1.komut.CommandType = CommandType.StoredProcedure;
            frm1.komut.Parameters.AddWithValue("@KisiID",  Convert.ToInt32(frm1.profil())  );
            frm1.komut.Parameters.AddWithValue("@Harcama",textBox2.Text.ToString() );
            frm1.komut.Parameters.AddWithValue("@Fiyat", textBox3.Text.ToString());
            frm1.komut.Parameters.AddWithValue("@aciklama",textBox4.Text.ToString());
            //  frm1.komut.CommandText = kayit;
            
        
            frm1.komut.ExecuteNonQuery();
            frm1.komut.Parameters.Clear();
            frm1.komut.Dispose();
            frm1.baglanti.Close();
            MessageBox.Show("Kayıt Gerçekleştirildi", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            frm1.TabloGider();

          
            this.Close();
        }
    }
}
