using System.Runtime.ExceptionServices;

namespace ProFak.UI;

class OknoPostepu : TDialog
{
	private readonly TText labelNaglowek;
	private readonly TButton buttonPrzerwij;
	private readonly Func<CancellationToken, Task> akcja;
	private readonly CancellationTokenSource ctsAnuluj;
	private ExceptionDispatchInfo? edi;

	private OknoPostepu(Func<CancellationToken, Task> akcja, Kontekst kontekst)
		: base("ProFak - czekaj", kontekst)
	{
		this.akcja = akcja;
		ctsAnuluj = new CancellationTokenSource();

		labelNaglowek = TKontrolki.Text("Trwa wykonywanie zleconej operacji");
		var pasek = TKontrolki.ProgressBar();
		buttonPrzerwij = TKontrolki.Button("Przerwij", akcja: Przerwij);
		var uklad = new TPionowo([labelNaglowek, pasek, new TPoziomo([buttonPrzerwij])]);
		UstawZawartosc(uklad);
	}

	private void Przerwij()
	{
		buttonPrzerwij.Enabled = false;
		labelNaglowek.Text = "Przerywanie operacji ...";
		ctsAnuluj.Cancel();
	}

	protected override void OknoGotowe()
	{
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
				Zamknij();
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
