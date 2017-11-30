﻿using GraphQL.Types;
using Snowflake.Configuration;
using Snowflake.Support.Remoting.GraphQl.Types.Values;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Snowflake.Support.Remoting.GraphQl.Types.Configuration
{
    public class ConfigurationOptionType : ObjectGraphType<IConfigurationOption>
    {
        public ConfigurationOptionType()
        {
            Name = "ConfigurationOption";
            Description = "Describes a configuration option.";
            Field(o => o.Description).Description("The option description.");
            Field(o => o.DisplayName).Description("The human readable name of this option.");
            Field(o => o.OptionName).Description("The option name as it is serialized into the configuration.");
            Field(o => o.KeyName).Description("The key name of this option.");
            Field(o => o.Simple).Description("Whether or not this option is for 'simple' display mode.");
            Field(o => o.Private).Description("Whether or not tihs option should be showed to the user.");
            Field(o => o.Flag).Description("Whether or not this option is a flag.");
            Field(o => o.IsPath).Description("Whether or not this option is a file path option.");
            Field(o => o.Min).Description("The minimum value allowed if this option is a numeric option.");
            Field(o => o.Max).Description("The maximum value allowed if this option is a numeric option.");
            Field(o => o.Increment).Description("The increment allowed if this option is a numeric option.");
            Field<ConfigurationOptionTypeEnum>("type",
                description: "The option value type",
                resolve: context => ConfigurationOptionTypeEnum.GetType(context.Source));
            Field<ListGraphType<CustomMetadataType>>("customMetadata",
                description: "Any custom metadata this option may have.",
                resolve: context => context.Source.CustomMetadata);
            Field<PrimitiveGraphType>("default",
                description: "The default value of this option.",
                resolve: context => context.Source.Default);
            Field<ValueGraphType>("typedDefault",
             description: "The default value of this option, boxed into a union of possible types.",
             resolve: context => context.Source.Default);
        }
    }
}
