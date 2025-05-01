using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarangController : ControllerBase
    {
        private readonly BarangManager<Barang> _barangManager;

        public BarangController(BarangManager<Barang> barangManager)
        {
            _barangManager = barangManager;
        }

        /// <summary>
        /// Mendapatkan semua barang di inventaris
        /// </summary>
        /// <returns>Daftar semua barang</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Barang>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Barang>> GetAll()
        {
            return Ok(_barangManager.GetSemuaBarang());
        }

        /// <summary>
        /// Mendapatkan barang berdasarkan ID
        /// </summary>
        /// <param name="id">ID barang</param>
        /// <returns>Detail barang</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Barang), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Barang> GetById(string id)
        {
            var barang = _barangManager.GetBarangById(id);
            if (barang == null)
                return NotFound(new { message = "Barang tidak ditemukan" });

            return Ok(barang);
        }

        /// <summary>
        /// Mencari barang berdasarkan kriteria
        /// </summary>
        /// <param name="kriteria">Kriteria pencarian (id, nama, kategori, supplier)</param>
        /// <param name="nilai">Nilai pencarian</param>
        /// <returns>Daftar barang yang sesuai</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<Barang>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Barang>> Search([FromQuery] string kriteria, [FromQuery] string nilai)
        {
            try
            {
                return Ok(_barangManager.CariBarang(kriteria, nilai));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Menambahkan barang baru
        /// </summary>
        /// <param name="barang">Data barang baru</param>
        /// <returns>Hasil penambahan barang</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Barang), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Barang> Create([FromBody] Barang barang)
        {
            try
            {
                bool success = _barangManager.TambahBarang(barang);
                if (success)
                    return CreatedAtAction(nameof(GetById), new { id = barang.Id }, barang);
                else
                    return BadRequest(new { message = "Gagal menambahkan barang" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Memperbarui data barang
        /// </summary>
        /// <param name="id">ID barang</param>
        /// <param name="barang">Data barang terbaru</param>
        /// <returns>Hasil pembaruan barang</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(string id, [FromBody] Barang barang)
        {
            try
            {
                if (id != barang.Id)
                    return BadRequest(new { message = "ID tidak cocok" });

                bool success = _barangManager.EditBarang(id, barang);
                if (!success)
                    return NotFound(new { message = "Barang tidak ditemukan" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Menghapus barang
        /// </summary>
        /// <param name="id">ID barang</param>
        /// <returns>Hasil penghapusan barang</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(string id)
        {
            try
            {
                bool success = _barangManager.HapusBarang(id);
                if (!success)
                    return NotFound(new { message = "Barang tidak ditemukan" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
