using AdamAsmaca.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using AdamAsmaca.Models;

namespace AdamAsmaca
{
    public partial class Form1 : Form
    {
        AdamAsmacaService adamAsmacaService = new AdamAsmacaService();
        const bool test = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            YeniOyun();
        }

        private void YeniOyun()
        {
            lBilgi.Text = "";
            lGirilen.Text = "";
            tbHarf.Clear();
            bTahmin.Enabled = true;
            adamAsmacaService.KelimeleriOlustur();
            tbKelime.Text = adamAsmacaService.RasgeleKelimeGetir();
            lBilgi.Text = "Kalan hakkınız: " + adamAsmacaService.HakkiSifirla();
            lBilgi.ForeColor = Color.Green;
            if (test)
                MessageBox.Show(adamAsmacaService.RasgeleKelime);
        }

        private void yeniOyunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YeniOyun();
        }

        private void bTahmin_Click(object sender, EventArgs e)
        {
            lBilgi.Text = "";
            lBilgi.ForeColor = Color.Green;
            if (string.IsNullOrWhiteSpace(tbHarf.Text))
            {
                lBilgi.Text = "Lütfen harf giriniz...";
                lBilgi.ForeColor = Color.Red;
                return;
            }
            string guncelKelime = adamAsmacaService.TahminEt(tbHarf.Text.ToUpper(), lGirilen.Text.ToUpper());
            if (!lGirilen.Text.Contains(tbHarf.Text.ToUpper()))
                lGirilen.Text += tbHarf.Text.ToUpper() + "   ";
            tbKelime.Text = guncelKelime;
            tbHarf.SelectAll();
            tbHarf.Focus();
            OyunSonucu oyunSonucu = adamAsmacaService.TahminSonucu(guncelKelime);
            if (oyunSonucu == OyunSonucu.KelimeBulundu)
            {
                lBilgi.Text = "Tebrikler, kelimeyi buldunuz.";
                bTahmin.Enabled = false;
            }
            else if (oyunSonucu == OyunSonucu.HakkiBitti)
            {
                lBilgi.Text = "Üzgünüm, kelimeyi bulamadınız. Kelime: " + adamAsmacaService.RasgeleKelime;
                bTahmin.Enabled = false;
            }
            else
            {
                lBilgi.Text = "Kalan hakkınız: " + adamAsmacaService.Hak;
            }
        }
    }
}
