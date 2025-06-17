using AplikasiInventarisToko.Models;

namespace AplikasiInventarisToko.Helpers
{
    public static class BarangFactory
    {
        public static IBarang Create(string nama, string kategori, int stok, decimal hargaBeli, decimal hargaJual, string supplier)
        {
            // Implementasi dasar untuk saat ini
            // Jika ada logika pembuatan berbeda berdasarkan kategori, bisa ditambahkan di sini
            return new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier);
        }
    }
}