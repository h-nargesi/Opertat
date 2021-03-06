using System;
using MathNet.Numerics.LinearAlgebra;
using Photon.NeuralNetwork.Chista.Implement;

namespace Photon.NeuralNetwork.Chista
{
    public class Sigmoind : IConduction
    {
        public int ExtraCount => 0;
        public Vector<double> Conduct(Vector<double> signal)
        {
            return 1 / (1 + (signal * -1).PointwiseExp());
        }
        public Vector<double> Conduct(NeuralNetworkFlash flash, int layer)
        {
            return 1 / (1 + (flash.SignalsSum[layer] * -1).PointwiseExp());
        }
        public Vector<double> ConductDerivative(NeuralNetworkFlash flash, int layer)
        {
            return flash.InputSignals[layer + 1]
                .PointwiseMultiply(1 - flash.InputSignals[layer + 1]);
        }

        public override string ToString()
        {
            return "Sigmoind";
        }
    }
}