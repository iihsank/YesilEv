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
    public partial class TedarikciEkle : Form
    {
        public TedarikciEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(textBox1, textBox2))
            {
                UreticiIslemDAL dal = new UreticiIslemDAL();
                string sonuc= dal.UreticiEkle(new UreticiEkleDTO()
                {
                    Ad = textBox1.Text,
                    Adres = textBox2.Text
                });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Bu isimde bir üretici zaten mevcut");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Uretici ekleme işlemi başarıyla gerçekleştirildi.");
                    Temizle();
                }
                else if (sonuc == "basarisiz")
                {
                    MessageBox.Show("Üretici eklerken bir hata oluştu lütfen bilgileri kontrol edip tekrar deneyiniz.");
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
                errorProvider1.SetError(textBox1, "Boş geçilemez");
                sayac++;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Boş geçilemez.");
                sayac++;
            }
            if (sayac > 0)
            {
                return false;
            }
            return true;
        }
    }
}
