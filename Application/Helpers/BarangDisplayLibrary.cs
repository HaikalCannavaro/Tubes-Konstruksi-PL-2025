using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;
using System.Collections.Generic;

namespace AplikasiInventarisToko.Helpers
{
    public static class BarangDisplayHelper
    {
        public static void TampilkanDaftarBarang(List<Barang> daftarBarang, bool showFullDetails = true)
        {
            var config = KonfigurasiAplikasi.Load();

            if (daftarBarang == null || daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang tersedia.");
                return;
            }

            if (showFullDetails)
            {
                TampilkanTabelLengkap(daftarBarang, config);
            }
            else
            {
                TampilkanTabelSingkat(daftarBarang);
            }
        }

        private static void TampilkanTabelLengkap(List<Barang> daftarBarang, dynamic config)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                "ID", "Nama", "Kategori", "Stok", "Harga Beli", "Harga Jual", "Supplier");
            Console.WriteLine(new string('-', 95));

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8} {4,-12} {5,-12} {6,-15}",
                    barang.Id,
                    TruncateString(barang.Nama, 17),
                    TruncateString(barang.Kategori, 12),
                    barang.Stok,
                    StokHelper.FormatCurrency(barang.HargaBeli, config),
                    StokHelper.FormatCurrency(barang.HargaJual, config),
                    TruncateString(barang.Supplier, 12));
            }
        }

        private static void TampilkanTabelSingkat(List<Barang> daftarBarang)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                "ID", "Nama", "Kategori", "Stok");
            Console.WriteLine(new string('-', 55));

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-8}",
                    barang.Id,
                    TruncateString(barang.Nama, 17),
                    TruncateString(barang.Kategori, 12),
                    barang.Stok);
            }
        }

        private static string TruncateString(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Length > maxLength ? text.Substring(0, maxLength - 3) + "..." : text;
        }

        public static void TampilkanDaftarPilihan(List<Barang> daftarBarang)
        {
            if (daftarBarang == null || daftarBarang.Count == 0)
            {
                Console.WriteLine("Tidak ada barang tersedia.");
                return;
            }

            foreach (var barang in daftarBarang)
            {
                Console.WriteLine($"[{barang.Id}] {barang.Nama} - Stok: {barang.Stok}");
            }
        }

        public static void TampilkanDetailBarang(Barang barang)
        {
            if (barang == null)
            {
                Console.WriteLine("Data barang tidak tersedia.");
                return;
            }

            var config = KonfigurasiAplikasi.Load();

            Console.WriteLine($"ID: {barang.Id}");
            Console.WriteLine($"Nama: {barang.Nama}");
            Console.WriteLine($"Kategori: {barang.Kategori}");
            Console.WriteLine($"Stok: {barang.Stok}");
            Console.WriteLine($"Harga Beli: {StokHelper.FormatCurrency(barang.HargaBeli, config)}");
            Console.WriteLine($"Harga Jual: {StokHelper.FormatCurrency(barang.HargaJual, config)}");
            Console.WriteLine($"Supplier: {barang.Supplier}");
            Console.WriteLine($"Tanggal Masuk: {barang.TanggalMasuk}");
        }
    }
}