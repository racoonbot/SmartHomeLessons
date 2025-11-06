namespace Test;

public abstract class SmartHomeSystem : IMenuDisplayable //
{
    private int maxModulesQuantity = 10;
    public string Brand { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public int SystemConsumptionLimit { get; set; }
    private float modulesPowerConsumption;
    private float maxModulesPowerLimit;
    
    public Tester Tester {get; set;}

    public float MaxModulesPowerLimit
    {
        get => GetMaxModulesPowerLimit();
    }


    //добавить количество модулей
    public List<Module> Modules = new List<Module>(10);

    public float ModulesPowerConsumption
    {
        get => modulesPowerConsumption;
        set
        {
            if (value >= 0 && value <= SystemConsumptionLimit)
            {
                modulesPowerConsumption = value;
                // CalculateConsumption();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Значение вышло за допустимые пределы");
            }
        }
    }


    public SmartHomeSystem(string brand, string name, string serialNumber, int systemConsumptionLimit, Tester tester)
    {
        Brand = brand;
        Name = name;
        SerialNumber = serialNumber;
        SystemConsumptionLimit = systemConsumptionLimit;
        Tester  = tester;
    }


//2. Добавить модуль в систему
    public Module AddModule() // У кастомных типов модулей не будет меню потму что нет таких классов-наследников
        //Добавить проверку на ограничение количества модулей
    {
        Console.Clear();
        ModuleTypes type = ModuleTypes.TempControl; // инициализация по умолчанию
        string name;
        int maxConsumption;

        Console.WriteLine("Выберете тип добовляемого модуля" +
                          "\n1. Температура" +
                          "\n2. Свет" +
                          "\n3. Добавить  новый тип" +
                          "\n4. Выйти в главное меню"); //!!!
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                type = ModuleTypes.TempControl;
                break;
            case "2":
                type = ModuleTypes.LightControl;
                break;
            case "3":
                type = GetModuleType();
                break;
            case "4":
                this.ShowMenu();
                break;
        }

        Console.WriteLine("Укажите название модуля");
        name = Console.ReadLine();
        Console.WriteLine("Укажите максимальное энергопотребление модуля");
        int.TryParse(Console.ReadLine(), out maxConsumption);
        if (MaxModulesPowerLimit <= SystemConsumptionLimit)
        {
            switch (type)
            {
                case ModuleTypes.TempControl:
                    return new TempModule(name, type, maxConsumption, this);

                case ModuleTypes.LightControl:
                    return new LightingModule(name, maxConsumption, 10,
                        type, this);
            }
        }
        else
        {
            Console.WriteLine("Предупреждение");
            Console.WriteLine("Максимальное количество потребления всех модулей " +
                              "превышает максимальное ограничение потребления в системе");
        }

        return null; /////////////////////! вызывает Exaption
    }
// удаление модуля1

    public void RemoveModule()
    {
        for (int i = 0; i < Modules.Count; i++) // выводим список модулей
        {
            Console.WriteLine(
                $"{i + 1}. Имя модуля: {Modules[i].Name}, Тип: {Modules[i].Type}, Максимальное потребление: {Modules[i].MaxConsumption}");
        }

        int selectedForDelete = RemoveModuleIndex();
        Console.WriteLine($"Удалён модуль: {Modules[selectedForDelete].Name}");
        this.Modules.Remove(Modules[selectedForDelete]); //удаление модуля по указанному индексу
    }

    public ModuleTypes GetModuleType() //метод для получия добавляемого типа модуля
        //(кастомные типы не работают так как под них не описаны классы)
    {
        ModuleTypes type;
        Console.WriteLine("Выберете тип добавляемого модуля");
        Console.WriteLine("\n0. TempControl" +
                          "\n1. LightControl" +
                          "\n2. SecuritySystem" +
                          "\n3. VideoSurveillance" +
                          "\n4. SmartLock" +
                          "\n5. Doorbell" +
                          "\n6. HVACSystem" +
                          "\n7. IrrigationControl" +
                          "\n8. EnergyManagement" +
                          "\n9. ApplianceControl" +
                          "\n10. SmokeDetector");
        string input = Console.ReadLine();
        type = (ModuleTypes)Enum.Parse(typeof(ModuleTypes), input); //!
        return type;
    }

    //Подсчет общего максимального количества энергии всех имеющихся модулей
    public float GetMaxModulesPowerLimit()
    {
        float powerLimit = 0;
        foreach (var module in Modules)
        {
            powerLimit += module.MaxConsumption;
        }

        return powerLimit;
    }

// Общее количество потребления всеми модулями // блин два метода об одном и том же ))) надо передалть!!!
    public float TotalConsumption()
    {
        float totalconsumption = 0;
        foreach (var module in Modules)
        {
            totalconsumption += module.CurrentConsumption;
        }

        return totalconsumption;
    }


// Проверка энергопотребления
    public void CheckConsumption()
    {
        Console.WriteLine("В системе используется: " + TotalConsumption());

        if (TotalConsumption() > SystemConsumptionLimit)
        {
            Console.WriteLine("Внимание!! Превышение энергопотребления!!");
            Console.WriteLine(
                $"В системе {Modules.Count} модулей. Используется {TotalConsumption()} / {SystemConsumptionLimit}");
        }
        else
        {
            Console.WriteLine($"Энергопотребление в пределах нормы {TotalConsumption()} / {SystemConsumptionLimit}");
        }
    }

// расчёт оставшегося постребления
    public float CalculateRemainingConsumption()
    {
        float consumption = 0;
        consumption = SystemConsumptionLimit - TotalConsumption();
        return consumption;
    }

// Удалить модуль из системы
    public int RemoveModuleIndex()
    {
        Console.WriteLine("Выберите из списка модуль который хотите удалить");
        int input = Convert.ToInt32(Console.ReadLine());
        return input - 1;
    }


//4. показать статус системы
    public void ShowSystemStatus()
    {
        Console.Clear();
        Console.WriteLine($"Всего модулей в системе: {Modules.Count}");
        float currentConsumption = TotalConsumption();
        Console.WriteLine(
            $"Используется {TotalConsumption()} из {SystemConsumptionLimit}. Остается: {CalculateRemainingConsumption()}");
        foreach (var module in Modules)
        {
            Console.WriteLine($"Модуль: {module.Name}, Текущее потребление: {module.CurrentConsumption}");
        }
    }

//показать информацию о системе
    public void ShowInfo()
    {
        Console.Clear();
        Console.WriteLine($"Производитель: {Brand}\nИмя: {Name}\nСерийный номер: {SerialNumber}\nОграничение: {SystemConsumptionLimit}\nмаксимальное количество модулей: {maxModulesQuantity}");

    }

    // показать список модулей
    public void ShowModulesList()
    {
        Console.Clear();
        Module module = null;
        if (Modules.Count > 0)
        {
            for (int i = 0; i < Modules.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. Имя модуля: {Modules[i].Name}, Тип: {Modules[i].Type}, Максимальное потребление: {Modules[i].MaxConsumption}, " +
                    $"Текущее энергопотребление {Modules[i].CurrentConsumption}");
            }

            if (int.TryParse(Console.ReadLine(), out int moduleInput))
            {
                module = Modules[moduleInput - 1];
                module.ShowMenu();
            }
        }
        else
        {
            Console.WriteLine("Модулей не найдено!");
        }
    }

    //Проверка количества модулей
    public void CheckQuantityModulesInSystem()
    {
        if (Modules.Count + 1 > maxModulesQuantity)
        {
            Console.WriteLine(
                $"Модуль не может быть добавлен. В системе {Modules.Count} модулей  из {maxModulesQuantity} ");
        }
        else
        {
            this.Modules.Add(this.AddModule());
        }
    }
    //Проверка потребления при добавлениии

    //Показать меню
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Меню системы: {Name}. Производителя: {Brand} ");
            Console.WriteLine($"Максимальное  количество модулей: {maxModulesQuantity}");
            Console.WriteLine($"Лимит энергопотребления: {SystemConsumptionLimit}");
            Console.WriteLine("Текущие модули в системе:");
            for (int i = 0; i < Modules.Count; i++)
            {
                Console.WriteLine(
                    $"\t{i + 1}. Имя модуля: {Modules[i].Name} | Тип: {Modules[i].Type} | Максимальное потребление: {Modules[i].MaxConsumption} | " +
                    $"Текущее энергопотребление {Modules[i].CurrentConsumption} | ");
            }
            Console.WriteLine("_________________");
            Console.WriteLine("Текущие модули в тестовой системе: ");
            if (Tester.TestModules.Count == 0)
            {
                Console.WriteLine("Тестовых модулей не найдено");
            }
            for (int i = 0; i < Tester.TestModules.Count; i++)
            {
                if (Tester.TestModules[i] != null )
                {
                    Console.WriteLine(
                        $"\t{i + 1}. Имя модуля: {Tester.TestModules[i].Name} | Тип: {Tester.TestModules[i].Type} | Максимальное потребление: {Tester.TestModules[i].MaxConsumption} | " +
                        $"Текущее энергопотребление {Tester.TestModules[i].CurrentConsumption} | ");
                }
                
            }
            Console.WriteLine("_________________");
            
            Console.WriteLine("\n1. \tПоказать доступные модули" +
                              "\n2. \tДобавить модуль в систему" +
                              "\n3. \tУдалить модуль из системы" +
                              "\n4. \tПоказать текущее состояние " +
                              "\n5. \tПроверить лимит энергопотребления" +
                              "\n6. \tДобавить нагрузку на сеть (имитация включения прибора)" +
                              "\n7. \tСбросить нагрузку (имитация выключения приборов)" +
                              "\n8.\tИнформация о системе" +
                              "\n9.\tВыключить систему");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ShowModulesList();
                    break;
                case "2":
                    CheckQuantityModulesInSystem();
                    break;
                case "3":
                    RemoveModule();
                    break;
                case "4":
                    Console.WriteLine("Показать текущее состояние ");
                    ShowSystemStatus();
                    break;
                case "5":
                    Console.WriteLine("Проверить лимит энергопотребления"); //Сделать более информативным
                    CheckConsumption();
                    break;
                case "6":
                    Console.WriteLine("Добавить нагрузку на сеть (имитация включения прибора)"); //
                    Tester = new Tester(this);
                    Tester.TestAddModule();
                    break;
                case "7":
                    Console.WriteLine("Сбросить нагрузку (имитация выключения приборов)"); //
                    Tester = new Tester(this);
                    Tester.TestRemoveModule();
                    break;
                case "8":
                    Console.WriteLine("Информация о системе");
                    ShowInfo();
                    break;
                case "9":
                    Console.WriteLine("Выключить систему"); //
                    break;
            }

            Console.ReadKey();
        }
    }
}