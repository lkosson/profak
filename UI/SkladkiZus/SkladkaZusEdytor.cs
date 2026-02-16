using ProFak.DB;

namespace ProFak.UI
{
	partial class SkladkaZusEdytor : SkladkaZusEdytorBase
	{
		public SkladkaZusEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(dateTimePickerMiesiac, skladka => skladka.Miesiac);

			kontroler.Powiazanie(numericUpDownPodstawaSpoleczne, skladka => skladka.PodstawaSpoleczne);
			kontroler.Powiazanie(numericUpDownPodstawaZdrowotne, skladka => skladka.PodstawaZdrowotne);
			kontroler.Powiazanie(numericUpDownSkladkaEmerytalna, skladka => skladka.SkladkaEmerytalna);
			kontroler.Powiazanie(numericUpDownSkladkaRentowa, skladka => skladka.SkladkaRentowa);
			kontroler.Powiazanie(numericUpDownSkladkaWypadkowa, skladka => skladka.SkladkaWypadkowa);
			kontroler.Powiazanie(numericUpDownSkladkaSpoleczna, skladka => skladka.SkladkaSpoleczna);
			kontroler.Powiazanie(numericUpDownSkladkaZdrowotna, skladka => skladka.SkladkaZdrowotna);
			kontroler.Powiazanie(numericUpDownRozliczenieRoczneSkladkiZdrowotnej, skladka => skladka.RozliczenieRoczneSkladkiZdrowotnej);
			kontroler.Powiazanie(numericUpDownFunduszPracy, skladka => skladka.SkladkaFunduszPracy);
			kontroler.Powiazanie(numericUpDownSumaSkladek, skladka => skladka.SumaSkladek);
			kontroler.Powiazanie(numericUpDownOdliczenieOdDochodu, skladka => skladka.OdliczenieOdDochodu);
		}

		private void buttonPrzelicz_Click(object? sender, EventArgs e)
		{
			try
			{
				Przelicz();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		private void Przelicz()
		{
			Rekord.Przelicz(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}
	}

	class SkladkaZusEdytorBase : Edytor<SkladkaZus>
	{
	}
}
