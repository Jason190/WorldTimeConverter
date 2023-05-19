using System;
using System.Collections.ObjectModel;

namespace WouldTimeConverter.core;

public class converter
{
    private DateTimeOffset inputDateTime;
    private DateTimeOffset outputDateTime;
    private TimeZoneInfo outputDateTimeZoneInfo;

    public DateTimeOffset InputDateTime { get => inputDateTime; set => inputDateTime = value; }
    public DateTimeOffset OutputDateTime { get => outputDateTime; set => outputDateTime = value; }
    public TimeZoneInfo OutputDateTimeZoneInfo { get => outputDateTimeZoneInfo; set => outputDateTimeZoneInfo = value; }

    public converter(DateTimeOffset input) { 
        InputDateTime = input;
    }
    public converter() { 
        InputDateTime= DateTimeOffset.Now;
    }
    public void setOutTimeZoneInfo(TimeZoneInfo target)
    {
        OutputDateTimeZoneInfo = target;
    }
    public DateTimeOffset setInputDateTime(DateTimeOffset time) {
        return InputDateTime = time;
    }

    public DateTimeOffset ConverterTime(DateTimeOffset inputTime,TimeZoneInfo outputTimeZoneInfo)
    {
        setInputDateTime(inputTime);
        setOutTimeZoneInfo(outputTimeZoneInfo);

        DateTimeOffset targetTime = TimeZoneInfo.ConvertTime(InputDateTime, OutputDateTimeZoneInfo);

        return targetTime;
    }
}
