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

        // Enum State untuk Automata
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
                // State 1: Validasi input
                Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
                Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");

                // State 2: Cari barang
                barang = _barangManager.GetBarangById(barangId);
                if (barang == null)
                    throw new ArgumentException("Barang tidak ditemukan");
                state = TransaksiState.BarangDitemukan;

                // State 3: Proses transaksi
                barang.Stok += jumlah;
                barang.StokAwal += jumlah;
                state = TransaksiState.StokValid;

                // State 4: Simpan transaksi
                var transaksi = new Transaksi(barangId, "MASUK", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);
                state = TransaksiState.TransaksiBerhasil;

                return true;
            }
            catch (Exception ex)
            {
                // Rollback jika gagal di state tertentu
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
                // State 1: Validasi input
                Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(barangId), "ID barang tidak boleh kosong");
                Contract.Requires<ArgumentException>(jumlah > 0, "Jumlah harus lebih dari 0");
                state = TransaksiState.BarangDitemukan;

                // State 2: Cari barang
                barang = _barangManager.GetBarangById(barangId);
                if (barang == null)
                    throw new ArgumentException("Barang tidak ditemukan");
                state = TransaksiState.BarangDitemukan;

                // State 3: Cek stok
                if (barang.Stok < jumlah)
                    throw new InvalidOperationException("Stok tidak cukup");
                state = TransaksiState.StokValid;

                // State 4: Proses transaksi
                barang.Stok -= jumlah;
                state = TransaksiState.StokValid;

                // State 5: Simpan transaksi
                var transaksi = new Transaksi(barangId, "KELUAR", jumlah, keterangan);
                _daftarTransaksi.Add(transaksi);
                state = TransaksiState.TransaksiBerhasil;

                return true;
            }
            catch (Exception ex)
            {
                // Rollback jika gagal setelah mengurangi stok
                if (state == TransaksiState.StokValid && barang != null)
                {
                    barang.Stok += jumlah;
                }

                throw new Exception($"Gagal transaksi (State: {state}): {ex.Message}");
            }
        }

        // Method lainnya tetap sama...
        public List<Transaksi> GetSemuaTransaksi() => _daftarTransaksi.OrderByDescending(t => t.Tanggal).ToList();

        public List<Transaksi> GetTransaksiByBarangId(string barangId) =>
            _daftarTransaksi.Where(t => t.BarangId == barangId).OrderByDescending(t => t.Tanggal).ToList();
    }
}