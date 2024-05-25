using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace SoldierTransfer
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        public void Run()
        {
            const string ShowAllCommand = "1";
            const string TransferCommand = "2";
            const string ExitCommand = "0";

            string userInput;
            
            bool isExit = false;

            Database database = new Database();
            database.CreateSoldiers();

            while (isExit == false)
            {
                WriteLine();
                WriteLine(ShowAllCommand + " - Показать всех");
                WriteLine(TransferCommand + $" - Перевести в другое отделение всех на букву {database.NameStartsWith}");
                WriteLine(ExitCommand + " - Выход\n");

                userInput = ReadLine();

                switch (userInput)
                {
                    case ShowAllCommand:
                        database.ShowAllSoldiers();
                        break;

                    case TransferCommand:
                        database.TransferSoldiers();
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Soldier
    {
        public Soldier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class Database
    {
        private int _ammountOfRecords = 10;
        private List<Soldier> _soldiersSquad1 = new List<Soldier>();
        private List<Soldier> _soldiersSquad2 = new List<Soldier>();
        
        public char NameStartsWith { get; private set; } = 'Б';

        public void ShowAllSoldiers()
        {
            List<Soldier>[] soldierSquads = { _soldiersSquad1, _soldiersSquad2 };

            for (int i = 0; i < soldierSquads.Length; i++)
            {
                WriteLine($"\nОтряд {i + 1}\n");

                foreach (var soldier in soldierSquads[i])
                {
                    WriteLine($"{soldier.Name}");
                }
            }
        }

        public void CreateSoldiers()
        {
            for (int j = 0; j < _ammountOfRecords; j++)
            {
                _soldiersSquad1.Add(new Soldier(GetName()));
                _soldiersSquad2.Add(new Soldier(GetName()));
            }
        }

        public void TransferSoldiers()
        {
            List<Soldier> tempList = _soldiersSquad1.Where(soldier => soldier.Name.ToUpper().StartsWith(NameStartsWith)).ToList();
            _soldiersSquad1 = _soldiersSquad1.Except(tempList).ToList();
            _soldiersSquad2 = _soldiersSquad2.Union(tempList).ToList();
            
            WriteLine("\nСолдаты на букву Б переведены из первого отряда во второй\n");
        }

        private string GetName()
        {
            string[] soldierNames =
            [
        "Иван Зевс",
        "Николай Лес",
        "Александр Сова",
        "Борис Бритва",
        "Егор Ветер",
        "Дмитрий Огонь",
        "Анатолий Гром",
        "Сергей Молния",
        "Михаил Шторм",
        "Артем Вихрь",
        "Алексей Молот",
        "Борис Бодунов",
        "Владимир Луч",
        "Андрей Громада",
        "Степан Буря",
        "Максим Ветеран",
        "Браззерс Лысый",
        "Григорий Тайфун",
        "Братислав Яйцеслав",
        "Артур Блиц"
            ];

            string name = soldierNames[Utils.GetRandomNumber(soldierNames.Length - 1)];
            return name;
        }
    }

    class Utils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static int GetRandomNumber(int maxValue)
        {
            return s_random.Next(maxValue);
        }
    }
}

//Есть 2 списка в солдатами.
//Всех бойцов из отряда 1, у которых фамилия начинается на букву Б, требуется перевести в отряд 2. 