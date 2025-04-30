using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AplikasiInventarisToko.Managers
{
    public class TransaksiManager
    {
        private List<Transaksi> _daftarTransaksi;
        private BarangManager<Barang> _barangManager;

        public TransaksiManager(BarangManager<Barang> barangManager)
        {
            _daftarTransaksi = new List<Transaksi>();
            _barangManager = barangManager ?? throw new ArgumentNullException(nameof(barangManager));
        }

        public bool TransaksiMasuk(string barangId, int jumlah, string keterangan)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
            Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");

            var barang = _barangManager.GetBarangById(barangId);
            if (barang == null)
                throw new ArgumentException("Barang tidak ditemukan");

            try
            {
                // Buat transaksi
                var transaksi = new Transaksi(barangId, "MASUK", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);

                // Update stok barang
                var barangBaru = new Barang(
                    barang.Nama,
                    barang.Kategori,
                    barang.Stok + jumlah,
                    barang.HargaBeli,
                    barang.HargaJual,
                    barang.Supplier)
                {
                    Id = barang.Id,
                    TanggalMasuk = barang.TanggalMasuk
                };

                return _barangManager.EditBarang(barangId, barangBaru);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal melakukan transaksi masuk: {ex.Message}");
            }
        }

        public bool TransaksiKeluar(string barangId, int jumlah, string keterangan)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
            Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");

            var barang = _barangManager.GetBarangById(barangId);
            if (barang == null)
                throw new ArgumentException("Barang tidak ditemukan");

            if (barang.Stok < jumlah)
                throw new InvalidOperationException("Stok barang tidak mencukupi");

            try
            {
                // Buat transaksi
                var transaksi = new Transaksi(barangId, "KELUAR", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);

                // Update stok barang
                var barangBaru = new Barang(
                    barang.Nama,
                    barang.Kategori,
                    barang.Stok - jumlah,
                    barang.HargaBeli,
                    barang.HargaJual,
                    barang.Supplier)
                {
                    Id = barang.Id,
                    TanggalMasuk = barang.TanggalMasuk
                };

                return _barangManager.EditBarang(barangId, barangBaru);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal melakukan transaksi keluar: {ex.Message}");
            }
        }

        public List<Transaksi> GetSemuaTransaksi()
        {
            return _daftarTransaksi.OrderByDescending(t => t.Tanggal).ToList();
        }

        public List<Transaksi> GetTransaksiByBarangId(string barangId)
        {
            return _daftarTransaksi
                .Where(t => t.BarangId == barangId)
                .OrderByDescending(t => t.Tanggal)
                .ToList();
        }
    }
}