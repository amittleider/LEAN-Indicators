# Streaming-TALib
The popular technical analysis library (TA-Lib), but allowing fast computation for streaming data. 

The problem with TA-Lib is that you must compute on the entire period, meaning that it can be used well for backtesting, but computing values on live data is slow. This repo aims to solve that by keeping an indicator state and using an API that only relies on a single new price to do subsequent calculations.

Quantconnect has solved the problem, but is compiled in net4.6. This project is compiled in dotnet standard 2.0.

## Builds
[![Build Status](https://dev.azure.com/amittleider/Streaming-TALib/_apis/build/status/amittleider.Streaming-TALib)](https://dev.azure.com/amittleider/Streaming-TALib/_build/latest?definitionId=1)

## Nuget package
Find the nuget package here: https://www.nuget.org/packages/AutoFinance.QuantConnect.Indicators/
