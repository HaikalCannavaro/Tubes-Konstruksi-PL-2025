using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using AplikasiInventarisToko.Helpers;
using System;
using System.Net.Http.Json;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulBarang
    {
        private static BarangManager<Barang> _barangManager = new BarangManager<Barang>();
        public static BarangManager<Barang> Manager => _barangManager;

        public static async Task TambahBarangBaru()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH BARANG BARU ===");

            try
            {
                Console.Write("Nama Barang: ");
                string nama = Console.ReadLine();

                Console.Write("Kategori: ");
                string kategori = Console.ReadLine();

                Console.Write("Stok Awal: ");
                int stok = ValidasiInput.ValidasiAngka(Console.ReadLine());

                Console.Write("Harga Beli: ");
                decimal hargaBeli = ValidasiInput.ValidasiDecimal(Console.ReadLine());

                Console.Write("Harga Jual: ");
                decimal hargaJual = ValidasiInput.ValidasiDecimal(Console.ReadLine());

                Console.Write("Supplier: ");
                string supplier = Console.ReadLine();

                var barang = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
                {
                    StokAwal = stok
                };

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7123");

                var response = await client.PostAsJsonAsync("/api/Barang", barang);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nBarang berhasil ditambahkan ke API!");
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"\nGagal menambahkan barang: {msg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        public static async Task CariBarang()
        {
            Console.Clear();
            Console.WriteLine("=== CARI BARANG ===");

            Console.Write("Masukkan kriteria (id/nama/kategori/supplier): ");
            string kriteria = Console.ReadLine()?.ToLower();

            Console.Write("Masukkan nilai pencarian: ");
            string nilai = Console.ReadLine();

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.GetAsync($"/api/Barang/search?kriteria={kriteria}&nilai={nilai}");

                if (response.IsSuccessStatusCode)
                {
                    var hasil = await response.Content.ReadFromJsonAsync<List<Barang>>();

                    if (hasil == null || hasil.Count == 0)
                    {
                        Console.WriteLine("Barang tidak ditemukan.");
                    }
                    else
                    {
                        BarangDisplayHelper.TampilkanDaftarBarang(hasil);
                    }
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Gagal mencari barang: {msg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        public static async Task EditBarang()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT BARANG ===");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.GetAsync("/api/Barang");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Gagal mengambil daftar barang.");
                    Console.ReadKey();
                    return;
                }

                var daftarBarang = await response.Content.ReadFromJsonAsync<List<Barang>>();
                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang untuk diedit.");
                    Console.ReadKey();
                    return;
                }

                BarangDisplayHelper.TampilkanDaftarPilihan(daftarBarang);

                Console.Write("\nMasukkan ID barang yang ingin diedit: ");
                string id = Console.ReadLine();

                var getBarang = await client.GetAsync($"/api/Barang/{id}");
                if (!getBarang.IsSuccessStatusCode)
                {
                    Console.WriteLine("Barang tidak ditemukan.");
                    Console.ReadKey();
                    return;
                }

                var barangLama = await getBarang.Content.ReadFromJsonAsync<Barang>();

                Console.WriteLine($"\nEdit Barang: {barangLama.Nama}");

                Console.Write($"Nama Barang [{barangLama.Nama}]: ");
                string nama = Console.ReadLine();
                nama = string.IsNullOrEmpty(nama) ? barangLama.Nama : nama;

                Console.Write($"Kategori [{barangLama.Kategori}]: ");
                string kategori = Console.ReadLine();
                kategori = string.IsNullOrEmpty(kategori) ? barangLama.Kategori : kategori;

                Console.Write($"Stok [{barangLama.Stok}]: ");
                string stokInput = Console.ReadLine();
                int stok = string.IsNullOrEmpty(stokInput) ? barangLama.Stok : ValidasiInput.ValidasiAngka(stokInput);

                Console.Write($"Harga Beli [{barangLama.HargaBeli}]: ");
                string beliInput = Console.ReadLine();
                decimal hargaBeli = string.IsNullOrEmpty(beliInput) ? barangLama.HargaBeli : ValidasiInput.ValidasiDecimal(beliInput);

                Console.Write($"Harga Jual [{barangLama.HargaJual}]: ");
                string jualInput = Console.ReadLine();
                decimal hargaJual = string.IsNullOrEmpty(jualInput) ? barangLama.HargaJual : ValidasiInput.ValidasiDecimal(jualInput);

                Console.Write($"Supplier [{barangLama.Supplier}]: ");
                string supplier = Console.ReadLine();
                supplier = string.IsNullOrEmpty(supplier) ? barangLama.Supplier : supplier;

                var barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
                {
                    Id = barangLama.Id,
                    TanggalMasuk = barangLama.TanggalMasuk,
                    StokAwal = stok
                };

                var put = await client.PutAsJsonAsync($"/api/Barang/{id}", barangBaru);
                if (put.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nBarang berhasil diperbarui!");
                }
                else
                {
                    var err = await put.Content.ReadAsStringAsync();
                    Console.WriteLine($"\nGagal memperbarui barang: {err}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        public static async Task LihatSemuaBarang()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR SEMUA BARANG ===");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.GetAsync("/api/Barang");

                if (response.IsSuccessStatusCode)
                {
                    var daftarBarang = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    BarangDisplayHelper.TampilkanDaftarBarang(daftarBarang);
                }
                else
                {
                    Console.WriteLine("Gagal mengambil data barang dari API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        private enum HapusBarangState
        {
            ListBarang,
            InputId,
            Konfirmasi,
            ProsesHapus,
            Selesai
        }

        private class HapusBarangContext
        {
            public HapusBarangState CurrentState { get; set; }
            public List<Barang> DaftarBarang { get; set; }
            public string IdBarang { get; set; }
            public Barang BarangTerpilih { get; set; }
            public bool HasilHapus { get; set; }
        }

        public static async Task HapusBarang()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS BARANG ===");

            var context = new HapusBarangContext
            {
                CurrentState = HapusBarangState.ListBarang,
                DaftarBarang = new List<Barang>()
            };

            while (context.CurrentState != HapusBarangState.Selesai)
            {
                switch (context.CurrentState)
                {
                    case HapusBarangState.ListBarang:
                        await HandleListBarangState(context);
                        break;
                    case HapusBarangState.InputId:
                        await HandleInputIdState(context);
                        break;
                    case HapusBarangState.Konfirmasi:
                        await HandleKonfirmasiState(context);
                        break;
                    case HapusBarangState.ProsesHapus:
                        await HandleProsesHapusState(context);
                        break;
                }
            }

            Console.WriteLine(context.HasilHapus
                ? "\nBarang berhasil dihapus!"
                : "\nPenghapusan barang dibatalkan atau gagal.");

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        private static async Task HandleListBarangState(HapusBarangContext context)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.GetAsync("/api/Barang");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Gagal mengambil daftar barang.");
                    context.CurrentState = HapusBarangState.Selesai;
                    return;
                }

                context.DaftarBarang = await response.Content.ReadFromJsonAsync<List<Barang>>();
                if (context.DaftarBarang == null || context.DaftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang untuk dihapus.");
                    context.CurrentState = HapusBarangState.Selesai;
                    return;
                }

                BarangDisplayHelper.TampilkanDaftarBarang(context.DaftarBarang, showFullDetails: false);

                context.CurrentState = HapusBarangState.InputId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal mengambil data: {ex.Message}");
                context.CurrentState = HapusBarangState.Selesai;
            }
        }

        private static async Task HandleInputIdState(HapusBarangContext context)
        {
            Console.Write("\nMasukkan ID barang yang ingin dihapus (atau 'batal' untuk kembali): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "batal")
            {
                context.CurrentState = HapusBarangState.Selesai;
                return;
            }

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.GetAsync($"/api/Barang/{input}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("ID barang tidak ditemukan. Silakan coba lagi.");
                    return;
                }

                var barang = await response.Content.ReadFromJsonAsync<Barang>();
                context.BarangTerpilih = barang;
                context.IdBarang = input;
                context.CurrentState = HapusBarangState.Konfirmasi;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal mengambil data: {ex.Message}");
            }
        }

        private static Task HandleKonfirmasiState(HapusBarangContext context)
        {
            Console.WriteLine($"\nDetail Barang yang akan dihapus:");

            BarangDisplayHelper.TampilkanDetailBarang(context.BarangTerpilih);

            Console.Write($"\nApakah Anda yakin ingin menghapus barang ini? (y/n/t=lihat lagi): ");
            string konfirmasi = Console.ReadLine().ToLower();

            switch (konfirmasi)
            {
                case "y":
                    context.CurrentState = HapusBarangState.ProsesHapus;
                    break;
                case "n":
                    context.CurrentState = HapusBarangState.Selesai;
                    context.HasilHapus = false;
                    break;
                case "t":
                    context.CurrentState = HapusBarangState.ListBarang;
                    break;
                default:
                    Console.WriteLine("Input tidak valid. Silakan pilih y/n/t.");
                    break;
            }

            return Task.CompletedTask;
        }

        private static async Task HandleProsesHapusState(HapusBarangContext context)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                var response = await client.DeleteAsync($"/api/Barang/{context.IdBarang}");
                context.HasilHapus = response.IsSuccessStatusCode;
                context.CurrentState = HapusBarangState.Selesai;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nGagal menghapus barang: {ex.Message}");
                context.CurrentState = HapusBarangState.Konfirmasi;
            }
        }
    }
}