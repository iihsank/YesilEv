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
    public partial class UrunDuzenle : Form
    {
        public UrunDuzenle()
        {
            InitializeComponent();
        }

        private void UrunDuzenle_Load(object sender, EventArgs e)
        {
            UrunGetir();
            UrunGetir1();
        }

        private void UrunGetir1()
        {
            KategoriIslemDAL dal = new KategoriIslemDAL();
            List<KategoriGetirDTO> kategoriler = dal.KategorileriGetir();
            if (kategoriler.Count > 0)
            {
                foreach (KategoriGetirDTO item in kategoriler)
                {
                    comboBox1.Items.Add(item);
                }
            }
            UreticiIslemDAL dal1 = new UreticiIslemDAL();
            List<UreticiGetirDTO> ureticiler = dal1.UreticileriGetir();
            if (ureticiler.Count > 0)
            {
                foreach (UreticiGetirDTO item in ureticiler)
                {
                    comboBox2.Items.Add(item);
                }
            }
        }

        private void UrunGetir()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            UrunIslemDAL dal = new UrunIslemDAL();
            List<UrunGetirDTO> urunler= dal.UrunleriGetir();
            if (urunler.Count > 0)
            {
                foreach (UrunGetirDTO item in urunler)
                {
                    listBox2.Items.Add(item);
                }
            }
           
            IcerikIslemDAL dal1 = new IcerikIslemDAL();
            List<IcerikGeDTO> icerikler= dal1.IceriklerGetir();
            if (icerikler.Count > 0)
            {
                foreach (IcerikGeDTO item in icerikler)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            UrunGetirDTO urun = listBox2.SelectedItem as UrunGetirDTO;
            textBox1.Text = urun.Ad;
            textBox2.Text = urun.BarkodNo;
            int sayac = 0;
            int sayac1 = 0;
            int sonuc = -1;
            int sonuc1 = -1;
            foreach (KategoriGetirDTO item in comboBox1.Items)
            {
                if (item.Id == urun.KategoriId)
                {
                    sonuc = sayac;
                }
                sayac++;
            }
            comboBox1.SelectedIndex = sonuc;

            foreach (UreticiGetirDTO item in comboBox2.Items)
            {
                if (item.Id == urun.UreticiId)
                {
                    sonuc1 = sayac1;
                }
                sayac1++;
            }
            comboBox2.SelectedIndex = sonuc1;

            pictureBox1.ImageLocation = urun.Resim;
            pictureBox2.ImageLocation = urun.Resim2;
            label1.Text = urun.Resim;
            label10.Text = urun.Resim2;
            label11.Visible = true;
            label12.Visible = true;
            foreach (IcerikGeDTO item in urun.Icerikler)
            {
                listBox3.Items.Add(item);
            }   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (listBox2.SelectedIndex != -1)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    int sayac = 0;
                    foreach (IcerikGeDTO item in listBox3.Items)
                    {
                        if (item.Id == (listBox1.SelectedItem as IcerikGeDTO).Id)
                        {
                            sayac++;
                        }
                    }
                    if (sayac == 0)
                    {
                        listBox3.Items.Add(listBox1.SelectedItem);
                    }
                    else
                    {
                        errorProvider1.SetError(listBox3, "Eklemek istediğiniz içerik zaten mevcut");
                    }
                }
                else
                {
                    errorProvider1.SetError(listBox1, "Eklemek istediğiniz içeriği seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Önce ürün seçiniz.");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (listBox2.SelectedIndex != -1)
            {
                if (listBox3.SelectedIndex != -1)
                {
                    listBox3.Items.Remove(listBox3.SelectedItem);
                }
                else
                {
                    errorProvider1.SetError(listBox3, "Lütfen çıkarmak istediğiniz içeriği seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Önce ürün seçiniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            label1.Text = openFileDialog1.FileName;
        }
        List<int> iceriklerId = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (KontolEt(textBox1,  textBox2,  comboBox1,  comboBox2,  listBox3,  label1, label10))
            {
                iceriklerId = new List<int>();
                UrunIslemDAL dal = new UrunIslemDAL();
                foreach (IcerikGeDTO item in listBox3.Items)
                {
                    iceriklerId.Add(item.Id);
                }
                string sonuc= dal.UrunGuncelle(new UrunGuncelleDTO
                {
                    Id = (listBox2.SelectedItem as UrunGetirDTO).Id,
                    UrunAdi = textBox1.Text,
                    BarkodNo = textBox2.Text,
                    KategoriId = (comboBox1.SelectedItem as KategoriGetirDTO).Id,
                    UreticiId = (comboBox2.SelectedItem as UreticiGetirDTO).Id,
                    Resim = label1.Text,
                    Resim2=label10.Text
                }, iceriklerId);
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Urun adı mevcut lütfen değiştiriniz.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Ürün güncelleme işlemi başarılı");
                    Temizle();
                    UrunGetir();
                }
                else
                {
                    MessageBox.Show("Ürün eklenirken bir hata oluştu lütfen tekrar deneyiniz.");
                }
            }
        }
        private bool KontolEt(TextBox textBox1, TextBox textBox2, ComboBox comboBox1, ComboBox comboBox2, ListBox listBox3, Label label1, Label label10)
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
            if (comboBox1.Items.Count < 1)
            {
                errorProvider1.SetError(comboBox1, "Önce kategori ekleyiniz.");
                sayac++;
            }
            else
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBox1, "Kategori seçmelisiniz.");
                    sayac++;
                }
            }
            if (comboBox2.Items.Count < 1)
            {
                errorProvider1.SetError(comboBox2, "Önce üretici ekleyiniz.");
                sayac++;
            }
            else
            {
                if (comboBox2.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBox2, "Üretici seçmelisiniz.");
                    sayac++;
                }
            }
            if (listBox3.Items.Count < 1)
            {
                errorProvider1.SetError(listBox2, "İçerik seçmelisiniz");
                sayac++;
            }
            if (label1.Text == 1.ToString())
            {
                errorProvider1.SetError(button1, "Resim eklemelisiniz");
                sayac++;
            }
            if (label10.Text == 1.ToString() || string.IsNullOrWhiteSpace(label10.Text))
            {
                errorProvider1.SetError(button6, "Resim eklemelisiniz");
                sayac++;
            }
            if (sayac > 0)
            {
                return false;
            }
            return true;
        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            pictureBox1.Image = null;
            label1.Text = 1.ToString();
            pictureBox2.Image = null;
            label10.Text = 1.ToString();
            label11.Visible = false;
            label12.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {

                UrunIslemDAL dal = new UrunIslemDAL();
                if(dal.softDelete((listBox2.SelectedItem as UrunGetirDTO).Id))
                {
                    MessageBox.Show("Ürün başarıyla silindi");
                    Temizle();
                    UrunGetir();
                }
                else
                {
                    MessageBox.Show("Ürün silinirken bir hata oluştu lütfen tekrar deneyiniz.");
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz ürünü seçiniz.");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog2.FileName;
            label10.Text = openFileDialog2.FileName;
            label12.Visible = true;
        }
    }
}
