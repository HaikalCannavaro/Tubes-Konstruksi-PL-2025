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

                Barang barangBaru = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier);

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
                    TanggalMasuk = barangLama.TanggalMasuk
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

        public static void LihatLaporanInventaris()
        {
            Console.Clear();
            Console.WriteLine("=== Laporan Inventaris Barang ===");

            Console.Write("Masukkan tanggal referensi (format: yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime tanggalReferensi))
            {
                Console.WriteLine("Format tanggal tidak valid.");
                Console.WriteLine("Tekan Enter untuk kembali...");
                Console.ReadLine();
                return;
            }

            var daftarBarang = _barangManager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data barang.");
            }
            else
            {
                foreach (var barang in daftarBarang)
                {
                    double selisihHari = (tanggalReferensi - barang.TanggalMasuk).TotalDays;
                    string status = selisihHari < 30 ? "Fast-moving" : "Slow-moving";

                    Console.WriteLine($"ID: {barang.Id}");
                    Console.WriteLine($"Nama: {barang.Nama}");
                    Console.WriteLine($"Tanggal Masuk: {barang.TanggalMasuk:dd-MM-yyyy}");
                    Console.WriteLine($"Selisih Hari: {selisihHari} hari");
                    Console.WriteLine($"Status: {status}");
                    Console.WriteLine("-------------------------------------");
                }
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }


        public static void ExportDataKeFile()
        {
            Console.Clear();
            Console.WriteLine("=== Export Data Barang ===");
            Console.Write("Masukkan nama file (contoh: data_barang.csv): ");
            string namaFile = Console.ReadLine();

            var daftarBarang = _barangManager.GetSemuaBarang();
            if (daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada data yang bisa diekspor.");
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(namaFile))
                {
                    writer.WriteLine("Id,Nama,Kategori,Supplier,HargaBeli,HargaJual,Stok,TanggalMasuk");
                    foreach (var barang in daftarBarang)
                    {
                        writer.WriteLine($"{barang.Id},{barang.Nama},{barang.Kategori},{barang.Supplier},{barang.HargaBeli},{barang.HargaJual},{barang.Stok},{barang.TanggalMasuk:yyyy-MM-dd}");
                    }
                }

                Console.WriteLine($"Data berhasil diekspor ke file '{namaFile}'.");
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }
    }
}
