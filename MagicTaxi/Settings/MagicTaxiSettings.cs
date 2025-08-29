using Colossal.IO.AssetDatabase;
using Game.Settings;
using Game.Modding;

namespace MagicTaxi;

[FileLocation(nameof(MagicTaxi))]
// These attributes are commented out as they are for a more complex UI than needed here.
// [SettingsUIGroupOrder(kPublicTransportGroup)]
// [SettingsUIShowGroupName(kPublicTransportGroup)]
public class MagicTaxiSettings(IMod mod) : ModSetting(mod)
{
    // We don't need UI groups for a single setting, so these can be removed for simplicity.
    // private const string kPublicTransportGroup = "PublicTransport";
    // private const string kTaxiSection = "Taxi";

    // This attribute will create a simple checkbox in the options menu.
    [SettingsUISection(kDllName)]
    public bool TaxiRemoverEnabled { get; set; }

    public override void SetDefaults()
    {
        // This ensures the setting is checked by default and when reset.
        TaxiRemoverEnabled = true;
    }
}
