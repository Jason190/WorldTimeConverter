using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Linq;

namespace WouldTimeConverter.viewmodel;

public class MainViewModel: ObservableObject
{
    string? inTimeZone,outTimeZone;
    string? inHour, inMin, inSec, outHour, outMin, outSec;

    string[]? years, months, days, hours, minutes,inTimeZoneArray,outTimeZoneArray;

    public string InTimeZone { get => inTimeZone; set => inTimeZone = value; }
    public string OutTimeZone { get => outTimeZone; set => outTimeZone = value; }
    public string InHour { get => inHour; set => inHour = value; }
    public string InMin { get => inMin; set => inMin = value; }
    public string InSec { get => inSec; set => inSec = value; }
    public string OutHour { get => outHour; set => outHour = value; }
    public string OutMin { get => outMin; set => outMin = value; }
    public string OutSec { get => outSec; set => outSec = value; }
    public string[] Years { get => years; set => years = value; }
    public string[] Months { get => months; set => months = value; }
    public string[] Days { get => days; set => days = value; }
    public string[] Hours { get => hours; set => hours = value; }
    public string[] Minutes { get => minutes; set => minutes = value; }
    public string[] InTimeZoneArray { get => inTimeZoneArray; set => inTimeZoneArray = value; }
    public string[] OutTimeZoneArray { get => outTimeZoneArray; set => outTimeZoneArray = value; }

    public MainViewModel() {
        init();
    }
    void init()
    {
        for (int i = 1; i <= 31;i++)
        {
            Years[i - 1] = (1995 + i).ToString();
            if (i <= 12)
            {
                Months[i-1] = i.ToString();
            }
            Days[i] = i.ToString(); 
        }
        for(int i = 0; i <= 59; i++)
        {
            if (i <= 23)
            {
                Hours[i] = (i + 1).ToString();

            }
            Minutes[i] = i.ToString();
        }

        foreach (TimeZoneInfo item in TimeZoneInfo.GetSystemTimeZones())
        {
            InTimeZoneArray.Append(item.DisplayName);
            OutTimeZoneArray.Append(item.DisplayName);
        }
    }
}
