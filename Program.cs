using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        private static SmartHomeSystem selectedSystem;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Выберете систему умного дома:" +
                              "\n1. Google Home" +
                              "\n2. Amazon Alexe" +
                              "\n3. Apple Home");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    selectedSystem = new GoogleHome("Google Home", "Система умного дома Google Home", "555", 400, null);
                    selectedSystem.Tester = new Tester(selectedSystem);
                    break;
                case "2":
                    selectedSystem = new AmazonAlexa("Amazon", "Умный дом от Amazon", "444", 12000,null);
                    selectedSystem.Tester = new Tester(selectedSystem);
                    break;
                case "3":
                    selectedSystem = new AppleHome("Apple", "Apple Home", "333", 9000, null);
                    selectedSystem.Tester = new Tester(selectedSystem);
                    break;
            }


            Console.WriteLine(
                "\n1. Показать меню" +
                "\n2. Показать общую информацию о выбранной системе" +
                "\n3. Выход");
            string input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    selectedSystem.ShowMenu();
                    break;
                case "2":
                    selectedSystem.ShowInfo();
                    break;
                case "3":
                    break;
            }
        }
    }
}