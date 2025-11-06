using System.Globalization;

namespace Test;

public class Module : IMenuDisplayable // надо было сразу делать абстрактным
{
    public SmartHomeSystem currentSystem { get; set; }
    public bool isOn { get; set; } = false;

    public string Name { get; set; }
    public ModuleTypes Type { get; set; }
    public float MaxConsumption { get; set; }


    protected float currentConsumption;

    public virtual float CurrentConsumption
    {
        get => currentConsumption;
        set
        {
            currentConsumption = value;
            // НЕ выключаем здесь автоматически — просто сохраняем и оставляем проверку при включении
        }
    }

    public Module(string name, ModuleTypes type, int maxConsumption, SmartHomeSystem smartHomeSystem)
    {
        this.Name = name;
        MaxConsumption = maxConsumption;
        Type = type;
        currentSystem = smartHomeSystem;
    }

    public virtual void UpdateCurrentConsumption()
    {
    }

    public virtual void ShowMenu()
    {
    }

    public void OnOffStatus()
    {
        if (isOn)
        {
            Console.WriteLine("Модуль включен");
        }
        else
        {
            Console.WriteLine("Модуль выключен");
        }
    }

    public void IsOnModule()
    {
        if (isOn)
        {
            Console.WriteLine("Ошибка. Модуль уже включен");
            return;
        }

        UpdateCurrentConsumption();

        if (currentConsumption <= MaxConsumption  )
        {
            isOn = true;
            Console.WriteLine("Модуль включен");
        }
        else
        {
            isOn =  false;
            Console.WriteLine(
                $"Ошибка: превышено максимальное энергопотребление: {currentConsumption} / {MaxConsumption}");
        }
    }


    public void IsOffModule()
    {
        if (isOn)
        {
            Console.WriteLine("Модуль  выключен");
            isOn = false;
        }
        else
        {
            Console.WriteLine("Ошибка. Модуль уже выключен");
        }
    }
}