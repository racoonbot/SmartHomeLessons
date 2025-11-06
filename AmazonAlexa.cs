namespace Test;

//  в комплекте 1 световой и 1 температурный модули 
public class AmazonAlexa : SmartHomeSystem
{
    public AmazonAlexa(string brand, string name, string serialNumber, int systemConsumptionLimit, Tester tester) : base(brand, name,
        serialNumber, systemConsumptionLimit, tester)
    {
        Modules.Add(
            new TempModule("Amazon indooor Termostat", ModuleTypes.TempControl, 400, this));
        new LightingModule("Amazon Ilyich's lamp", 100, 1, ModuleTypes.LightControl, this);
    }
}