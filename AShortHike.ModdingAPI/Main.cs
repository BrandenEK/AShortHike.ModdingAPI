using BepInEx;
using System.IO;
using System.Reflection;
using System;
using BepInEx.Logging;

namespace AShortHike.ModdingAPI;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
public class Main : BaseUnityPlugin
{
    public static ModdingAPI ModdingAPI { get; private set; }
    private static ManualLogSource MessageLogger { get; set; }

    private void Awake()
    {
        MessageLogger = Logger;
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadMissingAssemblies);

        ModdingAPI = new ModdingAPI();
    }

    private Assembly LoadMissingAssemblies(object send, ResolveEventArgs args)
    {
        string assemblyPath = Path.GetFullPath($"Modding/data/{args.Name.Substring(0, args.Name.IndexOf(","))}.dll");

        if (File.Exists(assemblyPath))
        {
            MessageLogger.LogWarning("Successfully loaded missing assembly: " + args.Name);
            return Assembly.LoadFrom(assemblyPath);
        }
        else
        {
            MessageLogger.LogWarning("Failed to load missing assembly: " + args.Name);
            return null;
        }
    }
}
