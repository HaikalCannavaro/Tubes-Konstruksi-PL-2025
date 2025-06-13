using System;
using System.Collections.Generic;
using System.Globalization;

namespace AplikasiInventarisToko.Utils
{
    public static class ValidasiInput
    {
        private const int MAX_LENGTH = 100;
        private static readonly HashSet<string> SuspiciousPatterns = new()
        {
            "script", "select", "insert", "delete", "drop", "exec", "union", "or 1=1"
        };

        public static int ValidasiAngka(string input)
        {
            ValidateInput(input);

            if (!int.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
                throw new FormatException("Input harus angka bulat yang valid");

            return result;
        }

        public static decimal ValidasiDecimal(string input)
        {
            ValidateInput(input);

            if (!decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                throw new FormatException("Input harus angka desimal yang valid");

            return result;
        }

        public static DateTime ValidasiTanggal(string input)
        {
            ValidateInput(input);

            if (!DateTime.TryParseExact(input.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                throw new FormatException("Format tanggal tidak valid. Gunakan DD/MM/YYYY");

            return result;
        }

        private static void ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input tidak boleh kosong");

            if (input.Length > MAX_LENGTH)
                throw new ArgumentException($"Input terlalu panjang (maksimal {MAX_LENGTH} karakter)");

            var lowerInput = input.ToLowerInvariant();
            foreach (var pattern in SuspiciousPatterns)
            {
                if (lowerInput.Contains(pattern))
                    throw new ArgumentException("Input mengandung karakter tidak diizinkan");
            }
        }
    }
}