using System;
using MathNet.Numerics.LinearAlgebra;
using Photon.NeuralNetwork.Chista.Implement;

namespace Photon.NeuralNetwork.Chista.Deprecated
{
    public class ErrorStack : IErrorFunction
    {
        private readonly Vector<double> indexed;

        public ErrorStack(int output_count)
        {
            var ix = new double[output_count];
            if (output_count == 1) ix[0] = 1;
            else
            {
                var r = output_count - 1;
                var c = r / 2D;
                var a = r - (0.4D / r);
                for (int i = 0; i < output_count; i++)
                    ix[output_count - (i + 1)] = 1 + (i - c) / a;
            }

            indexed = Vector<double>.Build.DenseOfArray(ix);
        }

        public int IndexCount { get { return indexed.Count; } }

        public Vector<double> NegativeErrorDerivative(Vector<double> output, Vector<double> values)
        {
            return indexed.PointwiseMultiply(values - output) / values.Count;
        }
        public double Accuracy(NeuralNetworkFlash flash, double[] _)
        {
            // actually 'TotalError' is avrage of errors
            // becaue in 'NegativeErrorDerivative' function output is divided by count
            return 1 - flash.TotalError;
        }

        public override string ToString()
        {
            return "ErrorStack ** deprecated";
        }
    }
}