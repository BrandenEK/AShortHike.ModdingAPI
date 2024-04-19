using BepInEx.Logging;

namespace AShortHike.ModdingAPI.Logging;

public class LogHandler
{
    private readonly ManualLogSource _logger;

    public LogHandler(ShortHikeMod mod)
    {
        _logger = Logger.CreateLogSource(mod.Name);
    }

    /// <summary>
    /// Logs a message in white to the console
    /// </summary>
    public void Info(object message) => _logger.LogMessage(message);

    /// <summary>
    /// Logs a message in yellow to the console
    /// </summary>
    public void Warning(object warning) => _logger.LogWarning(warning);

    /// <summary>
    /// Logs a message in red to the console
    /// </summary>
    public void Error(object error) => _logger.LogError(error);
}
