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
    public partial class Admin : Form
    {
        private KullaniciGosterDTO _kullanici;
        public Admin()
        {
            InitializeComponent();
        }

        public Admin(KullaniciGosterDTO kullanici):this()
        {
            _kullanici = kullanici;
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

        private void kategoriEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kategoriler frm = new Kategoriler();
            FormGetir(frm);
        }

        private void kategoriGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KategoriEkle frm = new KategoriEkle();
            FormGetir(frm);
        }

        private void kategoriGuncelleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            KategoriGuncelle frm = new KategoriGuncelle();
            FormGetir(frm);
        }

        private void tedarikcilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tedarikciler frm = new Tedarikciler();
            FormGetir(frm);
        }

        private void tedarikciEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TedarikciEkle frm = new TedarikciEkle();
            FormGetir(frm);
        }

        private void tedarikciGuncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TedarikciDuzenle frm = new TedarikciDuzenle();
            FormGetir(frm);
        }

        private void urunlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Urunler frm = new Urunler();
            FormGetir(frm);
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

        private void urunDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunDuzenle frm = new UrunDuzenle();
            FormGetir(frm);
        }

        private void ıceriklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Icerikler frm = new Icerikler();
            FormGetir(frm);
        }

        private void ıcerikEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IcerikEkle frm = new IcerikEkle();
            FormGetir(frm);
        }

        private void ıcerikDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IcerikDuzenle frm = new IcerikDuzenle();
            FormGetir(frm);
        }

        private void kullanicilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kullanicilar frm = new Kullanicilar();
            FormGetir(frm);
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

        private void cikisYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
