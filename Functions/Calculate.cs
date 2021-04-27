using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace KNN
{
    public class Calculate
    {
        private List<string> text= new List<string>();
        private ReadData data;
        private Dictionary<int,double> distance = new Dictionary<int, double>();
        private Dictionary<int,bool> state= new Dictionary<int, bool>();
        private int k;
        private double calcDistance;
        private int isPositive;
        private int isNegative;
        private int j;
        private bool result;
        private int truePositive;
        private int trueNegative;
        private int falsePositive;
        private int falseNegative;
        private bool userData;
        private int counter;
        public Calculate(ReadData data,int k,bool userData){
            this.data=data;
            if(k<data.fitData.GetLength(0) && k>5){
            this.k=k;
            }
            else {
                throw new Exception("Value is larger or smaller than expected!");
            }
            this.userData=userData;
            Calc();
        }
       
        private void Calc(){ 
            Console.ForegroundColor= ConsoleColor.DarkBlue;
            Console.Write("0%");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for(int i=0;i<data.testData.GetLength(0);i++){  
                for(j=0;j<data.fitData.GetLength(0);j++){
                    for(int p=0;p<8;p++){
                        
                        calcDistance=calcDistance + Math.Pow(data.testData[i,p]-data.fitData[j,p],2);
                    }
                    distance.Add(j,Math.Sqrt(calcDistance));
                    state.Add(j,data.fitDiab[j]);
                    calcDistance=0;
                }
                GetPrediction(i);
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine("Elapsed time: " + stopwatch.Elapsed.TotalSeconds.ToString() + "s");
            Console.ForegroundColor= ConsoleColor.White;
        }

        private void GetPrediction(int index){
            isPositive=0;
            isNegative=0;      
            counter=0;     
            foreach (KeyValuePair<int,double> item in distance.OrderBy(x => x.Value))
            {
                if(state.GetValueOrDefault(item.Key)){
                    isPositive+=1;
                }
                else{
                    isNegative+=1;
                }
                counter+=1;
                if(counter==k+1){
                    break;
                }
            }
            if(isPositive>isNegative){
                result=true;
            }
            else{
                result=false;
            }
            WriteProgress(index);
            AddText(index);
            Predictions(index);
            distance.Clear();
            state.Clear();
        }
        private void Predictions(int index){
            if(result && data.testDiab[index]){
                truePositive+=1;
            }
            else if(!result && data.testDiab[index]){
                falseNegative+=1;
            }
            if(!result && !data.testDiab[index]){
                trueNegative+=1;
            }
            else if(result && !data.testDiab[index]){
                falsePositive+=1;
            }
        }

        public void GetResult(){
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Magenta;
            double precision=Math.Round((double)(truePositive)/(truePositive+falsePositive),4);
            double recall=Math.Round((double)(truePositive)/(truePositive+falseNegative),4);
            Console.WriteLine("Accuracy: " + (Math.Round((double)(trueNegative+truePositive)/(data.testData.GetLength(0)),4)).ToString());
            Console.WriteLine("Precision: " + precision.ToString());
            Console.WriteLine("Recall: " + (recall).ToString());
            Console.WriteLine("F1: " + Math.Round(2*(precision*recall)/(precision+recall),4).ToString());
            Console.WriteLine("Error rate: " + (Math.Round((double)(falsePositive+falseNegative)/(data.testData.GetLength(0)),4)).ToString());
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.White;
        } 

        private void AddText(int index){  
            if(!userData){
                text.Add("Patient " + (1+data.fitData.GetLength(0)+index).ToString() + " positivity is " + result.ToString().ToLower() + ". Tested value is "+ data.testDiab[index].ToString().ToLower());
            }
            else{
                text.Add("Patient " + (1+index).ToString() + " positivity is " + result.ToString().ToLower() + ". Tested value is "+ data.testDiab[index].ToString().ToLower());
            }
        }

        public void WriteData(string path){
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine("Writing patients results to file at: " + "KNN/" + path);
            var paths=path.Split("/");
            int i=0;
            while(!paths[i].Contains(".")){
                if(!Directory.Exists(paths[i])){
                    Directory.CreateDirectory(paths[i]);
                }
                Directory.SetCurrentDirectory(paths[i]);
                i++;
            }
            Directory.SetCurrentDirectory("C:/Users/zelon/KNN");
            StreamWriter file = new StreamWriter(path);
            foreach (string item in text)
            {
                file.WriteLine(item);
            }
            file.Close();
            Console.WriteLine("Writing was successful!");
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.White;
        }

        private void WriteProgress(int i){
            int percent=int.Parse((100*(i+1)/data.testData.GetLength(0)).ToString());
            Console.Write("\r" + (percent).ToString() + "%");
            if(percent==100){
                Console.Write(" Done!");
            }
            
        }
    }
}