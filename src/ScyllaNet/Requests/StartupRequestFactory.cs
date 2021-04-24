// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal class StartupRequestFactory : IStartupRequestFactory
    {
        private readonly IStartupOptionsFactory _optionsFactory;

        public StartupRequestFactory(IStartupOptionsFactory optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public IRequest CreateStartupRequest(ProtocolOptions protocolOptions)
        {
            return new StartupRequest(_optionsFactory.CreateStartupOptions(protocolOptions));
        }
    }
}
