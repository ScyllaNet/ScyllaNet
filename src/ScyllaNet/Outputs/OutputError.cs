// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net
{
    internal abstract class OutputError : IOutput
    {
        // Cache of methods for creating instances of OutputError, indexed by the error code
        private static readonly Dictionary<int, Func<OutputError>> OutputErrorFactoryMethods = new Dictionary<int, Func<OutputError>>()
        {
            // Add factory methods for all known error codes
            {0x0000, () => new OutputServerError()          },
            {0x000A, () => new OutputProtocolError()        },
            {0x0100, () => new OutputBadCredentials()       },
            {0x1000, () => new OutputUnavailableException() },
            {0x1001, () => new OutputOverloaded()           },
            {0x1002, () => new OutputIsBootstrapping()      },
            {0x1003, () => new OutputTruncateError()        },
            {0x1100, () => new OutputWriteTimeout(false)    },
            {0x1200, () => new OutputReadTimeout(false)     },
            {0x1300, () => new OutputReadTimeout(true)      },
            {0x1400, () => new OutputFunctionFailure()      },
            {0x1500, () => new OutputWriteTimeout(true)     },
            {0x2000, () => new OutputSyntaxError()          },
            {0x2100, () => new OutputUnauthorized()         },
            {0x2200, () => new OutputInvalid()              },
            {0x2300, () => new OutputConfigError()          },
            {0x2400, () => new OutputAlreadyExists()        },
            {0x2500, () => new OutputUnprepared()           }
        };

        protected string Message { get; private set; }

        protected int Code { get; private set; }

        public void Dispose()
        {
        }

        public abstract DriverException CreateException();

        protected abstract void Load(FrameReader reader);

        internal static OutputError CreateOutputError(int code, string message, FrameReader cb)
        {
            if (!OutputErrorFactoryMethods.TryGetValue(code, out var factoryMethod))
            {
                throw new DriverInternalError(string.Format("Received unknown error with code {0} and message {1}", code, message));
            }

            var error = factoryMethod();
            error.Message = message;
            error.Code = code;
            error.Load(cb);
            return error;
        }

        public System.Guid? TraceId
        {
            get;
            internal set;
        }
    }
}
