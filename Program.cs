using System;
using System.Collections.Generic;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Modules;

namespace AplikasiInventarisToko
{
    class Program
    {
        static void Main(string[] args)
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
                Console.WriteLine("11. Jalankan Web API");
                Console.WriteLine("0. Keluar");
                Console.WriteLine("----------------------------------------");

                Console.Write("Pilih menu: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ModulBarang.TambahBarangBaru();
                        break;
                    case "2":
                        ModulBarang.EditBarang();
                        break;
                    case "3":
                        ModulBarang.HapusBarang();
                        break;
                    case "4":
                        ModulBarang.CariBarang();
                        break;
                    case "5":
                        ModulBarang.LihatSemuaBarang();
                        break;
                    case "6":
                        ModulTransaksi.TransaksiBarangMasuk();
                        break;
                    case "7":
                        ModulTransaksi.TransaksiBarangKeluar();
                        break;
                    case "8":
                        ModulTransaksi.LihatRiwayatTransaksi();
                        break;
                    case "9":
                        ModulLaporan.TampilkanLaporanInventaris();
                        break;
                    case "10":
                        ModulLaporan.ExportDataInventaris();
                        break;
                    case "11":
                        try
                        {
                            Console.WriteLine("Menjalankan API Web...");

                            var startInfo = new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "dotnet",
                                Arguments = "run --project ../AplikasiInventarisToko.Api",
                                UseShellExecute = false
                            };

                            System.Diagnostics.Process.Start(startInfo);
                            Console.WriteLine("API berjalan di background. Tekan tombol untuk kembali...");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Gagal menjalankan API: {ex.Message}");
                        }

                        Console.ReadKey();
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