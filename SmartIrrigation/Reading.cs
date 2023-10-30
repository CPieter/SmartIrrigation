namespace SmartIrrigation;

public readonly struct Reading
{
    public DateTime DateTime { get; }
    public float Value { get; }
    
    public Reading(DateTime dateTime, float value)
    {
        DateTime = dateTime;
        Value = value;
    }
}