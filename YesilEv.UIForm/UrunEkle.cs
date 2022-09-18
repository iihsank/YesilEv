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
    public partial class UrunEkle : Form
    {  
        public UrunEkle()
        {
            InitializeComponent();
        }


        public UrunEkle(KullaniciGosterDTO kullanici):this()
        {
            _kullanici = kullanici;
        }
        private KullaniciGosterDTO _kullanici;
        private void UrunEkle_Load(object sender, EventArgs e)
        {
            SayfaDoldur();
            SayfaDoldur1();
            label1.Visible = false;
        }

        private void SayfaDoldur1()
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

        List<IcerikGetirDTO> icerikler = null;
        private void SayfaDoldur()
        {

            IcerikIslemDAL dal2 = new IcerikIslemDAL();
            icerikler = dal2.IcerikleriGetir();
            if (icerikler.Count > 0)
            {
                foreach (IcerikGetirDTO item in icerikler)
                {
                    listBox1.Items.Add(item);
                }
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                IcerikGetirDTO ıcerik = listBox1.SelectedItem as IcerikGetirDTO;
                listBox1.Items.Remove(listBox1.SelectedItem);
                listBox2.Items.Add(ıcerik);
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
            }
            else
            {
                errorProvider1.Clear();
                errorProvider1.SetError(listBox1, "Lütfen eklemek istediğiniz içeriği seçiniz.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                IcerikGetirDTO ıcerik = listBox2.SelectedItem as IcerikGetirDTO;
                listBox2.Items.Remove(listBox2.SelectedItem);
                listBox1.Items.Add(ıcerik);
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
            }
            else
            {
                errorProvider1.Clear();
                errorProvider1.SetError(listBox2, "Lütfen çıkarmak istediğiniz içeriği seçiniz.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            label1.Text= openFileDialog1.FileName;
            label10.Text = "Ön Yüz Resmi";
        }
        List<int> iceriklerId = null;
        private KullaniciGosterDTO kullanici;
        private int kullaniciId;

        private void button2_Click(object sender, EventArgs e)
        {
            if (KontolEt(textBox1,textBox2,comboBox1,comboBox2,listBox1,listBox2,label1,label9))
            {
                UrunIslemDAL dal = new UrunIslemDAL();
                iceriklerId = new List<int>();
                foreach (IcerikGetirDTO item in listBox2.Items)
                {
                    
                    iceriklerId.Add(item.Id);
                }

                string sonuc= dal.UrunEkle(new UrunEkleDTO() 
                { 
                    UrunAdi= textBox1.Text,
                    BarkodNo=textBox2.Text,
                    KategoriId=(comboBox1.SelectedItem as KategoriGetirDTO).Id,
                    UreticiId=(comboBox2.SelectedItem as UreticiGetirDTO).Id,
                    Resim=label1.Text,
                    Resim2=label9.Text,
                    KullaniciId= _kullanici.KullaniciId,
                    OnayliMi=_kullanici.Rol=="Admin"?true:false
                }, iceriklerId);
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Eklemeye çalıştığınız urun mevcut.");
                }
                else if (sonuc == "basarili")
                {
                    Temizle();
                    SayfaDoldur();

                    MessageBox.Show("Ürün ekleme işlemi başarıyla gerçekleşti");
                }
                else
                {
                    MessageBox.Show("Ürün ekleme işlemi başarısız tekrar deneyiniz.");
                }
            }
        }

        private bool KontolEt(TextBox textBox1, TextBox textBox2, ComboBox comboBox1, ComboBox comboBox2, ListBox listBox1, ListBox listBox2, Label label1, Label label9)
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
            if (listBox2.Items.Count < 1)
            {
                errorProvider1.SetError(listBox2, "İçerik seçmelisiniz");
                sayac++;
            }
            if (label1.Text == 1.ToString())
            {
                errorProvider1.SetError(button1, "Resim eklemelisiniz");
                sayac++;
            }
            if(label9.Text == 1.ToString())
            {
                errorProvider1.SetError(button5, "Resim eklemelisiniz");
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
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            label1.Text = 1.ToString();
            label9.Text = 1.ToString();
            label10.Text = string.Empty;
            label11.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog2.FileName;
            label9.Text = openFileDialog2.FileName;
            label11.Text = "Arka Yüz Resmi";
        }
    }
}
