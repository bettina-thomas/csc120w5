using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulateMemory();
            Conversion();
            var inputPutDataFile = @"C:\Data\inputdata.txt";
            var truthTableInputs = Storage.ReadTruthTableData(inputPutDataFile);

            foreach (var input in truthTableInputs)
            {
                Console.WriteLine($"{input.A}, {input.X}, {input.D}, {input.R}");
            }
            
        }



        private static void Conversion()
        {
            var value = "1";
            var convertedValue = Utility.ConvertToBoolean(value);

            Console.WriteLine($"Conversion from string {value} to binary {convertedValue}");


            var bit = Utility.ConvertToBit(convertedValue);
            Console.WriteLine($"Conversion from Boolean {convertedValue} to Bit {bit}");

        }

        private static void SimulateMemory()
        {
            var a = 100;
            var storedValue = Storage.ReadData();
            if (storedValue > 0)
            {
                a = storedValue;
                Console.WriteLine($"Stored Value {a}");
                Console.WriteLine($"Press Any Key to continue");
                Console.ReadLine();
            }
            for (int i = storedValue; i < 10000; i++)
            {
                Storage.SaveData(a);
                Console.WriteLine($"Print Current Stored Value {i}");
                a = i;
            }

            Console.WriteLine($"Stored Value {a}");
        }
    }
    public class Storage
    {
        static string STORAGE = "inputdata.txt";
        public static bool SaveData(int val)
        {
            var fs = new FileStream(STORAGE, FileMode.Create);
            var sw = new StreamWriter(fs);
            sw.Write(val);
            sw.Flush();
            sw.Close();
            fs.Close();
            return true;
        }



        public static int ReadData()
        {
            if (!File.Exists(STORAGE))
            {
                return 0;
            }
            var data = File.ReadAllText(STORAGE);
            var inValue = int.Parse(data);
            return inValue;
        }



        public static bool SaveTruthTableData(int val1, int val2, int val3, int result)
        {
            var fs = new FileStream(STORAGE, FileMode.Create);
            var sw = new StreamWriter(fs);
            sw.WriteLine($"{val1}, {val2}, {val3}, {result}");
            sw.Flush();
            sw.Close();
            fs.Close();
            return true;
        }

        public static bool SaveTruthTableData(TruthTable tt)
        {

            return SaveTruthTableData(
                                        tt.A ? 1 : 0,
                                        tt.X ? 1 : 0,
                                        tt.D ? 1 : 0,
                                        tt.R ? 1 : 0
                                        );
        }
        public static TruthTable ReadTruthTable()
        {
            var inputRow = new TruthTable();
            if (!File.Exists(STORAGE))
            {
                return inputRow;
            }
            var data = File.ReadAllText(STORAGE);
            var dataElements = data.Split(','); // 0,1,0,1  will be split into arrays

            inputRow.A = Utility.ConvertToBoolean(dataElements[0]);
            inputRow.X = Utility.ConvertToBoolean(dataElements[1]);
            inputRow.D = Utility.ConvertToBoolean(dataElements[2]);
            inputRow.R = Utility.ConvertToBoolean(dataElements[3]);
            return inputRow;
        }

        public static List<TruthTable> ReadTruthTableData(string dataPath)
        {
            var inputList = new List<TruthTable>();
            var fs = new FileStream(dataPath, FileMode.Create);
            var sr = new StreamReader(fs);

            int counter = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var inputRow = new TruthTable();
                var dataElements = line.Split(',');

                inputRow.A = Utility.ConvertToBoolean(dataElements[0]);
                inputRow.X = Utility.ConvertToBoolean(dataElements[1]);
                inputRow.D = Utility.ConvertToBoolean(dataElements[2]);
                inputRow.R = Utility.ConvertToBoolean(dataElements[3]);
                inputList.Add(inputRow);
                System.Console.WriteLine(line);
                counter++;

            }
            System.Console.ReadLine();
            sr.Close();
            fs.Close();

            return inputList;
        }

    }
    public class Utility
    {
        public static Boolean ConvertToBoolean(string data)
        {
            return (data == "1" ? true : false);
        }

        public static int ConvertToBit(Boolean data)
        {
            return Convert.ToInt16(data);
        }
    }
    public class TruthTable
    {
        public Boolean A { get; set; }
        public Boolean X { get; set; }
        public Boolean D { get; set; }
        public Boolean R { get; set; }
    }
}
