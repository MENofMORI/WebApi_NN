using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_NN.ArtificialNeuralNetwork
{
    public class ANN_Builder
    {
        public NeuralNetworkEngin GetANN()
        {
            string[] fileLines;
            bool iSFile = false;
            try
            {
                string path = @"DataNN.csv";
                fileLines = Properties.Resources.ANN_Knowledge.Split('\n');
                for (int i = 0; i < fileLines.Length; i++)
                {
                    fileLines[i] = fileLines[i].Trim('\r');
                }
                iSFile = true;
            }
            catch (Exception)
            {
                fileLines = null;
            }
            if (!iSFile) return null;

            int LineCounter = 0;
            string NameOfActivationF = fileLines.GetValue(LineCounter++).ToString();

            Console.WriteLine($"Log 1 ");

            List<int> ListOfNumerOfNeurons;
            try
            {
                string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                string[] tablica = LineWithSpecyficData.Split(';');
                List<int> ConvertedRecord = new List<int>();
                foreach (string t in tablica)
                {
                    ConvertedRecord.Add(Int32.Parse(t));
                }
                ListOfNumerOfNeurons = ConvertedRecord;
            }
            catch (Exception)
            {
                return null;
            }

            Console.WriteLine($"Log 2 ");

            List<List<double>> ListOfBias = new List<List<double>>();
            for (int i = 0; i < ListOfNumerOfNeurons.Count - 1; i++)
            {
                string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                string[] tablica = LineWithSpecyficData.Split(';');
                List<double> ConvertedRecord = new List<double>();
                foreach (string t in tablica)
                {
                    List <string> bufor = t.Split(',').ToList();
                    if (bufor.Count == 2 )
                    {
                        double valueBufor = Double.Parse(bufor[0]);
                        int pow = bufor[1].Length;
                        if (pow > 1)
                        {
                            double div = Double.Parse(bufor[1]);
                            if (valueBufor > 0) valueBufor += div / Math.Pow(10, pow);
                            else valueBufor -= div / Math.Pow(10, pow);
                        }
                        else
                        {
                            valueBufor += Double.Parse(bufor[1])/10;
                        }
                        ConvertedRecord.Add(valueBufor); // NORMAL
                    }
                    else
                    {
                        ConvertedRecord.Add(Double.Parse(t)); // NORMAL
                    }
                }
                ListOfBias.Add(ConvertedRecord);
            }

            Console.WriteLine($"Log 3 ");

            List<List<List<double>>> ListOfWages = new List<List<List<double>>>();
            for (int i = 0; i < ListOfNumerOfNeurons.Count - 1; i++)
            {
                List<List<double>> BuforLayer = new List<List<double>>();
                for (int j = 0; j < ListOfNumerOfNeurons[i + 1]; j++)
                {
                    string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                    string[] tablica = LineWithSpecyficData.Split(';');
                    List<double> ConvertedRecord = new List<double>();
                    foreach (string t in tablica)
                    {
                        List<string> bufor = t.Split(',').ToList();
                        if (bufor.Count == 2)
                        {
                            double valueBufor = Double.Parse(bufor[0]);
                            int pow = bufor[1].Length;
                            if (pow > 1)
                            {
                                double div = Double.Parse(bufor[1]);
                                if (valueBufor > 0) valueBufor += div / Math.Pow(10, pow);
                                else valueBufor -= div / Math.Pow(10, pow);
                            }
                            else
                            {
                                valueBufor += Double.Parse(bufor[1]) / 10;
                            }
                            ConvertedRecord.Add(valueBufor); // NORMAL
                        }
                        else
                        {
                            ConvertedRecord.Add(Double.Parse(t)); // NORMAL
                        }
                    }
                    BuforLayer.Add(ConvertedRecord);
                }
                ListOfWages.Add(BuforLayer);
            }

            Console.WriteLine($"Log 4 ");

            NeuralNetworkEngin engin = new NeuralNetworkEngin(ListOfNumerOfNeurons, NameOfActivationF, ListOfWages, ListOfBias);
            return engin;
        }
    }
}
