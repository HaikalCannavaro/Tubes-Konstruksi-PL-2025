using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniTesting
{
    [TestClass]
    public class TransaksiManagerTests
    {
        private TransaksiManager _transaksiManager;
        private Mock<BarangManager<Barang>> _mockBarangManager;
        private List<Barang> _barangTestData;

        [TestInitialize]
        public void Setup()
        {
            _barangTestData = new List<Barang>
            {
                new Barang { Id = "B001", Nama = "Test Barang 1", Stok = 10, StokAwal = 10, HargaJual = 5000 },
                new Barang { Id = "B002", Nama = "Test Barang 2", Stok = 5, StokAwal = 5, HargaJual = 10000 }
            };

            _mockBarangManager = new Mock<BarangManager<Barang>>();

            _mockBarangManager.Setup(m => m.GetBarangById("B001"))
                .Returns(_barangTestData.FirstOrDefault(b => b.Id == "B001"));

            _mockBarangManager.Setup(m => m.GetBarangById("B002"))
                .Returns(_barangTestData.FirstOrDefault(b => b.Id == "B002"));

            _mockBarangManager.Setup(m => m.GetBarangById("NonExistentId"))
                .Returns((Barang)null);

            _transaksiManager = new TransaksiManager(_mockBarangManager.Object);
        }

        [TestMethod]
        public void TransaksiMasuk_ValidInput_IncreasesStock()
        {
            string barangId = "B001";
            int jumlahMasuk = 5;
            string keterangan = "Test masuk";
            int stokAwal = _barangTestData.First(b => b.Id == barangId).Stok;
            int stokAwalValue = _barangTestData.First(b => b.Id == barangId).StokAwal;

            bool result = _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);

            Assert.IsTrue(result);
            Assert.AreEqual(stokAwal + jumlahMasuk, _barangTestData.First(b => b.Id == barangId).Stok);
            Assert.AreEqual(stokAwalValue + jumlahMasuk, _barangTestData.First(b => b.Id == barangId).StokAwal);
            Assert.AreEqual(1, _transaksiManager.GetTransaksiByBarangId(barangId).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransaksiMasuk_NonExistentBarang_ThrowsException()
        {
            string barangId = "NonExistentId";
            int jumlahMasuk = 5;
            string keterangan = "Test masuk";

            _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TransaksiMasuk_NegativeAmount_ThrowsArgumentException()
        {
            string barangId = "B001";
            int jumlahMasuk = -5;
            string keterangan = "Test masuk negatif";

            _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TransaksiMasuk_EmptyBarangId_ThrowsArgumentException()
        {
            string barangId = "";
            int jumlahMasuk = 5;
            string keterangan = "Test masuk";

            _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);
        }

        [TestMethod]
        public void TransaksiKeluar_ValidInput_DecreasesStock()
        {
            string barangId = "B001";
            int jumlahKeluar = 3;
            string keterangan = "Test keluar";
            int stokAwal = _barangTestData.First(b => b.Id == barangId).Stok;

            bool result = _transaksiManager.TransaksiKeluar(barangId, jumlahKeluar, keterangan);

            Assert.IsTrue(result);
            Assert.AreEqual(stokAwal - jumlahKeluar, _barangTestData.First(b => b.Id == barangId).Stok);
            Assert.AreEqual(1, _transaksiManager.GetTransaksiByBarangId(barangId).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransaksiKeluar_InsufficientStock_ThrowsException()
        {
            string barangId = "B001";
            int jumlahKeluar = 20;
            string keterangan = "Test keluar";

            _transaksiManager.TransaksiKeluar(barangId, jumlahKeluar, keterangan);
        }

        [TestMethod]
        public void GetSemuaTransaksi_ReturnsAllTransactionsOrderedByDate()
        {
            _transaksiManager.TransaksiMasuk("B001", 5, "Test masuk 1");
            _transaksiManager.TransaksiKeluar("B001", 2, "Test keluar 1");
            _transaksiManager.TransaksiMasuk("B002", 3, "Test masuk 2");

            var transaksi = _transaksiManager.GetSemuaTransaksi();

            Assert.AreEqual(3, transaksi.Count);
            Assert.IsTrue(transaksi[0].Tanggal >= transaksi[1].Tanggal);
            Assert.IsTrue(transaksi[1].Tanggal >= transaksi[2].Tanggal);
        }

        [TestMethod]
        public void GetTransaksiByBarangId_ReturnsFilteredTransactionsOrderedByDate()
        {
            _transaksiManager.TransaksiMasuk("B001", 5, "Test masuk 1");
            _transaksiManager.TransaksiKeluar("B001", 2, "Test keluar 1");
            _transaksiManager.TransaksiMasuk("B002", 3, "Test masuk 2");

            var transaksiB001 = _transaksiManager.GetTransaksiByBarangId("B001");

            Assert.AreEqual(2, transaksiB001.Count);
            Assert.IsTrue(transaksiB001.All(t => t.BarangId == "B001"));
            // Test that transactions are ordered descending by date
            Assert.IsTrue(transaksiB001[0].Tanggal >= transaksiB001[1].Tanggal);
        }
    }
}