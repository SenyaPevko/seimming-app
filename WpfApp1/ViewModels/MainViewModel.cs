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

    public List<string> Competitions { get; } = new List<string> { "Чемпионат страны", "Международные соревнования" };
    public List<string> Genders { get; } = new List<string> { "Мужской", "Женский" };
    public List<string> Disciplines { get; } = new List<string> { "Вольный стиль", "Брасс", "Баттерфляй" };
    
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