using Serilog;
using ILogger = Serilog.ILogger;

namespace BasketballAcademy.Repository
{
    public class logger
    {
        private static readonly ILogger Log;
        static logger()
        {
            Log = new LoggerConfiguration().WriteTo.File(@"D:\Basketball_Academy_log.txt").CreateLogger();
        }

        /// <summary>
        /// Logs an exception to a log file.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void LogError(Exception ex)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Log.Error(ex, $"{timestamp} - An error occurred");
                Log.Error(ex, $"{timestamp} - ------------------");
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"An error occurred while logging: {logEx.Message}");
            }
        }


        public static void LogInfo(string message, string username)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Log.Information($"{timestamp} - {message}", username);
                Log.Information("------------------");
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"An error occurred while logging: {logEx.Message}");
            }
        }
    }
}
