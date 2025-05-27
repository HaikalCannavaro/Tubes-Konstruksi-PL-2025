using AplikasiInventarisToko.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniTesting
{
    [TestClass]
    public class ModulBarangTest
    {
        [TestMethod]
        public void BuatBarangBaru_InputValid_ReturnsBarang()
        {
            // inisialisasi awal
            string nama = "Pensil";
            string kategori = "ATK";
            string stokInput = "50";
            string hargaBeliInput = "1000";
            string hargaJualInput = "1500";
            string supplier = "Toko ABC";

            // Aksinya
            var barang = ModulBarang.BuatBarangBaru(
                nama, kategori, stokInput, hargaBeliInput, hargaJualInput, supplier);

            // Assert
            Assert.AreEqual(nama, barang.Nama);
            Assert.AreEqual(kategori, barang.Kategori);
            Assert.AreEqual(50, barang.Stok);
            Assert.AreEqual(1000m, barang.HargaBeli);
            Assert.AreEqual(1500m, barang.HargaJual);
            Assert.AreEqual(supplier, barang.Supplier);
            Assert.AreEqual(50, barang.StokAwal);
        }
    }
}
