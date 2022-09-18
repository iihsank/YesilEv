using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YesilEv.DTO;

namespace YesilEv.UIForm
{
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent();
        }
        private KullaniciGosterDTO _kullanici;
        public Kullanici(KullaniciGosterDTO kullanici):this()
        {
            _kullanici = kullanici;
        }

        private void cikisYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void favListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
            {
                frm2.Close();
                KulFavoriler frm = new KulFavoriler(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                KulFavoriler frm = new KulFavoriler(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }

        private void urunlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
            {
                frm2.Close();
                KulUrunler frm = new KulUrunler(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                KulUrunler frm = new KulUrunler(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }
        Form frm2 = null;
        private KullaniciGosterDTO kullanici;

        void FormGetir(Form frm)
        {
            if (frm2 != null)
            {
                frm2.Close();
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }

        private void urunEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
            {
                frm2.Close();
                UrunEkle frm = new UrunEkle(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                UrunEkle frm = new UrunEkle(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }

        private void karaListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
            {
                frm2.Close();
                KulKara frm = new KulKara(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                KulKara frm = new KulKara(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }

        private void profilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm2 != null)
            {
                frm2.Close();
                Profil frm = new Profil(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm.Top = 0;
                frm.Left = 0;
                frm2 = frm;
            }
            else
            {
                Profil frm = new Profil(_kullanici);
                frm.MdiParent = this;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Show();
                frm2 = frm;
            }
        }
    }
}
