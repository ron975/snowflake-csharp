﻿using HotChocolate.Types;
using Snowflake.Orchestration.Saving;
using Snowflake.Remoting.GraphQL.FrameworkQueries.Mutations.Relay;
using Snowflake.Remoting.GraphQL.Model.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Support.GraphQL.FrameworkQueries.Mutations.Saving
{
    internal sealed class DeleteSaveProfileInput
        : RelayMutationBase
    {
        public Guid GameID { get; set; }
        public Guid ProfileID { get; set; }
    }

    internal sealed class DeleteSaveProfileInputType
        : InputObjectType<DeleteSaveProfileInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<DeleteSaveProfileInput> descriptor)
        {
            descriptor.Name(nameof(DeleteSaveProfileInput))
                .WithClientMutationId();

            descriptor.Field(g => g.GameID)
                .Name("gameId")
                .Type<NonNullType<UuidType>>();

            descriptor.Field(g => g.ProfileID)
                .Name("profileId")
                .Type<NonNullType<UuidType>>();
        }
    }
}
