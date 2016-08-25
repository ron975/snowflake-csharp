﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Configuration;
using Snowflake.Configuration.Hotkey;
using Snowflake.Configuration.Input.Hotkey;
using Snowflake.Emulator;
using Snowflake.Extensibility;
using Snowflake.Plugin.EmulatorAdapter.RetroArch.Adapters.Nestopia.Configuration;
using Snowflake.Plugin.EmulatorAdapter.RetroArch.Executable;
using Snowflake.Plugin.EmulatorAdapter.RetroArch.Input.Hotkeys;
using Snowflake.Records.File;
using Snowflake.Records.Game;
using Snowflake.Service;

namespace Snowflake.Plugin.EmulatorAdapter.RetroArch.Adapters.Nestopia
{
    [Plugin("RetroArchNestopia")]
    public class NestopiaRetroArchAdapter : RetroArchCommonAdapter
    {
        public NestopiaRetroArchAdapter(string appDataDirectory, 
            RetroArchProcessHandler processHandler, IStoneProvider stoneProvider, 
            IConfigurationCollectionStore collectionStore,
            IHotkeyTemplateStore hotkeyStore,
            IBiosManager biosManager, ISaveManager saveManager) 
            : base(appDataDirectory, processHandler, stoneProvider, collectionStore, hotkeyStore, biosManager, saveManager)
        {
            this.DefaultConfigurations = new List<IConfigurationCollection>
            {
                new RetroArchConfiguration(),
                new NestopiaCoreConfigurationCollection()
            };

            this.DefaultHotkeys = new HotkeyTemplateCollection(new KeyboardHotkeyTemplate(), new ControllerHotkeyTemplate());
        }

        public override IEnumerable<IConfigurationCollection> DefaultConfigurations { get; }
        public override IHotkeyTemplateCollection DefaultHotkeys { get; }

        public override IEmulatorInstance Instantiate(IGameRecord gameRecord, IFileRecord file, int saveSlot,
            IList<IEmulatedPort> ports)
        {
            var retroarchConfig =
              this.CollectionStore.GetConfiguration<RetroArchConfiguration>(gameRecord.Guid);
            var nestopiaConfig =
                this.CollectionStore.GetConfiguration<NestopiaCoreConfigurationCollection>(gameRecord.Guid);
            var configurations = new Dictionary<string, IConfigurationCollection>
            {
                {retroarchConfig.FileName, retroarchConfig},
                {"retroarch-core-options.cfg", nestopiaConfig } //convention requires core options to have this file name.
            };
            var platform = this.StoneProvider.Platforms[gameRecord.PlatformId];

            return new RetroArchInstance(gameRecord, file, this, this.processHandler, saveSlot, platform, ports, configurations,
                new HotkeyTemplateCollection(this.hotkeyStore.GetTemplate<KeyboardHotkeyTemplate>(), this.hotkeyStore.GetTemplate<ControllerHotkeyTemplate>()));
        }
    }
}
