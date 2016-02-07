using System;
using System.Drawing;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Schedule
{
	[JsonObject]
	public class Station : IConvertible
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public bool Available { get; set; }

		public Station()
		{
			Name = string.Empty;
			Available = true;
		}

		public override string ToString()
		{
			return Name;
		}

		#region IConvertible Memebers
		public TypeCode GetTypeCode()
		{
			return Type.GetTypeCode(GetType());
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public string ToString(IFormatProvider provider)
		{
			return ToString();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException();
		}
		#endregion

		public void Dispose()
		{
			if (Logo != null)
				Logo.Dispose();
		}
	}
}
