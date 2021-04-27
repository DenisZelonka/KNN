using System;


namespace KNN
{
    class Program
    {
        static void Main(string[] args)
        {
            bool removeZeros=false;
            bool userData=false;

            Calculate calculate;
            ReadData data;
            string path="Output_file/CalculationData.txt";            
            if(args.GetLength(0)>0){
                switch(args[0]){
                    case "test":
                                data= new ReadData("CSV_files/test.csv");
                                if(args.GetLength(0)>1) path=args[1];
                                 break;
                    case "largefile":
                                data = new ReadData("CSV_files/largefile.csv");
                                if(args.GetLength(0)>1) path=args[1];
                                break;
                    case "myfile":
                                try
                                {
                                    data=new ReadData(args[1]);
                                    if(args.GetLength(0)>2) path=args[2];
                                }
                                catch (System.Exception)
                                {
                                    throw;
                                }
                                break;
                    case "diabetes":
                                data= new ReadData("CSV_files/diabetes_csv.csv");
                                if(args.GetLength(0)>1) path=args[1];
                                break;
                    default : 
                                data= new ReadData("CSV_files/diabetes_csv.csv");
                                path="Output_file/CalculationData.txt";
                                break;
                }
            }
            else{
                data= new ReadData("CSV_files/diabetes_csv.csv");
                path="Output_file/CalculationData.txt";
            }
            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Clear data? (y/n)");
            Console.ForegroundColor= ConsoleColor.White;

            if(Console.ReadLine()=="y"){
                removeZeros=true;
            }
            int[] skipColumn= {0,4,7};
            data.SplitData(removeZeros,skipColumn);

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
        }
    }
}
