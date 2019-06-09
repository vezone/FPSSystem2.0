//#define BANCHMARK

using System.Collections.Generic;


namespace FirePredictionSystem.Additional.C45
{
    public class Tree
    {
        public Leaf Root { get; set; }
        public int LeafCounter { get; set; }
        public List<int> LayersAnswers { get; set; } 
        public List<int> LayersLeaf    { get; set; }

        public void Build(Input input)
        {
#if BANCHMARK
            System.Diagnostics.Stopwatch banch = new System.Diagnostics.Stopwatch();
            banch.Start();
#endif
            Root = new Leaf { Parent = null };
            Root.Input = input;
            Root.SetChildren();

            int iter = 0, NumberOfAnswers = 0;
            
            List<Leaf> children1 = new List<Leaf>(50);
            List<Leaf> children2 = new List<Leaf>(50);

            children1.AddRange(Root.Children);

            while (children1.Count > 0 &&
                 iter < 1000)
            {
                foreach (var child in children1)
                {

                    if (!child.IsAnswer)
                    {
                        ++iter;
                        child.SetChildren();
                        foreach (var child2 in child.Children)
                        {
                            children2.Add(child2);
                        }
                    }
                }

                children1.Clear();
                children1.AddRange(children2);
                children2.Clear();
            }
#if BANCHMARK
            banch.Stop();
            System.IO.File.WriteAllText("banch_file.txt", banch.ElapsedMilliseconds.ToString());
#endif
        }

        public string Check(string[][] toCheck)
        {

            bool run = true;
            int i;
            string value = string.Empty;
            string result = string.Empty;
            Leaf pointer = Root;

            while (run)
            {
                string attributeName = pointer.Classifier.AttributeName;
                List<Leaf> children = pointer.Children;

                //SearchFor
                //value = SearchFor(Root.Classifier.AttributeName);
                for (i = 0; i < toCheck[0].Length; i++)
                {
                    if (toCheck[0][i].Equals(attributeName))
                    {
                        value = toCheck[1][i];
                        break;
                    }
                }

                List<Node> lnodes = pointer.Classifier.Nodes;

                for (i = 0; i < lnodes.Count; i++)
                {
                    if (value.Equals(lnodes[i].Name))
                    {
                        if (!pointer.Children[i].IsAnswer)
                            pointer = pointer.Children[i];
                        else
                        {
                            run = false;
                            if (lnodes[i].Negatives == 0)
                            {
                                result = "Будет пожар";
                            }
                            else
                            {
                                result = "Пожара не будет";
                            }
                            break;
                        }
                    }
                }

            }

            return result;
        }

        public void CountLeafInTree()
        {
            int iter = 0;
            int NumberOfAnswers = 0;
            int NumberOfLeafs = 0;
            List<Leaf> children1 = new List<Leaf>(50);
            List<Leaf> children2 = new List<Leaf>(50);
            LayersAnswers = new List<int>(10);
            LayersLeaf = new List<int>(10);

            children1.AddRange(Root.Children);
            
            while (children1.Count > 0 &&
                iter < 1000)
            {
                foreach (var child in children1)
                {
                    if (!child.IsAnswer)
                    {
                        ++iter;
                        ++LeafCounter;
                        ++NumberOfLeafs;
                        foreach (var child2 in child.Children)
                        {
                            children2.Add(child2);
                        }
                    }
                    else
                    {
                        ++NumberOfAnswers;
                    }
                }

                children1.Clear();
                children1.AddRange(children2);
                children2.Clear();

                LayersAnswers.Add(NumberOfAnswers);
                LayersLeaf.Add(NumberOfLeafs);
                NumberOfAnswers = 0;
                NumberOfLeafs = 0;
            }
        }
    }
}
