﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Auth
{
    internal interface IGssapiClient : IDisposable
    {
        void Init(string service, string host);
        byte[] EvaluateChallenge(byte[] challenge);
    }
}
