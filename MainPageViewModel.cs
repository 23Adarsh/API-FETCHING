using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace APIFetch
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public  MainPageViewModel()
        {
            GetApiList();
        }

        private async void GetApiList()
        {
            using (var client = new HttpClient())
            {
                var uri = "https://dog.ceo/api/breeds/image/random";
                var result = await client.GetStringAsync(uri);

                var apiResponse = JsonConvert.DeserializeObject<DogApiResponse>(result);

                
                var imageUrl = apiResponse.Message;

                
                var dogList = new List<DogApiResponse> { new DogApiResponse { Message = imageUrl } };

               
                Apidata = new ObservableCollection<DogApiResponse>(dogList);

            }
        }

        private ObservableCollection<DogApiResponse> _dogdata;
        public ObservableCollection<DogApiResponse> Apidata
        {
            get 

            { 
                return _dogdata;
            }

            set
            {
                _dogdata = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
