using System;

var data = new double[] { 101.35, 102.26, 99.07, 100.39, 100.76, 103.59, 99.26, 98.28, 99.98, 103.78, 102.54 };

int x = data.Length -2;
int workdays = 365;
int timeintervalforWeekinterval = 7;
var result = new VolatilituStatsModel[data.Length];
result[0] = new VolatilituStatsModel
{
    Week = 0,
    Price = data[0]
};

for (var i = 1; i < data.Length; i++)
{
    result[i] = new VolatilituStatsModel
    {
        Week = i,
        Price = data[i],
        Differrence = Math.Log(data[i]/ data[i-1])

    };
}
//находим мат ожидание
var Mathexpected = result.Select(x => x.Differrence).Average();
//Console.WriteLine(Mathexpected);
foreach (var r in result) r.Mathexpected = Mathexpected;
// добаляем поля отклонение и отклонение в квадрате 
for (var i = 1; i < data.Length; i++)
{
    result[i].Deviation = result[i].Differrence - Mathexpected;
    result[i].DeviationSqar = result[i].Deviation * result[i].Deviation;
}
//находим сумму квадратов отклонения
var SumDeviationSqar = result.Select(x => x.DeviationSqar).Sum();
foreach (var r in result) r.SumDeviationSqar = SumDeviationSqar;
//foreach (var r in result) r.SumDeviationSqar = SumDeviationSqar;

// находим стандартное отклонение логарифмических изменений

var STDSumDeviationSqar = Math.Sqrt((double)SumDeviationSqar / x);
//Console.WriteLine(STDSumDeviationSqar);
foreach (var r in result) r.STDSumDeviationSqar = STDSumDeviationSqar;

//находим годовую волотильность


var Volatilyty = STDSumDeviationSqar * Math.Sqrt(workdays / timeintervalforWeekinterval);
//foreach (var r in result) Console.WriteLine(r);

Console.WriteLine(Volatilyty);

Console.WriteLine($"Волатильность в процентах - {Volatilyty * 100}");

public class VolatilituStatsModel
{
    public int Week { get; set; }
    public double Price { get; set; }
    public double? Differrence { get; set; } = null!;
    public double? Mathexpected { get; set; } = null!;
    public double ? Deviation { get; set; } = null!;
    public double? DeviationSqar { get; set; } = null!;
    public double? SumDeviationSqar { get; set; } = null!;
    public double? STDSumDeviationSqar { get; set; } = null!;
    public double? Volatilyty { get; set; } = null!;
    public override string ToString() => $"Week: {Week},Price: {Price}, Differrence: {Differrence},Mathexpected: {Mathexpected}," +
        $"Deviation: {Deviation}, DeviationSqar:{DeviationSqar}, SumDeviationSqar: {SumDeviationSqar}, STDSumDeviationSqar: {STDSumDeviationSqar}," +
        $"STDSumDeviationSqar: {STDSumDeviationSqar},Volatilyty: {Volatilyty}";    
}


