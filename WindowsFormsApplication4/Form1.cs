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
    public partial class Form1 : Form
    {
       
        public Form2 frm2;
        public Form3 frm3;
        public Form4 frm4;
        public Form6 frm6;




        public static string conString = "Server=MAYNEYMIZMD\\MDSERVER ;Database=GGDB; Integrated Security=True";

        public SqlConnection baglanti = new SqlConnection(conString);
        public SqlCommand komut = new SqlCommand();
        public DataTable tablo = new DataTable();
        public DataTable tabloGider = new DataTable();
        public DataTable tabloGelir = new DataTable();

        public SqlDataAdapter adtr;

        public string profil()
        {


            string conString = "Server=MAYNEYMIZMD\\MDSERVER;Database=GGDB; Integrated Security=True";
            
              SqlConnection baglanti2 = new SqlConnection(conString);

              baglanti2.Open();

             string kayitt = "Select *from Kisi where Ad= '" + frm6.textBox1.Text.ToString() + "'";

             SqlCommand command = new SqlCommand(kayitt, baglanti2);
            

             SqlDataReader reader = command.ExecuteReader();
            

            string KisiId = "";

            while (reader.Read())
            {

                KisiId = reader["KisiID"].ToString();


            }

            command.Dispose();
            baglanti2.Close();
            reader.Close();
          

            return KisiId;

        }
            

        public void TabloGider()
        {
            dataGridView2.Refresh();
            tabloGider.Clear();
           
            
            
            string kayit = "Select ID,Harcama_Yeri,Fiyat,Aciklama,Tarih from Gider where KisiID='" + profil() + "' ";
            komut.CommandType = CommandType.Text;
            komut.CommandText = kayit;
            komut.Connection = baglanti;
           SqlDataAdapter da = new SqlDataAdapter(komut);

            da.Fill(tabloGider);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView2.DataSource = tabloGider;
            //Formumuzdaki DataGridViewin ver
         
            baglanti.Close();
        }
        public void TabloGelir()
        {
            dataGridView1.Refresh();
            tabloGelir.Clear();
            baglanti.Open();


            string kayit = "Select  GelirID,Tutar,Aciklama,Tarih from Gelir where KisiID='" + profil() + "' ";

            komut.CommandText = kayit;
            komut.Connection = baglanti;
            SqlDataAdapter da = new SqlDataAdapter(komut);
            

            da.Fill(tabloGelir);
            da.Dispose();
            
            dataGridView1.DataSource = tabloGelir;
       
            
            baglanti.Close();
        }

        public Form1()
        {

            InitializeComponent();

           

            frm2 = new Form2();
            frm3 = new Form3();
            frm4 = new Form4();
            frm6 = new Form6();
           



            
            
            frm6.frm1 = this;
            frm6.ShowDialog();
            //   frm6.Hide();
            label12.Text = frm6.textBox1.Text.ToString()+frm6.textBox2.Text.ToString();
            label1.Text = DateTime.Now.ToLongDateString();
        
            this.Refresh();
            
            frm2.frm1 = this;
            frm3.frm1 = this;
            frm4.frm1 = this;

            

        }


      



        private void button1_Click(object sender, EventArgs e)
        {
          
            
                //   frm2.ShowDialog();
                dataGridView3.Refresh();
                dataGridView3.Visible = true;
                DataTable butce = new DataTable();
                baglanti.Open();
            // string kayit = "insert into Gider(KisiID,Harcama_Yeri,Fiyat,Aciklama) values ('"+profil()+ "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')";
            //string kayit = "Select *from Butce where KisiID='"+ profil()+"'  ";
            string kayit = "spButce";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@kisiid",profil());
            komut.CommandText = kayit;
            komut.ExecuteNonQuery();
            komut.Parameters.Clear();
            string kayit2 = "Select ToplamHarcama,ToplamGelir,Net from Butce where KisiID='"+ profil()+"'  ";
            komut.CommandType = CommandType.Text;
            komut.CommandText = kayit2;
            komut.ExecuteNonQuery();


            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            SqlDataAdapter da = new SqlDataAdapter(komut);


                da.Fill(butce);
                da.Dispose();
                //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
                dataGridView3.DataSource = butce;
                komut.Parameters.Clear();
                komut.Dispose();
                baglanti.Close();
             


        }



        private void button3_Click(object sender, EventArgs e)
        {

            frm3.ShowDialog();
            



        }

       

       

        private void button4_Click(object sender, EventArgs e)
        {
            frm4.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            dataGridView2.Refresh();
            TabloGelir();
           

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            groupBox1.Visible = true;
  


        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            string kayit = "Update Gider Set Harcama_Yeri = '" + TxtHarcamaYeri.Text.ToString() + "', Fiyat='"
                + txtFiyatGider.Text.ToString() + "',Aciklama='" 
                + txtAciklamaGider.Text.ToString() + "' WHERE ID=" +dataGridView2.CurrentRow.Cells[0].Value ;


            //komut.Parameters.AddWithValue("@KisiID", textBox1.Text.ToString());
            //komut.Parameters.AddWithValue("@HarcamaYeri", textBox2.Text.ToString());
            //komut.Parameters.AddWithValue("@Fiyat", textBox3.Text.ToString());
            //komut.Parameters.AddWithValue("@Aciklama", textBox4.Text.ToString());
            //komut.Connection =baglanti;
            //komut.Parameters.AddWithValue("@id", dataGridView2.CurrentRow.Cells[0].Value);


            komut.CommandText = kayit;
            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            komut.Dispose();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();
            TabloGider();
          
            TxtHarcamaYeri.Clear();
            txtFiyatGider.Clear();
            txtAciklamaGider.Clear();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.Refresh();
            TabloGider();
            groupBox1.Visible = true;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         
            txtTutarGelir.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAciklamaGelir.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
          
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            TxtHarcamaYeri.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtFiyatGider.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtAciklamaGider.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            baglanti.Open();

            string kayit = "Update Gelir Set Tutar='"
                + txtTutarGelir.Text.ToString() 
                + "', Aciklama='" + txtAciklamaGelir.Text.ToString() 
               
                +  "' WHERE GelirID=" + dataGridView1.CurrentRow.Cells[0].Value;
           
            //komut.Parameters.AddWithValue("@KisiID", textBox1.Text.ToString());

            //komut.Parameters.AddWithValue("@Tutar", textBox3.Text.ToString());
            //komut.Parameters.AddWithValue("@Aciklama", textBox4.Text.ToString());
            //komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            komut.Connection = baglanti;

            
            komut.CommandText = kayit;
            
            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            komut.Dispose();
            
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();

            TabloGelir();
            
            txtAciklamaGelir.Clear();
            txtTutarGelir.Clear();


        }

        private void button2_Click_1(object sender, EventArgs e) // cıkıs butonu
        {
            this.Close();
            Application.Exit();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kayıtlarinizi Silmek İstiyormusunuz?", "Gelir ve Gider Kayıtlariniz Silinecek?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            baglanti.Open();
           
            string kayit = "spKayitSil";
            komut.Connection = baglanti;
           
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@KisiID", profil());
            komut.CommandText = kayit;
            komut.ExecuteNonQuery();
            komut.Parameters.Clear();

            baglanti.Close();
            
        }

        private void button9_Click_1(object sender, EventArgs e) // HESAP SİL 
        {
            MessageBox.Show("Hesabınızı Silmek İstiyormusunuz?", "Devam Ederseniz Hesabınız Silinecek?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            komut.Connection = baglanti;
            baglanti.Open();
            string kayit = "delete from Kisi where KisiID='" + profil() + "' ";
           

            //komut.CommandType = CommandType.StoredProcedure;
            //komut.Parameters.AddWithValue("@KisiID", profil());
            komut.CommandText = kayit;
            komut.ExecuteNonQuery();
            komut.Dispose();
            // komut.Parameters.Clear();
            MessageBox.Show("Hesap Silindi", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            baglanti.Close();
            this.Visible = false;
            frm6.ShowDialog();
            
            
            this.Refresh();
            label12.Refresh();
            this.Visible = true;
            
            
        }

        }
    }

