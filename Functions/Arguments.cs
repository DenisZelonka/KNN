using System;
using KNN;

namespace Functions
{
    public class Arguments
    {
        private readonly string[] args;
        private string pathOut;
        private string[] omitCol= {"0","4","7"};
        ReadData data;
        public Arguments(string[] args){
            this.args=args;
            Decode();
        }

        private void Decode(){
            if(args.GetLength(0)>0){
                switch(args[0]){
                    case "morecol":
                                data= new ReadData("CSV_files/morecol.csv");
                                if(args.GetLength(0)>1) pathOut=args[1];
                                if(args.GetLength(0)>2) omitCol=args[2].Split(',');
                                 break;
                    case "largefile":
                                data = new ReadData("CSV_files/largefile.csv");
                                if(args.GetLength(0)>1) pathOut=args[1];
                                if(args.GetLength(0)>2) omitCol=args[2].Split(',');
                                break;
                    case "myfile":
                                try
                                {
                                    data=new ReadData(args[1]);
                                    if(args.GetLength(0)>2) pathOut=args[2];
                                    if(args.GetLength(0)>3) omitCol=args[3].Split(',');
                                }
                                catch (System.Exception)
                                {
                                    throw;
                                }
                                break;
                    case "diabetes":
                                data= new ReadData("CSV_files/diabetes_csv.csv");
                                if(args.GetLength(0)>1) pathOut=args[1];
                                if(args.GetLength(0)>2) omitCol=args[2].Split(',');
                                break;
                    default : 
                                data= new ReadData("CSV_files/diabetes_csv.csv");
                                if(args.GetLength(0)>1) pathOut=args[1];
                                if(args.GetLength(0)>2) omitCol=args[2].Split(',');
                                break;
                }
            }
            else{
                data= new ReadData("CSV_files/diabetes_csv.csv");
                pathOut="Output_file/CalculationData.txt";
            }

        }

        public string GetSavePath(){
            return this.pathOut;
        }

        public int[] GetColToSkip(){
            int i=0;
            int[] skipColumn= {0,4,7};
            if(omitCol.GetLength(0)>0){
                Array.Clear(skipColumn,0,skipColumn.GetLength(0));
                skipColumn = new int[omitCol.GetLength(0)];
                foreach (string num in omitCol)
                {
                    skipColumn.SetValue(int.Parse(num),i);
                    i+=1;
                }
            }
            return skipColumn;
        }

        public ReadData GetData(){
            return this.data;
        }

    }
}