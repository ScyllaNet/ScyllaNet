// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Scylla.Net.Data
{
    /// <summary>
    /// Reads a forward-only stream of rows from Cassandra.
    /// </summary>
    /// <inheritdoc />
    public class CqlReader : DbDataReader
    {
        private readonly Dictionary<string, int> _colidx = new Dictionary<string, int>();
        private readonly IEnumerator<Row> _enumerRows;
        private readonly RowSet _popul;
        private readonly IEnumerable<Row> _enumRows;

        public override int Depth => 0;

        /// <inheritdoc />
        public override int FieldCount => _popul.Columns.Length;

        public override bool HasRows => true;

        public override bool IsClosed => false;

        public override int RecordsAffected => -1;

        public override object this[string name] => GetValue(GetOrdinal(name));

        public override object this[int ordinal] => GetValue(ordinal);

        internal CqlReader(RowSet rows)
        {
            _popul = rows;
            for (var idx = 0; idx < _popul.Columns.Length; idx++)
            {
                _colidx.Add(_popul.Columns[idx].Name, idx);
            }

            _enumRows = _popul.GetRows();
            _enumerRows = _enumRows.GetEnumerator();
        }

        public override void Close()
        {

        }

        public override DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override bool GetBoolean(int ordinal)
        {
            return (bool) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override byte GetByte(int ordinal)
        {
            return (byte) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override char GetChar(int ordinal)
        {
            return (char) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override string GetDataTypeName(int ordinal)
        {
            return _popul.Columns[ordinal].TypeCode.ToString();
        }

        /// <inheritdoc />
        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override decimal GetDecimal(int ordinal)
        {
            return (decimal) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override double GetDouble(int ordinal)
        {
            return (double) GetValue(ordinal);
        }

        public override IEnumerator GetEnumerator()
        {
            return new DbEnumerator(this, false);
        }

        /// <inheritdoc />
        public override Type GetFieldType(int ordinal)
        {
            return _popul.Columns[ordinal].Type;
        }

        /// <inheritdoc />
        public override float GetFloat(int ordinal)
        {
            return (float) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override Guid GetGuid(int ordinal)
        {
            return (Guid) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override short GetInt16(int ordinal)
        {
            return (short) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override int GetInt32(int ordinal)
        {
            return (int) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override long GetInt64(int ordinal)
        {
            return (long) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override string GetName(int ordinal)
        {
            return _popul.Columns[ordinal].Name;
        }

        /// <inheritdoc />
        public override int GetOrdinal(string name)
        {
            return _colidx[name];
        }

        public override string GetString(int ordinal)
        {
            return (string) GetValue(ordinal);
        }

        /// <inheritdoc />
        public override object GetValue(int ordinal)
        {
            return _enumerRows.Current[ordinal];
        }

        /// <inheritdoc />
        public override int GetValues(object[] values)
        {
            for (var i = 0; i < _enumerRows.Current.Length; i++)
            {
                values[i] = _enumerRows.Current[i];
            }

            return _enumerRows.Current.Length;
        }

        /// <inheritdoc />
        public override bool IsDBNull(int ordinal)
        {
            return _enumerRows.Current.IsNull(ordinal);
        }

        /// <inheritdoc />
        public override bool NextResult()
        {
            return _enumerRows.MoveNext();
        }

        /// <inheritdoc />
        public override bool Read()
        {
            return _enumerRows.MoveNext();
        }
    }
}
