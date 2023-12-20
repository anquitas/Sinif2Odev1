using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sinif2_odev1
{
    internal class arac
    {
        //## alanlar  ----------  ----------
        // alanlar private erişilebilirlik seviyesinde, sadece sınıf içerisinde erişilip değiştirilebilir
        private string tip;  
        private string marka;
        private string model;
        private int modelYili;
        private string saseNumarasi;
        private int kacinciEl;
        private string oncekiSahibi;
        // erişim seviyesi özellikle belirtilmeyen sınıf üyeleri de private erişim düzeyindedir
        DateTime edinmeTarihi;  
        int edinmeFiyati;

        // sahibi bulunmayan serbest araçların tutulduğu statik liste alanı
        public static List<arac> serbestAraclarListesi = new List<arac>();  



        //## oluşturucular  ----------  ----------
        public arac(string tip, string marka, string model, int modelYili, string saseNumarasi, int kacinciEl, string oncekiSahibi)
        {  // aracın kendisine ait bilgileri nesnenin oluşumu sırasında gerekli alanlara atar
            this.tip = tip;
            this.marka = marka;
            this.model = model;
            this.modelYili = modelYili;
            this.saseNumarasi = saseNumarasi;  // 17 haneli araca özel kimlik kodu

            this.kacinciEl = kacinciEl;
            this.oncekiSahibi = oncekiSahibi;  // varsa aracın önceki sahibi
        }  // arac oluşturucusu sonu



        //## metodlar  ----------  -----------
        public void edinim(DateTime edinmeTarihi, int edinmeFiyati)
        {  // aracın kişiye aktarımında edinilmesine ait bilgileri gerekli nesne alanlarına atar

            this.edinmeTarihi = edinmeTarihi;
            this.edinmeFiyati = edinmeFiyati;
        }  // edinim() metodu sonu

        
        public void aracBilgiYazdir()  // araç bilgilerine sınıf dışından erişip yazdırmak için
        {
            //## araç bilgileri
            Console.WriteLine($"tip: {this.tip}");
            Console.WriteLine($"marka: {this.marka}");
            Console.WriteLine($"model: {this.model}");
            Console.WriteLine($"model yili: {this.modelYili}");
            saseNumarasiYazdir();

            //## edinim bilgileri
            Console.WriteLine($"kaçıncı sahip: {this.kacinciEl}");
            Console.WriteLine($"önceki sahip: {this.oncekiSahibi}");
            Console.WriteLine($"edinme tarihi: {this.edinmeTarihi}");
            Console.WriteLine($"edinme fiyatı: {this.edinmeFiyati}");
            Console.WriteLine("________________________");
        }  // aracBilgiYazdir() metodu sonu


        public string saseNumarasiGizli()  // sase numarasını gizli formata çevirir, değeri string olarak dönderir
        {
            //saseNumarasi string formatla, gizle
            //WAUZZZF49HA036784
            //0-1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16 //17 eleman

            //string p1 = saseNumarasi.Substring(0,3); // string .Substring(int startIndex, int length)
            string p2 = saseNumarasi.Substring(3, 1);
            //string p3 = saseNumarasi.Substring(4,11);
            string p4 = saseNumarasi.Substring(15); // string .Substring(int startIndex)

            return "**" + p2 + "***********" + p4;
        }  // seseNumarasiGizli() metodu sonu


        public void saseNumarasiYazdir()  // gizlenmiş şase numarasını yazdırmak için kullanılır
        {
            Console.WriteLine(saseNumarasiGizli());
        }  // saseNumarasiYazdir() metodu sonu


        public static void araclariListele ( )  // serbest araç listesindeki araçları iterasyon ile listeler
        {
            int num = 1;
            foreach (arac iterasyonAraci in arac.serbestAraclarListesi)
            {
                Console.WriteLine($"{num} - {iterasyonAraci.tip} {iterasyonAraci.model} {iterasyonAraci.modelYili} {iterasyonAraci.saseNumarasiGizli()}");
                num++;
            }
        }

        public void aracSahipSayisiGuncelle ()
        {  // aktarımdan sonra sahip sayısını arttırmak için
            kacinciEl++;  
        }
    }  // arac sınıfı sonu  
      
}  // sinif2_odev1 namespace sonu


//lazım olursa dursun???
//parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
