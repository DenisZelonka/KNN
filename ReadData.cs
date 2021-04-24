using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Globalization;
namespace KNN
{
    public class ReadData
    {
        
        private float[,] data;//= new float[764,8];
        private bool[] diab;//= new bool[764];
        public float[,] testData;
        public float[,] fitData;
        public bool[] testDiab;
        public bool[] fitDiab;
        private int j;
        private int counter=0;
        private float[] avg;
        
        public ReadData(string path){
            int len= File.ReadAllLines(path).Count();
            var reader= new StreamReader(path);
            var elem= reader.ReadLine();
            data= new float[len,elem.Split(',').Count()];
            diab= new bool[len];
            avg= new float[len];
            while (!reader.EndOfStream){
                var line = reader.ReadLine();
                var values = line.Split(',');
                for(int i=0;i<values.Length-1;i++){
                    data[j,i]=float.Parse(values[i],CultureInfo.InvariantCulture);
                }
                if(values[8]=="tested_positive"){
                    diab[j]=true;
                }
                else{
                    
                    diab[j]=false;
                }
                j+=1;
            } 
            fitData= new float[(int)(data.GetLength(0)*0.8)+1,8];
            testData= new float[(int)(data.GetLength(0)*0.2),8];
            fitDiab= new bool[(int)(data.GetLength(0)*0.8)+1];
            testDiab= new bool[(int)(data.GetLength(0)*0.2)];
           
        }

        
        public void SplitData(bool removeZeros, int[] skipColumn){   
            if(removeZeros){  
                this.GetAvg(skipColumn);
            }
            for(int i=0;i<data.GetLength(1)-1;i++){
                for(int j=0;j<data.GetLength(0);j++){
                    if(j<data.GetLength(0)*0.8){
                        if(i==0){
                            fitDiab[j]=diab[j];
                        }
                        if(data[j,i]==0  && !skipColumn.Contains(i) && removeZeros){ 
                            fitData[j,i]=avg[i];                            
                        }
                        else{
                            fitData[j,i]=data[j,i];
                        }
                    }
                    else{
                        if(i==0){
                            testDiab[counter]=diab[j];
                        }
                        if(data[counter,i]==0 && !skipColumn.Contains(i)){
                            testData[counter,i]=avg[i];
                        }
                        else{
                            testData[counter,i]=data[j,i];
                        }
                            counter+=1;
                    }
                }
                counter=0;
            } 
            Array.Clear(data,0,data.Length);
            Array.Clear(diab,0,diab.Length);
        }
        private void GetAvg(int[] skipColumn){
            for(int i=0;i<data.GetLength(1)-1;i++){
                for(int j=0;j<data.GetLength(0);j++){
                    if(data[j,i]!=0 && !skipColumn.Contains(i)){
                        avg[i]=avg[i] + data[j,i];
                        counter=counter+1;
                    }
                }
                avg[i]=avg[i]/(counter);     
                counter=0;     
                                   
            }
        }
    }
}