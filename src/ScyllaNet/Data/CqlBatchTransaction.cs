// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Scylla.Net.Data
{
    public sealed class CqlBatchTransaction : DbTransaction
    {
        private readonly List<CqlCommand> commands = new List<CqlCommand>();
        internal CqlConnection CqlConnection;

        protected override DbConnection DbConnection
        {
            get { return CqlConnection; }
        }

        public override IsolationLevel IsolationLevel
        {
            get { return IsolationLevel.Unspecified; }
        }

        public CqlBatchTransaction(CqlConnection cqlConnection)
        {
            CqlConnection = cqlConnection;
        }

        public void Append(CqlCommand cmd)
        {
            if (!ReferenceEquals(CqlConnection, cmd.Connection))
                throw new InvalidOperationException();

            commands.Add(cmd);
        }

        public override void Commit()
        {
            foreach (CqlCommand cmd in commands)
                cmd.ExecuteNonQuery();
            commands.Clear();
            CqlConnection.ClearDbTransaction();
        }

        public override void Rollback()
        {
            commands.Clear();
            CqlConnection.ClearDbTransaction();
        }
    }
}
