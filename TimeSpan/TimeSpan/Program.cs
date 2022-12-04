// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var data1 = DateTime.Now;
var data2 = data1.AddMinutes(1);

var diffrent = (data1 - data2).TotalSeconds;

diffrent = diffrent < 0 ? diffrent * -1 : diffrent;

Console.WriteLine(diffrent);