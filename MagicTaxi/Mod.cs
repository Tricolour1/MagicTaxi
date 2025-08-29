using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace MagicTaxi;

public class Mod : IMod
{
    // The name used for the settings file.
    public const string ModName = "MagicTaxi";
    
    // Static properties to hold the logger and settings instance.
    public static ILog log = LogManager.GetLogger(nameof(MagicTaxi)).SetShowsErrorsInUI(true);
    public static MagicTaxiSettings Settings { get; private set; }

    public void OnLoad(UpdateSystem updateSystem)
    {
        log.Info("Mod loading...");

        // Create a new instance of the settings class
        Settings = new MagicTaxiSettings(this);
        Settings.RegisterInOptionsUI();

        // Load any saved settings from the player's computer
        AssetDatabase.global.LoadSettings(ModName, Settings, new MagicTaxiSettings(this));

        // Register the mod's main system to run during the game simulation phase
        updateSystem.UpdateAt<MagicTaxiSystem>(SystemUpdatePhase.GameSimulation);

        // Optional: Log the mod's asset path for debugging
        if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
        {
            log.Info($"Current mod asset at {asset.path}");
        }
    }

    public void OnDispose()
    {
        log.Info("Mod disposing...");

        // Unregister the settings from the options menu when the mod is disabled
        if (Settings != null)
        {
            Settings.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}
