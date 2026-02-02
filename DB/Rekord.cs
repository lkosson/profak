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
		protected static bool CzyPasuje(DateTime pole, string fraza) => CzyPasuje(pole.ToString(UI.Format.Data), fraza);
		protected static bool CzyPasuje(object pole, string fraza) => pole != null && CzyPasuje(pole.ToString(), fraza);

		public static string Format<TEnum>(TEnum value) where TEnum : Enum
		{
			var raw = value.ToString();
			if (raw.Contains('_')) return raw.Replace('_', ' ').Trim(); /* _10_L */

			var allup = true;
			for (int i = 0; i < raw.Length; i++)
			{
				var ch = raw[i];
				if (!Char.IsUpper(ch) && !Char.IsDigit(ch) && !Char.IsWhiteSpace(ch))
				{
					allup = false;
					break;
				}
			}

			if (allup) return raw; /* WZ, MP3 */

			var sb = new StringBuilder(raw.Length * 2);

			int start = 0;
			allup = true;
			for (int i = 1 /* !! */; i <= /* !! */ raw.Length; i++)
			{
				var end = i == raw.Length;
				var prevLower = Char.IsLower(raw[i - 1]);
				var curLower = end ? false : Char.IsLower(raw[i]);

				if ((prevLower && !curLower) || end)
				{
					// OdbiorWlasny
					// ^    ^^
					// s    pc
					for (int j = start; j < i; j++)
					{
						sb.Append(allup || j == 0 ? raw[j] : Char.ToLower(raw[j]));
					}
					if (!end) sb.Append(' ');
					start = i;
					allup = true;
				}
				else if (!prevLower && curLower)
				{
					var len = i - 1 - start;
					if (len == 0)
					{
						// Brak
						// ^^
						// sc
					}
					else if (len == 1)
					{
						if (start == 0)
						{
							// WMagazynie
							// ^^^
							// spc
							sb.Append(raw[start]);
						}
						else
						{
							// ZlecenieWPrzygotowaniu
							//         ^^^
							//         spc
							sb.Append(Char.ToLower(raw[start]));
						}
						if (!end) sb.Append(' ');
					}
					else if (len > 1)
					{
						// DokumentWZObcy
						//         ^ ^^
						//         s pc
						sb.Append(raw[start..(i - 1)]);
						if (!end) sb.Append(' ');
					}
					start = i - 1;
					allup = false;
				}
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
