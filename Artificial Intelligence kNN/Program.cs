using Artificial_Intelligence_kNN;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI4
{
    class Program
    {

        static void Main(string[] args)
        {
            Int32 k = 20;
            Flower[] irisData = LoadIrisData();

            Flower irisToClassify = new Flower();
            irisToClassify.CupLength = 5.2d;
            irisToClassify.CupWidth = 3.3d;
            irisToClassify.PetalWidth = 2.2d;
            irisToClassify.PetalLength = 1.4d;

            FlowerDataInfo[] irisDataAgainstUnknown = irisData.Select(iris =>
            {
                FlowerDataInfo ret = new FlowerDataInfo(iris);
                ret.ComputeDistance(irisToClassify);
                return ret;
            }).ToArray();

            Array.Sort(irisDataAgainstUnknown);

            Dictionary<FlowerKind, Int32> votingResults = new Dictionary<FlowerKind, Int32>
            {
                { FlowerKind.Setosa, 0 },
                { FlowerKind.Virginica, 0 },
                { FlowerKind.VersiColor, 0 }
            };

            for (Int32 i = 0; i < k; i++)
                votingResults[irisDataAgainstUnknown[i].OriginalFlower.Kind]++;

            Console.WriteLine($"Niewiadoma klasa abstrakcji została zklasyfikowana jako { Enum.GetName(typeof(FlowerKind), votingResults.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2).Key) }");
            Console.ReadKey();
        }

        private static Flower[] LoadIrisData()
        {
            String path = @"C:\Users\Intel\Desktop\iris.txt";
            String[] flowersData = File.ReadAllLines(path);
            List<Flower> flowers = new List<Flower>();
            int ie = 0;
            foreach (String flowerLine in flowersData)
            {
                ie++;
                String[] flowerDataSep = flowerLine.Split(',');
                FlowerKind kind = FlowerKind.Setosa;
                if (flowerDataSep[4] == "Iris-setosa")
                    kind = FlowerKind.Setosa;
                else if (flowerDataSep[4] == "Iris-versicolor")
                    kind = FlowerKind.VersiColor;
                else if (flowerDataSep[4] == "Iris-virginica")
                    kind = FlowerKind.Virginica;

                flowers.Add(new Flower
                {
                    CupLength = Convert.ToDouble(flowerDataSep[0], new CultureInfo("en-US")),
                    CupWidth = Convert.ToDouble(flowerDataSep[1], new CultureInfo("en-US")),
                    PetalLength = Convert.ToDouble(flowerDataSep[2], new CultureInfo("en-US")),
                    PetalWidth = Convert.ToDouble(flowerDataSep[3], new CultureInfo("en-US")),
                    Kind = kind
                });
            }
            return flowers.ToArray();
        }

    }

    [DebuggerDisplay("Kind")]
    public class Flower
    {
        public Double CupLength { get; set; }

        public Double CupWidth { get; set; }

        public Double PetalLength { get; set; }

        public Double PetalWidth { get; set; }

        public FlowerKind Kind { get; set; }

    }

    public enum FlowerKind
    {
        Setosa = 1,
        VersiColor = 2,
        Virginica = 3
    }
}