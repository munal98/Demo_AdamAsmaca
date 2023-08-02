using System;
using AdamAsmaca.Models;

namespace AdamAsmaca.Services
{
    public class AdamAsmacaService
    {
        string[] kelimeler;
        Random rasgele = new Random();
        string sonucKelime;
        
        const string gizliKarakter = "*";

        public byte Hak { get; set; }
        public string RasgeleKelime { get; set; }

        public void KelimeleriOlustur()
        {
            kelimeler = new string[]
            {
                "ARABA", "CAM", "TREN", "UÇAK", "KEDİ", "KÖPEK", "AT", "KALORİFER"
            };
        }

        public string RasgeleKelimeGetir()
        {
            int index = rasgele.Next(0, kelimeler.Length);
            RasgeleKelime = kelimeler[index];
            sonucKelime = "";
            foreach (char harf in RasgeleKelime)
            {
                sonucKelime += gizliKarakter;
            }
            return sonucKelime;
        }

        public byte HakkiSifirla()
        {
            Hak = 5;
            return Hak;
        }

        public string TahminEt(string harf, string girilen)
        {
            string sonuc = "";
            bool bulundu = false;
            for (int i = 0; i < RasgeleKelime.Length; i++)
            {
                if (RasgeleKelime[i].ToString() == harf)
                {
                    sonuc += harf;
                    bulundu = true;
                }
                else
                {
                    if (RasgeleKelime[i].ToString() == sonucKelime[i].ToString())
                        sonuc += sonucKelime[i];
                    else
                        sonuc += gizliKarakter;
                }
            }
            sonucKelime = sonuc;
            if (!bulundu && !girilen.Contains(harf))
                Hak--;
            return sonuc;
        }

        public OyunSonucu TahminSonucu(string guncelKelime)
        {
            OyunSonucu sonuc;
            if (!guncelKelime.Contains("*"))
            {
                sonuc = OyunSonucu.KelimeBulundu;
            }
            else
            {
                if (Hak == 0)
                {
                    sonuc = OyunSonucu.HakkiBitti;
                }
                else
                {
                    sonuc = OyunSonucu.DevamEdiyor;
                }
            }
            return sonuc;
        }
    }
}
