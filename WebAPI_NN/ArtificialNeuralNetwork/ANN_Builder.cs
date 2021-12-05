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

            List<List<double>> ListOfBias = new List<List<double>>();
            for (int i = 0; i < ListOfNumerOfNeurons.Count - 1; i++)
            {
                string LineWithSpecyficData = fileLines.GetValue(LineCounter++).ToString();
                string[] tablica = LineWithSpecyficData.Split(';');
                List<double> ConvertedRecord = new List<double>();
                foreach (string t in tablica)
                {
                    ConvertedRecord.Add(Double.Parse(t));
                }
                ListOfBias.Add(ConvertedRecord);
            }

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
                        ConvertedRecord.Add(Double.Parse(t));
                    }
                    BuforLayer.Add(ConvertedRecord);
                }
                ListOfWages.Add(BuforLayer);
            }

            NeuralNetworkEngin engin = new NeuralNetworkEngin(ListOfNumerOfNeurons, NameOfActivationF, ListOfWages, ListOfBias);
            return engin;
        }
    }
}
