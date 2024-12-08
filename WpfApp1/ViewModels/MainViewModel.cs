using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private Athlete _athlete;
    private string _result;

    private bool _isInputValid;

    public bool IsInputValid
    {
        get => _isInputValid;
        private set
        {
            _isInputValid = value;
            OnPropertyChanged();
            ((RelayCommand)CheckRankCommand).RaiseCanExecuteChanged();
        }
    }

    private void ValidateInput()
    {
        IsInputValid =
            Athlete is { Age: > 0, Place: > 0 }
            && !string.IsNullOrEmpty(Athlete.Competition)
            && !string.IsNullOrEmpty(Athlete.Discipline)
            && !string.IsNullOrEmpty(Athlete.Gender)
            && !string.IsNullOrEmpty(Athlete.WantedRank);
    }


    public MainViewModel()
    {
        _athlete = new Athlete();
        _athlete.PropertyChanged += (s, e) => ValidateInput();
        CheckRankCommand = new RelayCommand(CheckRank, () => IsInputValid);
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

    public List<string> Competitions { get; } = Competition.All;
    public List<string> Genders { get; } = Gender.All;

    public List<string> Ranks { get; } = Rank.All;

    public List<string> Disciplines { get; } = Discipline.All;

    private void CheckRank()
    {
        var isRanked = RankChecker.CheckRank(Athlete);
        Result = isRanked ? "Отлично" : "Провал";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}