using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sinif2_odev1
{
    internal class kisi
    {
        //## alanalar  ----------  ----------
        // ima edilen private erişim seviyesinde alanlar
        static int kisiSayaci = 0;  // eklenen kişiye özgün ID yaratmak için kullanılan statik değişken
        int kisiID;
        string isim;
        string soyad;
        int TCno;
        int dogumYili;

        //public erişim seviyesinde alanlar
        public int arabaSayisi;
        public static List<kisi> kisiListesi = new List<kisi>();  // sisteme eklenen kişilerin tutulduğu statik kişi listesi
        public List<arac> araclar = new List<arac>();  // bir kişi nesnesine ait araçların tutulduğu instance arac listesi



        //## oluşturucular  ----------  ----------
        public kisi(string isim, string soyad, int TCno, int dogumYili)  // kişiye ait tüm bilgiler nesne oluşumu sırasında alınır
        {  // private erişim seviyesine sahip alanlara sınıf içinde erişim sağlandı
            kisi.kisiSayaci++;  // kişi sayacını her eklenen kişi için arttır
            this.kisiID = kisiSayaci;  // kişiye özel ID eklenmesi
            this.isim = isim;
            this.soyad = soyad;
            this.TCno = TCno;
            this.dogumYili = dogumYili;

        }  // kisi sınıfı oluşturucusu sonu



        //## metodlar  ----------  ----------
        public void aracEkle(arac eklenecekArac)  // kişinin araç listesine araç eklemek için kullanılan metod
        {
            araclar.Add(eklenecekArac);  // kişiye ait araçlar listesine yeni araç ekler
        }  // aracEkle() metodu sonu


        public void aracSat(int aracIndex)
        {
            araclar.RemoveAt(aracIndex);
        }  // aracSat() metodu sonu


        public void araclarimiListele()
        {  // kişi araçlarını bilgileri ile birlikte konsola yazdırır
            //Console.WriteLine($"{this.isim} {this.soyad}");
            if (araclar.Count > 0) { // eğer aracı varsa döngüyü başlatan şart
                foreach (arac iterasyonAraci in araclar)
                {
                    iterasyonAraci.aracBilgiYazdir();  //arac nesnesine ait metod ile aracın bilgilerini ekrana yazdırır
                }
            } else
            {
                Console.WriteLine("hic aracınız bulunmuyor");
            }
        }  // araclarimiListele() metodu sonu


        public void bilgi() {  // kişiye ait bilgileri konsola yazdırır // privaate bilgiye dışarıdan erişim sağlar
            // kişinin tüm bilgilerini yazdırır
            Console.WriteLine(this.isim);
            Console.WriteLine(this.soyad);
            Console.WriteLine(this.TCno);
            Console.WriteLine(this.dogumYili);
        }

        public static void listele() {  // kişi listesini konsola yazdırır  // private bilgiye dışarıdan erişim sağlar
            //kişinin kapak bilgilerini yazdırır
            foreach (kisi iterasyonKisisi in kisiListesi)
            {
                Console.WriteLine($"{iterasyonKisisi.kisiID} - {iterasyonKisisi.isim} {iterasyonKisisi.soyad}");  
            }
        }

    } // kisi sınıfı sonu 
}  // sinif2_odev1 namespace sonu



//parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });