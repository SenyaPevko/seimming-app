namespace WpfApp1.Models;

public static class Discipline
{
    public const string OpenWater5km = "Открытая вода 5 км";
    public const string OpenWater7_5km = "Открытая вода 7,5 км";
    public const string OpenWater10km = "Открытая вода 10 км";
    public const string OpenWater16km = "Открытая вода 16 км";
    public const string OpenWater25kmAndMore = "Открытая вода 25 км и более";
    public const string OpenWaterRelay4x1250mMixed = "Открытая вода — эстафета 4×1250 м (смешанная)";

    public const string Pool25mFreestyle50m = "Вольный стиль 50 м (бассейн 25 м)";
    public const string Pool25mFreestyle100m = "Вольный стиль 100 м (бассейн 25 м)";
    public const string Pool25mFreestyle200m = "Вольный стиль 200 м (бассейн 25 м)";
    public const string Pool25mFreestyle400m = "Вольный стиль 400 м (бассейн 25 м)";
    public const string Pool25mFreestyle800m = "Вольный стиль 800 м (бассейн 25 м)";
    public const string Pool25mFreestyle1500m = "Вольный стиль 1500 м (бассейн 25 м)";
    public const string Pool25mBackstroke50m = "На спине 50 м (бассейн 25 м)";
    public const string Pool25mBackstroke100m = "На спине 100 м (бассейн 25 м)";
    public const string Pool25mBackstroke200m = "На спине 200 м (бассейн 25 м)";
    public const string Pool25mBreaststroke50m = "Брасс 50 м (бассейн 25 м)";
    public const string Pool25mBreaststroke100m = "Брасс 100 м (бассейн 25 м)";
    public const string Pool25mBreaststroke200m = "Брасс 200 м (бассейн 25 м)";
    public const string Pool25mButterfly50m = "Баттерфляй 50 м (бассейн 25 м)";
    public const string Pool25mButterfly100m = "Баттерфляй 100 м (бассейн 25 м)";
    public const string Pool25mButterfly200m = "Баттерфляй 200 м (бассейн 25 м)";
    public const string Pool25mMedley100m = "Комплексное плавание 100 м (бассейн 25 м)";
    public const string Pool25mMedley200m = "Комплексное плавание 200 м (бассейн 25 м)";
    public const string Pool25mMedley400m = "Комплексное плавание 400 м (бассейн 25 м)";
    public const string Pool25mRelay4x50mFreestyle = "Эстафета 4×50 м — вольный стиль (бассейн 25 м) (для спортсмена стартующего первым)";
    public const string Pool25mRelay4x100mFreestyle = "Эстафета 4×100 м — вольный стиль (бассейн 25 м) (для спортсмена стартующего первым)";
    public const string Pool25mRelay4x200mFreestyle = "Эстафета 4×200 м — вольный стиль (бассейн 25 м) (для спортсмена стартующего первым)";
    public const string Pool25mRelay4x50mMedley = "Эстафета 4×50 м — комбинированная (бассейн 25 м) (для спортсмена стартующего первым — на спине)";
    public const string Pool25mRelay4x100mMedley = "Эстафета 4×100 м — комбинированная (бассейн 25 м) (для спортсмена стартующего первым — на спине)";

    public const string Pool50mFreestyle50m = "Вольный стиль 50 м (бассейн 50 м)";
    public const string Pool50mFreestyle100m = "Вольный стиль 100 м (бассейн 50 м)";
    public const string Pool50mFreestyle200m = "Вольный стиль 200 м (бассейн 50 м)";
    public const string Pool50mFreestyle400m = "Вольный стиль 400 м (бассейн 50 м)";
    public const string Pool50mFreestyle800m = "Вольный стиль 800 м (бассейн 50 м)";
    public const string Pool50mFreestyle1500m = "Вольный стиль 1500 м (бассейн 50 м)";
    public const string Pool50mBackstroke50m = "На спине 50 м (бассейн 50 м)";
    public const string Pool50mBackstroke100m = "На спине 100 м (бассейн 50 м)";
    public const string Pool50mBackstroke200m = "На спине 200 м (бассейн 50 м)";
    public const string Pool50mBreaststroke50m = "Брасс 50 м (бассейн 50 м)";
    public const string Pool50mBreaststroke100m = "Брасс 100 м (бассейн 50 м)";
    public const string Pool50mBreaststroke200m = "Брасс 200 м (бассейн 50 м)";
    public const string Pool50mButterfly50m = "Баттерфляй 50 м (бассейн 50 м)";
    public const string Pool50mButterfly100m = "Баттерфляй 100 м (бассейн 50 м)";
    public const string Pool50mButterfly200m = "Баттерфляй 200 м (бассейн 50 м)";
    public const string Pool50mMedley200m = "Комплексное плавание 200 м (бассейн 50 м)";
    public const string Pool50mMedley400m = "Комплексное плавание 400 м (бассейн 50 м)";
    public const string Pool50mRelay4x100mFreestyle = "Эстафета 4×100 м — вольный стиль (бассейн 50 м) (для спортсмена стартующего первым)";
    public const string Pool50mRelay4x200mFreestyle = "Эстафета 4×200 м — вольный стиль (бассейн 50 м) (для спортсмена стартующего первым)";
    public const string Pool50mRelay4x100mMedley = "Эстафета 4×100 м — комбинированная (бассейн 50 м) (для спортсмена стартующего первым — на спине)";
    
    public static List<string> All { get; } =
    [
        OpenWater5km,
        OpenWater7_5km,
        OpenWater10km,
        OpenWater16km,
        OpenWater25kmAndMore,
        OpenWaterRelay4x1250mMixed,

        Pool25mFreestyle50m,
        Pool25mFreestyle100m,
        Pool25mFreestyle200m,
        Pool25mFreestyle400m,
        Pool25mFreestyle800m,
        Pool25mFreestyle1500m,
        Pool25mBackstroke50m,
        Pool25mBackstroke100m,
        Pool25mBackstroke200m,
        Pool25mBreaststroke50m,
        Pool25mBreaststroke100m,
        Pool25mBreaststroke200m,
        Pool25mButterfly50m,
        Pool25mButterfly100m,
        Pool25mButterfly200m,
        Pool25mMedley100m,
        Pool25mMedley200m,
        Pool25mMedley400m,
        Pool25mRelay4x50mFreestyle,
        Pool25mRelay4x100mFreestyle,
        Pool25mRelay4x200mFreestyle,
        Pool25mRelay4x50mMedley,
        Pool25mRelay4x100mMedley,

        Pool50mFreestyle50m,
        Pool50mFreestyle100m,
        Pool50mFreestyle200m,
        Pool50mFreestyle400m,
        Pool50mFreestyle800m,
        Pool50mFreestyle1500m,
        Pool50mBackstroke50m,
        Pool50mBackstroke100m,
        Pool50mBackstroke200m,
        Pool50mBreaststroke50m,
        Pool50mBreaststroke100m,
        Pool50mBreaststroke200m,
        Pool50mButterfly50m,
        Pool50mButterfly100m,
        Pool50mButterfly200m,
        Pool50mMedley200m,
        Pool50mMedley400m,
        Pool50mRelay4x100mFreestyle,
        Pool50mRelay4x200mFreestyle,
        Pool50mRelay4x100mMedley
    ];
}
