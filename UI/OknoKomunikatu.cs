namespace ProFak.UI;

class OknoKomunikatu
{
	public static void Informacja(string komunikat)
	{
		MessageBox.Show(komunikat, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	public static void Ostrzezenie(string komunikat)
	{
		MessageBox.Show(komunikat, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	public static bool PytanieTakNie(string komunikat, bool domyslnie = true)
	{
		return MessageBox.Show(komunikat, "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, domyslnie ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes;
	}

	public static bool? PytanieTakNieAnuluj(string komunikat, bool? domyslnie = true)
	{
		var wynik = MessageBox.Show(komunikat, "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, domyslnie switch { true => MessageBoxDefaultButton.Button1, false => MessageBoxDefaultButton.Button2, _ => MessageBoxDefaultButton.Button3 });
		if (wynik == DialogResult.Yes) return true;
		if (wynik == DialogResult.No) return false;
		return null;
	}
}
