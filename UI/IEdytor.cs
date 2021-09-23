using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	interface IEdytor<TRekord>
		where TRekord : DB.Rekord<TRekord>
	{
		TRekord Rekord { get; set; }
	}
}
