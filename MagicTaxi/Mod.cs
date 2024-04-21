using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using UnityEngine.Rendering;

namespace MagicTaxi;
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(MagicTaxi)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));
            updateSystem.UpdateAt<MagicTaxiSystem>(SystemUpdatePhase.GameSimulation);
            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");
        }
        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }