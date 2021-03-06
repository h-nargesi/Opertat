﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Photon.NeuralNetwork.Chista.Implement
{
    public interface IChistaNet : INeuralNetworkInformation
    {
        public double LearningFactor { get; set; }
        public double CertaintyFactor { get; set; }
        public double DropoutFactor { get; set; }

        public INeuralNetworkImage Image();

        public double[] Stimulate(double[] inputs);
        public NeuralNetworkFlash Test(double[] inputs, double[] values = null);
        public NeuralNetworkFlash Train(double[] inputs, double[] values);
        public void FillTotalError(NeuralNetworkFlash flash, double[] values);
    }
}
