using BepInEx;

namespace AShortHike.ModdingAPI;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("AShortHike.ModdingAPI", "0.1.0")]
public class Main : BaseUnityPlugin
{
    public static ModdingAPI ModdingAPI { get; private set; }

    private void Awake()
    {
        ModdingAPI = new ModdingAPI();
    }
}
