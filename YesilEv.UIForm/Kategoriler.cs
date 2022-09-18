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
    public partial class Kategoriler : Form
    {
        public Kategoriler()
        {
            InitializeComponent();
        }

        private void Kategoriler_Load(object sender, EventArgs e)
        {
            KategoriGetir();
           
        }
        List<KategoriGetirDTO> kategoriler = null;
        private void KategoriGetir()
        {
            listView1.Items.Clear();
            KategoriIslemDAL dal = new KategoriIslemDAL();
            kategoriler = dal.KategorileriGetir();
            if (kategoriler.Count > 0)
            {
                int sayac = 1;
                foreach (KategoriGetirDTO item in kategoriler)
                {
                    ListViewItem lstitem = new ListViewItem();
                    lstitem.Text = sayac.ToString();
                    lstitem.SubItems.Add(item.Id.ToString());
                    lstitem.SubItems.Add(item.Adı);
                    lstitem.SubItems.Add(item.Aciklama);
                    listView1.Items.Add(lstitem);
                    sayac++;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            KategoriGetir(textBox1.Text);
            
        }

        private void KategoriGetir(string ara)
        {
            listView1.Items.Clear();
            int sayac = 1;
            foreach (KategoriGetirDTO item in kategoriler)
            {
                if (item.Adı.ToLower().Contains(ara.ToLower()))
                {
                    ListViewItem lstitem = new ListViewItem();
                    lstitem.Text = sayac.ToString();
                    lstitem.SubItems.Add(item.Id.ToString());
                    lstitem.SubItems.Add(item.Adı);
                    lstitem.SubItems.Add(item.Aciklama);
                    listView1.Items.Add(lstitem);
                    sayac++;
                }
            }
        }
    }
}
