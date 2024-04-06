
namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        DeviceHelper _device;
        public MainPage()
        {
            InitializeComponent();

            _device = new DeviceHelper();
        }

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            lbnHola.Text = "Hola, Fernando!";

            var result = await _device.TakePhoto(this);

            if (result != null)
            {
                myImage.Source = result?.Path; //imagen;
                await Shell.Current.DisplayAlert("Foto realizada", "Visualizando foto!", "ok");
            }
            else
                await Shell.Current.DisplayAlert("No hay foto que mostrar", "Error", "ok");
        }
    }
}