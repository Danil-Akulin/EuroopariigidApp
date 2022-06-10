using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContryApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Country> Countrys { get; set; }
        Label lbl_list;
        ListView List;
        Button lisa, kustuta;
        Entry en;
        MediaFile file;
        string imageName;
        string filePath;
        public MainPage()
        {
            Countrys = new ObservableCollection<Country>
            {
                new Country {Nimetus="Eesti", Kapitali="Tallinn",Elanikkonnast="1 328 439", lipp="eesti.jpg"},
                new Country {Nimetus="Soome", Kapitali="Helsingi",Elanikkonnast="5 516 224", lipp="soome.PNG"},
                new Country {Nimetus="Ameerika Ühendriikide", Kapitali="Washington",Elanikkonnast="331 449 281", lipp="ameerika.PNG"},
                new Country {Nimetus="Šveits", Kapitali="Bern",Elanikkonnast=" 8 868 904", lipp="sveits.PNG"}

            };
            lbl_list = new Label
            {
                Text = "Riikide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            List = new ListView
            {
                SeparatorColor = Color.AliceBlue,
                Header = "Riik",
                Footer = DateTime.Now.ToString("T"),

                HasUnevenRows = true,
                ItemsSource = Countrys,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.White, DetailColor = Color.White };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Kapitali", StringFormat = " {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    Binding a = new Binding { Path = "Elanikkonnast", StringFormat = "Elanikkonnast: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, a);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "lipp");
                    return imageCell;

                })
            };
            lisa = new Button { Text = "Lisa riik" };
            kustuta = new Button { Text = "Kustuta riik" };
            List.ItemTapped += List_ItemTapped;
            kustuta.Clicked += Kustuta_Clicked;
            lisa.Clicked += Lisa_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, List, lisa, kustuta } };
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {

            //telefons.Add(new Telefon { Nimetus = "Telefon", Tootja = "Tootja", Hind = 1 });
            string site = await DisplayPromptAsync("Millise riigi soovite lisada?", "Palun kirjuta", keyboard: Keyboard.Text);
            string site2 = await DisplayPromptAsync("Mis on selle pealinn?", "Palun kirjuta", keyboard: Keyboard.Text);
            string site3 = await DisplayPromptAsync("Kui palju inimesi seal elab?", "Palun kirjuta", keyboard: Keyboard.Numeric);
            string site4 = await DisplayPromptAsync("Sisesta lipu foto", "Palun kirjuta", keyboard: Keyboard.Text);
            if (site == "" || site2 == "" || site3 == "" || site4 == "")
                return;
            Country newest = new Country { Nimetus = site, Kapitali = site2, Elanikkonnast = site3, lipp = site4 };
            foreach (Country thing in Countrys)
            {
                if (thing.Nimetus == newest.Nimetus)
                    return;
            }
            Countrys.Add(item: newest);
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Country country = List.SelectedItem as Country;
            if (country != null)
            {
                Countrys.Remove(country);
                List.SelectedItem = null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Country selectedPhone = e.Item as Country;
            if (selectedPhone != null)
                await DisplayAlert("Riik", $"{selectedPhone.Kapitali}-{selectedPhone.Nimetus}", "OK");
        }


    }
}