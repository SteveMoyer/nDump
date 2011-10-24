rm -rf .\coverage\*
.\lib\openCover\openCover.console.exe -register:smoyer -target:"c:\projects\nDump\lib\nunit-console-x86.exe" -targetargs:"/noshadow c:\projects\nDump\nDump.Unit\bin\Debug\nDump.Unit.dll" -targetdir:C:\projects\nDump\nDump.Unit\bin\Debug  -output:.\coverage\coverage.xml -filter:"-[nDump]nDump.Configuration.* -[nDump]nDump.*Exception.* -[nDump]nDump.Logging.* -[nDump]nDump.SqlServer.* -[nDump]nDump.Model.* -[nDump.Unit]* +[nDump].*"	
.\lib\reportgenerator\reportgenerator.exe .\coverage\coverage.xml .\coverage
