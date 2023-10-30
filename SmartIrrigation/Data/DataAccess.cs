namespace SmartIrrigation.Data;

public abstract class DataAccess : IDataAccess
{
    private Reading Reading;

    public Reading Set(float value)
    {
        Reading = new Reading(DateTime.UtcNow, value);
        return Reading;
    }

    public Reading Get()
    {
        return Reading;
    }

    public IEnumerable<Reading> Get(DateTime dateTime)
    {
        throw new NotImplementedException();
    }
}