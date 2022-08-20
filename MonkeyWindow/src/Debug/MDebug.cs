
namespace MonkeyWindow.Debug;

public static class MDebug
{

    public static void Log(object message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[{GetCurrentTime()}] LOG - {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Warn(object message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[{GetCurrentTime()}] WARN - {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Error(object message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{GetCurrentTime()}] ERROR - {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static string GetCurrentTime()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }

}