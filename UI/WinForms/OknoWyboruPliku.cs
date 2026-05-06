#if WINFORMS
namespace ProFak.UI;

class OknoWyboruPliku
{
	public static string? OtworzJeden(string tytul, string? opis = null, string? maska = null, string? katalog = null)
	{
		using var dialog = new OpenFileDialog();
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) dialog.Filter = $"{opis} ({maska})|{maska}|Wszystkie pliki (*.*)|*.*";
		else dialog.Filter = "Wszystkie pliki (*.*)|*.*";
		dialog.Title = tytul;
		dialog.RestoreDirectory = true;
		if (!string.IsNullOrEmpty(katalog)) dialog.InitialDirectory = katalog;
		if (dialog.ShowDialog() != DialogResult.OK) return null;
		return dialog.FileName;
	}

	public static string[]? OtworzWiele(string tytul, string? opis = null, string? maska = null)
	{
		using var dialog = new OpenFileDialog();
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) dialog.Filter = $"{opis} ({maska})|{maska}|Wszystkie pliki (*.*)|*.*";
		else dialog.Filter = "Wszystkie pliki (*.*)|*.*";
		dialog.Title = tytul;
		dialog.RestoreDirectory = true;
		dialog.Multiselect = true;
		if (dialog.ShowDialog() != DialogResult.OK) return null;
		return dialog.FileNames;
	}

	public static string? Zapisz(string tytul, string? opis = null, string? maska = null, string? nazwa = null, string? katalog = null)
	{
		using var dialog = new SaveFileDialog();
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) dialog.Filter = $"{opis} ({maska})|{maska}";
		dialog.Title = tytul;
		dialog.RestoreDirectory = true;
		if (!String.IsNullOrEmpty(nazwa)) dialog.FileName = nazwa;
		if (!string.IsNullOrEmpty(katalog)) dialog.InitialDirectory = katalog;
		if (dialog.ShowDialog() != DialogResult.OK) return null;
		return dialog.FileName;
	}

	public static string? Katalog(string tytul)
	{
		using var dialog = new FolderBrowserDialog();
		dialog.Description = tytul;
		dialog.AutoUpgradeEnabled = false;
		if (dialog.ShowDialog() != DialogResult.OK) return null;
		return dialog.SelectedPath;
	}
}
#endif