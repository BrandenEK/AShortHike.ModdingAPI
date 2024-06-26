﻿using AShortHike.ModdingAPI.Files;
using AShortHike.ModdingAPI.Logging;
using HarmonyLib;

namespace AShortHike.ModdingAPI;

public abstract class ShortHikeMod
{
    // Mod info

    /// <summary>
    /// The unique id of the mod
    /// </summary>
    public string Id => id;
    private readonly string id;

    /// <summary>
    /// The display name of the mod
    /// </summary>
    public string Name => name;
    private readonly string name;

    /// <summary>
    /// The developer of the mod
    /// </summary>
    public string Author => author;
    private readonly string author;

    /// <summary>
    /// The file version of the mod
    /// </summary>
    public string Version => version;
    private readonly string version;

    // Helpers

    /// <summary>
    /// Handles scene loading, such as checking if on main menu
    /// </summary>
    public LoadStatus LoadStatus { get; }

    // Handlers

    /// <summary>
    /// Handles file IO, such as loading data or writing to a file
    /// </summary>
    public FileHandler FileHandler { get; }

    /// <summary>
    /// Handles logging messages to the console and to a file
    /// </summary>
    public LogHandler LogHandler { get; }

    // Events

    /// <summary>
    /// Called when starting the game, at the same time as other managers
    /// </summary>
    protected internal virtual void OnInitialize() { }

    /// <summary>
    /// Called when starting the game, after all other mods have been initialized
    /// </summary>
    protected internal virtual void OnAllInitialized() { }

    /// <summary>
    /// Called when exiting the game, at the same time as other managers
    /// </summary>
    protected internal virtual void OnDispose() { }

    /// <summary>
    /// Called every frame after initialization
    /// </summary>
    protected internal virtual void OnUpdate() { }

    /// <summary>
    /// Called at the end of every frame after initialization
    /// </summary>
    protected internal virtual void OnLateUpdate() { }

    /// <summary>
    /// Called when a new level is loaded, including the main menu
    /// </summary>
    protected internal virtual void OnLevelLoaded(string level) { }

    /// <summary>
    /// Called when an old level is unloaded, including the main menu
    /// </summary>
    protected internal virtual void OnLevelUnloaded(string level) { }

    /// <summary>
    /// Called when starting a new game on the main menu, after data is reset
    /// </summary>
    protected internal virtual void OnNewGame() { }

    /// <summary>
    /// Called when loading an existing game on the main menu, after data is reset
    /// </summary>
    protected internal virtual void OnLoadGame() { }

    /// <summary>
    /// Called when quiting a game, after returning to the main menu
    /// </summary>
    protected internal virtual void OnExitGame() { }

    // Constructor

    /// <summary>
    /// Initializes and registers a new BlasII mod
    /// </summary>
    public ShortHikeMod(string id, string name, string author, string version)
    {
        // Set data
        this.id = id;
        this.name = name;
        this.author = author;
        this.version = version;

        // Set helpers
        LoadStatus = new LoadStatus();

        // Set handlers
        FileHandler = new FileHandler(this);
        LogHandler = new LogHandler(this);

        // Register and patch mod
        if (Main.ModLoader.RegisterMod(this))
        {
            new Harmony(id).PatchAll(GetType().Assembly);
        }
    }

}
