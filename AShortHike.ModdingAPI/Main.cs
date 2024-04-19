using BepInEx;

namespace AShortHike.ModdingAPI;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
public class Main : BaseUnityPlugin
{
    public static ModdingAPI ModdingAPI { get; private set; }

    private void Awake()
    {
        ModdingAPI = new ModdingAPI();
    }
}
