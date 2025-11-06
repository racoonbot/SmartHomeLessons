using System;
using System.Linq;

namespace Test
{
    public class Tester
    {
        private SmartHomeSystem _system;
        private float _systemLimit;
        public bool IsTestOk;
        private Module testModule;
        public List<Module> TestModules = new List<Module>();

        public Tester(SmartHomeSystem smartHome)
        {
             _system = smartHome ?? throw new ArgumentNullException(nameof(smartHome));
            
            _systemLimit = smartHome.SystemConsumptionLimit;
            testModule = new Module("TestModule", ModuleTypes.TempControl, 100, _system);
            IsTestOk = true;
        }

// просто сделал копию  TotalConsumption из базового класса .. по хорошему нужно было бы использовать методы которые уже есть.. подумать
        private float TestTotalConsumption()
        {
            float sum = 0f;
            foreach (var m in TestModules)
                sum += m.CurrentConsumption;
            return sum;
        }

// просто сделал копию TestConsumption из базового класса
        public void CheckTestConsumption()
        {
            float total = TestTotalConsumption();
            if (total > _systemLimit)
            {
                Console.WriteLine("Внимание!! Превышение энергопотребления (тест)!!");
                Console.WriteLine(
                    $"В тестовой системе {TestModules.Count} модулей. Используется {total} / {_systemLimit}");
            }
            else
            {
                Console.WriteLine($"Энергопотребление в тестовой системе в пределах нормы {total} / {_systemLimit}");
            }
        }

        // обавляем тестовый модуль
        public void TestAddModule()
        {
            foreach (var module in _system.Modules)
            {
                TestModules.Add(module);
            }

            Console.WriteLine("Добавляем тестовый модуль в систему c потреблением 100");
            testModule.CurrentConsumption = 100;
            TestModules.Add(testModule);
            CheckTestConsumption();

            Console.WriteLine("Список модулей (тест):");
            for (int i = 0; i < TestModules.Count; i++)
            {
                var module = TestModules[i];
                Console.WriteLine($"{i + 1}. {module.Name} (Consumption: {module.CurrentConsumption})");
            }
        }

        public void TestRemoveModule()
        {
            Console.WriteLine("TestRemoveModule");
            foreach (var module in TestModules)
            {
                module.IsOffModule();
            }

            Console.WriteLine("Все модули  в тестовой системе отключены");
            for (int i = 0; i < TestModules.Count; i++)
            {
                var module = TestModules[i];
                Console.WriteLine($"{i + 1}. {module.Name} (Consumption: {module.CurrentConsumption})");
            }
        }
    }
}