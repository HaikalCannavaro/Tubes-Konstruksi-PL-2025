using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulBarang
    {
        private static BarangManager<Barang> _manager = new BarangManager<Barang>();

        public static void TampilkanMenuBarang()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("         MENU KELOLA BARANG            ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Tambah Barang Baru");
                Console.WriteLine("2. Edit Barang");
                Console.WriteLine("3. Lihat Semua Barang");
                Console.WriteLine("0. Kembali ke Menu Utama");
                Console.WriteLine("----------------------------------------");

                Console.Write("\nPilih menu: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        TambahBarangBaru();
                        break;
                    case "2":
                        EditBarang();
                        break;
                    case "3":
                        LihatSemuaBarang();
                        break;
                    case "0":
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
        private static void TambahBarangBaru()
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

                Barang barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier);

                bool sukses = _manager.TambahBarang(barangBaru);

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

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        private static void EditBarang()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT BARANG ===");
            Console.WriteLine("Daftar Barang Tersedia:");

            var daftarBarang = _manager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang tersedia.");
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

            var barangLama = _manager.GetBarangById(id);

            if (barangLama == null)
            {
                Console.WriteLine("Barang dengan ID tersebut tidak ditemukan.");
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
                string hargaBeliInput = Console.ReadLine();
                decimal hargaBeli = string.IsNullOrEmpty(hargaBeliInput) ? barangLama.HargaBeli : ValidasiInput.ValidasiDecimal(hargaBeliInput);

                Console.Write($"Harga Jual [{barangLama.HargaJual}]: ");
                string hargaJualInput = Console.ReadLine();
                decimal hargaJual = string.IsNullOrEmpty(hargaJualInput) ? barangLama.HargaJual : ValidasiInput.ValidasiDecimal(hargaJualInput);

                Console.Write($"Supplier [{barangLama.Supplier}]: ");
                string supplier = Console.ReadLine();
                supplier = string.IsNullOrEmpty(supplier) ? barangLama.Supplier : supplier;

                Barang barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
                {
                    Id = barangLama.Id,
                    TanggalMasuk = barangLama.TanggalMasuk
                };

                bool sukses = _manager.EditBarang(id, barangBaru);

                if (sukses)
                {
                    Console.WriteLine("\nBarang berhasil diperbarui!");
                }
                else
                {
                    Console.WriteLine("\nGagal memperbarui barang.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }

        private static void LihatSemuaBarang()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR SEMUA BARANG ===");

            var daftarBarang = _manager.GetSemuaBarang();

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
                        barang.HargaBeli,
                        barang.HargaJual,
                        barang.Supplier.Length > 12 ? barang.Supplier.Substring(0, 12) + "..." : barang.Supplier);
                }
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }
    }
}
