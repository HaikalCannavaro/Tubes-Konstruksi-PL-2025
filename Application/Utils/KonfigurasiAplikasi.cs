using System.Text.Json;

public class KonfigurasiAplikasi
{
    public string ExportPath { get; set; }
    public CurrencyKonfig CurrencySetting { get; set; }
    public Thresholds KategoriThreshold { get; set; }

    public class CurrencyKonfig
    {
        public string Current { get; set; }
        public Dictionary<string, CurrencyDetail> Options { get; set; }
    }

    public class CurrencyDetail
    {
        public string Symbol { get; set; }
        public string Culture { get; set; }
    }

    public class Thresholds
    {
        public FastMovingThreshold FastMoving { get; set; }
        public SlowMovingThreshold SlowMoving { get; set; }
        public int BelumTerkategoriHari { get; set; }

        public class FastMovingThreshold
        {
            public int MinHari { get; set; }
            public int MaxPersentaseStok { get; set; }
        }

        public class SlowMovingThreshold
        {
            public int MinHari { get; set; }
            public int MinPersentaseStok { get; set; }
        }
    }


    public static KonfigurasiAplikasi Load(string path = "appsettings.json")
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<KonfigurasiAplikasi>(json);
    }
}
