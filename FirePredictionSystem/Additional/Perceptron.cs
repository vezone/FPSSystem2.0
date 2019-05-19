namespace FirePredictionSystem.Additional
{
    using System;

    public class Perceptron
    {
        private int[][] m_X;
        private int[] m_Target;
        public double[] Weights { get; private set; }

        public Perceptron(int[][] x, double[] weights, int[] target)
        {
            m_X = x;
            Weights = new double[weights.Length];
            m_Target = new int[target.Length];
            Array.Copy(weights, Weights, Weights.Length);
            Array.Copy(target, m_Target, m_Target.Length);
        }

        public void Learn()
        {
            double gerror = 1.0, lerror = 1.0;
            double output = 0.0;
            int iter = 0;
            do
            {
                gerror = 0.0;
                if (iter >= 100_000)
                {
                    return;
                }
                for (int r = 0; r < m_X.Length; r++)
                {
                    output = 0.0;
                    for (int c = 0; c < m_X[r].Length; c++)
                    {
                        output += m_X[r][c] * Weights[r];
                    }

                    output = output > 0.5 ? 1.0 : 0.0;
                    lerror = m_Target[r] - output;

                    if (lerror != 0)
                    {
                        gerror += System.Math.Abs(lerror);

                        for (int el = 0; el < Weights.Length; el++)
                        {
                            Weights[r] += 0.1 * lerror * ((m_X[r][el] == 0) ? 1 : m_X[r][el]);
                        }
                    }
                }

                ++iter;
            }
            while (gerror != 0);
        }

        public double[] Test(int[][] X)
        {
            double[] result = new double[X.Length];

            for (int r = 0; r < X.Length; r++)
            {
                double output = 0.0;
                for (int c = 0; c < X[0].Length; c++)
                {
                    output += X[r][c] * Weights[r];
                }

                result[r] = (output > 0.5) ? 1.0 : 0.0;
            }

            return result;
        }

        public void ShowWeights()
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Console.Write($"W[{i}] = {Weights[i]}\n");
            }
        }
    }
}
