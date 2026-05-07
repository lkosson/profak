#if AVALONIA
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ProFak.UI;

class OknoWyboruPliku
{
	public static string? OtworzJeden(string tytul, string? opis = null, string? maska = null, string? katalog = null)
	{
		var storageProvider = new Window().StorageProvider;
		var opts = new FilePickerOpenOptions();
		opts.Title = tytul;
		opts.AllowMultiple = false;
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) opts.FileTypeFilter =  [ new FilePickerFileType(opis) { Patterns = [maska] }, FilePickerFileTypes.All ];
		if (!String.IsNullOrEmpty(katalog)) opts.SuggestedStartLocation = storageProvider.TryGetFolderFromPathAsync(new Uri("file://" + katalog)).GetAwaiter().GetResult();
		var pliki = storageProvider.OpenFilePickerAsync(opts).GetAwaiter().GetResult();
		if (pliki == null || pliki.Count == 0) return null;
		return pliki[0].TryGetLocalPath();
	}

	public static string[]? OtworzWiele(string tytul, string? opis = null, string? maska = null)
	{
		var storageProvider = new Window().StorageProvider;
		var opts = new FilePickerOpenOptions();
		opts.Title = tytul;
		opts.AllowMultiple = false;
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) opts.FileTypeFilter = [new FilePickerFileType(opis) { Patterns = [maska] }, FilePickerFileTypes.All];
		var pliki = storageProvider.OpenFilePickerAsync(opts).GetAwaiter().GetResult();
		if (pliki == null || pliki.Count == 0) return null;
		return pliki.Select(e => e.TryGetLocalPath()!).Where(e => e != null).ToArray();
	}

	public static string? Zapisz(string tytul, string? opis = null, string? maska = null, string? nazwa = null, string? katalog = null)
	{
		var storageProvider = new Window().StorageProvider;
		var opts = new FilePickerSaveOptions();
		opts.Title = tytul;
		if (!String.IsNullOrEmpty(nazwa)) opts.SuggestedFileName = nazwa;
		if (!String.IsNullOrEmpty(katalog)) opts.SuggestedStartLocation = storageProvider.TryGetFolderFromPathAsync(katalog).GetAwaiter().GetResult();
		if (!String.IsNullOrEmpty(opis) && !String.IsNullOrEmpty(maska)) opts.FileTypeChoices = [new FilePickerFileType(opis) { Patterns = [maska] }, FilePickerFileTypes.All];
		if (!String.IsNullOrEmpty(maska)) opts.DefaultExtension = maska;
		var plik = storageProvider.SaveFilePickerAsync(opts).GetAwaiter().GetResult();
		return plik?.TryGetLocalPath();
	}

	public static string? Katalog(string tytul)
	{
		var storageProvider = new Window().StorageProvider;
		var opts = new FolderPickerOpenOptions();
		opts.Title = tytul;
		var katalogi = storageProvider.OpenFolderPickerAsync(opts).GetAwaiter().GetResult();
		if (katalogi == null || katalogi.Count == 0) return null;
		return katalogi[0].TryGetLocalPath();
	}
}
#endif