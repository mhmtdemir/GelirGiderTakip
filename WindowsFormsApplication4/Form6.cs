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
    public partial class Form6 : Form
    {
        public Form2 frm2;

        public Form1 frm1;
        public static string conString = "Server=MAYNEYMIZMD\\MDSERVER ;Database=GGDB; Integrated Security=True";
      
         SqlConnection baglanti = new SqlConnection(conString);



        public Form6()
        {
            
            InitializeComponent();

            this.Refresh();
            frm2 = new Form2();






            frm2.frm6 = this;


        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           baglanti.Open();
            

            string kAdi = textBox1.Text.ToString();

            string kayit = "select Ad,Parola from kisi where Ad= '" + kAdi + "'";

            SqlCommand command = new SqlCommand(kayit,baglanti);
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
              
                if(textBox2.Text.ToString() == read["Parola"].ToString())
                {

              
                    this.Hide();
                }

            }
            

            baglanti.Close();
            command.Dispose();
            read.Close();

           


        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();

            
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
           {

        }
    }
}
