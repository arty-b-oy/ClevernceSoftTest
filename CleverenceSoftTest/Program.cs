using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CleverenceSoftTest
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Тестовое задание Бережного А.И.");
            Console.WriteLine("Для начала нажмите Enter");
            Console.ReadLine();
            Test1();
            await Test2();
            Test3();
            Console.WriteLine("Спасибо за внимание! Хорошего вам дня!");

            Console.ReadLine();

        }
        static void Test1()
        {
            //1 Задание
            StringСonversion task1 = new StringСonversion();
            Console.WriteLine("Задание 1 : преобразование строки");
            Console.WriteLine();
            string[] strs = new string[] {
                                            "",
                                            "aaacccbbddddd",
                                            "abcdefghij",
                                            "yyyyncbhhhhhcmkechhhhdclnknnnnchbjjjj"
                                          };
            for (int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("Входные данные 1 теста : " + strs[i]);
                string result1 = task1.ConvertString(strs[i]);
                Console.WriteLine("Результат 1 теста : " + result1);
                Console.WriteLine();

            }
            Console.WriteLine("Для продолжения нажмите Enter");
            Console.ReadLine();
        }
        static async Task Test2() {
            Console.WriteLine();
            Console.WriteLine("Задание 2 : Сервер с переменной");
            Console.WriteLine();
            Console.WriteLine("Тест Записи");
            Console.WriteLine("Значение count = 0");

            var tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => ServerForCount.AddToCount(10));
                Console.WriteLine("Начало " + (i + 1).ToString() + " записи, count = " + ServerForCount.GetCount()); ;
            }

            Console.WriteLine("Создано 5 потоков с добавлениями переменных");
            await Task.Delay(1000);
            Console.WriteLine("Прошла 1 секунда, count = " + ServerForCount.GetCount());
            Console.WriteLine();
            Console.WriteLine("Тест Чтения");
            Console.WriteLine("Создаем массив на 50 элементов и считываем переменные из разных потоков");
            int[] arr = new int[50];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Task.Run(() => ServerForCount.GetCount()).Result;
            }
            Console.Write("Выводим Массив : " );
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i].ToString()+" ");

            }
            Console.WriteLine();

            Console.WriteLine("Для продолжения нажмите Enter");
            Console.ReadLine();
        }
        
        static void Test3()
        {
            Console.WriteLine();

            Console.WriteLine("Задание 3 : Парсер");
            Console.WriteLine();
            Parser parser = new Parser();
            string[] strs = new string[] {
                                            "",
                                            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
                                            "10.03.2025 15:1449.523 INFORMATION Версия программы: '3.4.0.48729'",
                                            "10.03.2025 15:14:49.523  Версия программы: '3.4.0.48729'",
                                            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'",
                                            "2025-03-10 15:14:51.5882| WARNING|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'",
                                            "TEST",
                                            "2025-03-10 15:14:51.5882| WARN|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'",
                                            "2025-03-10 15:14:51.5882| ERROR|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'",
                                            "2025-03-10 15:14:51.5882| DEBUG|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'",
                                            "2025-03-10 15:14:51.5882| W|11|MobileComputer.GetDeviceId| Кодустройства: '@MINDEO-M40-D-410244015546'"

                                          };
            for (int i = 0; i < strs.Length; i++)
            {
                parser.AddLog(strs[i]);
                Console.WriteLine("В Парсер отправлено сообщение №"+(i+1).ToString()+ " : "+strs[i]);
                Console.WriteLine();
            }
            Console.WriteLine("Для вывода файла log.txt нажмите Enter");
            Console.ReadLine();
            Console.WriteLine("Файл log.txt");
            Console.WriteLine();
            Console.WriteLine(parser.GetLog());
            Console.WriteLine();
            Console.WriteLine("Для вывода файла problems.txt нажмите Enter");
            Console.ReadLine();
            Console.WriteLine("Файл problems.txt");
            Console.WriteLine();
            Console.WriteLine(parser.GetProblems());
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите Enter");
            Console.ReadLine();

        }
    }
}
