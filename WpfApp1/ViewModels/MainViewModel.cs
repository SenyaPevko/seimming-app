using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private Athlete _athlete;
    private string _result;

    public MainViewModel()
    {
        _athlete = new Athlete();
        CheckRankCommand = new RelayCommand(CheckRank);
    }

    public Athlete Athlete
    {
        get => _athlete;
        set
        {
            _athlete = value;
            OnPropertyChanged();
        }
    }

    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public ICommand CheckRankCommand { get; }

    public List<string> Competitions { get; } =
    [
        "Олимпийские игры", 
        "Чемпионат мира",
        "Чемпионат Европы",
        "Другие международные соревнования (ЕКП)",
        "Чемпионат России",
        "Кубок России",
        "Первенство России",
        "Другие всероссийские соревнования (ЕКП)"

    ];
    public List<string> Genders { get; } = ["Мужской", "Женский"];

    public List<string> Disciplines { get; } =
    [
        "Вольный стиль 50 м", "Вольный стиль 100 м", "Вольный стиль 200 м", "Вольный стиль 400 м",
        "Вольный стиль 800 м", "Вольный стиль 1500 м", "На спине 50 м", "На спине 100 м", "На спине 200 м",
        "Брасс 50 м", "Брасс 100 м", "Брасс 200 м", "Баттерфляй 50 м", "Баттерфляй 100 м", "Баттерфляй 200 м",
        "Комплексное плавание 100 м", "Комплексное плавание 200 м", "Комплексное плавание 400 м",
        "Эстафета 4x50 м - вольный стиль", "Эстафета 4x100 м - вольный стиль", "Эстафета 4x200 м - вольный стиль",
        "Эстафета 4x50 м - комбинированная", "Эстафета 4x100 м - комбинированная", 
        "Открытая вода 5 км", "Открытая вода 7,5 км", "Открытая вода 10 км", "Открытая вода 16 км", "Открытая вода 25 км и более"
    ];
    
    private void CheckRank()
    {
        Result = RankChecker.CheckRank(_athlete);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}