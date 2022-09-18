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
    public partial class TedarikciDuzenle : Form
    {
        public TedarikciDuzenle()
        {
            InitializeComponent();
        }

        private void TedarikciDuzenle_Load(object sender, EventArgs e)
        {
            UreticilerGetir();
        }

        private void UreticilerGetir()
        {
            listBox1.Items.Clear();
            UreticiIslemDAL dal = new UreticiIslemDAL();
            List<UreticiGetirDTO> ureticiler= dal.UreticileriGetir();
            if (ureticiler.Count > 0)
            {
                foreach (UreticiGetirDTO item in ureticiler)
                {
                    listBox1.Items.Add(item);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (KontrolEt(listBox1, textBox1, textBox2))
            {
                UreticiIslemDAL dal = new UreticiIslemDAL();
                string sonuc= dal.UreticiGuncelle(new UreticiGuncelleDTO() { Id = (listBox1.SelectedItem as UreticiGetirDTO).Id,Ad=textBox1.Text,Adres=textBox2.Text });
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Güncellemeye çalıştığınız üretici adı mevcuttur.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Güncelleme işlemi başarıyla gerçekleşmiştir.");
                    UreticilerGetir();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Güncelleme işlemi başarısız. Lütfen bilgileri kontrol edip tekrar deneyiniz.");
                }
            }
        }

        private void Temizle()
        {
            listBox1.SelectedIndex = -1;
            textBox1.Clear();
            textBox2.Clear();
        }

        private bool KontrolEt(ListBox listBox1, TextBox textBox1, TextBox textBox2)
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
                sayac++;
            }
            if (listBox1.SelectedIndex < 0)
            {
                errorProvider1.SetError(listBox1, "Güncellemek için uretici seçiniz.");
                sayac++;
            }
            if (sayac > 0)
            {
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                UreticiIslemDAL dal = new UreticiIslemDAL();
                if(dal.UreticiSoftDelete(listBox1.SelectedItem as UreticiGetirDTO))
                {
                    MessageBox.Show("Üretici başarıyla silinmiştir.");
                    UreticilerGetir();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Üretici silme işlemi başarısız tekrar deneyiniz");
                }
            }
            else
            {
                errorProvider1.SetError(listBox1, "Silmek için üretici seçiniz.");
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UreticiGetirDTO uretici = listBox1.SelectedItem as UreticiGetirDTO;
            textBox1.Text = uretici.Ad;
            textBox2.Text = uretici.Adres;
        }
    }
}
