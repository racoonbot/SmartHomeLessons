namespace Test;

public class TempModule : Module
{
    public float Temperature { get; set; }

    private float currentTemperature;

    // private float currentConsumption;
    // private float MaxConsumption = 400;

    public override float CurrentConsumption
    {
        get => isOn ? currentConsumption : 0;
        set
        {
            if (value > MaxConsumption)
            {
                Console.WriteLine("!!!Превышено максимальное энергопотребление модуля.!!! " +
                                  "__Модуль выключен__");
                IsOffModule();
            }
            else
            {
                currentConsumption = value;
            }
        }
    }

    public float SetTemperature
    {
        get => currentTemperature;

        set
        {
            if (value < 0 || value > 100)
            {
                Console.WriteLine("Значение должно быть в пределах 0-100");
                Console.WriteLine("...........");
                return;
            }

            currentTemperature = value;
            Temperature = value;
            TotalConsumption();
        }
    }

    public TempModule(string name, ModuleTypes type, int maxConsumption, SmartHomeSystem smartHomeSystem)
        : base(name, type, maxConsumption, smartHomeSystem)
    {
    }

//Расчет потребления
    private void TotalConsumption()
    {
        float calculated = Temperature * 5f;
        currentConsumption = calculated;

        if (currentConsumption > MaxConsumption)
        {
            Console.WriteLine($"!!!Превышено максимальное энергопотребление: {currentConsumption} / {MaxConsumption}. Модуль выключен.");
            IsOffModule();
        }
    }


    //обновление  текущего потребления
    public override void UpdateCurrentConsumption()
    {
        TotalConsumption();
    }

    //Установка температуры.
    private void SetNewTemperature()
    {
        // if (isOn)
        // {
            while (true)
            {
                Console.WriteLine("Задайте температуру");
                if (float.TryParse(Console.ReadLine(), out float temperature))
                {
                    SetTemperature = temperature;
                    Console.WriteLine($"Установлена температура: {SetTemperature}");
                    if (currentTemperature > MaxConsumption)
                    {
                        Console.WriteLine("Превышено энергопотребление");
                        IsOffModule();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        // }
        // else
        // {
        //     Console.WriteLine("Ошибка! Модуль выключен!");
        //     Console.WriteLine("Включить модуль?" +
        //                       "\n1. Включить модуль" +
        //                       "\n2. главное меню");
        //     string input = Console.ReadLine();
        //     if (input == "1")
        //     {
        //         UpdateCurrentConsumption(); 
        //         if (currentConsumption <= MaxConsumption)
        //             this.IsOnModule();
        //         else
        //             Console.WriteLine($"Нельзя включить: потребление {currentConsumption} > {MaxConsumption}");
        //     }
        // }
    }

    // Показать состояние
    public void ShowStatus()
    {
        Console.Clear();
        Console.WriteLine("____________");
        Console.WriteLine($"Название модуля: {Name}" +
                          $"\nТип модуля: {Type}");
        Console.WriteLine("____________");
        OnOffStatus();
        Console.WriteLine("____________");
        Console.WriteLine($"Установлено значение температуры: {Temperature}");
        Console.WriteLine("____________");
    }

    // Показать меню
    public override void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Меню модуля {Name}");
            Console.WriteLine($"\n1. Информация о модуле." +
                              "\n2. Текущее энергопотребление" +
                              "\n3. Установить значение" +
                              "\n4. Выключить" +
                              "\n5. Включить" +
                              "\n6. Главное меню");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    this.ShowStatus();
                    break;
                case "2":
                    Console.WriteLine($"Текущее энергопотребление: {CurrentConsumption}");
                    break;
                case "3":
                    SetNewTemperature();
                    break;
                case "4":
                    IsOffModule();
                    break;
                case "5":
                    IsOnModule();
                    break;
                case "6":
                    currentSystem.ShowMenu();
                    break;
            }

            Console.ReadKey();
        }
    }
}