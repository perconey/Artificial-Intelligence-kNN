using AI4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence_kNN
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FlowerDataInfo : IComparable<FlowerDataInfo>
    {
        public Flower OriginalFlower { get; set; }
        public Double Distance { get; set; }

        private string DebuggerDisplay
        {
            get
            {
                return $"{Enum.GetName(typeof(FlowerKind), OriginalFlower.Kind)}";
            }
        }

        public FlowerDataInfo(Flower org)
        {
            OriginalFlower = org;
        }

        public void ComputeDistance(Flower flowerAgainst)
        {
            Double sum = 0.0;
            sum += Math.Pow(flowerAgainst.CupLength - this.OriginalFlower.CupLength, 2);
            sum += Math.Pow(flowerAgainst.CupWidth - this.OriginalFlower.CupWidth, 2);
            sum += Math.Pow(flowerAgainst.PetalLength - this.OriginalFlower.PetalLength, 2);
            sum += Math.Pow(flowerAgainst.PetalWidth - this.OriginalFlower.PetalWidth, 2);

            Distance = Math.Sqrt(sum);
        }

        public int CompareTo(FlowerDataInfo other)
        {
            if (Distance < other.Distance)
                return -1;
            else if (Distance > other.Distance)
                return +1;
            else
                return 0;
        }
    }
}
