using Microsoft.Win32;
using System;

public static class StartupManager
{
    public static void SetStartup()
    {
        string appName = "ValorantDRPC"; // Replace with your application name
        string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
        {
            key.SetValue(appName, appPath);
        }

        Console.WriteLine($"{appName} set to run on startup.");
    }

    public static bool IsStartupSet()
    {
        string appName = "ValorantDRPC"; // Replace with your application name

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
        {
            return key.GetValue(appName) != null;
        }
    }
}
