using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaporanController : ControllerBase
    {
        private readonly BarangManager<Barang> _barangManager;
        private readonly TransaksiManager _transaksiManager;
        private readonly KonfigurasiAplikasi _config;

        public LaporanController(BarangManager<Barang> barangManager, TransaksiManager transaksiManager)
        {
            _barangManager = barangManager;
            _transaksiManager = transaksiManager;
            _config = KonfigurasiAplikasi.Load();
        }

        /// <summary>
        /// Mendapatkan laporan inventaris barang
        /// </summary>
        /// <returns>Daftar laporan inventaris</returns>
        [HttpGet("inventaris")]
        [ProducesResponseType(typeof(List<LaporanInventaris>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<LaporanInventaris>> GetLaporanInventaris()
        {
            var daftarBarang = _barangManager.GetSemuaBarang();
            var hasil = new List<LaporanInventaris>();

            foreach (var barang in daftarBarang)
            {
                int stokAwal = StokHelper.HitungStokAwal(barang, _transaksiManager);
                double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                string status = StokHelper.TentukanStatus(barang, persentaseStok, _config);
                int lamaDiGudang = (int)(DateTime.Now - barang.TanggalMasuk).TotalDays;

                hasil.Add(new LaporanInventaris
                {
                    Id = barang.Id,
                    Nama = barang.Nama,
                    Kategori = barang.Kategori,
                    Supplier = barang.Supplier,
                    StokSekarang = barang.Stok,
                    StokAwal = stokAwal,
                    PersentaseStok = persentaseStok,
                    HargaBeli = barang.HargaBeli,
                    HargaJual = barang.HargaJual,
                    TanggalMasuk = barang.TanggalMasuk,
                    LamaDiGudang = lamaDiGudang,
                    Status = status
                });
            }

            return Ok(hasil);
        }

        /// <summary>
        /// Mengekspor data inventaris dalam format CSV
        /// </summary>
        /// <returns>File CSV laporan inventaris</returns>
        [HttpGet("export-csv")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public ActionResult ExportCsv()
        {
            var daftarBarang = _barangManager.GetSemuaBarang();

            if (daftarBarang.Count == 0)
            {
                return BadRequest(new { message = "Tidak ada data untuk diekspor" });
            }

            var sb = new StringBuilder();
            sb.AppendLine("Id,Nama,Kategori,Supplier,HargaBeli,HargaJual,StokSekarang,StokAwal,Persentase,Status");

            foreach (var barang in daftarBarang)
            {
                int stokAwal = StokHelper.HitungStokAwal(barang, _transaksiManager);
                double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                string status = StokHelper.TentukanStatus(barang, persentaseStok, _config);

                sb.AppendLine($"{barang.Id},{barang.Nama},{barang.Kategori},{barang.Supplier}," +
                             $"{StokHelper.FormatCurrency(barang.HargaBeli, _config)}," +
                             $"{StokHelper.FormatCurrency(barang.HargaJual, _config)}," +
                             $"{barang.Stok},{stokAwal},{persentaseStok:N0},{status}");
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            string fileName = $"data_barang_{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.csv";

            return File(fileBytes, "text/csv", fileName);
        }
    }

    /// <summary>
    /// Model untuk laporan inventaris barang
    /// </summary>
    public class LaporanInventaris
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Supplier { get; set; }
        public int StokSekarang { get; set; }
        public int StokAwal { get; set; }
        public double PersentaseStok { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public DateTime TanggalMasuk { get; set; }
        public int LamaDiGudang { get; set; }
        public string Status { get; set; }
    }
}
