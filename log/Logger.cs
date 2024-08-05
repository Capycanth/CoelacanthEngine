namespace CoelacanthEngine.log
{
    public sealed class Logger
    {
        private readonly string SaveDirectory;

        public Logger(Type clazz)
        {
            //this.SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ChekhovSettings.Settings.GameName, "Log", $"{NullSafeClassName(clazz)}.txt");
        }

        private void Log(string message)
        {
            string currentTime = DateTime.Now.ToString("MM-dd-yy_HH:mm:ss.ffff");

            File.AppendAllText(SaveDirectory, $"{currentTime} : {message}\n");
        }

        public void Error(string message)
        {
            Log($"ERROR : {message}");
        }

        public void Debug(string message)
        {
            Log($"DEBUG : {message}");
        }

        private static string NullSafeClassName(Type clazz)
        {
            return clazz.Name ?? "unknown";
        }
    }
}