using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication4
{
    public partial class Form2 : Form
    {
        public Form1 frm1;
        public Form6 frm6;

        public static string conk = "Server=MAYNEYMIZMD\\MDSERVER ;Database=GGDB; Integrated Security=True";
       public SqlConnection baglanti = new SqlConnection(conk);

        public Form2()
        {
          

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
         

            baglanti.Open();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            string kayit = "insert into kisi(ad,parola) values (@ad,@soyad)";
            
            komut.Parameters.AddWithValue("@ad", textBox1.Text.ToString());
            komut.Parameters.AddWithValue("@soyad",textBox2.Text.ToString());
            komut.CommandText = kayit;

            komut.CommandText = kayit;
            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("Kayıt Gerçekleştirildi", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            kayit = "insert into Butce (KisiID) values('" + profil2()+"') ";

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand();
            komut2.Connection = baglanti;
            komut2.CommandText = kayit;
            komut2.ExecuteNonQuery();

            komut2.Dispose();
            baglanti.Close();




            this.Close();
        }


        public string profil2()
        {

            


            baglanti.Open();
            // kayıt yapılırken olusan kisi ıd almak amacı ıle yazıldı.. 
            string kayitt = "Select *from Kisi where Ad= '" + textBox1.Text.ToString() + "' and  Parola='" + textBox2.Text.ToString() + "' ";

            SqlCommand command = new SqlCommand(kayitt, baglanti);


            SqlDataReader reader = command.ExecuteReader();


            string KisiId = "";

            while (reader.Read())
            {

                KisiId = reader["KisiID"].ToString();


            }

            command.Dispose();
            baglanti.Close();
            reader.Close();


            return KisiId;

        }

    }
}
