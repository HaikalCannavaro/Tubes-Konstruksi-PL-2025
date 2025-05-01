using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Modules;

namespace AplikasiInventarisToko
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    APLIKASI INVENTARIS BARANG TOKO     ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Tambah Barang");
                Console.WriteLine("2. Edit Barang");
                Console.WriteLine("3. Hapus Barang");
                Console.WriteLine("4. Cari Barang");
                Console.WriteLine("5. Tampilkan Semua Barang");
                Console.WriteLine("6. Transaksi Barang Masuk");
                Console.WriteLine("7. Transaksi Barang Keluar");
                Console.WriteLine("8. Riwayat Transaksi");
                Console.WriteLine("9. Laporan Inventaris");
                Console.WriteLine("10. Export Data");
                Console.WriteLine("0. Keluar");
                Console.WriteLine("----------------------------------------");

                Console.Write("Pilih menu: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await ModulBarang.TambahBarangBaru();
                        break;
                    case "2":
                        await ModulBarang.EditBarang();
                        break;
                    case "3":
                        await ModulBarang.HapusBarang();
                        break;
                    case "4":
                        await ModulBarang.CariBarang();
                        break;
                    case "5":
                        await ModulBarang.LihatSemuaBarang();
                        break;
                    case "6":
                        await ModulTransaksi.TransaksiBarangMasuk();
                        break;
                    case "7":
                        await ModulTransaksi.TransaksiBarangKeluar();
                        break;
                    case "8":
                        await ModulTransaksi.LihatRiwayatTransaksi();
                        break;
                    case "9":
                        await ModulLaporan.TampilkanLaporanInventaris();
                        break;
                    case "10":
                        await ModulLaporan.ExportDataInventaris();
                        break;
                    case "0":
                        Console.WriteLine("\nTerima kasih telah menggunakan aplikasi ini.");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Menu tidak tersedia. Tekan sembarang tombol...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
