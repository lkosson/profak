using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Rekord<T> where T : Rekord<T>
	{
		public int Id { get; set; }
		public Ref<T> Ref => new Ref<T>(Id);

		public override string ToString() => Ref.ToString();

		public override bool Equals(object otherObj) => otherObj is Rekord<T> other && other == this;
		public override int GetHashCode() => Id.GetHashCode();
		public static bool operator ==(Rekord<T> rekord1, Rekord<T> rekord2) => rekord1 is null ? rekord2 is null : rekord2 is not null && rekord1.Id == rekord2.Id;
		public static bool operator !=(Rekord<T> rekord1, Rekord<T> rekord2) => !(rekord1 == rekord2);

		public T Kopia() => (T)MemberwiseClone();
	}
}
