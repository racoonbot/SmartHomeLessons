namespace Test;

public class LightingModule : Module
{
    public string Name { get; set; }
   
    public float MaxLightDeviceQuantity { get; set; } 
    public float ActiveLightDeviceQuantity { get; set; }
    private float currentConsumption;
    private float MaxConsumption = 300;

    public override float CurrentConsumption
    {
        get => currentConsumption;
        
        set
        {
            if (isOn)
            {
                currentConsumption = value; 
            }
            else
            {
                currentConsumption = 0;
            }
        }
    }

    public LightingModule(string name, int maxConsumption, int maxLightDeviceQuantity,
        ModuleTypes type, SmartHomeSystem smartHomeSystem) : base(name, type, maxConsumption, smartHomeSystem)
    {
        MaxLightDeviceQuantity = maxLightDeviceQuantity;
    }

    public override  void UpdateCurrentConsumption()
    {
        CurrentConsumption = ActiveLightDeviceQuantity * 30;

        if (currentConsumption > MaxConsumption)
        {
            Console.WriteLine("!!!Превышено максимальное энергопотребление модуля.!!!" +
                              "__Модуль выключен__");
            IsOffModule();
        }
    }

    public void QuickOnLighting() //Метод для быстрого включения
    {
        Console.WriteLine("Задайте количество приборов. Ограничение" + MaxLightDeviceQuantity);
        if (float.TryParse(Console.ReadLine(), out float deviceQuantity))
        {
            SetActiveLightings(deviceQuantity);
        }

    }

    public void ShowStatus()
    {
        Console.Clear();
        Console.WriteLine("____________");
        Console.WriteLine($"Модуль {Name}" +
                          $"\nТип модуля {Type}");
        Console.WriteLine("____________");
        OnOffStatus();
        Console.WriteLine("____________");
        Console.WriteLine($"Количество активных световых приборов {ActiveLightDeviceQuantity}");
        Console.WriteLine("____________");
    }

    public override void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Меню модуля {Name}");
            Console.WriteLine($"\n1. Информация о модуле." +
                              "\n2. Текущее энергопотребление" +
                              "\n3. Добавить световые приборы" +
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
                    QuickOnLighting();
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
    
    public void SetActiveLightings(float deviceQuantity) // Установка нужного количества световых приборов
    {
        if (!isOn)
        {
            Console.WriteLine("Модуль выключен.");
            return; 
        }

        if (deviceQuantity < 0)
        {
            Console.WriteLine("Не может быть отрицательным.");
            return;
        }

        if (deviceQuantity > MaxLightDeviceQuantity)
        {
            Console.WriteLine($"Максимальное значение {MaxLightDeviceQuantity}.");
            return; 
        }

       
        ActiveLightDeviceQuantity = deviceQuantity;
        UpdateCurrentConsumption(); 

        Console.WriteLine($"Добавлено приборов: {deviceQuantity}. Текущее потребление: {CurrentConsumption}");
    }

}