using Snowflake.Configuration;

// autogenerated using generate_retroarch.py
namespace Snowflake.Plugin.Emulators.RetroArch.Configuration.Internal
{
    /// <summary>
    ///     Used internally by Retroarch GUI
    ///     Since we wrap this, we don't care about this.
    /// </summary>
    [ConfigurationSection("bundle", "Bundle Options")]
    public partial interface BundleConfiguration
    {
        [ConfigurationOption("bundle_assets_extract_enable", false, DisplayName = "Bundle Assets Extract Enable",
            Private = true)]
        bool BundleAssetsExtractEnable { get; set; }

        [ConfigurationOption("bundle_assets_extract_last_version", 0,
            DisplayName = "Bundle Assets Extract Last Version",
            Private = true)]
        int BundleAssetsExtractLastVersion { get; set; }

        [ConfigurationOption("bundle_assets_extract_version_current", 0,
            DisplayName = "Bundle Assets Extract Version Current", Private = true)]
        int BundleAssetsExtractVersionCurrent { get; set; }
    }
}
