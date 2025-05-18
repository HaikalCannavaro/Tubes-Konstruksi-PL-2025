using Microsoft.VisualStudio.TestTools.UnitTesting;
using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UniTesting
{
    [TestClass]
    public class TransaksiManagerTests
    {
        private BarangManager<Barang> _barangManager;
        private TransaksiManager _transaksiManager;
        private Barang _testBarang;

        [TestInitialize]
        public void Setup()
        {
            _barangManager = new BarangManager<Barang>();
            _transaksiManager = new TransaksiManager(_barangManager);

            _testBarang = new Barang
            {
                Id = "TEST001",
                Nama = "Barang Test",
                Kategori = "Test",
                Stok = 10,
                StokAwal = 10,
                HargaBeli = 5000,
                HargaJual = 7500,
                Supplier = "Supplier Test"
            };

            _barangManager.TambahBarang(_testBarang);
        }

        [TestMethod]
        public void TransaksiMasuk_ValidInput_ShouldIncreaseStockAndAddTransaction()
        {
            string barangId = "TEST001";
            int jumlahMasuk = 5;
            string keterangan = "Restok barang";
            int stokAwal = _testBarang.Stok;

            bool result = _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);
            var transaksiList = _transaksiManager.GetTransaksiByBarangId(barangId);

            Assert.IsTrue(result, "Transaksi masuk seharusnya berhasil");
            Assert.AreEqual(stokAwal + jumlahMasuk, _testBarang.Stok, "Stok seharusnya bertambah setelah transaksi masuk");
            Assert.IsTrue(transaksiList.Any(t => t.Jenis == "MASUK" && t.Jumlah == jumlahMasuk), "Transaksi masuk seharusnya tercatat");
        }

        [TestMethod]
        public void TransaksiKeluar_ValidInput_ShouldDecreaseStockAndAddTransaction()
        {
            string barangId = "TEST001";
            int jumlahKeluar = 3;
            string keterangan = "Penjualan";
            int stokAwal = _testBarang.Stok;

            bool result = _transaksiManager.TransaksiKeluar(barangId, jumlahKeluar, keterangan);
            var transaksiList = _transaksiManager.GetTransaksiByBarangId(barangId);

            Assert.IsTrue(result, "Transaksi keluar seharusnya berhasil");
            Assert.AreEqual(stokAwal - jumlahKeluar, _testBarang.Stok, "Stok seharusnya berkurang setelah transaksi keluar");
            Assert.IsTrue(transaksiList.Any(t => t.Jenis == "KELUAR" && t.Jumlah == jumlahKeluar), "Transaksi keluar seharusnya tercatat");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Transaksi keluar dengan stok tidak cukup seharusnya gagal")]
        public void TransaksiKeluar_InsufficientStock_ShouldThrowException()
        {
            string barangId = "TEST001";
            int jumlahKeluar = _testBarang.Stok + 5; // Jumlah melebihi stok yang ada
            string keterangan = "Penjualan gagal";

            _transaksiManager.TransaksiKeluar(barangId, jumlahKeluar, keterangan); // Seharusnya melempar exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Transaksi dengan ID barang kosong seharusnya gagal")]
        public void TransaksiMasuk_EmptyBarangId_ShouldThrowException()
        {
            string barangId = "";
            int jumlahMasuk = 5;
            string keterangan = "Restok gagal";

            _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan); // Seharusnya melempar exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Transaksi dengan jumlah tidak valid seharusnya gagal")]
        public void TransaksiMasuk_InvalidAmount_ShouldThrowException()
        {
            string barangId = "TEST001";
            int jumlahMasuk = -1; // Jumlah tidak valid
            string keterangan = "Restok gagal";

            _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan); // Seharusnya melempar exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Transaksi dengan ID barang yang tidak ada seharusnya gagal")]
        public void TransaksiKeluar_NonExistentBarangId_ShouldThrowException()
        {
            string barangId = "TIDAK_ADA";
            int jumlahKeluar = 5;
            string keterangan = "Penjualan gagal";

            _transaksiManager.TransaksiKeluar(barangId, jumlahKeluar, keterangan); // Seharusnya melempar exception
        }

        [TestMethod]
        public void GetSemuaTransaksi_ShouldReturnAllTransactionsOrderedByDateDesc()
        {
            _transaksiManager.TransaksiMasuk(_testBarang.Id, 5, "Transaksi 1");
            System.Threading.Thread.Sleep(10);
            _transaksiManager.TransaksiKeluar(_testBarang.Id, 2, "Transaksi 2");

            var transaksiList = _transaksiManager.GetSemuaTransaksi();

            Assert.AreEqual(2, transaksiList.Count, "Seharusnya ada 2 transaksi");
            Assert.AreEqual("Transaksi 2", transaksiList[0].Keterangan, "Transaksi terakhir seharusnya muncul pertama");
            Assert.AreEqual("Transaksi 1", transaksiList[1].Keterangan, "Transaksi pertama seharusnya muncul terakhir");
        }

        [TestMethod]
        public void GetTransaksiByBarangId_ShouldReturnTransactionsForSpecificBarang()
        {
            Barang barangLain = new Barang
            {
                Id = "TEST002",
                Nama = "Barang Test 2",
                Kategori = "Test",
                Stok = 15,
                HargaBeli = 8000,
                HargaJual = 12000,
                Supplier = "Supplier Test"
            };
            _barangManager.TambahBarang(barangLain);

            _transaksiManager.TransaksiMasuk(_testBarang.Id, 5, "Transaksi Barang 1");
            _transaksiManager.TransaksiMasuk(barangLain.Id, 10, "Transaksi Barang 2");

            var transaksiBarang1 = _transaksiManager.GetTransaksiByBarangId(_testBarang.Id);
            var transaksiBarang2 = _transaksiManager.GetTransaksiByBarangId(barangLain.Id);

            Assert.AreEqual(1, transaksiBarang1.Count, "Seharusnya ada 1 transaksi untuk barang 1");
            Assert.AreEqual(1, transaksiBarang2.Count, "Seharusnya ada 1 transaksi untuk barang 2");
            Assert.AreEqual("Transaksi Barang 1", transaksiBarang1[0].Keterangan);
            Assert.AreEqual("Transaksi Barang 2", transaksiBarang2[0].Keterangan);
        }

        [TestMethod]
        public void TransaksiMasuk_ShouldUpdateStokAwal()
        {
            string barangId = "TEST001";
            int jumlahMasuk = 5;
            string keterangan = "Restok barang";
            int stokAwalBefore = _testBarang.StokAwal;

            bool result = _transaksiManager.TransaksiMasuk(barangId, jumlahMasuk, keterangan);

            Assert.AreEqual(stokAwalBefore + jumlahMasuk, _testBarang.StokAwal, "StokAwal seharusnya bertambah setelah transaksi masuk");
        }

        [TestMethod]
        public void TransaksiManager_NullBarangManager_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TransaksiManager(null),
                "Konstruktor TransaksiManager dengan BarangManager null seharusnya melempar ArgumentNullException");
        }
    }
}