using CoinFlipMVVM.ViewModels;

namespace CoinFlipMVVM.Views;

public partial class CoinView : ContentPage
{
	public CoinView()
	{
		InitializeComponent();

		this.BindingContext = new CoinViewModel();
	}
}