using System;
using System.Globalization;
using System.IO;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using AplikasiInventarisToko.Modules;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulLaporan
    {
        private static BarangManager<Barang> _barangManager = ModulBarang.Manager;
        private static readonly KonfigurasiAplikasi _config = KonfigurasiAplikasi.Load();

        public static void TampilkanLaporanInventaris()
        {
            Console.Clear();
            Console.WriteLine("=== LAPORAN INVENTARIS BARANG ===\n");

            var daftarBarang = _barangManager.GetSemuaBarang();
            var transaksiManager = new TransaksiManager(_barangManager);
            var config = KonfigurasiAplikasi.Load();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data barang.");
            }
            else
            {
                foreach (var barang in daftarBarang)
                {
                    int stokAwal = StokHelper.HitungStokAwal(barang, transaksiManager);
                    double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                    string status = StokHelper.TentukanStatus(barang, persentaseStok, config);

                    Console.WriteLine($"ID           : {barang.Id}");
                    Console.WriteLine($"Nama         : {barang.Nama}");
                    Console.WriteLine($"Tanggal Masuk: {barang.TanggalMasuk:dd-MM-yyyy}");
                    Console.WriteLine($"Stok         : {barang.Stok}/{stokAwal} ({persentaseStok:N0}%)");
                    Console.WriteLine($"Harga Beli   : {StokHelper.FormatCurrency(barang.HargaBeli, config)}");
                    Console.WriteLine($"Status       : {status}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }




        public static void ExportDataInventaris()
        {
            Console.Clear();
            Console.WriteLine("=== EXPORT DATA BARANG ===");

            var config = KonfigurasiAplikasi.Load();
            string filePath = config.ExportPath ?? $"data_barang_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            var daftarBarang = _barangManager.GetSemuaBarang();
            var transaksiManager = new TransaksiManager(_barangManager);

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data yang bisa diekspor.");
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Id,Nama,Kategori,Supplier,HargaBeli,HargaJual,StokSekarang,StokAwal,Persentase,Status");

                    foreach (var barang in daftarBarang)
                    {
                        int stokAwal = StokHelper.HitungStokAwal(barang, transaksiManager);
                        double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                        string status = StokHelper.TentukanStatus(barang, persentaseStok, config);

                        writer.WriteLine($"{barang.Id},{barang.Nama},{barang.Kategori},{barang.Supplier}," +
                                         $"{StokHelper.FormatCurrency(barang.HargaBeli, config)}," +
                                         $"{StokHelper.FormatCurrency(barang.HargaJual, config)}," +
                                         $"{barang.Stok},{stokAwal},{persentaseStok:N0},{status}");
                    }
                }

                Console.WriteLine($"Data berhasil diekspor ke file: {filePath}");
            }

            Console.WriteLine("Tekan Enter untuk kembali...");
            Console.ReadLine();
        }


    }
}
