namespace ProFak.UI;

class ComboBoxFix : ComboBox
{
	private bool zmienionoTekst;
	private int wartoscWybranaPodczasEdycji = -1;
	private AutoCompleteMode poprzednieAutoCompleteMode;

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		if (!Focused) Select(0, 0);
	}

	protected override void OnEnter(EventArgs e)
	{
		if (poprzednieAutoCompleteMode != AutoCompleteMode.None)
		{
			// Ustawianie AutoCompleteMode wywołuje RecreateHandleCore,
			// co wywołuje ponownie OnEnter powodując nieskończoną rekurencję.
			var odtwarzaneAutoCompleteMode = poprzednieAutoCompleteMode;
			poprzednieAutoCompleteMode = AutoCompleteMode.None;
			AutoCompleteMode = odtwarzaneAutoCompleteMode;
		}
		wartoscWybranaPodczasEdycji = SelectedIndex;
		zmienionoTekst = false;
		base.OnEnter(e);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		if (Focused) zmienionoTekst = true;
		base.OnTextChanged(e);
	}

	protected override void OnSelectedIndexChanged(EventArgs e)
	{
		wartoscWybranaPodczasEdycji = SelectedIndex;
		zmienionoTekst = false;
		base.OnSelectedIndexChanged(e);
	}

	protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
	{
		if (AutoCompleteMode != AutoCompleteMode.None && !zmienionoTekst)
		{
			poprzednieAutoCompleteMode = AutoCompleteMode;
			// W trybie AutoComplete w OnValidating zostanie nadpisany SelectedIndex na podstawie bieżącego tekstu.
			// Jeśli jest kilka pozycji z takim samym tekstem, to nadpisuje to wybraną pozycję pierwszą pasującą.
			// Jeśli zmieniono tekst ręcznie (na zgodny ze słownikiem lub nie), to mimo wszystko trzeba dopuścić
			// domyślną logikę i pozwolić na wybór pierwszej pasującej podpowiedzianej pozycji.
			AutoCompleteMode = AutoCompleteMode.None;
			SelectedIndex = wartoscWybranaPodczasEdycji;
		}

		base.OnValidating(e);

		// Po zakończeniu base.OnValidating nie można przywracać oryginalnej wartości AutoCompleteMode,
		// bo wywołuje to RecreateHandleCore, które przebudowuje kontrolkę i ponownie nadaje jej focus,
		// przez co nie da się wyjść z pola TABem.
	}
}
