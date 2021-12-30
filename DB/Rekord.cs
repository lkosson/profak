using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Rekord
	{
		public int Id { get; set; }

		public virtual void WypelnijDomyslnePola(Baza baza)
		{
		}

		public virtual bool CzyPasuje(string fraza) => CzyPasuje(Id, fraza);

		protected static bool CzyPasuje(string pole, string fraza) => pole != null && pole.Contains(fraza, StringComparison.CurrentCultureIgnoreCase);
		protected static bool CzyPasuje(int pole, string fraza) => Int32.TryParse(fraza, out var wartosc) && pole == wartosc;
		protected static bool CzyPasuje(decimal pole, string fraza) => Decimal.TryParse(fraza, out var wartosc) && pole == wartosc;
		protected static bool CzyPasuje(DateTime pole, string fraza) => CzyPasuje(pole.ToString("yyyy-MM-dd"), fraza);
		protected static bool CzyPasuje(object pole, string fraza) => pole != null && CzyPasuje(pole.ToString(), fraza);

		protected decimal Zaokragl(decimal wartosc) => Decimal.Round(wartosc, 2, MidpointRounding.AwayFromZero);

		public static string Format<TEnum>(TEnum value) where TEnum : Enum
		{
			var raw = value.ToString();
			var sb = new StringBuilder(raw.Length + 4);
			for (int i = 0; i < raw.Length; i++)
			{
				var ch = raw[i];
				if (Char.IsUpper(ch) && i > 0 /* && !Char.IsUpper(raw[i - 1]) ZlecenieWPrzygotowaniu */)
				{
					sb.Append(' ');
					if (i < raw.Length - 1) ch = Char.ToLower(ch); /* PaczkomatB */
				}
				sb.Append(ch);
			}
			return sb.ToString();
		}
	}

	class Rekord<T> : Rekord, IConvertible
		where T : Rekord<T>
	{
		public Ref<T> Ref => new Ref<T>(Id);

		public override string ToString() => Ref.ToString();

		public override bool Equals(object otherObj) => otherObj is Rekord<T> other && other == this;
		public override int GetHashCode() => Id.GetHashCode();
		public static bool operator ==(Rekord<T> rekord1, Rekord<T> rekord2) => rekord1 is null ? rekord2 is null : rekord2 is not null && rekord1.Id == rekord2.Id;
		public static bool operator !=(Rekord<T> rekord1, Rekord<T> rekord2) => !(rekord1 == rekord2);

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
