using System;
using System.Threading.Tasks;
using Boutiquei.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
namespace Boutiquei.ViewModels
{
    public class BotiquesViewModel : BaseViewModel
    {

        public ObservableRangeCollection<StoreModel> Boutique { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }

        public BotiquesViewModel()
        {
            Boutique = new ObservableRangeCollection<StoreModel>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);

            LoadMoreCommand = new Command(LoadMore);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        StoreModel selectedBoutique;
        StoreModel previousSelected;
        public StoreModel SelectedBoutique
        {
            get => selectedBoutique;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.BName, "OK");
                    previousSelected = value;
                    value = null;
                }
                selectedBoutique = value;
                OnPropertyChanged();
            }
        }
        //public StoreModel SelectedBoutique
        //{
        //    get => selectedBoutique;
        //    set => SetProperty(ref selectedBoutique, value);
        //}

        //async Task Selected(object arg)
        //{
        //    var boutique = arg as StoreModel;
        //    if(boutique == null)
        //    {
        //        return;
        //    }
        //    SelectedBoutique = null;

        //    //await AppShell.Current.GoToAsync(nameof(Page));
        //    await Application.Current.MainPage.DisplayAlert("Selected", boutique.BName, "OK");
        //}



        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Boutique.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            var image1 = "https://cdn.discordapp.com/attachments/924024471755567124/955258823843676170/275766656_391237242376590_1553927296572176900_n.png";
            var image2 = "https://cdn.discordapp.com/attachments/924024471755567124/955258843556880394/275379707_540699471027454_4428530398476858160_n.png";
            var image3 = "https://cdn.discordapp.com/attachments/924024471755567124/955258857976901712/275955822_4840494826067082_1042685898013086821_n.png";
            var image4 = "https://cdn.discordapp.com/attachments/924024471755567124/955258872136871986/275073208_537716854362551_8018523921874305659_n.png";
            var image5 = "https://cdn.discordapp.com/attachments/924024471755567124/955258966651330590/274720697_986498812285985_8840338544609036108_n.png";
            var image6 = "https://cdn.discordapp.com/attachments/924024471755567124/955258978554773594/276034740_491152432388036_7182820544372323363_n.png";
            var image7 = "https://cdn.discordapp.com/attachments/924024471755567124/955258994400825354/275843570_1085016752055316_3369223921635088145_n.png";
            var cover = "https://cdn.discordapp.com/attachments/924024471755567124/955259005935173682/275419393_1569353190117359_5624166761770086930_n.png";


            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image1, BName = "Lana Line", Id = "id", Type = "Boutique" });
            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image2, BName = "Nagham Zalabieh", Id = "id", Type = "Boutique" });
            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image3, BName = "Wessam Qutob", Id = "id", Type = "Boutique" });
            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image4, BName = "Hussam Silawy", Id = "id", Type = "Boutique" });
            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image1, BName = "Lana Line", Id = "id", Type = "Boutique" });
            Boutique.Add(new StoreModel { BCoverPic = cover, BMainPic = image2, BName = "Nagham Zalabieh", Id = "id", Type = "Boutique" });

        }

        void DelayLoadMore()
        {
            LoadMore();
        }

    }
}

