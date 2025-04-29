using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiInventarisToko.Models
{
    public class Barang
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public int Stok { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public DateTime TanggalMasuk { get; set; }
        public string Supplier { get; set; }

        public Barang()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            TanggalMasuk = DateTime.Now;
        }

        public Barang(string nama, string kategori, int stok, decimal hargaBeli, decimal hargaJual, string supplier)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            Nama = nama;
            Kategori = kategori;
            Stok = stok;
            HargaBeli = hargaBeli;
            HargaJual = hargaJual;
            TanggalMasuk = DateTime.Now;
            Supplier = supplier;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nama: {Nama}, Kategori: {Kategori}, Stok: {Stok}, Harga Beli: {HargaBeli}, Harga Jual: {HargaJual}, Tanggal Masuk: {TanggalMasuk.ToShortDateString()}, Supplier: {Supplier}";
        }
    }
}
