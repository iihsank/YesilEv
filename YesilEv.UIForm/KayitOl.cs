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
    public partial class KayitOl : Form
    {
        public KayitOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontolEt(textBox1, textBox2, textBox3, textBox4))
            {
                KullaniciIslemDAL dal = new KullaniciIslemDAL();
                string sonuc = dal.KullaniciEkle(new KullaniciEkleDTO() { Adi = textBox1.Text, Soyadi = textBox2.Text, kullaniciAdi = textBox3.Text, Password = textBox4.Text });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Kullanıcı adı mevcut lütfen başka kullanıcı adı seçiniz.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Kullanici kaydı başarıyla oluşturuldu.");
                    Temizle();
                    this.Close();
                }
                else if (sonuc == "basarisiz")
                {
                    MessageBox.Show("Kullanici eklerken hata oluştu lütfen bilgilerin doğruluğunu kontrol edip tekrar deneyiniz.");
                }
            }
           

        }

        private bool KontolEt(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
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
                errorProvider1.SetError(textBox2, "Soyad alanı boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Kullanıcı adı alanı boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Parola alanı boş geçilemez");
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

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
