using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;

namespace AplikasiInventarisToko.API
{
    public static class InventarisAPI
    {
        private static BarangManager<Barang> _barangManager = ModulBarang.Manager;
        private static TransaksiManager _transaksiManager = new TransaksiManager(ModulBarang.Manager);

        // Barang API
        public static Barang GetBarang(string id)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(id), "ID barang tidak boleh kosong");
            return _barangManager.GetBarangById(id);
        }

        public static List<Barang> GetAllBarang()
        {
            return _barangManager.GetSemuaBarang();
        }

        public static Barang AddBarang(Barang barang)
        {
            Contract.Requires<ArgumentNullException>(barang != null, "Barang tidak boleh null");
            bool success = _barangManager.TambahBarang(barang);
            return success ? barang : null;
        }

        public static Barang UpdateBarang(string id, Barang barang)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(id), "ID barang tidak boleh kosong");
            Contract.Requires<ArgumentNullException>(barang != null, "Barang tidak boleh null");

            bool success = _barangManager.EditBarang(id, barang);
            return success ? barang : null;
        }

        // Transaksi API
        public static bool TransaksiMasuk(string barangId, int jumlah, string keterangan)
        {
            return _transaksiManager.TransaksiMasuk(barangId, jumlah, keterangan);
        }

        public static bool TransaksiKeluar(string barangId, int jumlah, string keterangan)
        {
            return _transaksiManager.TransaksiKeluar(barangId, jumlah, keterangan);
        }

        public static List<Transaksi> GetRiwayatTransaksi()
        {
            return _transaksiManager.GetSemuaTransaksi();
        }

        public static List<Transaksi> GetRiwayatTransaksiByBarang(string barangId)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
            return _transaksiManager.GetTransaksiByBarangId(barangId);
        }
    }
}