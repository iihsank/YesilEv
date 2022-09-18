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
    public partial class KulKara : Form
    {
        private KullaniciGosterDTO kullanici;

        public KulKara()
        {
            InitializeComponent();
        }

        public KulKara(KullaniciGosterDTO kullanici):this()
        {
            this.kullanici = kullanici;
        }

        private void KulKara_Load(object sender, EventArgs e)
        {
            UrunlerGetir();
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

        private void UrunlerGetir()
        {
            listBox1.Items.Clear();
            KullaniciIslemDAL dal = new KullaniciIslemDAL();
            List<UrunGetirDTO> liste = dal.KaraListeGetir(this.kullanici.KullaniciId);
            if (liste.Count > 0)
            {
                foreach (UrunGetirDTO i in liste)
                {
                    listBox1.Items.Add(i);
                }
                listBox1.SelectedIndex = 0;
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                KullaniciIslemDAL dal = new KullaniciIslemDAL();
                if(dal.ListeCikar(this.kullanici.KullaniciId, (listBox1.SelectedItem as UrunGetirDTO).Id))
                {
                    MessageBox.Show("Başarıyla çıkarıldı.");
                    label5.Text = string.Empty;
                    pictureBox1.ImageLocation = null;
                    pictureBox2.ImageLocation = null;
                    UrunlerGetir();
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu tekrar deneyiniz.");
                }

            }

        }
    }
}
