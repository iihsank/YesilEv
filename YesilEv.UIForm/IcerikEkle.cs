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
    public partial class IcerikEkle : Form
    {
        public IcerikEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2,comboBox1))
            {
                IcerikIslemDAL dal = new IcerikIslemDAL();
                string sonuc = dal.IcerikEkle(new IcerikEkleDTO()
                {
                    Ad = textBox1.Text,
                    Aciklama = textBox2.Text,
                    RiskSeviyesi = (int)comboBox1.SelectedItem
                });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Eklemeye çalıştığınız içerik isminde başka bir içerik mevcut.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("İçerik ekleme işlemi başarılı.");
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
            comboBox1.SelectedIndex = -1;
        }

        private bool KontrolEt(TextBox textBox1, TextBox textBox2, ComboBox comboBox1)
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
            if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Risk Seviyesi seçiniz.");
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

        private void IcerikEkle_Load(object sender, EventArgs e)
        {
            RiskEkle();
        }

        private void RiskEkle()
        {
            for (int i = 1; i <= 5; i++)
            {
                comboBox1.Items.Add(i);
            }
        }
    }
}
