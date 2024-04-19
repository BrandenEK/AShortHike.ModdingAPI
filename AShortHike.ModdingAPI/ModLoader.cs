using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AShortHike.ModdingAPI;

internal class ModLoader
{
    private readonly List<ShortHikeMod> _mods = new();

    private bool _initialized = false;
    private bool _loadedMenu = false;

    public ModLoader()
    {
        SceneManager.sceneLoaded += LevelLoaded;
        SceneManager.sceneUnloaded += LevelUnloaded;
        Application.quitting += Dispose;
    }

    /// <summary>
    /// Loops over the list of registered mods and performs an action on each one
    /// </summary>
    public void ProcessModFunction(System.Action<ShortHikeMod> action)
    {
        foreach (var mod in _mods)
        {
            try
            {
                action(mod);
            }
            catch (System.Exception e)
            {
                mod.LogHandler.Error($"Encountered error: {e.Message}\n{e.CleanStackTrace()}");
            }
        }
    }

    /// <summary>
    /// Disposes all mods
    /// </summary>
    public void Dispose()
    {
        ProcessModFunction(mod => mod.OnDispose());

        SceneManager.sceneLoaded -= LevelLoaded;
        SceneManager.sceneUnloaded -= LevelUnloaded;
        Application.quitting -= Dispose;
        Main.ModdingAPI.LogHandler.Info("All mods disposed!");
    }

    /// <summary>
    /// Updates all mods
    /// </summary>
    public void Update()
    {
        if (!_initialized) return;

        ProcessModFunction(mod => mod.OnUpdate());
    }

    /// <summary>
    /// Late updates all mods
    /// </summary>
    public void LateUpdate()
    {
        if (!_initialized) return;

        ProcessModFunction(mod => mod.OnLateUpdate());
    }

    /// <summary>
    /// Processes a LoadScene event for all mods
    /// </summary>
    public void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        Main.ModdingAPI.LogHandler.Info($"Loaded level: {scene.name}");

        if (scene.name == "TitleScene")
        {
            if (!_initialized)
            {
                Main.ModdingAPI.LogHandler.Info("Initializing mods...");
                ProcessModFunction(mod => mod.OnInitialize());

                Main.ModdingAPI.LogHandler.Info("All mods initialized!");
                ProcessModFunction(mod => mod.OnAllInitialized());
            }

            if (_loadedMenu)
                ProcessModFunction(mod => mod.OnExitGame());

            _initialized = true;
            _loadedMenu = true;
        }

        ProcessModFunction(mod => mod.OnLevelLoaded(scene.name));
    }

    /// <summary>
    /// Processes an UnloadScene event for all mods
    /// </summary>
    public void LevelUnloaded(Scene scene)
    {
        ProcessModFunction(mod => mod.OnLevelUnloaded(scene.name));
    }

    /// <summary>
    /// Registers a new mod whenever it is first created
    /// </summary>
    public bool RegisterMod(ShortHikeMod mod)
    {
        if (_mods.Any(m => m.Id == mod.Id))
        {
            Main.MessageLogger.LogError($"Mod with id '{mod.Id}' already exists!");
            return false;
        }

        Main.MessageLogger.LogMessage($"Registering mod: {mod.Id} ({mod.Version})");
        _mods.Add(mod);
        return true;
    }
}
