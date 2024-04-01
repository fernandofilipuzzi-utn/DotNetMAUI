

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnPermiso_Clicked(object sender, EventArgs e)
        {
            lbnHola.Text = "Hola, Fernando!";
            //SemanticScreenReader.Announce(CounterBtn.Text);

            var status = await Permissions.CheckStatusAsync<CameraAndStorage>();

            //verifica los permisos
            if (status == PermissionStatus.Granted)
            {
                lbnHola.Text = "ya estas ok!";
                TakePhoto();
                return;
            }

            //informo para que quiero los permisos
            if (Permissions.ShouldShowRationale<Permissions.LocationAlways>())
            {
                await Shell.Current.DisplayAlert("dale, entregame el celular!", "te lo pido yo!", "ok");
            }

            //los solicita!
            status = await Permissions.RequestAsync<CameraAndStorage>();

            if (status != PermissionStatus.Granted)
            { 
                await Shell.Current.DisplayAlert("no me diste los permisos", "blabla!", "ok");
            }

            
        }


        public async void TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker?view=net-maui-8.0&tabs=windows
               
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }

    }
}