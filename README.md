# Streaming-TALib
The popular technical analysis library (TA-Lib), but allowing fast computation for streaming data. 

The problem with TA-Lib is that you must compute on the entire period, meaning that it can be used well for backtesting, but computing values on live data is slow. This repo aims to solve that by keeping an indicator state and using an API that only relies on a single new price to do subsequent calculations.

## Indicators
Currently implemented:
RSI

Would like to have:
ADX
ATR
PLUS_DI (part of ADX calculation)
MINUS_DI (part of ADX calculation)
SMA 
EMA
BBAND
