using Snowflake.Configuration;
using Snowflake.Configuration.Attributes;

//autogenerated using generate_retroarch.py
namespace Snowflake.Plugin.EmulatorHandler.RetroArch.Configuration.Internal
{
    public class LogConfiguration : ConfigurationSection
    {
        [ConfigurationOption("log_verbosity", DisplayName = "Log Verbosity", Private = true)]
        public bool LogVerbosity { get; set; } = false;

        //todo isenum
        [ConfigurationOption("libretro_log_level", DisplayName = "Libretro Log Level", Private = true)]
        public int LibretroLogLevel { get; set; } = 0;

        [ConfigurationOption("perfcnt_enable", DisplayName = "Enable Performance Counter", Private = true)]
        public bool PerfcntEnable { get; set; } = false;

        public LogConfiguration() : base("log", "Log Options")
        {

        }

    }
}
