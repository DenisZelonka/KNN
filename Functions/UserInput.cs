using System;

namespace KNN
{
    public class UserInput
    {
        public UserInput(ReadData data){
                Array.Clear(data.testData,0,data.testData.Length);
                Array.Clear(data.testDiab,0,data.testDiab.Length);
                int i=0;
                Console.WriteLine("Number of patients");
                int pac= int.Parse(Console.ReadLine());
                data.testData = new float[pac,8];
                data.testDiab= new bool[pac];
                while(i<pac){
                    Console.Clear();
                    Console.WriteLine("Number of pregnancies");
                    data.testData[i,0]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Glucose level");
                    data.testData[i,1]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Pressure");
                    data.testData[i,2]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Skin thickness");
                    data.testData[i,3]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Insulin level");
                    data.testData[i,4]= float.Parse(Console.ReadLine());
                    Console.WriteLine("BMI");
                    data.testData[i,5]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Pedigree");
                    data.testData[i,6]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Age");
                    data.testData[i,7]= float.Parse(Console.ReadLine());
                    Console.WriteLine("Prediction");
                    data.testDiab[i]= bool.Parse(Console.ReadLine());
                    i+=1;
               }
        }
    }
}