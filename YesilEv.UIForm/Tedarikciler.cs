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
    public partial class Tedarikciler : Form
    {
        public Tedarikciler()
        {
            InitializeComponent();
        }

        private void Tedarikciler_Load(object sender, EventArgs e)
        {
            UreticiEkle();
        }
        List<UreticiGetirDTO> ureticiler = null;
        private void UreticiEkle()
        {
            listView1.Items.Clear();
            UreticiIslemDAL dal = new UreticiIslemDAL();
            ureticiler = dal.UreticileriGetir();
            if (ureticiler != null)
            {
                int sayac = 1;
                foreach (var item in ureticiler)
                {
                    ListViewItem lstItem = new ListViewItem();
                    lstItem.Text = sayac.ToString();
                    lstItem.SubItems.Add(item.Id.ToString());
                    lstItem.SubItems.Add(item.Ad);
                    lstItem.SubItems.Add(item.Adres);
                    listView1.Items.Add(lstItem);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UreticiEkle(textBox1.Text);
        }

        private void UreticiEkle(string ara)
        {
            listView1.Items.Clear();
            int sayac = 1;
            foreach (UreticiGetirDTO item in ureticiler)
            {
                if (item.Ad.ToLower().Contains(ara.ToLower()))
                {
                    ListViewItem lstItem = new ListViewItem();
                    lstItem.Text = sayac.ToString();
                    lstItem.SubItems.Add(item.Id.ToString());
                    lstItem.SubItems.Add(item.Ad);
                    lstItem.SubItems.Add(item.Adres);
                    listView1.Items.Add(lstItem);
                }
            }
        }
    }
}
