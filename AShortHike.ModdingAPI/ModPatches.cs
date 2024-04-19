using HarmonyLib;

namespace AShortHike.ModdingAPI;

// Call newgame event
[HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.StartNewGame))]
class TitleScreen_NewGame_Patch
{
    public static void Postfix()
    {
        Main.ModLoader.ProcessModFunction(mod => mod.OnNewGame());
    }
}

// Call loadgame event
[HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.ContinueGame))]
class TitleScreen_ContinueGame_Patch
{
    public static void Postfix()
    {
        Main.ModLoader.ProcessModFunction(mod => mod.OnLoadGame());
    }
}

/// <summary>
/// Main object is always destroyed so we will update the game through the player in the GameScene
/// </summary>
[HarmonyPatch(typeof(Player), "Update")]
class Player_Update_Patch
{
    public static void Postfix() => Main.ModLoader.ProcessModFunction(mod => mod.OnUpdate());
}
[HarmonyPatch(typeof(Player), "LateUpdate")]
class Player_LateUpdate_Patch
{
    public static void Postfix() => Main.ModLoader.ProcessModFunction(mod => mod.OnLateUpdate());
}
