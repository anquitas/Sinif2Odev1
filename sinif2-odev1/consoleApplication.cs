using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace sinif2_odev1
{
    internal static class consoleApplication  // ## consoleApplication sınıfı  ----------  ----------
    {
        //## alanalar  ----------  ----------
        static List<string> secenekler = new List<string>();


        //## metodlar  ----------  ----------
        static public bool anaMenu()  // konsolda gösterilecek ana menü ve diğer alt menülerin kontolünu sağlayan consoleApplication metodu
        {
            Console.Clear();  //her seferinde ana menünün yazdırılması için konsolu temizler
            int secenekNumarası = 1;  //varsayılan seçenek numarası, nullReferance hatası ile uğraşmamak için
            int secim;  // yapılan seçimin tutulduğu sayı değişkeni
            foreach (string alinanSecenek in secenekler)  //  ana menü seçeneklerini yazdıran döngü
            {
                Console.WriteLine(secenekNumarası + " " + alinanSecenek);
                secenekNumarası++;
            }

            secim = sayiOku(); // seçimin konsoldan alınması
            if (secim == 5) return false;  // çıkışı sağlayan ve consol menü döngüsünü durduran çıkış koşulu yapısı  // fonksiyonun programı sonlandıran çıkışı
            secenekAnalizi(secim); // yapılan çıkış olmayan seçimin statik consolApplication metodu ile kontrolü

            return true;  // fonksiyonun consol menü döngüsünü devam ettiren çıkışı
        }  // anaMenu() metodu sonu


        static public void anaMenuyeEkle(string secenek)
        {
            secenekler.Add(secenek);  // ana menüye seçenek ekleyen metod
        }  // anaMenuyeEkle() metodu sonu


        static public void anaMenuyuOlustur()
        {  // ana menüye eklenecek seçenekleri belirleye ve son olarak ekleyen metod //anaMenuyeEkle yardımcı statik metodunu kullanır
            anaMenuyeEkle("kisi ekle");  // sisteme kişi eklemek içim
            anaMenuyeEkle("arac ekle");  // sisteme araç eklemek için
            anaMenuyeEkle("kisi goruntule");   // kişi listesine erişmek ve kişi ayrıntılı bilgilerini görüntülemek için
            anaMenuyeEkle("arac aktarimi");  // sistemdeki serbest araçları kişilerin galerisine eklemek için
            anaMenuyeEkle("çıkış");  // programı sonlandırmak için
        }  // anaMenuyuOlustur() medotu sonu


        static public bool kullanicidanKisiAl ()  // sistem kullanıcısından kişi eklemek için gerekli kişi bilgilerini alır
        {  
            Console.WriteLine("##kişi bilgileri giriniz##");  //bilgilendirme başlığı
            //gerekli kullanıcı bilgilerinin alınıp temp değişkenlerde depolanması
            Console.WriteLine("isim: ");  
            string isim_temp = Console.ReadLine();
            Console.WriteLine("soyad: ");
            string soyad_temp = Console.ReadLine();
            Console.WriteLine("doğum yılı:  ");
            string dogumYili_temp = Console.ReadLine();
            int dogumYili_temp_int = int.Parse(dogumYili_temp);  //str to int dönüşüm
            Console.WriteLine("TC no: ");
            string TCno_temp = Console.ReadLine();
            int TCno_temp_int = int.Parse(TCno_temp);  //str to int dönüşüm

            //alınınan kişi bilgileri ile temp bir kişi nesnesi oluşturulur ve bir değişkende tutulur
            kisi kisi_temp = new kisi(isim_temp, soyad_temp, TCno_temp_int, dogumYili_temp_int);  
            kisi.kisiListesi.Add(kisi_temp);  // temp kişi kişi sınıfına ait statik kişiler listesine eklenir

            Console.WriteLine("kişi eklendi");  // durum bildirimi
            Console.WriteLine("başka kişi eklemek istermisiniz (E/h)");  // tekrar kişi eklenmek isteniyorsa durum recursive olarak tekrarlanır
            string cevap = Console.ReadLine();
            if (cevap == "E" || cevap == "e")
            {
                kullanicidanKisiAl();  // recursive tekrar
            }
            //Console.ReadKey();
            return true;


        }  // kisiAl() metodu sonu

        static public bool KullanicidanAracAl() {  
            // araç eklemesi sırasında sistem kullanıcısından araç bilgilerini aral consoleApplication statik metodu
            // edinilen bilgiler temp değişkenlerinde tutulur
            //araç genel bilgileri
            Console.WriteLine("##araç bilgileri giriniz##");
            Console.WriteLine("tipi: ");
            string tip_temp = Console.ReadLine();
            Console.WriteLine("marka: ");
            string marka_temp = Console.ReadLine();
            Console.WriteLine("model: ");
            string model_temp = Console.ReadLine();
            Console.WriteLine("model yılı: ");
            string modelYili_temp = Console.ReadLine();
            int modelYili_temp_int = int.Parse(modelYili_temp);  // str to int
            Console.WriteLine("sase numarası: ");
            string seseNumarasi_temp = Console.ReadLine();

            //araç geçmiş edinim bilgileri
            // public void edinim(int kacinciEl, string oncekiSahibi, DateTime edinmeTarihi, int edinmeFiyati)
            Console.WriteLine("kaçıncı el: ");
            string kacinciEl_temp = Console.ReadLine();
            int kacinciEl_temp_int = int.Parse(kacinciEl_temp);  // str to int
            Console.WriteLine("önceki sahibi: ");
            string oncekiSahibi_temp = Console.ReadLine();




            // temp değişkenler arac nesnesine ait public erişime ait oluşturucu ile yaratılıp geçici bir değişkende tutulur
            arac arac_temp = new arac(tip_temp, marka_temp, model_temp, modelYili_temp_int, seseNumarasi_temp,kacinciEl_temp_int, oncekiSahibi_temp);
            // geçici değişkendeki araç araç sınıfına ait statik serbest araçlar listesine eklenir
            arac.serbestAraclarListesi.Add(arac_temp);
            Console.WriteLine("araç eklendi");  // durum bildirimi yapılır
            Console.ReadKey();

            return true;

            // arac(string tip, string marka, string model, int modelYili, string saseNumarasi)

        }  // KullanicidanAracAl() metodu sonu

        public static bool kisileriListele () 
        {  // consol uygulaması için ekrana kişi listesi yazdırır
            Console.WriteLine("## kişiler");
            kisi.listele();  // kişi statik nesnesine kişiler statik metodu ile erişim sağlar
            // nesneye ait özellikler nesneye ait metod ile yazdırırlır
            //Console.ReadKey();  //yapım aşaması kontrolü için  // sakın açma potansiyel bug

            return true;
        }

        public static int sayiOku()
        {  // kullanıcıdan sayı alır // yapılan seçimi almak için kullanılıyor
            int girilenSayi = 0;
            girilenSayi = int.Parse(Console.ReadLine());
            return girilenSayi;
        }  // sayiOku() sınıfı sonu


        public static void secenekAnalizi (int yapilanSecim)
        {  // yapılan seçeneğin analizini yapar ve seçeneğe göre gereken fonksiyonları çağırır
            // menü navigasyonunu sağlayan en önemli yardımcı fonksiyon !!!!!!!!
            // seçim kontrolleri
            if (yapilanSecim == 1)  // 1- kişi eklse
            {
                Console.Clear();
                kullanicidanKisiAl();  // sistemde kullanıcıdan kişi bilgisi alan ve kişi nesnesini yaratıp uygun listeye ekleyen yardımcı fonksiyon
            } else if (yapilanSecim == 2) {  // 2- araç ekle
                Console.Clear(); 
                KullanicidanAracAl();  // sistemde kullanıcıdan arac bilgisi alan ve arac nesnesini oluşturup uygun listeye ekleyen yardımcı fonksiyon
            } else if (yapilanSecim == 3) {  // 3- kişi görüntüle
                Console.Clear();
                kisileriListele();  // seçim yapılmak üzere kişi listesini yazdırır
                Console.WriteLine("");
                kisiyiGoruntule(sayiOku());  // seçilen kişiye göre o kişinin bilgilerini gösteren yardımcı fonksiyon
            } else if (yapilanSecim == 4)  // 4- araç aktarımı
            {
                Console.Clear();
                aracAktarimi();  // araç aktarımını sağlayan yardımcı fonksiyon
            }


        }  // secenekAnalizi() metodu sonu

        public static void kisiyiGoruntule(int kisiID)
        {  // kişinin özel bilgilerini public metodlarla konsol ekranına yazdırır
            if (kisiID == 0) {
                Console.WriteLine("ölümcül bir hata gerçekleşti !!!");
            } else {

                Console.Clear();
                Console.WriteLine("## kişi bilgisi");
                kisi.kisiListesi[kisiID - 1].bilgi();  // seçilen seçim numarasını liste index sistemine uyumlar, //nesneye ait metod ile nesne bilgilerini yazdırır
                Console.WriteLine("araçları görüntülemek istermisiniz? (E/h)");
                string cevap = Console.ReadLine();
                if (cevap == "E" || cevap == "e") {  // araç bilgilerini yazdırmak için soru sorar
                    Console.WriteLine("________________________");
                    kisiAraclarınıYazdir(kisiID);  //kişi araçlarını listeleyen consoleApplication yardımcı metodu
                    Console.ReadKey();
                }
            }
        }// kisiyiGoruntule() metodu sonu

        public static void kisiAraclarınıYazdir(int kisiID)
        {  // kişi araçlarını yazdıran consoleApplication metodu
            kisi.kisiListesi[kisiID - 1].araclarimiListele();  // seçim numarasını liste index sistemine uyumlar
            //kişiler statik listesine erişir
            // kişiye ait araçları bilgileri ile listelemek için kişi nesnesine ait araçlarımı listele metodunu kullanır
        }  // kisiAraclariniYazdir() metodu sonu

        public static void aracAktarimi ()
        {
            Console.WriteLine("## serbest araclar");
               araclariListele();
                int secimArac = sayiOku();
                Console.Clear();
                kisileriListele();
                int secimKisi = sayiOku();
                Console.Clear();
                AracEdinimBilgiAl(secimArac);
                arac arac_temp = arac.serbestAraclarListesi[secimArac-1];
                arac.serbestAraclarListesi.RemoveAt(secimArac-1);
                kisi.kisiListesi[secimKisi-1].araclar.Add(arac_temp);
            
        }

        public static void araclariListele()  // serbest araçları listelemek için kullanılıacak consoleApplication metodu
        {  // araç aktarımı sırasında kullanılır
            arac.araclariListele();  // arac sınıfına ait publik statik metod istenilen bilgiye dışarıdan erişim sağlar
        }

        public static void AracEdinimBilgiAl (int secilenAracIndex)  // aracın kişiye özel edinim bilgileri
        {  // aracın bir kişiye aktarımı sırasında alınır
            Console.WriteLine("edinim tarihi(gg/aa/yyyy): ");  // TR tarih formatına uygun tarih istemi
            string edinimTarihi_temp = Console.ReadLine();  // tarih okuması
            // tarihin DateTime veri tipine göre TR tarih formatında parslanması
            DateTime edinimTarihi_temp_dateTime =  DateTime.ParseExact(edinimTarihi_temp, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine("edinim fiyatı: ");
            string edinimFiyati_temp = Console.ReadLine();
            int edinimFiyati_temp_int = int.Parse(edinimFiyati_temp);
            //bilgiler arac nesnesine ait public metod ile istenilen araca aktarılır
            arac.serbestAraclarListesi[secilenAracIndex-1].edinim(edinimTarihi_temp_dateTime, edinimFiyati_temp_int);
            arac.serbestAraclarListesi[secilenAracIndex - 1].aracSahipSayisiGuncelle(); //aktarımdan sonra sahip sayısını 1 arttır
        }
    } // consoleApplication sınıfı sonu
} // sinif2_odev1 namespace sonu


//parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });