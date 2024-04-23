using Colossal.IO.AssetDatabase;
using Game;
using Game.Settings;
using Game.Modding;
using Game.Prefabs;
using Game.Prefabs.Effects;
using Game.Simulation;
using UnityEngine.PlayerLoop;

namespace MagicTaxi;

[FileLocation((nameof(MagicTaxi)))]

[SettingsUIGroupOrder(kPublicTransportGroup)]

[SettingsUIShowGroupName(kPublicTransportGroup)]

public class MagicTaxiSettings(IMod mod) : ModSetting(mod)
{
    private const string kPublicTransportGroup = "PublicTransport";
    private const string kTaxiSection = "Taxi";
    private const string kBusSection = "Bus";
    private const string kTrainSection = "Train";

    [SettingsUISection(kTaxiSection, kPublicTransportGroup)]
    public bool TaxiRemoverEnabled { get; set; } = true;
    public override void SetDefaults()
    {   
        
    }
}