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
using YesilEv.DTO.OdevDTO;

namespace YesilEv.UIForm
{
    public partial class Odev : Form
    {
        public Odev()
        {
            InitializeComponent();
        }

        private void Odev_Load(object sender, EventArgs e)
        {
            UrunGetir();
        }

        private void UrunGetir()
        {
           OdevDAL dal = new OdevDAL();
            foreach (UrunGetirDTO item in dal.UrunleriGetir())
            {
                comboBox1.Items.Add(item);
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrunGetirDTO urun = comboBox1.SelectedItem as UrunGetirDTO;
            OdevDAL dal = new OdevDAL();
            textBox1.Text= dal.MaddeSayisi(urun.UrunId).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdevDAL dal = new OdevDAL();
            List<UrunGetirDTO> urunler =dal.UrunGetir(textBox2.Text);
            if (urunler.Count > 0)
            {
                foreach (UrunGetirDTO item in urunler)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OdevDAL dal = new OdevDAL();
            textBox3.Text= dal.Onay().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OdevDAL dal = new OdevDAL();
            foreach (UrunGetirDTO item in dal.EnRiskliUrunler())
            {
                listBox2.Items.Add(item);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OdevDAL dal = new OdevDAL();
            //dal.FavoriListIlkUc();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OdevDAL dal = new OdevDAL();
            Dictionary<string, int> dic = dal.KullaniciAdminSayisi();
            foreach (var deger in dic)
            {
                label4.Text += deger;

            }
        }
    }
}
