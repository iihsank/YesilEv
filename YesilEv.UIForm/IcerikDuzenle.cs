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
    public partial class IcerikDuzenle : Form
    {
        public IcerikDuzenle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2, listBox1))
            {
                IcerikIslemDAL dal = new IcerikIslemDAL();
                string sonuc = dal.IcerikGuncelle(new IcerikGuncelleDTO() { Id = (listBox1.SelectedItem as IcerikGetirDTO).Id, Adı = textBox1.Text, Aciklama = textBox2.Text,RiskSeviyesi=(int)comboBox1.SelectedItem });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Eklemeye çalıştığınız içerik adı mevcuttur.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("İçerik güncelleme işlemi başarılı");
                    IcerikEkle();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("İçerik güncelleme işlemi başarısız lütfen bilgileri kontrol edip tekrar deneyiniz.");
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
            if (listBox1.SelectedIndex < 0)
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
        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            listBox1.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
        }

        private void IcerikDuzenle_Load(object sender, EventArgs e)
        {
            IcerikEkle();
            RiskEkle();
        }

        private void RiskEkle()
        {
            for (int i = 1; i <= 5; i++)
            {
                comboBox1.Items.Add(i);
            }
        }

        private void IcerikEkle()
        {
            listBox1.Items.Clear();

            IcerikIslemDAL dal = new IcerikIslemDAL();
            if (dal.IcerikleriGetir().Count > 0)
            {
                foreach (IcerikGetirDTO item in dal.IcerikleriGetir())
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                IcerikIslemDAL dal = new IcerikIslemDAL();
                if (dal.IcerikSoftDelete(listBox1.SelectedItem as IcerikGetirDTO))
                {
                    MessageBox.Show("Kategori başarıyla silindi.");
                    IcerikEkle();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Silmek için kategori seçiniz.");
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IcerikGetirDTO ıcerik = listBox1.SelectedItem as IcerikGetirDTO;
            textBox1.Text = ıcerik.Ad;
            textBox2.Text = ıcerik.Aciklama;
            comboBox1.SelectedIndex = ıcerik.RiskSeviyesi -1;
        }
    }
}
