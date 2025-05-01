using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulBarang
    {
        private static BarangManager<Barang> _barangManager = new BarangManager<Barang>();
        public static BarangManager<Barang> Manager => _barangManager;

        public static void TambahBarangBaru()
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

                Barang barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
                {
                    StokAwal = stok
                };


                bool sukses = Manager.TambahBarang(barangBaru);

                if (sukses)
                {
                    Console.WriteLine("\nBarang berhasil ditambahkan!");
                    Console.WriteLine($"ID Barang: {barangBaru.Id}");
                }
                else
                {
                    Console.WriteLine("\nGagal menambahkan barang.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        // Implementasi fitur Cari Barang
        public static void CariBarang()
        {
            Console.Clear();
            Console.WriteLine("=== CARI BARANG ===");

            var daftarBarang = Manager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang tersedia.");
            }
            else
            {
                foreach (var barang in daftarBarang)
                {
                    Console.WriteLine($"[{barang.Id}] {barang.Nama} - Stok: {barang.Stok}");
                }

                Console.Write("\nMasukkan ID barang yang dicari: ");
                string id = Console.ReadLine();

                var item = Manager.GetBarangById(id);

                if (item != null)
                {
                    Console.WriteLine("\nDetail Barang:");
                    Console.WriteLine($"ID: {item.Id}");
                    Console.WriteLine($"Nama: {item.Nama}");
                    Console.WriteLine($"Kategori: {item.Kategori}");
                    Console.WriteLine($"Stok: {item.Stok}");
                    Console.WriteLine($"Harga Beli: {item.HargaBeli:C}");
                    Console.WriteLine($"Harga Jual: {item.HargaJual:C}");
                    Console.WriteLine($"Supplier: {item.Supplier}");
                    Console.WriteLine($"Tanggal Masuk: {item.TanggalMasuk}");
                }
                else
                {
                    Console.WriteLine("Barang tidak ditemukan.");
                }
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }


        public static void EditBarang()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT BARANG ===");

            var daftarBarang = Manager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang untuk diedit.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
                return;
            }

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine($"[{barang.Id}] {barang.Nama} - Stok: {barang.Stok}");
            }

            Console.Write("\nMasukkan ID barang yang ingin diedit: ");
            string id = Console.ReadLine();

            var barangLama = Manager.GetBarangById(id);

            if (barangLama == null)
            {
                Console.WriteLine("Barang tidak ditemukan.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
                return;
            }

            try
            {
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

                Barang barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
                {
                    Id = barangLama.Id,
                    TanggalMasuk = barangLama.TanggalMasuk,
                    StokAwal = stok
                };

                bool sukses = Manager.EditBarang(id, barangBaru);

                Console.WriteLine(sukses ? "\nBarang berhasil diperbarui!" : "\nGagal memperbarui barang.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        public static void LihatSemuaBarang()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR SEMUA BARANG ===");

            var daftarBarang = Manager.GetSemuaBarang();
            var config = KonfigurasiAplikasi.Load();


            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang tersedia.");
            }
            else
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                    "ID", "Nama", "Kategori", "Stok", "Harga Beli", "Harga Jual", "Supplier");
                Console.WriteLine(new string('-', 95));

                foreach (var barang in daftarBarang)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12:C} {5,-12:C} {6,-15}",
                        barang.Id,
                        barang.Nama.Length > 17 ? barang.Nama.Substring(0, 17) + "..." : barang.Nama,
                        barang.Kategori.Length > 12 ? barang.Kategori.Substring(0, 12) + "..." : barang.Kategori,
                        barang.Stok,
                        StokHelper.FormatCurrency(barang.HargaBeli, config),
                        StokHelper.FormatCurrency(barang.HargaJual, config),
                        barang.Supplier.Length > 12 ? barang.Supplier.Substring(0, 12) + "..." : barang.Supplier);
                }
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

        public static void HapusBarang()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS BARANG ===");

            var context = new HapusBarangContext
            {
                CurrentState = HapusBarangState.ListBarang,
                DaftarBarang = Manager.GetSemuaBarang()
            };

            while (context.CurrentState != HapusBarangState.Selesai)
            {
                switch (context.CurrentState)
                {
                    case HapusBarangState.ListBarang:
                        HandleListBarangState(context);
                        break;
                    case HapusBarangState.InputId:
                        HandleInputIdState(context);
                        break;
                    case HapusBarangState.Konfirmasi:
                        HandleKonfirmasiState(context);
                        break;
                    case HapusBarangState.ProsesHapus:
                        HandleProsesHapusState(context);
                        break;
                }
            }

            Console.WriteLine(context.HasilHapus
                ? "\nBarang berhasil dihapus!"
                : "\nPenghapusan barang dibatalkan atau gagal.");

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        private static void HandleListBarangState(HapusBarangContext context)
        {
            if (context.DaftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang untuk dihapus.");
                context.CurrentState = HapusBarangState.Selesai;
                return;
            }

            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                "ID", "Nama", "Kategori", "Stok");
            Console.WriteLine(new string('-', 55));

            foreach (var barang in context.DaftarBarang)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                    barang.Id,
                    barang.Nama.Length > 17 ? barang.Nama.Substring(0, 17) + "..." : barang.Nama,
                    barang.Kategori.Length > 12 ? barang.Kategori.Substring(0, 12) + "..." : barang.Kategori,
                    barang.Stok);
            }

            context.CurrentState = HapusBarangState.InputId;
        }

        private static void HandleInputIdState(HapusBarangContext context)
        {
            Console.Write("\nMasukkan ID barang yang ingin dihapus (atau 'batal' untuk kembali): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "batal")
            {
                context.CurrentState = HapusBarangState.Selesai;
                return;
            }

            var barang = context.DaftarBarang.FirstOrDefault(b => b.Id == input);
            if (barang == null)
            {
                Console.WriteLine("ID barang tidak ditemukan. Silakan coba lagi.");
                return;
            }

            context.BarangTerpilih = barang;
            context.IdBarang = input;
            context.CurrentState = HapusBarangState.Konfirmasi;
        }

        private static void HandleKonfirmasiState(HapusBarangContext context)
        {
            Console.WriteLine($"\nDetail Barang yang akan dihapus:");
            Console.WriteLine($"ID: {context.BarangTerpilih.Id}");
            Console.WriteLine($"Nama: {context.BarangTerpilih.Nama}");
            Console.WriteLine($"Kategori: {context.BarangTerpilih.Kategori}");
            Console.WriteLine($"Stok: {context.BarangTerpilih.Stok}");

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
        }

        private static void HandleProsesHapusState(HapusBarangContext context)
        {
            try
            {
                context.HasilHapus = Manager.HapusBarang(context.IdBarang);
                context.CurrentState = HapusBarangState.Selesai;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                context.CurrentState = HapusBarangState.Konfirmasi;
            }
        }

    }
}