#!/bin/bash
cd ..
dotnet build
cd Tools
dotnet minicover instrument --workdir ../ --assemblies Sudoku.Test/bin/**/*.dll --sources Sudoku/**/*.cs
dotnet minicover reset
cd ../Sudoku.Test
dotnet test --no-build
cd ../Tools
dotnet minicover uninstrument --workdir ../
dotnet minicover htmlreport --workdir ../ --threshold 90
dotnet minicover report --workdir ../ --threshold 90
