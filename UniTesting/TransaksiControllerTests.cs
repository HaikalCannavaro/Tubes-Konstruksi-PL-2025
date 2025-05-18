using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniTesting
{
    [TestClass]
    public class TransaksiControllerTests
    {
        private TransaksiController _controller;
        private Mock<TransaksiManager> _mockTransaksiManager;
        private List<Transaksi> _transaksiTestData;

        [TestInitialize]
        public void Setup()
        {
            _transaksiTestData = new List<Transaksi>
            {
                new Transaksi
                {
                    Id = "T001",
                    BarangId = "B001",
                    Tanggal = DateTime.Now.AddDays(-5),
                    Jumlah = 10,
                    Jenis = "MASUK",
                    Keterangan = "Pembelian awal"
                },
                new Transaksi
                {
                    Id = "T002",
                    BarangId = "B001",
                    Tanggal = DateTime.Now.AddDays(-2),
                    Jumlah = 5,
                    Jenis = "KELUAR",
                    Keterangan = "Penjualan"
                },
                new Transaksi
                {
                    Id = "T003",
                    BarangId = "B002",
                    Tanggal = DateTime.Now.AddDays(-1),
                    Jumlah = 8,
                    Jenis = "MASUK",
                    Keterangan = "Restock"
                }
            };

            _mockTransaksiManager = new Mock<TransaksiManager>(MockBehavior.Loose, new Mock<BarangManager<Barang>>().Object);

            _mockTransaksiManager.Setup(m => m.GetSemuaTransaksi())
                .Returns(_transaksiTestData);

            _mockTransaksiManager.Setup(m => m.GetTransaksiByBarangId("B001"))
                .Returns(_transaksiTestData.Where(t => t.BarangId == "B001").OrderByDescending(t => t.Tanggal).ToList());

            _mockTransaksiManager.Setup(m => m.GetTransaksiByBarangId("B002"))
                .Returns(_transaksiTestData.Where(t => t.BarangId == "B002").OrderByDescending(t => t.Tanggal).ToList());

            _mockTransaksiManager.Setup(m => m.TransaksiMasuk("B001", 5, "Test masuk"))
                .Returns(true);

            _mockTransaksiManager.Setup(m => m.TransaksiMasuk("NonExistentId", It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception("Barang tidak ditemukan"));

            _mockTransaksiManager.Setup(m => m.TransaksiMasuk("ErrorId", It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception("Simulasi error"));

            _mockTransaksiManager.Setup(m => m.TransaksiKeluar("B001", 3, "Test keluar"))
                .Returns(true);

            _mockTransaksiManager.Setup(m => m.TransaksiKeluar("B001", 100, It.IsAny<string>()))
                .Throws(new Exception("Stok tidak cukup"));

            _mockTransaksiManager.Setup(m => m.TransaksiKeluar("ErrorId", It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception("Simulasi error"));

            _controller = new TransaksiController(_mockTransaksiManager.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsOkResultWithAllTransaksi()
        {
            var result = _controller.GetAll();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Transaksi>));
            var transaksiList = okResult.Value as List<Transaksi>;
            Assert.AreEqual(3, transaksiList.Count);
        }

        [TestMethod]
        public void GetByBarangId_ReturnsFilteredTransaksi()
        {
            var result = _controller.GetByBarangId("B001");

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Transaksi>));
            var transaksiList = okResult.Value as List<Transaksi>;
            Assert.AreEqual(2, transaksiList.Count);
            Assert.IsTrue(transaksiList.All(t => t.BarangId == "B001"));
        }

        [TestMethod]
        public void TransaksiMasuk_ValidRequest_ReturnsOk()
        {
            var request = new TransaksiRequest
            {
                BarangId = "B001",
                Jumlah = 5,
                Keterangan = "Test masuk"
            };

            var result = _controller.TransaksiMasuk(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            dynamic response = okResult.Value;
            Assert.AreEqual("Transaksi masuk berhasil", response.message.ToString());
        }

        [TestMethod]
        public void TransaksiMasuk_NonExistentBarang_ReturnsBadRequest()
        {
            var request = new TransaksiRequest
            {
                BarangId = "NonExistentId",
                Jumlah = 5,
                Keterangan = "Test masuk"
            };

            var result = _controller.TransaksiMasuk(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            dynamic response = badRequestResult.Value;
            Assert.AreEqual("Barang tidak ditemukan", response.message.ToString());
        }

        [TestMethod]
        public void TransaksiMasuk_ErrorOccurs_ReturnsBadRequest()
        {
            var request = new TransaksiRequest
            {
                BarangId = "ErrorId",
                Jumlah = 5,
                Keterangan = "Test masuk"
            };

            var result = _controller.TransaksiMasuk(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            dynamic response = badRequestResult.Value;
            Assert.AreEqual("Simulasi error", response.message.ToString());
        }

        [TestMethod]
        public void TransaksiKeluar_ValidRequest_ReturnsOk()
        {
            var request = new TransaksiRequest
            {
                BarangId = "B001",
                Jumlah = 3,
                Keterangan = "Test keluar"
            };

            var result = _controller.TransaksiKeluar(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            dynamic response = okResult.Value;
            Assert.AreEqual("Transaksi keluar berhasil", response.message.ToString());
        }

        [TestMethod]
        public void TransaksiKeluar_InsufficientStock_ReturnsBadRequest()
        {
            var request = new TransaksiRequest
            {
                BarangId = "B001",
                Jumlah = 100,
                Keterangan = "Test keluar"
            };

            var result = _controller.TransaksiKeluar(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            dynamic response = badRequestResult.Value;
            Assert.AreEqual("Stok tidak cukup", response.message.ToString());
        }

        [TestMethod]
        public void TransaksiKeluar_ErrorOccurs_ReturnsBadRequest()
        {
            var request = new TransaksiRequest
            {
                BarangId = "ErrorId",
                Jumlah = 3,
                Keterangan = "Test keluar"
            };

            var result = _controller.TransaksiKeluar(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            dynamic response = badRequestResult.Value;
            Assert.AreEqual("Simulasi error", response.message.ToString());
        }
    }
}