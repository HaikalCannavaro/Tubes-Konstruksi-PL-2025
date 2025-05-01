using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaksiController : ControllerBase
    {
        private readonly TransaksiManager _transaksiManager;

        public TransaksiController(TransaksiManager transaksiManager)
        {
            _transaksiManager = transaksiManager;
        }

        /// <summary>
        /// Mendapatkan semua transaksi
        /// </summary>
        /// <returns>Daftar semua transaksi</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transaksi>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Transaksi>> GetAll()
        {
            return Ok(_transaksiManager.GetSemuaTransaksi());
        }

        /// <summary>
        /// Mendapatkan transaksi berdasarkan ID barang
        /// </summary>
        /// <param name="barangId">ID barang</param>
        /// <returns>Daftar transaksi untuk barang tertentu</returns>
        [HttpGet("barang/{barangId}")]
        [ProducesResponseType(typeof(List<Transaksi>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Transaksi>> GetByBarangId(string barangId)
        {
            return Ok(_transaksiManager.GetTransaksiByBarangId(barangId));
        }

        /// <summary>
        /// Membuat transaksi barang masuk
        /// </summary>
        /// <param name="request">Data transaksi masuk</param>
        /// <returns>Status transaksi</returns>
        [HttpPost("masuk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult TransaksiMasuk([FromBody] TransaksiRequest request)
        {
            try
            {
                bool success = _transaksiManager.TransaksiMasuk(request.BarangId, request.Jumlah, request.Keterangan);
                if (success)
                    return Ok(new { message = "Transaksi masuk berhasil" });
                else
                    return BadRequest(new { message = "Gagal melakukan transaksi masuk" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Membuat transaksi barang keluar
        /// </summary>
        /// <param name="request">Data transaksi keluar</param>
        /// <returns>Status transaksi</returns>
        [HttpPost("keluar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult TransaksiKeluar([FromBody] TransaksiRequest request)
        {
            try
            {
                bool success = _transaksiManager.TransaksiKeluar(request.BarangId, request.Jumlah, request.Keterangan);
                if (success)
                    return Ok(new { message = "Transaksi keluar berhasil" });
                else
                    return BadRequest(new { message = "Gagal melakukan transaksi keluar" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    /// <summary>
    /// Data transfer object untuk request transaksi
    /// </summary>
    public class TransaksiRequest
    {
        /// <summary>
        /// ID Barang yang ditransaksikan
        /// </summary>
        public string BarangId { get; set; }

        /// <summary>
        /// Jumlah barang dalam transaksi
        /// </summary>
        public int Jumlah { get; set; }

        /// <summary>
        /// Keterangan transaksi
        /// </summary>
        public string Keterangan { get; set; }
    }
}
