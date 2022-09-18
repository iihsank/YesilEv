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
    public partial class Kullanicilar : Form
    {
        public Kullanicilar()
        {
            InitializeComponent();
        }

        private void Kullanicilar_Load(object sender, EventArgs e)
        {
            KullaniciGetir();
            button1.Enabled = false;
        }
        void KullaniciGetir()
        {
            listBox1.Items.Clear();
            KullaniciIslemDAL dal = new KullaniciIslemDAL();
            if (dal.KullanicilariGetir().Count > 0)
            {
                foreach (KullaniciGetirDTO item in dal.KullanicilariGetir())
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KullaniciGetirDTO kullanici = listBox1.SelectedItem as KullaniciGetirDTO;
            textBox1.Text = kullanici.Rolu;
            if (kullanici.Rolu == "Admin")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
        void Temizle()
        {
            textBox1.Clear();
            listBox1.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                KullaniciIslemDAL dal = new KullaniciIslemDAL();
                if(dal.SoftDelete(listBox1.SelectedItem as KullaniciGetirDTO))
                {
                    MessageBox.Show("Kullanici silme işlemi başarılı.");
                    KullaniciGetir();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Kullanici silme işlemi başarısız.Tekrar deneyiniz.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciRolIslemDAL dal = new KullaniciRolIslemDAL();
            if(dal.AdminYap(new AdminYapDTO() { KulId = (listBox1.SelectedItem as KullaniciGetirDTO).Id, RolId = (listBox1.SelectedItem as KullaniciGetirDTO).RolId })) 
            {
                MessageBox.Show("Kullanici rolü başarılı bir şekilde admin yapıldı.");
                KullaniciGetir();
                Temizle();
            }
            else
            {
                MessageBox.Show("Kullanici rolü değiştirilemedi lütfen tekrar deneyiniz.");
            }
        }
    }
}
