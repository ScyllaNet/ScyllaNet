// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.Helpers;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class PlatformInfoProvider : IInsightsInfoProvider<InsightsPlatformInfo>
    {
        public InsightsPlatformInfo GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var cpuInfo = PlatformHelper.GetCpuInfo();
            var dependencies = typeof(PlatformInfoProvider).GetTypeInfo().Assembly.GetReferencedAssemblies().Select(name =>
                new AssemblyInfo
                {
                    FullName = name.FullName,
                    Name = name.Name,
                    Version = name.Version?.ToString()
                });

            var dependenciesDictionary = new Dictionary<string, AssemblyInfo>();
            foreach (var p in dependencies)
            {
                // Use the first value in list
                if (!dependenciesDictionary.ContainsKey(p.Name))
                {
                    dependenciesDictionary[p.Name] = p;
                }
            }

            return new InsightsPlatformInfo
            {
                CentralProcessingUnits = new CentralProcessingUnitsInfo
                {
                    Length = cpuInfo.Length,
                    Model = cpuInfo.FirstCpuName
                },
                OperatingSystem = new OperatingSystemInfo
                {
                    Name = RuntimeInformation.OSDescription,
                    Version = RuntimeInformation.OSDescription,
                    Arch = RuntimeInformation.OSArchitecture.ToString()
                },
                Runtime = new RuntimeInfo
                {
                    Dependencies = dependenciesDictionary,
                    RuntimeFramework = RuntimeInformation.FrameworkDescription,
                    TargetFramework = PlatformHelper.GetTargetFramework(),
                }
            };
        }
    }
}
