using System;
using Functions;

namespace KNN
{
    class Program
    {
        static void Main(string[] args)
        {
            var color= Console.BackgroundColor;
            Console.BackgroundColor= ConsoleColor.Black;

            bool removeZeros=false;
            bool userData=false;
            Calculate calculate;
            Arguments arguments= new Arguments(args);
            ReadData data=arguments.GetData();
             
            string path= arguments.GetSavePath();
                    
            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Clear data? (y/n)");
            Console.ForegroundColor= ConsoleColor.White;

            if(Console.ReadLine()=="y"){
                removeZeros=true;
            }
            
            data.SplitData(removeZeros,arguments.GetColToSkip());

            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Use testing values? (y/n)");
            Console.ForegroundColor= ConsoleColor.White;

            if(Console.ReadLine()=="n"){
                userData=true;
                UserInput userInput= new UserInput(data);
            }

            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("K-faktor. Sqrt(n) = " + ((int)Math.Sqrt(data.testData.GetLength(0)+data.fitData.GetLength(0))).ToString());
            Console.ForegroundColor= ConsoleColor.White;

            int k= int.Parse(Console.ReadLine());

            try
            {
                calculate= new Calculate(data,k,userData);
                calculate.GetResult();
                calculate.WriteData(path);  //"Output_file/CalculationData.txt"
            }

            catch (System.Exception)
            {
                throw;
            }  
            Console.BackgroundColor= color;         
        }
    }
}
