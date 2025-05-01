using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System.Net.Http.Json;

namespace AplikasiInventarisToko.Modules
{
    public static class ModulTransaksi
    {
        private static readonly string baseUrl = "https://localhost:7123";

        private static HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(baseUrl);
            return client;
        }

        public static async Task TransaksiBarangMasuk()
        {
            Console.Clear();
            Console.WriteLine("=== TRANSAKSI BARANG MASUK ===");

            using var client = GetHttpClient();

            try
            {
                var daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
                var config = KonfigurasiAplikasi.Load();

                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang tersedia.");
                    return;
                }

                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                    "ID", "Nama", "Kategori", "Stok", "Harga Beli", "Harga Jual", "Supplier");
                Console.WriteLine(new string('-', 95));
                foreach (var barang in daftarBarang)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                        barang.Id,
                        barang.Nama.Length > 17 ? barang.Nama.Substring(0, 17) + "..." : barang.Nama,
                        barang.Kategori.Length > 12 ? barang.Kategori.Substring(0, 12) + "..." : barang.Kategori,
                        barang.Stok,
                        StokHelper.FormatCurrency(barang.HargaBeli, config),
                        StokHelper.FormatCurrency(barang.HargaJual, config),
                        barang.Supplier.Length > 12 ? barang.Supplier.Substring(0, 12) + "..." : barang.Supplier);
                }

                Console.Write("\nMasukkan ID barang: ");
                string id = Console.ReadLine();

                Console.Write("Jumlah barang masuk: ");
                int jumlah = ValidasiInput.ValidasiAngka(Console.ReadLine());

                Console.Write("Keterangan (opsional): ");
                string keterangan = Console.ReadLine();

                var transaksi = new Transaksi
                {
                    BarangId = id,
                    Jumlah = jumlah,
                    Jenis = "Masuk",
                    Keterangan = keterangan
                };

                var response = await client.PostAsJsonAsync("/api/Transaksi/masuk", transaksi);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nTransaksi barang masuk berhasil!");
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Gagal: {msg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        public static async Task TransaksiBarangKeluar()
        {
            Console.Clear();
            Console.WriteLine("=== TRANSAKSI BARANG KELUAR ===");

            using var client = GetHttpClient();

            try
            {
                var daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
                var config = KonfigurasiAplikasi.Load();

                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang tersedia.");
                    return;
                }

                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                    "ID", "Nama", "Kategori", "Stok", "Harga Beli", "Harga Jual", "Supplier");
                Console.WriteLine(new string('-', 95));
                foreach (var barang in daftarBarang)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                        barang.Id,
                        barang.Nama.Length > 17 ? barang.Nama.Substring(0, 17) + "..." : barang.Nama,
                        barang.Kategori.Length > 12 ? barang.Kategori.Substring(0, 12) + "..." : barang.Kategori,
                        barang.Stok,
                        StokHelper.FormatCurrency(barang.HargaBeli, config),
                        StokHelper.FormatCurrency(barang.HargaJual, config),
                        barang.Supplier.Length > 12 ? barang.Supplier.Substring(0, 12) + "..." : barang.Supplier);
                }

                Console.Write("\nMasukkan ID barang: ");
                string id = Console.ReadLine();

                Console.Write("Jumlah barang keluar: ");
                int jumlah = ValidasiInput.ValidasiAngka(Console.ReadLine());

                Console.Write("Keterangan (opsional): ");
                string keterangan = Console.ReadLine();

                var transaksi = new Transaksi
                {
                    BarangId = id,
                    Jumlah = jumlah,
                    Jenis = "Keluar",
                    Keterangan = keterangan
                };

                var response = await client.PostAsJsonAsync("/api/Transaksi/keluar", transaksi);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nTransaksi barang keluar berhasil!");
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Gagal: {msg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        public static async Task LihatRiwayatTransaksi()
        {
            Console.Clear();
            Console.WriteLine("=== RIWAYAT TRANSAKSI ===");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                // Ambil data transaksi
                var response = await client.GetAsync("/api/Transaksi");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Gagal mengambil data transaksi.");
                    Console.ReadKey();
                    return;
                }

                var transaksi = await response.Content.ReadFromJsonAsync<List<Transaksi>>();
                if (transaksi == null || transaksi.Count == 0)
                {
                    Console.WriteLine("Belum ada transaksi.");
                    Console.ReadKey();
                    return;
                }

                // Ambil data barang
                var barangResponse = await client.GetAsync("/api/Barang");
                var daftarBarang = await barangResponse.Content.ReadFromJsonAsync<List<Barang>>() ?? new();
                var barangDict = daftarBarang.ToDictionary(b => b.Id, b => b.Nama);

                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10} {4,-8} {5,-25} {6,-30}",
                    "ID", "Jenis", "BarangID", "Barang", "Jumlah", "Tanggal", "Keterangan");
                Console.WriteLine(new string('-', 110));

                foreach (var t in transaksi)
                {
                    string nama = barangDict.ContainsKey(t.BarangId) ? barangDict[t.BarangId] : "Tidak diketahui";

                    Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10} {4,-8} {5,-25} {6,-30}",
                        t.Id, t.Jenis, t.BarangId, nama, t.Jumlah, t.Tanggal.ToString("dd-MM-yyyy HH:mm"), t.Keterangan);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nTerjadi kesalahan: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

    }
}
