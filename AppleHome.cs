using System.Globalization;

namespace Test;

//В комплекте 1 световой модуль
public class AppleHome : SmartHomeSystem
{
    public AppleHome(string brand, string name, string serialNumber, int systemConsumptionLimit, Tester tester) : base(brand, name,
        serialNumber, systemConsumptionLimit, tester)
    {
        Modules.Add(new LightingModule("Apple Lighting ", 200, 20, ModuleTypes.LightControl, this));
    }

   
}