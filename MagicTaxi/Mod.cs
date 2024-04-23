using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace MagicTaxi;
    public class Mod : IMod
    {
        public static MagicTaxiSettings activeSettings { get; private set; }
        public static ILog log = LogManager.GetLogger($"{nameof(MagicTaxi)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public const string Name = "RollW_TaxiRemover";
        public void OnLoad(UpdateSystem updateSystem)
        {
            /*(MagicTaxiSystem magicTaxiSystem = new MagicTaxiSystem();
            MagicTaxiSettings activeSettings = new (this);
            activeSettings.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings(Name,
                activeSettings, new MagicTaxiSettings(this));*/
            updateSystem.UpdateAt<MagicTaxiSystem>(SystemUpdatePhase.GameSimulation);
            log.Info(nameof(OnLoad));
            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");
            
        }
        public void OnDispose()
        {
            //activeSettings.UnregisterInOptionsUI();
            log.Info(nameof(OnDispose));
        }
    }