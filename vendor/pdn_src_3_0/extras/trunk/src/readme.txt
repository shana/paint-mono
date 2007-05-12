Paint.NET Source Code Readme

Prerequisites
-------------
1. Windows XP or Windows Server 2003, or newer. 

2. Visual Studio .NET 2005
   You should install the C++ x64 compiler as well, which may not come installed
   by default. 

3. .NET Framework 2.0

4. Tablet PC SDK v1.7
   Download and install this from Microsoft:
   http://www.microsoft.com/downloads/details.aspx?FamilyID=b46d4b83-a821-40bc-aa85-c9ee3d6e9699&DisplayLang=en


Instructions
------------
1. Open src/paintdotnet.sln with Microsoft Visual Studio .NET 2005. 

2. Make sure the project configuration is set to "Release and Package."
   This can be done by going to the "Build" menu, selecting "Configuration
   Manager...", selecting "Release and Package" under "Active Solution 
   Configuration:" and then clicking Close.
    
3. Go to the "Build" menu and click "Rebuild Solution."

4. Assuming all went well, the output files are now in src/Setup/Release:

   * PaintDotNet.msi
     This is the MSI that installs Paint.NET, but you shouldn't launch it
     directly, otherwise certain MSI properties won't be set up right.
     
   * PaintDotNet_M_m.exe
     The "real" installer, where M is the major version and m is the minor
     version, i.e. PaintDotNet_2_6.exe
     
     Suitable for web-based distribution. This is fairly small and installs
     just Paint.NET.             
               
   * PaintDotNet_M_m_Full.exe
     This is the "full" installer that will install .NET 2.0 if it is not 
     already installed. This file is much larger than PaintDotNetSetup.exe
     but provides a very convenient all-in-one installation package.


For normal development work, use either the 'Release' or 'Debug' configuration.
This will skip the process of building all the setup packages, help file, and 
merge modules. When you are working in this mode, you should make sure that
the /skipRepairAttempt command line parameter is present in the Debug tab of
the 'paintdotnet' project's properties. Otherwise, Paint.NET will see that some
files are missing and attempt to repair itself. Not all of these files are 
necessary when doing development or debugging.

Also, you will need to make sure that mt.exe and signtool.exe are in a
directory that is in your PATH. These are available as part of the Windows SDK
which can be found at Microsoft's website. Usually it's sufficient to copy
these to %SYSTEMROOT%, which is usually C:\Windows.


Directory Layout
----------------
src/
    The main folder containing all the Paint.NET source code.

src/bin
    This is where the main Paint.NET executable and DLLs will be placed.
    When you build PDN, you should be able to run PaintDotNet.exe from this
    directory.

src/BuildTools
    Some exe's that are used by the build process.

src/Data
    Contains all data-related code, including loading and saving of images.

src/Effects
    Contains the code that is built for the PaintDotNet.Effects.dll. This is
    the Effects subsystem of Paint.NET that plugins will have to reference.
    
src/GeneratedCode
    Contains a project with build events that generate code for other projects
    in the paintdotnet solution. Currently it only generates the user blend
    ops in the Data project.

src/Help
    Contains all the help files that are compiled into PaintDotNet.chm.
    
src/Interop.WIA
    Contains the .NET interop DLL for the Windows Image Acquisition (WIA)
    Automation Layer.

src/Manifests
    Contains XML manifests that are embedded in some of the EXE's that are
    built. These are used in Windows Vista to mark the executable as either
    requiring administrator privilege, or to run with the privilege that
    the calling process has. The latter is important for the installer so
    that the executable is not deemed 'legacy', and thus certain
    compatibility modes are bypassed.

src/obj
    Intermediate files used during compilation go here.

src/PdnLib
    Contains the Paint.NET "library." This is code that is plausibly usable 
    either outside of Paint.NET or required for plugins to link against.

src/Resources
    Contains all the resources for Paint.NET, and some code for managing them.

src/Setup
    Contains a project that is used to build PaintDotNet.msi. Note that the
    MSI file is not complete until the "Setup-Config" project has finished!

src/Setup-Config
    This is the final stage of the build process. It modifies PaintDotNet.msi
    using a VBS script so that it defaults to "Install for Everyone" instead
    of "Install for Just Me." It then packages together PaintDotNet.msi with
    dotnetfx.exe using NSIS (Nullsoft Scriptable Installation System).

src/SetupFrontEnd
    Contains our front-end to the setup MSI. This was written so that we can
    localize (translate) the setup wizard, and also so that the installation
    options can be preserved when updating or reinstalling.

src/SetupNgen
    This is a program that is run as part of install and uninstall that "pre-
    JITs" our DLLs, as well as performing a few other tasks.

src/SetupShim
    This is the setup "shim" which determines if .NET is installed and tries
    to install it if required. If .NET is already installed, it launches
    SetupFrontEnd.

src/SharpZipLib
    Contains the DLL for #ziplib, by Mike Krueger.

src/ShellExtension
    Contains the code for a Windows Explorer shell extension that displays
    thumbnails. This is a COM object written in C++.

src/Strings
    Contains the strings.resx for English/neutral locales.
    
src/StylusReader
    Contains the code for interfacing with the Tablet PC SDK.

src/SystemLayer
    All P/Invoke and "system dependent" code, as well as hacks or workarounds,
    go in to the SystemLayer assembly.

src/WIAAutSDK
    Contains the WIA 2.0 Automation library.

src/WIAProxy32
    Contains the code for the WIA proxy executable.