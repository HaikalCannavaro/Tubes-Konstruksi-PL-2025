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

        public static void HapusBarang()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS BARANG ===");

            var daftarBarang = Manager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang untuk dihapus.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                "ID", "Nama", "Kategori", "Stok");
            Console.WriteLine(new string('-', 55));

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                    barang.Id,
                    barang.Nama.Length > 17 ? barang.Nama.Substring(0, 17) + "..." : barang.Nama,
                    barang.Kategori.Length > 12 ? barang.Kategori.Substring(0, 12) + "..." : barang.Kategori,
                    barang.Stok);
            }

            Console.Write("\nMasukkan ID barang yang ingin dihapus: ");
            string id = Console.ReadLine();

            Console.Write($"\nApakah Anda yakin ingin menghapus barang dengan ID {id}? (y/n): ");
            string konfirmasi = Console.ReadLine().ToLower();

            if (konfirmasi != "y")
            {
                Console.WriteLine("\nPenghapusan dibatalkan.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
                return;
            }

            try
            {
                bool sukses = Manager.HapusBarang(id);

                if (sukses)
                {
                    Console.WriteLine("\nBarang berhasil dihapus!");
                }
                else
                {
                    Console.WriteLine("\nGagal menghapus barang. ID tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }
    }
}