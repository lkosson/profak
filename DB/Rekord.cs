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

		public override string ToString() => GetType().Name + "#" + Id;

		public override bool Equals(object otherObj) => otherObj is Rekord other && other.Id == Id && otherObj.GetType() == GetType();
		public override int GetHashCode() => Id;
		public static bool operator ==(Rekord rekord1, Rekord rekord2) => rekord1 is null ? rekord2 is null : rekord1.Equals(rekord2);
		public static bool operator !=(Rekord rekord1, Rekord rekord2) => !(rekord1 == rekord2);
	}
}
