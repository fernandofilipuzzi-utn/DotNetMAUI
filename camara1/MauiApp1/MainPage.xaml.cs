
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

        private async void btnPermiso_Clicked(object sender, EventArgs e)
        {
            lbnHola.Text = "Hola, Fernando!";

            //string imagen=await _device.TakePhoto(this);
            var imagen = await _device.TakePhoto1(this);

            if (imagen != null)
            {
                myImage.Source = imagen;
                await Shell.Current.DisplayAlert("ahí va calorina", "hecho!", "ok");
            }
            else
                await Shell.Current.DisplayAlert("ahí no va!", "nada", "ok");
        }
    }
}