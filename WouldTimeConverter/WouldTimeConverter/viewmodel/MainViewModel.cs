using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using WouldTimeConverter.core;

namespace WouldTimeConverter.viewmodel;

public class MainViewModel : ObservableObject
{
    string? inTimeZone, outTimeZone;
    string? inHour, inMin, inYear,inMonth,inDay, outDateAndTime;

    List<string>? years, months, days, hours, minutes, inTimeZoneArray, outTimeZoneArray;

    public MainViewModel()
    {
        init();
    }

    public string? InTimeZone { get => inTimeZone; set => SetProperty(ref inTimeZone, value); }
    public string? OutTimeZone { get => outTimeZone; set => SetProperty(ref outTimeZone, value); }
    public string? InHour { get => inHour; set => SetProperty(ref inHour, value); }
    public string? InMin { get => inMin; set => SetProperty(ref inMin, value); }
    public string? InYear { get => inYear; set => SetProperty(ref inYear, value); }
    public string? InMonth { get => inMonth; set => SetProperty(ref inMonth, value); }
    public string? InDay { get => inDay; set => SetProperty(ref inDay, value); }
    public string? OutDateAndTime { get => outDateAndTime; set => SetProperty(ref outDateAndTime, value); }
    public List<string>? Years { get => years; set => SetProperty(ref years, value); }
    public List<string>? Months { get => months; set => SetProperty(ref months, value); }
    public List<string>? Days { get => days; set => SetProperty(ref days, value); }
    public List<string>? Hours { get => hours; set => SetProperty(ref hours, value); }
    public List<string>? Minutes { get => minutes; set => SetProperty(ref minutes, value); }
    public List<string>? InTimeZoneArray { get => inTimeZoneArray; set => SetProperty(ref inTimeZoneArray, value); }
    public List<string>? OutTimeZoneArray { get => outTimeZoneArray; set => SetProperty(ref outTimeZoneArray, value); }

    void init()
    {
        int startYear = 1995;
        Years = new List<string>();Months = new List<string>();Days=new List<string>();Hours=new List<string>();Minutes=new List<string>();InTimeZoneArray = new List<string>();OutTimeZoneArray = new List<string>();
        InYear=DateTime.Now.Year.ToString();
        InMonth=DateTime.Now.Month.ToString();
        InDay=DateTime.Now.Day.ToString();
        InHour=DateTime.Now.Hour.ToString();
        InMin=DateTime.Now.Minute.ToString();
        InTimeZone = TimeZoneInfo.Local.DisplayName;
        OutTimeZone = TimeZoneInfo.Local.DisplayName;

        for (int i = 1; i <= 31; i++)
        {
            Years.Add((startYear + i).ToString());
            if (i <= 12)
            {
                Months.Add( i.ToString());
            }
            Days.Add(i.ToString());
        }
        for (int i = 0; i <= 59; i++)
        {
            if (i <= 23)
            {
                Hours.Add((i).ToString());

            }
            Minutes.Add(i.ToString());
        }

        foreach (TimeZoneInfo item in TimeZoneInfo.GetSystemTimeZones())
        {
            InTimeZoneArray.Add(item.DisplayName);
            OutTimeZoneArray.Add(item.DisplayName);
            if (item.DisplayName.Equals(TimeZoneInfo.Local.DisplayName))
            {
                InTimeZone = item.DisplayName;
                OutTimeZone = item.DisplayName;
            }
        }

    }

    private RelayCommand _OnCalcCommand;
    public RelayCommand OnCalcCommand =>
        _OnCalcCommand ??= new RelayCommand(() =>
        {
            calc();
        });

    private void calc()
    {
        int inyearInt;
        if (int.TryParse(InYear,out inyearInt))
        {
            string dateText= string.Format("{0}-{1}-{2} {3}:{4}", InYear, InMonth, InDay, InHour, InMin);
            DateTimeOffset inDate;
            if(DateTimeOffset.TryParse(dateText,out inDate))
            {
                inDate = new DateTimeOffset(inyearInt, int.Parse(InMonth), int.Parse(InDay), int.Parse(InHour), int.Parse(InMin), 0, getTimeSpanFromDisplayName(InTimeZone));
            }
            else
            {
                return;
            }
            
            
            DateTimeOffset temp = (getTimeZoneInfoFromDisplayName(InTimeZone).IsDaylightSavingTime(inDate) == true) ? inDate.AddHours(-1) : inDate;

            converter converter = new converter(temp);
            TimeZoneInfo outDate = getTimeZoneInfoFromDisplayName(OutTimeZone);
            DateTimeOffset result = converter.ConverterTime(temp, outDate);
            OutDateAndTime = result.DateTime.ToString();
        }

    }
    private TimeSpan getTimeSpanFromDisplayName(string displayName)
    {
        foreach(TimeZoneInfo item in TimeZoneInfo.GetSystemTimeZones())
        {
            if (item.DisplayName==displayName)
            {
                return item.BaseUtcOffset;
            }
        }
        return default(TimeSpan);
    }
    private TimeZoneInfo? getTimeZoneInfoFromDisplayName(string displayName)
    {
        foreach (TimeZoneInfo item in TimeZoneInfo.GetSystemTimeZones())
        {
            if (item.DisplayName == displayName)
            {
                return item;
            }
        }
        return null;
    }
}
