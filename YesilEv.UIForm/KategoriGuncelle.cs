using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YesilEv.Dal;
using YesilEv.Dal.Concrete;
using YesilEv.DTO;

namespace YesilEv.UIForm
{
    public partial class KategoriGuncelle : Form
    {
        public KategoriGuncelle()
        {
            InitializeComponent();
        }

        private void KategoriGuncelle_Load(object sender, EventArgs e)
        {
            kategorilerEkle();

        }

        private void kategorilerEkle()
        {
            listBox1.Items.Clear();

            KategoriIslemDAL dal = new KategoriIslemDAL();
            if (dal.KategorileriGetir().Count > 0)
            {
                foreach (KategoriGetirDTO item in dal.KategorileriGetir())
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KategoriGetirDTO kategori = listBox1.SelectedItem as KategoriGetirDTO;
            textBox1.Text = kategori.Adı;
            textBox2.Text = kategori.Aciklama;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2, listBox1)) 
            {
                KategoriIslemDAL dal = new KategoriIslemDAL();
                string sonuc = dal.KategoriDüzenle(new KategoriGuncelleDTO() { Id = (listBox1.SelectedItem as KategoriGetirDTO).Id, Adı = textBox1.Text, Aciklama = textBox2.Text });
                if(sonuc=="mevcut")
                {
                    MessageBox.Show("Eklemeye çalıştığınız kategori adı mevcuttur.");
                }
                else if(sonuc=="basarili")
                {
                    MessageBox.Show("Kategori güncelleme işlemi başarılı");
                    kategorilerEkle();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Kategori güncelleme işlemi başarısız lütfen bilgileri kontrol edip tekrar deneyiniz.");
                }
            }    
            
        }

        private bool KontrolEt(TextBox textBox1, TextBox textBox2, ListBox listBox1)
        {
            errorProvider1.Clear();
            int sayac = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Boş geçilemez.");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Boş geçilemez");
                sayac++;
            }
            if (listBox1.SelectedIndex <0)
            {
                errorProvider1.SetError(listBox1, "Lütfen değiştirmek istediğiniz kategoriyi seçiniz.");
                sayac++;
            }
            if (sayac > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                KategoriIslemDAL dal = new KategoriIslemDAL();
                if (dal.SoftDelete(listBox1.SelectedItem as KategoriGetirDTO))
                {
                    MessageBox.Show("Kategori başarıyla silindi.");
                    kategorilerEkle();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Silmek için kategori seçiniz.");
                }
                
            }

        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            listBox1.SelectedIndex = -1;
        }
    }
}
