namespace CoelacanthEngine.config
{
    public class CoelacanthSettings
    {
        // Singleton instance.
        private static CoelacanthSettings _instance;

        // Private constructor to prevent instantiation from outside.
        private CoelacanthSettings(string gameName)
        {
            GameName = gameName;
        }

        public string GameName { get; private set; }

        // Method to initialize the singleton
        public static void Initialize(string gameName)
        {
            if (_instance == null)
            {
                _instance = new CoelacanthSettings(gameName);
            }
        }

        // Property to get the singleton instance.
        public static CoelacanthSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("CoelacanthSettings is not initialized.");
                }
                return _instance;
            }
        }
    }
}
