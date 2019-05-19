using System.Collections.Generic;

namespace FirePredictionSystem.Additional.C45
{
    public class Tree
    {
        public Leaf Root { get; set; }
    
        public void Build(Input input)
        {
            Root = new Leaf { Parent = null };
            Root.Input = input;
            Root.SetChildren();

            List<Leaf> children1 = new List<Leaf>();
            List<Leaf> children2 = new List<Leaf>();

            foreach (var child in Root.Children)
                children1.Add(child);

            int NumberOfAnswers = 0;
            List<int> LayersAnswers = new List<int>(10);
            int iter = 0;
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
                    else
                    {
                        ++NumberOfAnswers;
                    }
                }

                LayersAnswers.Add(NumberOfAnswers);
                NumberOfAnswers = 0;
                
                children1.Clear();
                children1 = new List<Leaf>(children2);
                children2.Clear();
            }
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
                                result = "Yes";
                            }
                            else
                            {
                                result = "No";
                            }
                            break;
                        }
                    }
                }

            }

            return result;
        }


    }
}
