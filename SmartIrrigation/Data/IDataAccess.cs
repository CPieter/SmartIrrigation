namespace SmartIrrigation.Data;

public interface IDataAccess
{
    Reading Set(float value);
    Reading Get();
    IEnumerable<Reading> Get(DateTime dateTime);
}