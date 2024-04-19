
namespace AShortHike.ModdingAPI;

public class LoadStatus
{
    /// <summary>
    /// Whether an actual in-game scene is loaded
    /// </summary>
    public bool GameSceneLoaded => AnySceneLoaded && !MenuSceneLoaded;

    /// <summary>
    /// Whether the main menu scene is loaded
    /// </summary>
    public bool MenuSceneLoaded => Main.ModLoader.CurrentScene == "TitleScene";

    /// <summary>
    /// Whether the main menu or an in-game scene is loaded
    /// </summary>
    public bool AnySceneLoaded => Main.ModLoader.CurrentScene.Length > 0;

    /// <summary>
    /// The name of the currently loaded scene, or ""
    /// </summary>
    public string CurrentScene => Main.ModLoader.CurrentScene;
}
