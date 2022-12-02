using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListViewBug;

public partial class MainPage : ContentPage
{
    public ICommand ReloadCommand { get; set; }
    public bool _isRefreshing = false;
    public bool IsRefreshing
    {
        get { return _isRefreshing; }
        set
        {
            _isRefreshing = value;
            OnPropertyChanged();
        }
    }
    private ObservableCollection<ClientDetails> _clientList = new ObservableCollection<ClientDetails>();
    public ObservableCollection<ClientDetails> ClientList
    {
        get { return _clientList; }
        set
        {
            _clientList = value;
            OnPropertyChanged();
        }
    }

    private DateTime start;
    private int range;

    public MainPage()
    {
        InitializeComponent();
        ReloadCommand = new Command(() => ReloadClientList());
        start = new DateTime(1984, 1, 1);
        range = (DateTime.Today - start).Days;
        ReloadClientList();
        this.BindingContext = this;
    }

    public Random rand = new Random();

    public string GetRandomString()
    {

        int stringlen = rand.Next(5, 12);
        int randValue;
        string str = "";
        char letter;
        for (int i = 0; i < stringlen; i++)
        {
            randValue = rand.Next(0, 26);
            letter = Convert.ToChar(randValue + 65);
            str = str + letter;
        }
        return str;
    }

    public DateTime GetRandomDate()
    {
        return start.AddDays(rand.Next(range));
    }

    private void LoadClientList()
    {
        for (int i = 0; i < 10; i++)
        {
            ClientList.Add(new ClientDetails() { FullName = GetRandomString(), City = GetRandomString(), StartDate = GetRandomDate() });
        }
    }

    public void ReloadClientList()
    {
        try
        {
            IsRefreshing = true;
            //var newClientList = new ObservableCollection<ClientDetails>();
            ClientList.Clear();
            for (int i = 0; i < 10; i++)
            {
                ClientList.Add(new ClientDetails() { FullName = GetRandomString(), City = GetRandomString(), StartDate = GetRandomDate() });
            }
            //ClientList = newClientList;
            IsRefreshing = false;
        }
        catch(Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Cancel");
        }
    }
}

public class ClientDetails
{
    public string FullName { get; set; }
    public string City { get; set; }
    public DateTime StartDate { get; set; }
}

