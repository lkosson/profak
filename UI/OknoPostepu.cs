using System.Runtime.ExceptionServices;

namespace ProFak.UI;

class OknoPostepu : Dialog
{
	private readonly Label labelNaglowek;
	private readonly Button buttonPrzerwij;
	private readonly Func<CancellationToken, Task> akcja;
	private readonly CancellationTokenSource ctsAnuluj;
	private ExceptionDispatchInfo? edi;

	private OknoPostepu(Func<CancellationToken, Task> akcja, Kontekst kontekst)
		: base("ProFak - czekaj", kontekst)
	{
		this.akcja = akcja;
		ctsAnuluj = new CancellationTokenSource();

		labelNaglowek = Kontrolki.Text("Trwa wykonywanie zleconej operacji");
		var pasek = new ProgressBar { Style = ProgressBarStyle.Marquee };
		buttonPrzerwij = Kontrolki.Button("Przerwij", akcja: Przerwij);
		var uklad = new Pionowo([labelNaglowek, pasek, new Poziomo([buttonPrzerwij])]);
		UstawZawartosc(uklad);
	}

	private void Przerwij()
	{
		buttonPrzerwij.Enabled = false;
		labelNaglowek.Text = "Przerywanie operacji ...";
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
				Invoke(Close);
			}
		});
	}

	public static void Uruchom(Func<CancellationToken, Task> akcja)
	{
		using var kontekst = new Kontekst();
		using var okno = new OknoPostepu(akcja, kontekst);
		okno.Pokaz();
		okno.edi?.Throw();
	}
}
