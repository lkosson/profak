using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Zawartosc : Rekord<Zawartosc>
	{
		public byte[] Dane { get; set; }
		public int? PlikId { get; set; }

		public Ref<Plik> PlikRef { get => PlikId; set => PlikId = value; }

		public Plik Plik { get; set; }
	}
}
