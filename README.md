# AMPP (AuxMoney Project Parser)
This program parses [AuxMoney](https://www.auxmoney.com/) project details and tracks current interests and fees. The output file can be imported by [Portfolio Performance](https://www.portfolio-performance.info/).

This project was inspired by https://github.com/StegSchreck/PP-Auxmoney-Parser. It is rewritten in .NET5, usable on **all** environments and utilizes no additional driver (*Geckodriver*). Furthermore it uses no visual browser component and extracts the data directly from JSON.

## Note
This is currently only for the **GERMAN** language settings for Portfolio Performance! All country specific export settings can be changed at [OutputSettings](https://github.com/helloingob/AMPP/blob/master/AMPP/Data/OutputSettings.cs).

## Usage
1. Build "dotnet build AuxMoneyProjectParser.sln"
2. Execute "AMPP.exe email password"
3. Locate output in AMPP.exe folder (e.g. ampp_30122020122237.csv)
4. Import into Portfolio Performance
