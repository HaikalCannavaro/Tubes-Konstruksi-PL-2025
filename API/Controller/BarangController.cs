using Microsoft.AspNetCore.Mvc;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Managers;

namespace AplikasiInventarisToko.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarangController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var daftar = ModulBarang.Manager.GetSemuaBarang();
            return Ok(daftar);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var barang = ModulBarang.Manager.GetBarangById(id);
            return barang != null ? Ok(barang) : NotFound();
        }

        [HttpPost]
        public IActionResult Create(Barang barang)
        {
            bool sukses = ModulBarang.Manager.TambahBarang(barang);
            return sukses ? Ok(barang) : BadRequest("Gagal menambahkan barang.");
        }
    }
}
