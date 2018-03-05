#!/bin/bash
dotnet minicover instrument --workdir ../ --assemblies Sudoku.Xunit/bin/**/*.dll --sources Sudoku/**/*.cs
dotnet minicover reset
cd ../Sudoku.Xunit
dotnet test --no-build
cd ../tools
dotnet minicover uninstrument --workdir ../
dotnet minicover htmlreport --workdir ../ --threshold 90
dotnet minicover report --workdir ../ --threshold 90
