using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace BaseballDataRecode
{
  public class Program
  {
    static void Main(string[] args)
    {
      try {
        var rootDataPath = Environment.GetEnvironmentVariable("DATAPATH") ?? throw new Exception("Could not find environment variable");
        var dataPath = Path.Join(rootDataPath, "MLDotNet-BaseballClassification","MLDotNet-BaseballClassification", "Data", "MLBBaseballBattersSplitTest.csv");
        if (!File.Exists(dataPath)) {
      	  throw new Exception($"Could not find data in specified path {dataPath}");
        }

        var ctx = new MLContext();
        var data = ctx.Data.LoadFromTextFile<MLBBaseballBatter>(path: dataPath, hasHeader: true, separatorChar: ',', allowQuoting: false);

	//var ppl = ctx.Transforms.Conversion.MapValueToKey("classkey", "class");
	var ppl = ctx.Transforms.SelectColumns("FullPlayerName", "YearsPlayed");
        var transformedData = ppl.Fit(data).Transform(data);

  	using (var fileStream = File.Create("ProcessedBaseballData.csv")) {
          ctx.Data.SaveAsText(transformedData, fileStream, separatorChar: ',', headerRow: true, schema: true);
        }

    	Console.WriteLine("Written transformed data!");
      } catch (Exception ex)  {  
        Console.WriteLine($"{ex.GetType()} says {ex.Message}");
	Environment.Exit(1);
      }
    }
  }

  public class MLBBaseballBatter
  {
    [LoadColumn(0), ColumnName("InductedToHallOfFame")]
    public bool InductedToHallOfFame { get; set; }

    [LoadColumn(1), ColumnName("OnHallOfFameBallot")]
    public bool OnHallOfFameBallot { get; set; }

    [LoadColumn(2), ColumnName("FullPlayerName")]
    public string FullPlayerName { get; set; }

    [LoadColumn(3), ColumnName("YearsPlayed")]
    public float YearsPlayed { get; set; }

    [LoadColumn(4), ColumnName("AB")]
    public float AB { get; set; }

    [LoadColumn(5), ColumnName("R")]
    public float R { get; set; }

    [LoadColumn(6), ColumnName("H")]
    public float H { get; set; }

    [LoadColumn(7), ColumnName("Doubles")]
    public float Doubles { get; set; }

    [LoadColumn(8), ColumnName("Triples")]
    public float Triples { get; set; }

    [LoadColumn(9), ColumnName("HR")]
    public float HR { get; set; }

    [LoadColumn(10), ColumnName("RBI")]
    public float RBI { get; set; }

    [LoadColumn(11), ColumnName("SB")]
    public float SB { get; set; }

    [LoadColumn(12), ColumnName("BattingAverage")]
    public float BattingAverage { get; set; }

    [LoadColumn(13), ColumnName("SluggingPct")]
    public float SluggingPct { get; set; }

    [LoadColumn(14), ColumnName("AllStarAppearances")]
    public float AllStarAppearances { get; set; }

    [LoadColumn(15), ColumnName("TB")]
    public float TB { get; set; }

    [LoadColumn(16), ColumnName("TotalPlayerAwards")]
    public float TotalPlayerAwards { get; set; }

    [LoadColumn(17), ColumnName("LastYearPlayed")]
    public float LastYearPlayed { get; set; }

    [LoadColumn(18), ColumnName("ID")]
    public string ID { get; set; }
  }
}

