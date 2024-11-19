using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models;
public class Athlete : INotifyPropertyChanged
{
    // Статус спортивных соревнований
    private string _competition;
    // Пол
    private string _gender;
    // Спортивная дисциплина
    private string _discipline;
    // Возраст
    private int _age;
    // Занятое место
    private int _place;
    // Время отставания от победителя
    private double _timeDifference;
    // Время на дистанции
    private double _time;
    // Желаемое звание: МСМК, МС, КМС
    private string _wantedRank;

    public string Competition
    {
        get => _competition;
        set
        {
            _competition = value;
            OnPropertyChanged();
        }
    }

    public string Gender
    {
        get => _gender;
        set
        {
            _gender = value;
            OnPropertyChanged();
        }
    }

    public string Discipline
    {
        get => _discipline;
        set
        {
            _discipline = value;
            OnPropertyChanged();
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            _age = value;
            OnPropertyChanged();
        }
    }

    public int Place
    {
        get => _place;
        set
        {
            _place = value;
            OnPropertyChanged();
        }
    }

    public double TimeDifference
    {
        get => _timeDifference;
        set
        {
            _timeDifference = value;
            OnPropertyChanged();
        }
    }
    
    public double Time
    {
        get => _time;
        set
        {
            _time = value;
            OnPropertyChanged();
        }
    }

    public string WantedRank
    {
        get => _wantedRank;
        set
        {
            _wantedRank = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
