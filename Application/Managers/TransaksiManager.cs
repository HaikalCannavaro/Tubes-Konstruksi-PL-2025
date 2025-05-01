using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AplikasiInventarisToko.Managers
{
    public class TransaksiManager
    {
        private List<Transaksi> _daftarTransaksi;
        private BarangManager<Barang> _barangManager;

        private enum TransaksiState
        {
            Idle,
            BarangDitemukan,
            StokValid,
            TransaksiBerhasil,
            Error
        }

        public TransaksiManager(BarangManager<Barang> barangManager)
        {
            _daftarTransaksi = new List<Transaksi>();
            _barangManager = barangManager ?? throw new ArgumentNullException(nameof(barangManager));
        }

        public bool TransaksiMasuk(string barangId, int jumlah, string keterangan)
        {
            TransaksiState state = TransaksiState.Idle;
            Barang barang = null;

            try
            {
                Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
                Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");

                barang = _barangManager.GetBarangById(barangId);
                if (barang == null)
                    throw new ArgumentException("Barang tidak ditemukan");
                state = TransaksiState.BarangDitemukan;

                barang.Stok += jumlah;
                barang.StokAwal += jumlah;
                state = TransaksiState.StokValid;

                var transaksi = new Transaksi(barangId, "MASUK", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);
                state = TransaksiState.TransaksiBerhasil;

                return true;
            }
            catch (Exception ex)
            {
                if (state == TransaksiState.StokValid && barang != null)
                {
                    barang.Stok -= jumlah;
                    barang.StokAwal -= jumlah;
                }

                throw new Exception($"Gagal transaksi (State: {state}): {ex.Message}");
            }
        }

        public bool TransaksiKeluar(string barangId, int jumlah, string keterangan)
        {
            TransaksiState state = TransaksiState.Idle;
            Barang barang = null;

            try
            {
                Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
                Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");
                state = TransaksiState.BarangDitemukan;

                barang = _barangManager.GetBarangById(barangId);
                if (barang == null)
                    throw new ArgumentException("Barang tidak ditemukan");
                state = TransaksiState.BarangDitemukan;

                if (barang.Stok < jumlah)
                    throw new InvalidOperationException("Stok tidak cukup");
                state = TransaksiState.StokValid;

                barang.Stok -= jumlah;
                state = TransaksiState.StokValid;

                var transaksi = new Transaksi(barangId, "KELUAR", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);
                state = TransaksiState.TransaksiBerhasil;

                return true;
            }
            catch (Exception ex)
            {
                if (state == TransaksiState.StokValid && barang != null)
                {
                    barang.Stok += jumlah;
                }

                throw new Exception($"Gagal transaksi (State: {state}): {ex.Message}");
            }
        }

        public List<Transaksi> GetSemuaTransaksi() => _daftarTransaksi.OrderByDescending(t => t.Tanggal).ToList();

        public List<Transaksi> GetTransaksiByBarangId(string barangId) =>
            _daftarTransaksi.Where(t => t.BarangId == barangId).OrderByDescending(t => t.Tanggal).ToList();
    }
}