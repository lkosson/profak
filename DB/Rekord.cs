using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Rekord<T> : IConvertible
		where T : Rekord<T>
	{
		public int Id { get; set; }
		public Ref<T> Ref => new Ref<T>(Id);

		public override string ToString() => Ref.ToString();

		public override bool Equals(object otherObj) => otherObj is Rekord<T> other && other == this;
		public override int GetHashCode() => Id.GetHashCode();
		public static bool operator ==(Rekord<T> rekord1, Rekord<T> rekord2) => rekord1 is null ? rekord2 is null : rekord2 is not null && rekord1.Id == rekord2.Id;
		public static bool operator !=(Rekord<T> rekord1, Rekord<T> rekord2) => !(rekord1 == rekord2);

		public T Kopia() => (T)MemberwiseClone();

		public TypeCode GetTypeCode() => TypeCode.Object;
		public bool ToBoolean(IFormatProvider provider) => throw new NotSupportedException();
		public byte ToByte(IFormatProvider provider) => throw new NotSupportedException();
		public char ToChar(IFormatProvider provider) => throw new NotSupportedException();
		public DateTime ToDateTime(IFormatProvider provider) => throw new NotSupportedException();
		public decimal ToDecimal(IFormatProvider provider) => throw new NotSupportedException();
		public double ToDouble(IFormatProvider provider) => throw new NotSupportedException();
		public short ToInt16(IFormatProvider provider) => throw new NotSupportedException();
		public int ToInt32(IFormatProvider provider) => Id;
		public long ToInt64(IFormatProvider provider) => Id;
		public sbyte ToSByte(IFormatProvider provider) => throw new NotSupportedException();
		public float ToSingle(IFormatProvider provider) => throw new NotSupportedException();
		public string ToString(IFormatProvider provider) => ToString();
		public object ToType(Type conversionType, IFormatProvider provider) => conversionType == typeof(Ref<T>) ? Ref : throw new NotSupportedException();
		public ushort ToUInt16(IFormatProvider provider) => throw new NotSupportedException();
		public uint ToUInt32(IFormatProvider provider) => throw new NotSupportedException();
		public ulong ToUInt64(IFormatProvider provider) => throw new NotSupportedException();
	}
}
