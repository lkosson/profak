namespace ProFak.DB;

public class StanMenu : Rekord<StanMenu>
{
	public string Pozycja { get; set; } = "";
	public bool CzyZwinieta { get; set; }
	public bool CzyUkryta { get; set; }
	public bool CzyAktywna { get; set; }

	public override string ToString() => Pozycja;
}
