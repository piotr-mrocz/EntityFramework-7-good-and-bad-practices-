``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1555/22H2/2022Update/SunValley2)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100
  [Host] : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2 [AttachedDebugger]


```
|                        Method | Mean | Error |
|------------------------------ |-----:|------:|
| AddUsersWithSaveChangesInLoop |   NA |    NA |

Benchmarks with issues:
  AdditionService.AddUsersWithSaveChangesInLoop: DefaultJob
