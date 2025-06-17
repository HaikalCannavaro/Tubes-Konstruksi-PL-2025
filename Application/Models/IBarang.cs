using System;

namespace AplikasiInventarisToko.Models
{
    // Mendefinisikan interface IBarang
    public interface IBarang
    {
        string Id { get; }
        string Nama { get; set; }
        string Kategori { get; set; }
        int Stok { get; set; }
        int StokAwal { get; set; }
        decimal HargaBeli { get; set; }
        decimal HargaJual { get; set; }
        DateTime TanggalMasuk { get; set; }
        string Supplier { get; set; }

        string ToString();
    }
}