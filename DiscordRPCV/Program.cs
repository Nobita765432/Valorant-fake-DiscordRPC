using System;
using System.Runtime.InteropServices;
using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Win32;

class Program
{
    // Import necessary WinAPI functions
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0; // Hides the window

    private static DiscordRpcClient client;

    public static Timestamps rpctimestamp { get; set; }

    static void Main(string[] args)
    {
        // Hide the console window
        IntPtr hWndConsole = GetConsoleWindow();
        if (hWndConsole != IntPtr.Zero)
        {
            ShowWindow(hWndConsole, SW_HIDE);
        }

        // Discord RPC setup
        string clientId = "1255874329477124128"; // Replace with your Discord application's Client ID

        client = new DiscordRpcClient(clientId);
        client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

        client.OnReady += (sender, e) =>
        {
            Console.WriteLine($"Connected to Discord as {e.User.Username}");
        };

        client.OnPresenceUpdate += (sender, e) =>
        {
            Console.WriteLine($"Presence updated: {e.Presence}");
        };

        client.Initialize();

        UpdatePresence("Example Details", "Example State");

        if (!StartupManager.IsStartupSet())
        {
            StartupManager.SetStartup();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        client.Dispose();
    }

    static void UpdatePresence(string details, string state)
    {

        DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long currentTimeStamp = (long)(DateTime.UtcNow - startTime).TotalSeconds;

        client.SetPresence(new RichPresence()
        {
            Assets = new Assets()
            {
                SmallImageKey = "https://asset.brandfetch.io/id1t-fbPVK/idoZlnVnEt.png"

            },
            Timestamps = new Timestamps()
            {
                Start = DateTimeOffset.FromUnixTimeSeconds(currentTimeStamp).UtcDateTime
            }
        });
    }
}