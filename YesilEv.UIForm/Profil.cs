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
    public partial class Profil : Form
    {
        private KullaniciGosterDTO _kullanici;

        public Profil()
        {
            InitializeComponent();
        }

        public Profil(KullaniciGosterDTO kullanici):this()
        {

            _kullanici = kullanici;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Kontrol(textBox1, textBox2, textBox3, textBox4))
            {
                KullaniciIslemDAL dal = new KullaniciIslemDAL();
                string sonuc= dal.KullaniciGuncelle(new KullaniciGuncelleDTO()
                {
                    Ad = textBox1.Text,
                    Soyad = textBox2.Text,
                    KullaniciAdi = textBox3.Text,
                    Parola = textBox4.Text,
                    Id = _kullanici.KullaniciId
                });
                if (sonuc=="mevcut")
                {
                    MessageBox.Show("Kullanici adı mevcut");
                }
                else if(sonuc == "basarili")
                {
                    MessageBox.Show("Kullanici güncelleme işlemi başarılı.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı güncelleme işlemi başarısız lütfen bilgilerin doğruluğunu kontrol edip tekrar deneyiniz.");
                }
            }

        }

        private bool Kontrol(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
        {
            errorProvider1.Clear();
            int sayac = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Boş geçilemez");
                sayac++;
            }
            if (sayac > 0)
            {
                return false;
            }
            return true;
        }

        void KullaniciGetir()
        {
            KullaniciIslemDAL dal = new KullaniciIslemDAL();
            KullaniciGuncelleDTO kullanici= dal.KullaniciGetir(_kullanici.KullaniciId);
            textBox1.Text = kullanici.Ad;
            textBox2.Text = kullanici.Soyad;
            textBox3.Text = kullanici.KullaniciAdi;
            textBox4.Text = kullanici.Parola;
        }

        private void Profil_Load(object sender, EventArgs e)
        {
            KullaniciGetir();
        }
    }
}
