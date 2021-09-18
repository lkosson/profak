using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class ProFakContext : DbContext
	{
		public string Sciezka { get; set; }

		public ProFakContext()
		{
			Sciezka = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "profak.sqlite3");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlite($"Data Source={Sciezka}");
		}
	}
}
