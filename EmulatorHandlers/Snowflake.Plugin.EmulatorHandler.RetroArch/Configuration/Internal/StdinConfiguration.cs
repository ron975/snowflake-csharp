using Snowflake.Configuration;
using Snowflake.Configuration.Attributes;

//autogenerated using generate_retroarch.py
namespace Snowflake.Plugin.EmulatorHandler.RetroArch.Configuration.Internal
{
    public class StdinConfiguration : ConfigurationSection
    {
        //use network cmds on windows instead
        [ConfigurationOption("stdin_cmd_enable", DisplayName = "Enable Commands through STDIN", Private = true)]
        public bool StdinCmdEnable { get; set; } = false;

        public StdinConfiguration() : base("stdin", "Stdin Options")
        {

        }

    }
}
