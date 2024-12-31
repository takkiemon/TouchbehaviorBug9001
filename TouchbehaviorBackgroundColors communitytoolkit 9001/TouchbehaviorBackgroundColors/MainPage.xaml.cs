using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TouchbehaviorBackgroundColors
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool Selected { get; set; } = false;
        public Color StatusBackgroundColor => Selected ? Colors.Yellow : Colors.Purple;
        public Color ReverseStatusBackgroundColor => Selected ? Colors.MediumPurple : Colors.LightGoldenrodYellow;
        public ObservableCollection<TestListItem> ItemListSource { get; set; } = new ObservableCollection<TestListItem>();

        public MainPage()
        {
            InitializeComponent();
            FillList();
            BindingContext = new TestListItem()
            {
                Text = "Test item 1"
            };
        }

        public void FillList()
        {
            for (int i = 0; i < 9; i++)
            {
                ItemListSource.Add(new TestListItem()
                {
                    Text = $"Test item {i + 1}",
                    mainPage = this
                });
            }
        }
    }

    public class TestListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Color StatusBackgroundColor => Selected ? Colors.Yellow : Colors.Purple;
        public Color ReverseStatusBackgroundColor => Selected ? Colors.MediumPurple : Colors.LightGoldenrodYellow;
        public bool Selected { get; set; } = false;
        public string IsSelectedText => Selected ? "Selected is set to true" : "Selected is set to false";
        public MainPage mainPage { get; set; }
        public string Text {  get; set; }
        public Command TestTapCommand => new Command(() =>
        {
            Selected = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReverseStatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelectedText)));
        });
        public Command TestLongPressCommand => new Command(() =>
        {
            Selected = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReverseStatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelectedText)));
        });
        public Command ToggleCommand => new Command(() =>
        {
            Selected = !Selected;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReverseStatusBackgroundColor)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelectedText)));
        });
    }
}
