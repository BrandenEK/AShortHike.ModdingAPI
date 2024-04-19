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
