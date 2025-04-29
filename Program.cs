using System;
using System.Collections.Generic;
using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;

namespace AplikasiInventarisToko
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    APLIKASI INVENTARIS BARANG TOKO     ");
            Console.WriteLine("========================================");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    APLIKASI INVENTARIS BARANG TOKO     ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Tambah & Edit Barang (Haikal)");
                Console.WriteLine("2. Hapus & Pencarian Barang (Subhan)");
                Console.WriteLine("3. Transaksi Barang Keluar/Masuk (Aslam)");
                Console.WriteLine("4. Laporan & Export Data (Angga)");
                Console.WriteLine("5. Runtime Config & Validasi Input (Devon)");
                Console.WriteLine("0. Keluar");
                Console.WriteLine("----------------------------------------");

                Console.Write("\nPilih menu: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ModulBarang.TampilkanMenuBarang();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "0":
                        Console.WriteLine("Terima kasih telah menggunakan aplikasi ini.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Menu tidak tersedia. Silakan pilih kembali.");
                        Console.WriteLine("\nTekan sembarang tombol untuk melanjutkan...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}