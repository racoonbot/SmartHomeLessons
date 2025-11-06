namespace Test;

// В комплекте 1 температурный модуль
public class GoogleHome : SmartHomeSystem
{
    public GoogleHome(string brand, string name, string serialNumber, int systemConsumptionLimit, Tester tester) : base(brand, name,
        serialNumber, systemConsumptionLimit, tester)
    {
        Modules.Add(new TempModule("OutdoorTemp", ModuleTypes.TempControl, 400,
            this)); 
        this.ModulesPowerConsumption = TotalConsumption();
    }
}