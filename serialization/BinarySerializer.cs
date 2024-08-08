using CoelacanthEngine.config;
using CoelacanthEngine.log;

namespace CoelacanthEngine.serialization
{
    public static class BinarySerializer
    {
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CoelacanthSettings.Instance.GameName, "Data");
        private static readonly Logger _logger = new Logger(typeof(BinarySerializer));

        public static void Save<T>(T obj, string fileName) where T : IBinary
        {
            try
            {
                using FileStream fs = new FileStream(Path.Combine(SaveDirectory, fileName), FileMode.Create, FileAccess.Write);
                using BinaryWriter writer = new BinaryWriter(fs);
                obj.Serialize(writer);
            }
            catch (IOException ex)
            {
                _logger.Error($"IO error occurred while saving the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error($"Access error occurred while saving the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occurred while saving the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
        }

        public static T Load<T>(string fileName) where T : IBinary, new()
        {
            try
            {
                T obj = new T();
                using FileStream fs = new FileStream(Path.Combine(SaveDirectory, fileName), FileMode.Open, FileAccess.Read);
                using BinaryReader reader = new BinaryReader(fs);
                obj.Deserialize(reader);
                return obj;
            }
            catch (FileNotFoundException ex)
            {
                _logger.Error($"File not found: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
            catch (IOException ex)
            {
                _logger.Error($"IO error occurred while loading the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Error($"Access error occurred while loading the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
            catch (Exception ex)
            {
                _logger.Error($"An unexpected error occurred while loading the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it if necessary
            }
        }
    }
}
