.\lib\PartCover\PartCover.exe --target .\lib\nunit-console-x86.exe --target-args /noshadow --include [nDump]*  --exclude [nDump]nDump.Configuration* --exclude [nDump]nDump.*Exception --exclude [nDump]nDump.Logging* --exclude [nDump]nDump.SqlServer* --exclude [nDump]nDump.Model* --target-work-dir .\nDump.Unit\bin\debug\ --target-args nDump.Unit.dll --output coverage.xml

