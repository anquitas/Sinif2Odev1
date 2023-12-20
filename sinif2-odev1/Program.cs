using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sinif2_odev1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            consoleApplication.anaMenuyuOlustur();  //ana menüyü ve console uygulamasını oluşturan metod
            bool menuyuGoster = true;  // çıkış kontrol değişkeni
            while (menuyuGoster)  //her döngüde çıkış talebi kontrol edilir
            {
                menuyuGoster = consoleApplication.anaMenu();  // çıkış kontrolü, ana menünü ve konsol uygulamasının çalıştırılması
            }
        }
    }
}
