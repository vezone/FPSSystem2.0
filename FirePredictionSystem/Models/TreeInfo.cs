namespace FirePredictionSystem.Models
{
    using System.Text;
    using System.Collections.Generic;

    public class TreeInfo
    {
        public List<int> LayerIndex  { get; set; }
        public List<int> LeafsOnLayer   { get; set; }
        public List<int> AnswersOnLayer { get; set; }
        public int LayersCount
        {
            get => LeafsOnLayer.Count;
        }
        public int LeafCount
        {
            get
            {
                int counter = 0;

                for (int i = 0; i < LeafsOnLayer.Count; i++)
                {
                    counter += LeafsOnLayer[i];
                }

                return counter;
            }
        }
        public int AnswersCount
        {
            get
            {
                int counter = 0;

                for (int i = 0; i < AnswersOnLayer.Count; i++)
                {
                    counter += AnswersOnLayer[i];
                }

                return counter;
            }
        }

        public TreeInfo()
        {
            LayerIndex = new List<int>();
            LeafsOnLayer = new List<int>();
            AnswersOnLayer = new List<int>();
        }

        public override string ToString()
        {
            System.Console.ForegroundColor = System.ConsoleColor.White;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < AnswersOnLayer.Count; i++)
            {
                result.AppendFormat($"{LayerIndex[i]} {LeafsOnLayer[i]} {AnswersOnLayer[i]}\n");
            }
            return result.ToString();
        }

    }
}
