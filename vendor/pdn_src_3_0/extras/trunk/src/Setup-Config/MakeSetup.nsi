;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Paint.NET                                                                   ;;
;; Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            ;;
;; Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          ;;
;; See src/Resources/Files/License.txt for full licensing and attribution      ;;
;; details.                                                                    ;;
;; 1                                                                           ;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; MakeSetup.nsi
;
; Copies files necessary to do a Paint.NET installation.
; After the PDN setup completes, the temp files will be deleted.

;--------------------------------

!ifdef Compress
  !ifdef FullInstaller
    SetCompressor /SOLID lzma
    SetCompressorDictSize 32
    ;SetCompress off
  !else
    SetCompressor /SOLID lzma
    SetCompressorDictSize 11
  !endif
!else
  SetCompress off
!endif

; The name of the installer
Name "Paint.NET SFX"

; The default installation directory
InstallDir $TEMP\PdnSetup

!ifdef FullInstaller
!else
  SilentInstall silent
!endif

Icon ..\Resources\Icons\PaintDotNet.ico

VIAddVersionKey ProductName "Paint.NET Setup"
VIAddVersionKey ProductVersion "3.0.0.0"
VIAddVersionKey FileVersion "3.0.0.0"
VIAddVersionKey LegalCopyright "Copyright © 2007 Rick Brewster, Tom Jackson, and past contributors. Portions Copyright © 2007 Microsoft Corporation. All Rights Reserved."
VIAddVersionKey FileDescription "Installs Paint.NET."
VIProductVersion "3.0.0.0"

; The file to write
!ifdef Debug

!ifdef FullInstaller
  OutFile "..\Setup\Debug\PaintDotNetWithDotNetSetup.exe"
!else
  OutFile "..\Setup\Debug\PaintDotNetSetup.exe"
!endif

!else

!ifdef FullInstaller
  OutFile "..\Setup\Release\PaintDotNetWithDotNetSetup.exe"
!else
  OutFile "..\Setup\Release\PaintDotNetSetup.exe"
!endif 

!endif

;--------------------------------

; Pages

;Page directory
;Page instfiles

Var "Args"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important
    
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Command-line parameters
  Call GetParameters
  Pop $Args
  
  ; Put file there
  File ..\Setup\Release\PaintDotNet.msi
  File ..\SetupFrontEnd\bin\Release\PaintDotNet.Resources.dll
  File ..\SetupFrontEnd\bin\Release\PaintDotNet.SystemLayer.dll
  File ..\SetupFrontEnd\bin\Release\PdnLib.dll
  File /nonfatal ..\Resources.mui\*.resources
  File /nonfatal /r /x CVS ..\Resources.mui\*.rtf
  File /nonfatal /r /x CVS ..\Resources.mui\*.png
  File /nonfatal /r /x CVS ..\Resources.mui\*.gif
  File ..\SetupFrontEnd\bin\Release\PaintDotNet.Strings.3.resources
  File ..\SetupFrontEnd\bin\Release\SetupFrontEnd.exe
  File ..\SetupShim\Release\SetupShim.exe

!ifdef FullInstaller
  ; Ordering these files is important so that files that are similar
  ; are next to each other and can be compressed together via the
  ; solid archive compression.
  File /r /x CVS ..\..\programs\dotnet_2_0\*.ini
  File /r /x CVS ..\..\programs\dotnet_2_0\*.txt
  File /r /x CVS ..\..\programs\dotnet_2_0\*.dll
  File /r /x CVS ..\..\programs\dotnet_2_0\*.exe
  File /r /x CVS ..\..\programs\dotnet_2_0\*.bmp
  File /r /x CVS ..\..\programs\dotnet_2_0\*.msi
  File /r /x CVS ..\..\programs\dotnet_2_0\*.cab
!endif
  
!ifdef FullInstaller
  SetAutoClose true
  HideWindow
!endif

  ExecWait "SetupShim.exe $Args"

  Delete $INSTDIR\SetupShim.exe
  Delete $INSTDIR\SetupFrontEnd.exe
  Delete $INSTDIR\PaintDotNet.Strings.3.resources
  Delete $INSTDIR\*.resources
  Delete $INSTDIR\PaintDotNet.Resources.dll
  Delete $INSTDIR\PaintDotNet.SystemLayer.dll
  Delete $INSTDIR\PdnLib.dll
  Delete $INSTDIR\PaintDotNet.msi
  
  RMDir /r $INSTDIR
  
SectionEnd

; GetParameters
; input, none
; output, top of stack (replaces, with e.g. whatever)
; modifies no other variables.
Function GetParameters

    Push $R0
    Push $R1
    Push $R2
    Push $R3

    StrCpy $R2 1
    StrLen $R3 $CMDLINE

    ;Check for quote or space
    StrCpy $R0 $CMDLINE $R2
    StrCmp $R0 '"' 0 +3
        StrCpy $R1 '"'
        Goto loop
    StrCpy $R1 " "

    loop:
        IntOp $R2 $R2 + 1
        StrCpy $R0 $CMDLINE 1 $R2
        StrCmp $R0 $R1 get
        StrCmp $R2 $R3 get
        Goto loop

    get:
        IntOp $R2 $R2 + 1
        StrCpy $R0 $CMDLINE 1 $R2
        StrCmp $R0 " " get
        StrCpy $R0 $CMDLINE "" $R2

    Pop $R3
    Pop $R2
    Pop $R1
    Exch $R0

FunctionEnd
