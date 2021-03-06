// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Reflection;

namespace Scylla.Net.DataStax.Auth.Sspi
{
    [AttributeUsage( AttributeTargets.Field )]
    internal class EnumStringAttribute : Attribute
    {
        public EnumStringAttribute( string text )
        {
            Text = text;
        }

        public string Text { get; private set; }
    }

    internal class EnumMgr
    {
        public static string ToText( Enum value )
        {
            var field = value.GetType().GetField( value.ToString() );

            if (field == null)
            {
                return null;
            }

            var attribs = (EnumStringAttribute[])field.GetCustomAttributes( typeof( EnumStringAttribute ), false );

            if( attribs == null || attribs.Length == 0 )
            {
                return null;
            }
            else
            {
                return attribs[0].Text;
            }
        }

        public static T FromText<T>( string text )
        {
            var fields = typeof( T ).GetFields();

            EnumStringAttribute[] attribs;

            foreach( var field in fields )
            {
                attribs = (EnumStringAttribute[])field.GetCustomAttributes( typeof( EnumStringAttribute ), false );

                foreach( var attrib in attribs )
                {
                    if( attrib.Text == text )
                    {
                        return (T)field.GetValue( null );
                    }
                }
            }

            throw new ArgumentException( "Could not find a matching enumeration value for the text '" + text + "'." );
        }
    }
}
