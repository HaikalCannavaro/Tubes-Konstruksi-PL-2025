using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System.Globalization;
using System.Net.Http.Json;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulLaporan
    {
        private static readonly KonfigurasiAplikasi _config = KonfigurasiAplikasi.Load();

        private static HttpClient GetClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7123")
            };

            return client;
        }

        public static async Task TampilkanLaporanInventaris()
        {
            Console.Clear();
            Console.WriteLine("=== LAPORAN INVENTARIS BARANG ===\n");

            using var client = GetClient();

            var daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
            var daftarTransaksi = await client.GetFromJsonAsync<List<Transaksi>>("/api/Transaksi");

            if (daftarBarang == null || daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data barang.");
            }
            else
            {
                foreach (var barang in daftarBarang)
                {
                    int stokAwal = StokHelper.HitungStokAwal(barang, daftarTransaksi);
                    double persentaseStok = StokHelper.HitungPersentaseStok(barang, barang.StokAwal);
                    string status = StokHelper.TentukanStatus(barang, persentaseStok, _config);
                    int lamaDiGudang = (int)(DateTime.Now - barang.TanggalMasuk).TotalDays;

                    Console.WriteLine($"ID             : {barang.Id}");
                    Console.WriteLine($"Nama           : {barang.Nama}");
                    Console.WriteLine($"Tanggal Masuk  : {barang.TanggalMasuk:dd-MM-yyyy}");
                    Console.WriteLine($"Lama di Gudang : {lamaDiGudang} hari");
                    Console.WriteLine($"Stok           : {barang.Stok}/{barang.StokAwal} ({persentaseStok:N0}%)");
                    Console.WriteLine($"Harga Beli     : {StokHelper.FormatCurrency(barang.HargaBeli, _config)}");
                    Console.WriteLine($"Status         : {status}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }

        public static async Task ExportDataInventaris()
        {
            Console.Clear();
            Console.WriteLine("=== EXPORT DATA BARANG ===");

            var filePath = _config.ExportPath ?? $"data_barang_{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.csv";

            using var client = GetClient();

            var daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
            var daftarTransaksi = await client.GetFromJsonAsync<List<Transaksi>>("/api/Transaksi");

            if (daftarBarang == null || daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data yang bisa diekspor.");
            }
            else
            {
                using var writer = new StreamWriter(filePath);
                writer.WriteLine("Id,Nama,Kategori,Supplier,HargaBeli,HargaJual,StokSekarang,StokAwal,Persentase,Status");

                foreach (var barang in daftarBarang)
                {
                    int stokAwal = StokHelper.HitungStokAwal(barang, daftarTransaksi);
                    double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                    string status = StokHelper.TentukanStatus(barang, persentaseStok, _config);

                    writer.WriteLine($"{barang.Id},{barang.Nama},{barang.Kategori},{barang.Supplier}," +
                                     $"{StokHelper.FormatCurrency(barang.HargaBeli, _config)}," +
                                     $"{StokHelper.FormatCurrency(barang.HargaJual, _config)}," +
                                     $"{barang.Stok},{stokAwal},{persentaseStok:N0},{status}");
                }

                Console.WriteLine($"Data berhasil diekspor ke file: {filePath}");
            }

            Console.WriteLine("Tekan Enter untuk kembali...");
            Console.ReadLine();
        }
    }
}
