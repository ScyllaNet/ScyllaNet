// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Text;
using System.Threading.Tasks;
using Scylla.Net.Tasks;

namespace Scylla.Net.Data.Linq
{
    internal class BatchV2 : Batch
    {
        private readonly BatchStatement _batchScript = new BatchStatement();

        public override bool IsEmpty
        {
            get { return _batchScript.IsEmpty; }
        }

        internal BatchV2(ISession session, BatchType batchType) : base(session, batchType)
        {
        }

        public override void Append(CqlCommand cqlCommand)
        {
            if (cqlCommand.GetTable().GetTableType() == TableType.Counter)
            {
                _batchType = BatchType.Counter;
            }
            _batchScript.Add(cqlCommand);
        }
        
        protected override Task<RowSet> InternalExecuteAsync()
        {
            return InternalExecuteAsync(Configuration.DefaultExecutionProfileName);
        }

        protected override Task<RowSet> InternalExecuteAsync(string executionProfile)
        {
            if (_batchScript.IsEmpty)
            {
                return TaskHelper.FromException<RowSet>(new RequestInvalidException("The Batch must contain queries to execute"));
            }
            _batchScript.SetBatchType(_batchType);
            this.CopyQueryPropertiesTo(_batchScript);
            return _session.ExecuteAsync(_batchScript, executionProfile);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN " + BatchTypeString() + "BATCH");
            foreach (var q in _batchScript.Queries)
            {
                sb.AppendLine(q + ";");
            }

            sb.Append("APPLY BATCH");
            return sb.ToString();
        }
    }
}
