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
    public partial class KulUrunler : Form
    {
        public KulUrunler()
        {
            InitializeComponent();
        }

        public KulUrunler(KullaniciGosterDTO kullanici) : this()
        {
            _kullanici = kullanici;
        }

        private void KulUrunler_Load(object sender, EventArgs e)
        {
            UrunGetir();
            //Gizle();
        }

        //private void Gizle()
        //{
        //    groupBox1.Visible = false;
        //    label6.Visible = false;
        //    label7.Visible = false;
        //    label8.Visible = false;
        //    label9.Visible = false;
        //    label10.Visible = false;
        //}
        //private void GizlilikAc()
        //{
        //    groupBox1.Visible = true;
        //    label6.Visible = true;
        //    label7.Visible = true;
        //    label8.Visible = true;
        //    label9.Visible = true;
        //    label10.Visible = true;
        //}

        List<UrunGetirDTO> urunler = null;
        private KullaniciGosterDTO _kullanici;

        private void UrunGetir()
        {
            UrunIslemDAL dal = new UrunIslemDAL();
            urunler = dal.UrunleriGetir();
            if (urunler.Count > 0)
            {
                foreach (UrunGetirDTO item in urunler)
                {
                    listBox1.Items.Add(item);
                }
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            groupBox1.Controls.Clear();
            UrunGetirDTO urun = listBox1.SelectedItem as UrunGetirDTO;
            if (urun.Ad != null)
            {
                //GizlilikAc();
                int top = 20;
                foreach (IcerikGeDTO item in urun.Icerikler)
                {
                    if (item.Risk == 1)
                    {

                        Label lbl = new Label();
                        lbl.ForeColor = Color.Green;
                        lbl.Text = item.Ad;
                        lbl.Left = 10;
                        lbl.Top = top;
                        groupBox1.Controls.Add(lbl);

                    }
                    else if (item.Risk == 2)
                    {
                        Label lbl = new Label();
                        lbl.ForeColor = Color.Purple;
                        lbl.Text = item.Ad;
                        lbl.Left = 10;
                        lbl.Top = top;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (item.Risk == 3)
                    {
                        Label lbl = new Label();
                        lbl.ForeColor = Color.Blue;
                        lbl.Text = item.Ad;
                        lbl.Left = 10;
                        lbl.Top = top;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (item.Risk == 4)
                    {
                        Label lbl = new Label();
                        lbl.ForeColor = Color.Orange;
                        lbl.Text = item.Ad;
                        lbl.Left = 10;
                        lbl.Top = top;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (item.Risk == 5)
                    {
                        Label lbl = new Label();
                        lbl.ForeColor = Color.Red;
                        lbl.Text = item.Ad;
                        lbl.Left = 10;
                        lbl.Top = top;
                        groupBox1.Controls.Add(lbl);
                    }
                    top += 20;
                }
                label5.Text = urun.Ad;
                pictureBox1.ImageLocation = urun.Resim;
                pictureBox2.ImageLocation = urun.Resim2;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Ara(textBox2.Text);
            //Gizle();
            //label5.Text = string.Empty;
            //pictureBox1.ImageLocation = null;
        }

        private void Ara(string ara)
        {
            listBox1.Items.Clear();
            foreach (UrunGetirDTO item in urunler)
            {
                if (item.BarkodNo.ToLower().Contains(ara.ToLower()))
                {
                    listBox1.Items.Add(item);
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AraUrunAdi(textBox1.Text);
            //Gizle();
            //label5.Text = string.Empty;
            //pictureBox1.ImageLocation = null;
        }

        private void AraUrunAdi(string text)
        {
            listBox1.Items.Clear();
            foreach (UrunGetirDTO item in urunler)
            {
                if (item.Ad.ToLower().Contains(text.ToLower()))
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                FavKaraListeIslemDAL dal = new FavKaraListeIslemDAL();
                string sonuc= dal.KaraListEkle(_kullanici.KullaniciId, (listBox1.SelectedItem as UrunGetirDTO).Id);
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Ürün zaten listenizde");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Başarıyla listenize eklendi.");
                
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                FavKaraListeIslemDAL dal = new FavKaraListeIslemDAL();
                string sonuc = dal.FavListEkle(_kullanici.KullaniciId, (listBox1.SelectedItem as UrunGetirDTO).Id);
                if (sonuc == "mevcut")
                {
                    MessageBox.Show("Ürün zaten listenizde.");
                }
                else if (sonuc == "basarili")
                {
                    MessageBox.Show("Başarıyla listenize eklendi.");
                }
            }
        }
    }
}
