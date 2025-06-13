using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System.Net.Http.Json;

namespace AplikasiInventarisToko.Modules
{
    public static class ModulTransaksi
    {
        private static readonly string BASE_URL = "https://localhost:7123";
        private static readonly string API_BARANG_ENDPOINT = "/api/Barang";
        private static readonly string API_TRANSAKSI_ENDPOINT = "/api/Transaksi";
        private static readonly string API_TRANSAKSI_MASUK_ENDPOINT = "/api/Transaksi/masuk";
        private static readonly string API_TRANSAKSI_KELUAR_ENDPOINT = "/api/Transaksi/keluar";

        private static readonly int NAMA_MAX_LENGTH = 17;
        private static readonly int KATEGORI_MAX_LENGTH = 12;
        private static readonly int SUPPLIER_MAX_LENGTH = 12;
        private static readonly int TABLE_SEPARATOR_LENGTH = 95;
        private static readonly int RIWAYAT_TABLE_SEPARATOR_LENGTH = 110;

        private static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(BASE_URL);
            client.Timeout = TimeSpan.FromSeconds(30);
            return client;
        }

        public static async Task TransaksiBarangMasuk()
        {
            await HandleTransaksiBarang("MASUK", "Masuk", API_TRANSAKSI_MASUK_ENDPOINT);
        }

        public static async Task TransaksiBarangKeluar()
        {
            await HandleTransaksiBarang("KELUAR", "Keluar", API_TRANSAKSI_KELUAR_ENDPOINT);
        }

        private static async Task HandleTransaksiBarang(string jenisTransaksiDisplay, string jenisTransaksi, string apiEndpoint)
        {
            Console.Clear();
            Console.WriteLine($"=== TRANSAKSI BARANG {jenisTransaksiDisplay} ===");

            using var client = CreateHttpClient();

            try
            {
                var daftarBarang = await GetDaftarBarang(client);
                if (!IsBarangDataValid(daftarBarang))
                {
                    DisplayNoDataMessage();
                    return;
                }

                DisplayBarangTable(daftarBarang);

                var transaksiData = GetTransaksiInputFromUser(jenisTransaksi);
                if (transaksiData == null)
                {
                    DisplayErrorMessage("Input tidak valid");
                    return;
                }

                var success = await ProcessTransaksi(client, transaksiData, apiEndpoint);
                DisplayTransaksiResult(success, jenisTransaksiDisplay.ToLower());
            }
            catch (HttpRequestException ex)
            {
                DisplayErrorMessage($"Kesalahan koneksi: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                DisplayErrorMessage($"Request timeout: {ex.Message}");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Terjadi kesalahan tidak terduga: {ex.Message}");
            }

            WaitForUserInput();
        }

        private static async Task<List<Barang>?> GetDaftarBarang(HttpClient client)
        {
            var response = await client.GetAsync(API_BARANG_ENDPOINT);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Barang>>();
        }

        private static bool IsBarangDataValid(List<Barang>? daftarBarang)
        {
            return daftarBarang != null && daftarBarang.Count > 0;
        }

        private static void DisplayNoDataMessage()
        {
            Console.WriteLine("\nBelum ada barang yang terdata.");
            Console.WriteLine("Silakan tambahkan barang terlebih dahulu sebelum melakukan transaksi.");
            WaitForUserInput();
        }

        private static void DisplayBarangTable(List<Barang> daftarBarang)
        {
            var config = KonfigurasiAplikasi.Load();

            PrintTableHeader();
            PrintTableSeparator(TABLE_SEPARATOR_LENGTH);

            foreach (var barang in daftarBarang)
            {
                PrintBarangRow(barang, config);
            }
        }

        private static void PrintTableHeader()
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                "ID", "Nama", "Kategori", "Stok", "Harga Beli", "Harga Jual", "Supplier");
        }

        private static void PrintTableSeparator(int length)
        {
            Console.WriteLine(new string('-', length));
        }

        private static void PrintBarangRow(Barang barang, KonfigurasiAplikasi config)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                barang.Id,
                TruncateString(barang.Nama, NAMA_MAX_LENGTH),
                TruncateString(barang.Kategori, KATEGORI_MAX_LENGTH),
                barang.Stok,
                StokHelper.FormatCurrency(barang.HargaBeli, config),
                StokHelper.FormatCurrency(barang.HargaJual, config),
                TruncateString(barang.Supplier, SUPPLIER_MAX_LENGTH));
        }

        private static string TruncateString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.Length > maxLength ? input.Substring(0, maxLength) + "..." : input;
        }

        private static Transaksi? GetTransaksiInputFromUser(string jenisTransaksi)
        {
            try
            {
                Console.Write("\nMasukkan ID barang: ");
                string? id = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("ID barang tidak boleh kosong.");
                    return null;
                }

                Console.Write($"Jumlah barang {jenisTransaksi.ToLower()}: ");
                string? jumlahInput = Console.ReadLine();
                int jumlah = ValidasiInput.ValidasiAngka(jumlahInput);

                if (jumlah <= 0)
                {
                    Console.WriteLine("Jumlah harus lebih dari 0.");
                    return null;
                }

                Console.Write("Keterangan (opsional): ");
                string? keterangan = Console.ReadLine();

                return new Transaksi
                {
                    BarangId = id.Trim(),
                    Jumlah = jumlah,
                    Jenis = jenisTransaksi,
                    Keterangan = keterangan?.Trim() ?? string.Empty
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static async Task<bool> ProcessTransaksi(HttpClient client, Transaksi transaksi, string endpoint)
        {
            var response = await client.PostAsJsonAsync(endpoint, transaksi);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Gagal: {errorMessage}");
                return false;
            }

            return true;
        }

        private static void DisplayTransaksiResult(bool success, string jenisTransaksi)
        {
            if (success)
            {
                Console.WriteLine($"\nTransaksi barang {jenisTransaksi} berhasil!");
            }
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.WriteLine($"\nError: {message}");
        }

        private static void WaitForUserInput()
        {
            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        public static async Task LihatRiwayatTransaksi()
        {
            Console.Clear();
            Console.WriteLine("=== RIWAYAT TRANSAKSI ===");

            using var client = CreateHttpClient();

            try
            {
                var (transaksiList, barangDict) = await GetTransaksiAndBarangData(client);

                if (!IsTransaksiDataValid(transaksiList))
                {
                    DisplayNoTransaksiMessage();
                    return;
                }

                DisplayRiwayatTransaksiTable(transaksiList, barangDict);
            }
            catch (HttpRequestException ex)
            {
                DisplayErrorMessage($"Kesalahan koneksi: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                DisplayErrorMessage($"Request timeout: {ex.Message}");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Terjadi kesalahan tidak terduga: {ex.Message}");
            }

            WaitForUserInput();
        }

        private static async Task<(List<Transaksi>?, Dictionary<string, string>)> GetTransaksiAndBarangData(HttpClient client)
        {
            var transaksiResponse = await client.GetAsync(API_TRANSAKSI_ENDPOINT);
            if (!transaksiResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Gagal mengambil data transaksi.");
            }

            var transaksiList = await transaksiResponse.Content.ReadFromJsonAsync<List<Transaksi>>();

            var barangResponse = await client.GetAsync(API_BARANG_ENDPOINT);
            var daftarBarang = await barangResponse.Content.ReadFromJsonAsync<List<Barang>>() ?? new List<Barang>();
            var barangDict = CreateBarangDictionary(daftarBarang);

            return (transaksiList, barangDict);
        }

        private static Dictionary<string, string> CreateBarangDictionary(List<Barang> daftarBarang)
        {
            return daftarBarang.ToDictionary(b => b.Id, b => b.Nama);
        }

        private static bool IsTransaksiDataValid(List<Transaksi>? transaksiList)
        {
            return transaksiList != null && transaksiList.Count > 0;
        }

        private static void DisplayNoTransaksiMessage()
        {
            Console.WriteLine("Belum ada transaksi.");
        }

        private static void DisplayRiwayatTransaksiTable(List<Transaksi> transaksiList, Dictionary<string, string> barangDict)
        {
            PrintRiwayatTableHeader();
            PrintTableSeparator(RIWAYAT_TABLE_SEPARATOR_LENGTH);

            foreach (var transaksi in transaksiList)
            {
                PrintRiwayatTransaksiRow(transaksi, barangDict);
            }
        }

        private static void PrintRiwayatTableHeader()
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-20} {4,-8} {5,-25} {6,-30}",
                "ID", "Jenis", "ID", "Barang", "Jumlah", "Tanggal", "Keterangan");
        }

        private static void PrintRiwayatTransaksiRow(Transaksi transaksi, Dictionary<string, string> barangDict)
        {
            string namaBarang = GetNamaBarang(transaksi.BarangId, barangDict);
            string tanggalFormatted = transaksi.Tanggal.ToString("dd-MM-yyyy HH:mm");

            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-20} {4,-8} {5,-25} {6,-30}",
                transaksi.Id,
                transaksi.Jenis,
                transaksi.BarangId,
                namaBarang,
                transaksi.Jumlah,
                tanggalFormatted,
                transaksi.Keterangan ?? string.Empty);
        }

        private static string GetNamaBarang(string barangId, Dictionary<string, string> barangDict)
        {
            return barangDict.TryGetValue(barangId, out string? nama) ? nama : "Tidak diketahui";
        }
    }
}