using SmartIrrigation.Logic;

namespace SmartIrrigation.Data;

public class ConfiguredData
{
    private GrassType GrassType = GrassType.BermudaGrass;
    private SoilType SoilType = SoilType.Sand;
    private float IrrigationOutput = 1f; // mm/minute
    private bool DemoMode = false;

    public void SetGrassType(GrassType grassType)
    {
        GrassType = grassType;
    }
    
    public GrassType GetGrassType()
    {
        return GrassType;
    }

    public void SetSoilType(SoilType soilType)
    {
        SoilType = soilType;
    }
    
    public SoilType GetSoilType()
    {
        return SoilType;
    }

    public void SetIrrigationOutput(float irrigationOutput)
    {
        IrrigationOutput = irrigationOutput;
    }
    
    public float GetIrrigationOutput()
    {
        return IrrigationOutput;
    }

    public void SetDemoMode(bool demoMode)
    {
        DemoMode = demoMode;
    }
    
    public bool InDemoMode()
    {
        return DemoMode;
    }
}