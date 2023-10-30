namespace SmartIrrigation.Logic;

public static class SoilConstants
{
    public static float FieldCapacity(this SoilType soilType)
    {
        switch (soilType)
        {
            case SoilType.Sand:
                return 0.10f;
            case SoilType.Loam:
                return 0.27f;
            case SoilType.Clay:
                return 0.40f;
            default:
                throw new ArgumentOutOfRangeException("soilType");
        }
    }
    
    public static float PermanentWiltingPoint(this SoilType soilType)
    {
        switch (soilType)
        {
            case SoilType.Sand:
                return 0.04f;
            case SoilType.Loam:
                return 0.12f;
            case SoilType.Clay:
                return 0.22f;
            default:
                throw new ArgumentOutOfRangeException("soilType");
        }
    }
    
    public static float TotalAvailableWater(this SoilType soilType)
    {
        switch (soilType)
        {
            case SoilType.Sand:
                return 0.06f;
            case SoilType.Loam:
                return 0.15f;
            case SoilType.Clay:
                return 0.18f;
            default:
                throw new ArgumentOutOfRangeException("soilType");
        }
    }
}