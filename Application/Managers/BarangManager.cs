using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiInventarisToko.Managers
{
    public class BarangManager<T> where T : Barang
    {
        private List<T> _daftarBarang;
        private Dictionary<string, Func<T, string, bool>> _pencarianTable;

        public BarangManager()
        {
            _daftarBarang = new List<T>();
            InisialisasiTabelPencarian();
        }
        private void InisialisasiTabelPencarian()
        {
            _pencarianTable = new Dictionary<string, Func<T, string, bool>>
            {
                { "id", (barang, nilai) => barang.Id.ToLower().Contains(nilai.ToLower()) },
                { "nama", (barang, nilai) => barang.Nama.ToLower().Contains(nilai.ToLower()) },
                { "kategori", (barang, nilai) => barang.Kategori.ToLower().Contains(nilai.ToLower()) },
                { "supplier", (barang, nilai) => barang.Supplier.ToLower().Contains(nilai.ToLower()) }
            };
        }

        public bool TambahBarang(T barang)
        {
            if (barang == null)
                throw new ArgumentNullException(nameof(barang), "Barang tidak boleh null");

            if (string.IsNullOrEmpty(barang.Nama))
                throw new ArgumentException("Nama barang tidak boleh kosong", nameof(barang.Nama));

            if (barang.Stok < 0)
                throw new ArgumentException("Stok barang tidak boleh negatif", nameof(barang.Stok));

            if (barang.HargaBeli <= 0)
                throw new ArgumentException("Harga beli harus lebih dari 0", nameof(barang.HargaBeli));

            if (barang.HargaJual <= 0)
                throw new ArgumentException("Harga jual harus lebih dari 0", nameof(barang.HargaJual));

            _daftarBarang.Add(barang);

            bool success = _daftarBarang.Contains(barang);
            return success;
        }

        public bool EditBarang(string id, T barangBaru)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID barang tidak boleh kosong", nameof(id));

            if (barangBaru == null)
                throw new ArgumentNullException(nameof(barangBaru), "Data barang baru tidak boleh null");

            int index = _daftarBarang.FindIndex(b => b.Id == id);
            if (index == -1)
                return false;

            _daftarBarang[index] = barangBaru;

            bool success = _daftarBarang[index].Equals(barangBaru);
            return success;
        }

        public List<T> CariBarang(string kriteria, string nilai)
        {
            if (string.IsNullOrEmpty(kriteria))
                throw new ArgumentException("Kriteria pencarian tidak boleh kosong", nameof(kriteria));

            if (string.IsNullOrEmpty(nilai))
                throw new ArgumentException("Nilai pencarian tidak boleh kosong", nameof(nilai));

            if (!_pencarianTable.ContainsKey(kriteria.ToLower()))
                throw new ArgumentException($"Kriteria pencarian '{kriteria}' tidak valid", nameof(kriteria));

            return _daftarBarang.Where(b => _pencarianTable[kriteria.ToLower()](b, nilai)).ToList();
        }

        public List<T> GetSemuaBarang()
        {
            return _daftarBarang;
        }

        public T GetBarangById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID barang tidak boleh kosong", nameof(id));

            foreach (var barang in _daftarBarang)
            {
                if (barang.Id == id)
                {
                    return barang;
                }
            }
            return null;
        }

        public bool HapusBarang(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID barang tidak boleh kosong", nameof(id));

            var barang = _daftarBarang.FirstOrDefault(b => b.Id == id);
            if (barang == null)
                return false;

            return _daftarBarang.Remove(barang);
        }
    }
}
