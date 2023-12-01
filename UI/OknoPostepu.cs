using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI;

public partial class OknoPostepu : Form
{
	private readonly Func<Task> akcja;
	private ExceptionDispatchInfo edi;

	public OknoPostepu()
	{
		InitializeComponent();
	}

	private OknoPostepu(Func<Task> akcja)
		: this()
	{
		this.akcja = akcja;
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		Task.Run(async delegate
		{
			try
			{
				await akcja();
			}
			catch (Exception exc)
			{
				edi = ExceptionDispatchInfo.Capture(exc);
			}
			finally
			{
				await ThreadSwitcher.ResumeForegroundAsync(this);
				Close();
			}
		});
	}

	public static void Uruchom(Func<Task> akcja)
	{
		using var okno = new OknoPostepu(akcja);
		okno.ShowDialog();
		if (okno.edi != null) okno.edi.Throw();
	}
}
