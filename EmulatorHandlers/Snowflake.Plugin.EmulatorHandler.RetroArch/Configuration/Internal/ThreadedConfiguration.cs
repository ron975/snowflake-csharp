using Snowflake.Configuration;
using Snowflake.Configuration.Attributes;

//autogenerated using generate_retroarch.py
namespace Snowflake.Plugin.EmulatorHandler.RetroArch.Configuration.Internal
{
    public class ThreadedConfiguration : ConfigurationSection
    {
        //this probably shouldn't be touched
        [ConfigurationOption("threaded_data_runloop_enable", DisplayName = "Threaded Data Runloop Enable", Private = false)]
        public bool ThreadedDataRunloopEnable { get; set; } = true;

        public ThreadedConfiguration() : base("threaded", "Threaded Options")
        {

        }

    }
}
