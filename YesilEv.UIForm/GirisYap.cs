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
    public partial class GirisYap : Form
    {
        public GirisYap()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KayitOl frm = new KayitOl();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2))
            {
                KullaniciIslemDAL dal = new KullaniciIslemDAL();
                KullaniciGosterDTO kullanici = dal.KullaniciGiris(new KullaniciGirisDTO()
                {
                    KullaniciAdi = textBox1.Text,
                    Parola = textBox2.Text
                });
                if (kullanici != null)
                {
                    if (kullanici.Rol == "Admin")
                    {
                        Temizle();
                        Admin frm = new Admin(kullanici);
                        frm.Show();
                    }
                    else if (kullanici.Rol == "Kullanici")
                    {
                        Temizle();
                        Kullanici frm = new Kullanici(kullanici);
                        frm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış");
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
                errorProvider1.SetError(textBox1, "Boş geçilemez.");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Boş geçilemez.");
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
