using System.Runtime.ExceptionServices;

namespace ProFak.UI;

public partial class OknoPostepu : Form
{
	private readonly Func<CancellationToken, Task> akcja;
	private ExceptionDispatchInfo? edi;
	private CancellationTokenSource ctsAnuluj;

	private OknoPostepu()
	{
		InitializeComponent();
		ctsAnuluj = new CancellationTokenSource();
		akcja = default!;
	}

	private OknoPostepu(Func<Task> akcja)
		: this()
	{
		this.akcja = _ => akcja();
	}

	private OknoPostepu(Func<CancellationToken, Task> akcja)
		: this()
	{
		this.akcja = akcja;
		buttonAnuluj.Enabled = true;
	}

	private void buttonAnuluj_Click(object? sender, EventArgs e)
	{
		buttonAnuluj.Enabled = false;
		labelInfo.Text = "Przerywanie operacji ...";
		ctsAnuluj.Cancel();
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		Task.Run(async delegate
		{
			try
			{
				await akcja(ctsAnuluj.Token);
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

	public static void Uruchom(Func<CancellationToken, Task> akcja)
	{
		using var okno = new OknoPostepu(akcja);
		okno.ShowDialog();
		if (okno.edi != null) okno.edi.Throw();
	}
}
