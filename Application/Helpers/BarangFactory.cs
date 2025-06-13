using AplikasiInventarisToko.Models;

namespace AplikasiInventarisToko.Helpers
{
    public static class BarangFactory
    {
        public static Barang Create(string nama, string kategori, int stok, decimal hargaBeli, decimal hargaJual, string supplier)
        {
            var barang = new Barang(nama, kategori, stok, hargaBeli, hargaJual, supplier)
            {
                StokAwal = stok 
            };
            return barang;
        }
    }
}
