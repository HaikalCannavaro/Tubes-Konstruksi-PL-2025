using System;
using System.Collections.Generic;

namespace AplikasiInventarisToko.Utils
{
    public static class ValidasiInput
    {
        private static readonly Dictionary<string, Func<string, object>> _validatorTable;

        static ValidasiInput()
        {
            _validatorTable = new Dictionary<string, Func<string, object>>(StringComparer.OrdinalIgnoreCase)
            {
                { "int", ValidasiAngkaInternal },
                { "decimal", ValidasiDecimalInternal },
                { "datetime", ValidasiTanggalInternal }
            };
        }

        public static int ValidasiAngka(string input)
        {
            return (int)Validasi("int", input);
        }

        public static decimal ValidasiDecimal(string input)
        {
            return (decimal)Validasi("decimal", input);
        }

        public static DateTime ValidasiTanggal(string input)
        {
            return (DateTime)Validasi("datetime", input);
        }

        private static object Validasi(string tipe, string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input tidak boleh kosong");

            if (!_validatorTable.ContainsKey(tipe))
                throw new ArgumentException($"Tipe validasi '{tipe}' tidak didukung");

            return _validatorTable[tipe](input);
        }

        private static object ValidasiAngkaInternal(string input)
        {
            if (!int.TryParse(input, out int hasil))
                throw new FormatException("Input harus berupa angka bulat");

            return hasil;
        }

        private static object ValidasiDecimalInternal(string input)
        {
            if (!decimal.TryParse(input, out decimal hasil))
                throw new FormatException("Input harus berupa angka desimal");

            return hasil;
        }

        private static object ValidasiTanggalInternal(string input)
        {
            if (!DateTime.TryParse(input, out DateTime hasil))
                throw new FormatException("Format tanggal tidak valid. Gunakan format DD/MM/YYYY");

            return hasil;
        }
    }
}
