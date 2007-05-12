This is where .NET 2.0 must be placed in order to build the 'full' installer for Paint.NET.

To do this, download both the 32-bit x86 and 64-bit x64 .NET redistributables:

32-bit: http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5
64-bit: http://www.microsoft.com/downloads/details.aspx?FamilyID=b44a0000-acf8-4fa1-affb-40e78d788b00

Now, you must extract the contents of these to the x86 and x64 directories, respectively.

To do this at the command-line, use the following syntax:

    dotnetfx.exe /T:[full path to programs/x86] /C
    netfx64.exe /T:[full path to programs/x64] /C

For example:

    dotnetfx.exe /T:c:\src\paintdotnet\programs\x86 /C
    netfx64.exe /T:c:\src\paintdotnet\programs\x64 /C