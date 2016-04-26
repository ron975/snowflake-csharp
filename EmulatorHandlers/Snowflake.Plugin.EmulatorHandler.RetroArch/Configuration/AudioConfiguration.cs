using Snowflake.Configuration;
using Snowflake.Configuration.Attributes;
using Snowflake.Plugin.EmulatorHandler.RetroArch.Selections.AudioConfiguration;

//autogenerated using generate_retroarch.py
//checked
namespace Snowflake.Plugin.EmulatorHandler.RetroArch.Configuration
{
    public class AudioConfiguration : ConfigurationSection
    {
        [ConfigurationOption("audio_block_frames", DisplayName = "Block Frames", Private = true)]
        public int AudioBlockFrames { get; set; } = 0;

        [ConfigurationOption("audio_device", DisplayName = "Audio Device", Private = true)]
        public string AudioDevice { get; set; } = "";

        [ConfigurationOption("audio_driver", DisplayName = "Audio Driver", Simple = true)]
        public AudioDriver AudioDriver { get; set; } = AudioDriver.XAudio;
        
        [ConfigurationOption("audio_dsp_plugin", DisplayName = "Audio DSP Plugin", Private = true)]
        public string AudioDspPlugin { get; set; } = "";

        [ConfigurationOption("audio_enable", DisplayName = "Audio Enable", Simple = true)]
        public bool AudioEnable { get; set; } = true;

        [ConfigurationOption("audio_filter_dir", DisplayName = "Audio Filter Dir", IsPath = true)]
        public string AudioFilterDir { get; set; } = "default";

        [ConfigurationOption("audio_latency", DisplayName = "Audio Latency (ms)", Min = 8, Max = 504, Increment = 24)]
        public int AudioLatency { get; set; } = 64;

        [ConfigurationOption("audio_max_timing_skew", DisplayName = "Audio Max Timing Skew", Max = 0.5, Increment = 0.01)]
        public double AudioMaxTimingSkew { get; set; } = 0.050000;

        [ConfigurationOption("audio_mute_enable", DisplayName = "Audio Mute Enable")]
        public bool AudioMuteEnable { get; set; } = false;

        [ConfigurationOption("audio_out_rate", DisplayName = "Audio Output Rate", Private = true)]
        public int AudioOutRate { get; set; } = 48000;

        [ConfigurationOption("audio_rate_control", DisplayName = "Audio Rate Control", Private = true)]
        public bool AudioRateControl { get; set; } = true;

        [ConfigurationOption("audio_rate_control_delta", DisplayName = "Audio Rate Control Delta", Increment = 0.001, Max = 1.0 )]
        public double AudioRateControlDelta { get; set; } = 0.005000;

        [ConfigurationOption("audio_resampler", DisplayName = "Audio Resampler")]
        public AudioResampler AudioResampler { get; set; } = AudioResampler.Sinc;

        [ConfigurationOption("audio_sync", DisplayName = "Audio Sync Enable")]
        public bool AudioSync { get; set; } = true;

        [ConfigurationOption("audio_volume", DisplayName = "Audio Volume (db)", Increment = 1, Max = 10, Min = -80)]
        public double AudioVolume { get; set; } = 0.000000;

        public AudioConfiguration() : base("audio", "Audio Options")
        {

        }

    }
}
