for %%f in (. Data Effects PdnLib PdnLib\Threading Resources SystemLayer StylusReader Setup) do (
    rmdir /s /q "%%f\bin"
    rmdir /s /q "%%f\obj"
    rmdir /s /q "%%f\Release"
    rmdir /s /q "%%f\Debug"
    rmdir /s /q "%%f\Release and Package"
)

del Help\PaintDotNet.chm

