using System;
using System.Threading.Tasks;

namespace AplikasiInventarisToko.Models
{
    public class Transaksi
    {
        public string Id { get; set; }
        public string BarangId { get; set; }
        public string Jenis { get; set; } // "MASUK" atau "KELUAR"
        public int Jumlah { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }

        public Transaksi()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            Tanggal = DateTime.Now;
        }

        public Transaksi(string barangId, string jenis, int jumlah, string keterangan)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            BarangId = barangId;
            Jenis = jenis;
            Jumlah = jumlah;
            Tanggal = DateTime.Now;
            Keterangan = keterangan;
        }
        public override string ToString()
        {
            return $"ID: {Id}, Barang ID: {BarangId}, Jenis: {Jenis}, Jumlah: {Jumlah}, Tanggal: {Tanggal.ToShortDateString()}, Keterangan: {Keterangan}";
        }
    }
}