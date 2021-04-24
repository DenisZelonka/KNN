using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Globalization;
namespace KNN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            bool removeZeros=false;
            bool userData=false;
            Calculate calculate;

            ReadData data = new ReadData("diabetes_csv.csv");
            //ReadData data = new ReadData("largefile.csv");

            Console.WriteLine("Clear data? (y/n)");
            if(Console.ReadLine()=="y"){
                removeZeros=true;
            }
            int[] skipColumn= {0,4,7};
            data.SplitData(removeZeros,skipColumn);

            Console.WriteLine("K-faktor. Best results are between 30-40");
            int k= int.Parse(Console.ReadLine());

            Console.WriteLine("Use testing values? (y/n)");
            if(Console.ReadLine()=="n"){
                userData=true;
                UserInput userInput= new UserInput(data);
            }

            try
            {
                calculate= new Calculate(data,k,userData);
                calculate.GetResult();
                calculate.WriteData();
            }

            catch (System.Exception)
            {
                throw;
            }           
        }
    }
}
