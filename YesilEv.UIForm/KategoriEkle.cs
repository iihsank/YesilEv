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
using YesilEv.DTO;

namespace YesilEv.UIForm
{
    public partial class KategoriEkle : Form
    {
        public KategoriEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2))
            {
                KategoriIslemDAL dal = new KategoriIslemDAL();
                string sonuc = dal.KategoriEkle(new KategoriEkleDTO()
                {
                    Ad = textBox1.Text,
                    Aciklama = textBox2.Text
                });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Eklemeye çalıştığınız kategori isminde başka bir kategori mevcut.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Kategori ekleme işlemi başarılı.");
                    Temizle();
                }
                else if (sonuc == "basarisiz")
                {
                    MessageBox.Show("Eklemeye işlemi başarısız lütfen bilgilerin doğruluğunu kontrol ediniz.");
                }
            }
           
        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private bool KontrolEt(TextBox textBox1, TextBox textBox2)
        {
            errorProvider1.Clear();
            int sayac = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Ad alanı boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Açıklama alanı boş geçilemez.");
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
    }
}
