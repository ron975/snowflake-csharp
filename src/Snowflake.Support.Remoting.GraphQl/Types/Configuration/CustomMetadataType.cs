﻿using GraphQL.Types;
using Snowflake.Support.Remoting.GraphQl.Types.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Support.Remoting.GraphQl.Types.Configuration
{
    public class CustomMetadataType : ObjectGraphType<KeyValuePair<string, object>>
    {
        public CustomMetadataType()
        {
            Field(k => k.Key).Description("The custom metadata key.");
            Field<PrimitiveGraphType>("value",
                description: "The value of the custom metadata.",
                resolve: context => context.Source.Value);
            Field<ValueGraphType>("typedValue",
               description: "The value of the custom metadata.",
               resolve: context => context.Source.Value);
        }
    }
}
