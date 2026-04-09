namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class RecipesPage : ContentPage
{
	public RecipesPage()
	{
		InitializeComponent();
		BindingContext = new RecipesViewModel();
	}
}