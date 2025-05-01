using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using System;
using System.Globalization;
using System.Linq;

namespace AplikasiInventarisToko.Utils
{
    public static class StokHelper
    {
        public static int HitungStokAwal(Barang barang, TransaksiManager transaksiManager)
        {
            return barang.StokAwal == 0 ? barang.Stok : barang.StokAwal;
        }

        public static int HitungStokAwal(Barang barang, List<Transaksi> transaksiList)
        {
            if (barang == null || transaksiList == null) return 0;

            var masuk = transaksiList
                .Where(t => t.BarangId == barang.Id && t.Jenis.ToLower() == "masuk")
                .Sum(t => t.Jumlah);

            var keluar = transaksiList
                .Where(t => t.BarangId == barang.Id && t.Jenis.ToLower() == "keluar")
                .Sum(t => t.Jumlah);

            return barang.Stok + keluar - masuk;
        }



        public static double HitungPersentaseStok(Barang barang, int stokAwal)
        {
            return stokAwal == 0 ? 0 : ((double)barang.Stok / stokAwal) * 100;
        }

        public static string TentukanStatus(Barang barang, double persentaseStok, KonfigurasiAplikasi config)
        {
            double selisihHari = (DateTime.Now - barang.TanggalMasuk).TotalDays;

            if (selisihHari < config.KategoriThreshold.BelumTerkategoriHari)
            {
                return "Belum Terkategori";
            }
            else if (selisihHari >= config.KategoriThreshold.FastMoving.MinHari &&
                     persentaseStok < config.KategoriThreshold.FastMoving.MaxPersentaseStok)
            {
                return "Fast-moving";
            }
            else if (selisihHari >= config.KategoriThreshold.SlowMoving.MinHari &&
                     persentaseStok >= config.KategoriThreshold.SlowMoving.MinPersentaseStok)
            {
                return "Slow-moving";
            }
            else
            {
                return "Normal";
            }
        }

        public static string FormatCurrency(decimal angka, KonfigurasiAplikasi config)
        {
            var selectedCurrency = config.CurrencySetting.Options[config.CurrencySetting.Current];
            var culture = new CultureInfo(selectedCurrency.Culture);
            var symbol = selectedCurrency.Symbol;

            return symbol + " " + angka.ToString("N0", culture);
        }
    }
}
