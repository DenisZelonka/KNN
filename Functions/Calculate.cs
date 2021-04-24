using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNN
{
    public class Calculate
    {
        private List<string> text= new List<string>();
        private ReadData data;
        private SortedDictionary<double,bool> distance = new SortedDictionary<double, bool>();
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
            for(int i=0;i<data.testData.GetLength(0);i++){  
                for(j=0;j<data.fitData.GetLength(0);j++){
                    for(int p=0;p<8;p++){
                        
                        calcDistance=calcDistance + Math.Pow(data.testData[i,p]-data.fitData[j,p],2);
                    }
                    if(!distance.ContainsKey(Math.Sqrt(calcDistance))){
                        distance.Add(Math.Sqrt(calcDistance),data.fitDiab[j]);
                    }
                    calcDistance=0;
                }
                GetPrediction(i);
            }
        }

        private void GetPrediction(int index){
            isPositive=0;
            isNegative=0;           
            for(int i=0;i<this.k;i++){
                if(distance.ElementAt(i).Value){
                    isPositive+=1;
                }
                else{
                    isNegative+=1;
                }
            }
            if(isPositive>isNegative){
                result=true;
            }
            else{
                result=false;
            }
            AddText(index);
            Predictions(index);
            distance.Clear();
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
            else if(result && data.testDiab[index]){
                falsePositive+=1;
            }
        }

        public void GetResult(){
            Console.Clear();
            Console.WriteLine("Accuracy: " + (100*(trueNegative+truePositive)/(data.testData.GetLength(0))).ToString() + '%');
            Console.WriteLine();
            Console.WriteLine("False positivity: " + falsePositive.ToString());
            Console.WriteLine("False negativity: " + falseNegative.ToString());
        } 

        private void AddText(int index){  
            if(!userData){
                text.Add(" Patient " + (1+data.fitData.GetLength(0)+index).ToString() + " positivity is " + result.ToString().ToLower() + ". Tested value is "+ data.testDiab[index].ToString().ToLower());
            }
            else{
                text.Add(" Patient " + (1+index).ToString() + " positivity is " + result.ToString().ToLower() + ". Tested value is "+ data.testDiab[index].ToString().ToLower());
            }
        }

        public void WriteData(){
            StreamWriter file = new StreamWriter("Output_file/CalculationData.txt");
            foreach (string item in text)
            {
                file.WriteLine(item);
            }
            file.Close();
        }
    }
}