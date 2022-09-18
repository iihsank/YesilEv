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
    public partial class Icerikler : Form
    {
        public Icerikler()
        {
            InitializeComponent();
        }

        private void Icerikler_Load(object sender, EventArgs e)
        {
            IcerikGetir();
        }
        List<IcerikGetirDTO> ıcerikler = null;
        private void IcerikGetir()
        {
            listView1.Items.Clear();
            IcerikIslemDAL dal = new IcerikIslemDAL();
            ıcerikler= dal.IcerikleriGetir();
            if (ıcerikler.Count > 0)
            {
                int sayac = 1;
                foreach (IcerikGetirDTO item in ıcerikler)
                {
                    ListViewItem lstItem = new ListViewItem();
                    lstItem.Text = sayac.ToString();
                    lstItem.SubItems.Add(item.Id.ToString());
                    lstItem.SubItems.Add(item.Ad);
                    lstItem.SubItems.Add(item.Aciklama);
                    lstItem.SubItems.Add(item.RiskSeviyesi.ToString());
                    listView1.Items.Add(lstItem);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IcerikGetir(textBox1.Text);
        }

        private void IcerikGetir(string ara)
        {
            listView1.Items.Clear();
            int sayac = 1;
            foreach (IcerikGetirDTO item in ıcerikler)
            {
                if (item.Ad.ToLower().Contains(ara.ToLower()))
                {
                    ListViewItem lstItem = new ListViewItem();
                    lstItem.Text = sayac.ToString();
                    lstItem.SubItems.Add(item.Id.ToString());
                    lstItem.SubItems.Add(item.Ad);
                    lstItem.SubItems.Add(item.Aciklama);
                    listView1.Items.Add(lstItem);
                }
            }
        }
    }
}
