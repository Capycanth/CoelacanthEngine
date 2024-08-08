using CoelacanthEngine.config;
using CoelacanthEngine.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoelacanthEngine.serialization
{
    public static class JSONSerializer
    {
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CoelacanthSettings.Instance.GameName, "Data");
        private static readonly Logger _logger = new Logger(typeof(JSONSerializer));

        public static void Save<T>(T obj, string fileName)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(obj);
                File.WriteAllText(Path.Combine(SaveDirectory, fileName), jsonString);
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

        public static T Load<T>(string fileName)
        {
            try
            {
                // Read the JSON string from the file
                string jsonString = File.ReadAllText(Path.Combine(SaveDirectory, fileName));

                // Deserialize the JSON string to an object of type T
                return JsonSerializer.Deserialize<T>(jsonString);
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
            catch (JsonException ex)
            {
                _logger.Error($"JSON error occurred while deserializing the file: {ex.Message}");
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
