using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YesilEv.Dal.Concrete;
using YesilEv.DTO;

namespace YesilEv.UIForm
{
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }

        private void Urunler_Load(object sender, EventArgs e)
        {

            UrunleriGetir();
        }

        private void UrunleriGetir()
        {
            listBox1.Items.Clear();
            UrunIslemDAL dal = new UrunIslemDAL();
            List<UrunOnayDTO> urunler= dal.OnayBekleyenUrunleriGetir();
            if (urunler.Count > 0)
            {
                foreach (UrunOnayDTO item in urunler)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                UrunIslemDAL dal = new UrunIslemDAL();
                if(dal.UrunOnayla((listBox1.SelectedItem as UrunOnayDTO).Id))
                {
                    MessageBox.Show("Urun onaylama işlemi başarılı.");
                    UrunleriGetir();
                }
                else
                {
                    MessageBox.Show("Ürün onaylanırken bir hata oluştu lütfen tekrar deneyiniz.");
                }
                
            }
            else
            {
                errorProvider1.SetError(listBox1, "Onaylamak istediğiniz ürünü seçiniz.");
            }
        }
    }
}
