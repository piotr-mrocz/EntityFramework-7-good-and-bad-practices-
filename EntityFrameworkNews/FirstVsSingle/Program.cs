// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using FirstVsSingle;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<OrderService>();
