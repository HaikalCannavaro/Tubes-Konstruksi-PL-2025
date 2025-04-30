using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;

namespace AplikasiInventarisToko.Managers
{
    public static class ModulTransaksi
    {
        private static TransaksiManager _transaksiManager = new TransaksiManager(ModulBarang.Manager);

        public static void TransaksiBarangMasuk()
        {
            Console.Clear();
            Console.WriteLine("=== TRANSAKSI BARANG MASUK ===");

            try
            {
                // Tampilkan daftar barang
                var daftarBarang = ModulBarang.Manager.GetSemuaBarang();
                if (daftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang tersedia. Harap tambahkan barang terlebih dahulu.");
                    Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                    Console.ReadKey();
                    return;
                }

                ModulBarang.LihatSemuaBarang();

                Console.Write("\nMasukkan ID barang: ");
                string id = Console.ReadLine();

                Console.Write("Jumlah barang masuk: ");
                int jumlah = ValidasiInput.ValidasiAngka(Console.ReadLine());

                Console.Write("Keterangan (opsional): ");
                string keterangan = Console.ReadLine();

                bool sukses = _transaksiManager.TransaksiMasuk(id, jumlah, keterangan);

                if (sukses)
                {
                    Console.WriteLine("\nTransaksi barang masuk berhasil!");
                    var barang = ModulBarang.Manager.GetBarangById(id);
                    Console.WriteLine($"Stok {barang.Nama} sekarang: {barang.Stok}");
                }
                else
                {
                    Console.WriteLine("\nGagal melakukan transaksi barang masuk.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        public static void TransaksiBarangKeluar()
        {
            Console.Clear();
            Console.WriteLine("=== TRANSAKSI BARANG KELUAR ===");

            try
            {
                // Tampilkan daftar barang
                var daftarBarang = ModulBarang.Manager.GetSemuaBarang();
                if (daftarBarang.Count == 0)
                {
                    Console.WriteLine("Tidak ada barang tersedia. Harap tambahkan barang terlebih dahulu.");
                    Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                    Console.ReadKey();
                    return;
                }

                ModulBarang.LihatSemuaBarang();

                Console.Write("\nMasukkan ID barang: ");
                string id = Console.ReadLine();

                Console.Write("Jumlah barang keluar: ");
                int jumlah = ValidasiInput.ValidasiAngka(Console.ReadLine());

                Console.Write("Keterangan (opsional): ");
                string keterangan = Console.ReadLine();

                bool sukses = _transaksiManager.TransaksiKeluar(id, jumlah, keterangan);

                if (sukses)
                {
                    Console.WriteLine("\nTransaksi barang keluar berhasil!");
                    var barang = ModulBarang.Manager.GetBarangById(id);
                    Console.WriteLine($"Stok {barang.Nama} sekarang: {barang.Stok}");
                }
                else
                {
                    Console.WriteLine("\nGagal melakukan transaksi barang keluar.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        public static void LihatRiwayatTransaksi()
        {
            Console.Clear();
            Console.WriteLine("=== RIWAYAT TRANSAKSI ===");

            var transaksi = _transaksiManager.GetSemuaTransaksi();
            if (transaksi.Count == 0)
            {
                Console.WriteLine("Belum ada transaksi.");
            }
            else
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-8} {4,-25} {5,-30}",
                    "ID", "Jenis", "Barang ID", "Jumlah", "Tanggal", "Keterangan");
                Console.WriteLine(new string('-', 95));

                foreach (var t in transaksi)
                {
                    var barang = ModulBarang.Manager.GetBarangById(t.BarangId);
                    string namaBarang = barang != null ? barang.Nama : "Unknown";

                    Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-8} {4,-25} {5,-30}",
                        t.Id,
                        t.Jenis,
                        $"{t.BarangId} ({namaBarang})",
                        t.Jumlah,
                        t.Tanggal,
                        t.Keterangan);
                }
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
            Console.ReadKey();
        }
    }
}