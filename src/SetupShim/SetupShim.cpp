/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

#define _WIN32_WINNT 0x0501
#include <tchar.h>
#include <windows.h>
#include <strsafe.h>

// .NET FX detection code from: http://blogs.msdn.com/astebner/archive/2004/09/18/231253.aspx
//              which links to: http://astebner.sts.winisp.net/Tools/detectFX.cpp.txt

const TCHAR *g_szNetfx20RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727");
const TCHAR *g_szNetfxRegValueName = _T("Install");
const TCHAR *g_szNetfxSPxRegValueName = _T("SP");

const TCHAR *g_szMessageBoxTitle = _T("Paint.NET");

const TCHAR *g_szNetfx20FoundText = 
    _T("[English] Paint.NET must first install the .NET Framework 2.0. On some systems this may take 5-10 minutes, during which it is safe to use your computer for other tasks.\n"
       "\n"
       "[Deutsch] Vor Paint.NET muss zuerst das .NET Framework 2.0 installiert werden. Auf einigen Systemen kann dies 5-10 Minuten dauern. Sie können in dieser Zeit den Computer problemlos weiterverwenden.");

const TCHAR *g_szNetfx20NotFoundText = 
    _T("[English] Paint.NET requires that the .NET Framework 2.0 is installed. Click OK to go to Microsoft's webpage where you may download and install this.\n"
       "\n"
       "[Deutsch] Paint.NET setzt die Installation des .NET Frameworks 2.0 voraus. Klicken sie auf OK um die Microsoft Homepage zu öffnen und das Framework zu downloaden.");

const TCHAR *g_szNetfxInstallFailureTextFormat =
    _T("[English] Installation of the .NET Framework failed with the following error code: %d\n"
       "\n"
       "[Deutsch] Die Installation des .NET Frameworks ist mit folgendem Fehlercode fehlgeschlagen: %d");

// This isn't the most user-friendly way to get the system restarted.
// However, this should not affect many people as .NET generally doesn't
// require a restart after installation.
const TCHAR *g_szNetfxInstallRebootRequired =
    _T("[English] You must now manually restart your computer before continuing with the installation of Paint.NET.\n"
       "\n"
       "[Deutsch] Sie müssen ihren Computer nun neu starten bevor sie mit der Installation von Paint.NET fortfahren können.");

const TCHAR *g_szNetfx20x86DownloadUrl = _T("http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5");
const TCHAR *g_szNetfx20x64DownloadUrl = _T("http://www.microsoft.com/downloads/details.aspx?FamilyID=b44a0000-acf8-4fa1-affb-40e78d788b00");
const TCHAR *g_szNetfx20DispatchDownloadUrl = _T("http://msdn.microsoft.com/netframework/downloads/updates/default.aspx");

const TCHAR *g_szNetfx20x86InstallerFileName = _T("x86\\install.exe");
const TCHAR *g_szNetfx20x64InstallerFileName = _T("x64\\install.exe");

const TCHAR *g_szPdnInstallerFileName = _T("SetupFrontEnd.exe");

BOOL RegistryGetValue(HKEY hk, const TCHAR* pszKey, const TCHAR* pszValue, 
                      DWORD dwType, LPBYTE data, DWORD dwSize)
{
    HKEY hkOpened;

    // Try to open the key
    if (RegOpenKeyEx(hk, pszKey, 0, KEY_READ, &hkOpened) != ERROR_SUCCESS)
    {
        return false;
    }

    // If the key was opened, try to retrieve the value
    if (RegQueryValueEx(hkOpened, pszValue, 0, &dwType, (LPBYTE)data, &dwSize) != ERROR_SUCCESS)
    {
        RegCloseKey(hkOpened);
        return false;
    }
    
    // Clean up
    RegCloseKey(hkOpened);

    return true;
}

bool IsNetfx20Installed()
{
    bool bRetValue = false;
    DWORD dwRegValue = 0;

    if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx20RegKeyName, g_szNetfxRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
    {
        if (1 == dwRegValue)
        {
            bRetValue = true;
        }
    }

    return bRetValue;
}

bool FileExists(const TCHAR* pszFileName)
{
    return INVALID_FILE_ATTRIBUTES != GetFileAttributes(pszFileName);
}

// Returns 0xffffffff on failure, 0 if bWait is false and the operation was successful,
// or the exit code of the process launched if bWait is true and the operation was successful
DWORD OurShellExecute(const TCHAR* pszFileName, const TCHAR* pszParameters, bool bWait)
{
    DWORD dwRetVal = 0;
    SHELLEXECUTEINFO sei;

    ZeroMemory(&sei, sizeof(sei));
    sei.cbSize = sizeof(sei);
    sei.fMask = bWait ? SEE_MASK_NOCLOSEPROCESS : 0;
    sei.lpVerb = _T("open");
    sei.lpFile = pszFileName;
    sei.lpParameters = pszParameters;
    sei.nShow = SW_SHOWNORMAL;

    BOOL bResult = ShellExecuteEx(&sei);

    if (!bResult)
    {
        dwRetVal = 0xffffffff;
    }
    else
    {
        if (!bWait)
        {
            dwRetVal = 0;
        }
        else 
        {
            if (WAIT_OBJECT_0 == WaitForSingleObject(sei.hProcess, INFINITE))
            {
                DWORD dwExitCode = 0;
                BOOL bResult2 = GetExitCodeProcess(sei.hProcess, &dwExitCode);

                if (bResult2)
                {
                    dwRetVal = dwExitCode;
                }
                else
                {
                    dwRetVal = 0xffffffff;
                }
            }
            else
            {
                dwRetVal = 0xffffffff;
            }

            CloseHandle(sei.hProcess);
            sei.hProcess = NULL;
        }
    }

    return dwRetVal;
}

typedef BOOL (* IsWow64ProcessFnPtr)(HANDLE hProcess, PBOOL Wow64Process);
typedef void (* GetSystemInfoFnPtr)(LPSYSTEM_INFO lpSystemInfo);

WORD GetProcessorArchitecture(void)
{
    HMODULE hKernel32 = LoadLibrary(_T("kernel32.dll"));

    GetSystemInfoFnPtr gsifp = NULL;

    if (NULL != hKernel32)
    {
        gsifp = (GetSystemInfoFnPtr)GetProcAddress(hKernel32, _T("GetNativeSystemInfo"));
    }

    if (NULL == gsifp)
    {
        gsifp = (GetSystemInfoFnPtr)GetProcAddress(hKernel32, _T("GetSystemInfo"));
    }

    WORD wPA = PROCESSOR_ARCHITECTURE_UNKNOWN;

    if (NULL != gsifp)
    {
        SYSTEM_INFO sysInfo;
        ZeroMemory(&sysInfo, sizeof(sysInfo));
        gsifp(&sysInfo);
        wPA = sysInfo.wProcessorArchitecture;
    }

    if (NULL != hKernel32)
    {
        FreeLibrary(hKernel32);
        hKernel32 = NULL;
    }

    return wPA;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    int nReturnVal;

    // Stage 1: Figure out the situation with the installation of .NET
    if (IsNetfx20Installed())
    {
        // .NET's already installed, our job is done!
        nReturnVal = 0;
    }
    else
    {
        const TCHAR* szNetfxInstallerFileName;
        const TCHAR* szNetfxDownloadUrl;

        WORD wCpu = GetProcessorArchitecture();

        switch (wCpu)
        {
            case PROCESSOR_ARCHITECTURE_INTEL:
                szNetfxInstallerFileName = g_szNetfx20x86InstallerFileName;
                szNetfxDownloadUrl = g_szNetfx20x86DownloadUrl;
                break;

            case PROCESSOR_ARCHITECTURE_AMD64:
                szNetfxInstallerFileName = g_szNetfx20x64InstallerFileName;
                szNetfxDownloadUrl = g_szNetfx20x64DownloadUrl;
                break;

            default:
                szNetfxInstallerFileName = NULL;
                szNetfxDownloadUrl = g_szNetfx20DispatchDownloadUrl;
                break;
        }

        // .NET 2.0 is not installed. But if we have the installer in front of us,
        // then try to install it!
        if (NULL != szNetfxInstallerFileName && FileExists(szNetfxInstallerFileName))
        {
            // First, tell the user what we're about to do.
            MessageBox(NULL, g_szNetfx20FoundText, g_szMessageBoxTitle, MB_OK | MB_ICONINFORMATION);

            // .NET installer is present, so let's try and install it
            DWORD dwResult = OurShellExecute(szNetfxInstallerFileName, NULL, true);

            switch (dwResult)
            {
                // Success
                case 0:    
                    nReturnVal = 0;
                    break;

                // Reboot required
                case 8192: 
                    MessageBox(NULL, g_szNetfxInstallRebootRequired, g_szMessageBoxTitle, 
                        MB_OK | MB_ICONINFORMATION);

                    nReturnVal = 1;
                    break;

                // They cancelled. No sense in putting up another error message.
                case 1602:
                    nReturnVal = 1;
                    break;

                // Other. Interpreted as failure.
                default:   
                    {
                        TCHAR szErrorText[1024];

                        HRESULT hr = StringCchPrintf(
                            szErrorText, 
                            sizeof(szErrorText) / sizeof(szErrorText[0]), 
                            g_szNetfxInstallFailureTextFormat,
                            dwResult,
                            dwResult);

                        if (SUCCEEDED(hr))
                        {
                            MessageBox(NULL, szErrorText, g_szMessageBoxTitle, MB_OK | MB_ICONERROR);
                        }
                    }

                    nReturnVal = 1;
                    break;
            }
        }
        else
        {
            // .NET not installed, and the installer isn't around. Ask them to go to a website in order to install it.
            int nResult = MessageBox(NULL, g_szNetfx20NotFoundText, g_szMessageBoxTitle, 
                MB_OKCANCEL | MB_ICONERROR);

            if (IDOK == nResult)
            {
                OurShellExecute(szNetfxDownloadUrl, NULL, false);
            }

            nReturnVal = 1;
        }
    }

    // Stage 2: Launch our setup app!
    if (0 == nReturnVal)
    {
        OurShellExecute(g_szPdnInstallerFileName, GetCommandLine(), true);
    }

    return nReturnVal;
}