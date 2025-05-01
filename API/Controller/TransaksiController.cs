using Microsoft.AspNetCore.Mvc;
using AplikasiInventarisToko.Modules;
using AplikasiInventarisToko.Models;

namespace AplikasiInventarisToko.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaksiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var daftar = ModulTransaksi.GetSemuaTransaksi();
            return Ok(daftar);
        }

        [HttpPost("masuk")]
        public IActionResult BarangMasuk([FromBody] Transaksi data)
        {
            bool sukses = ModulTransaksi.CreateTransaksiMasuk(data);
            return sukses ? Ok() : BadRequest("Gagal transaksi masuk.");
        }

        [HttpPost("keluar")]
        public IActionResult BarangKeluar([FromBody] Transaksi data)
        {
            bool sukses = ModulTransaksi.CreateTransaksiKeluar(data);
            return sukses ? Ok() : BadRequest("Gagal transaksi keluar.");
        }
    }
}
