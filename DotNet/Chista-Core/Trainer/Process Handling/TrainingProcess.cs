﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Photon.NeuralNetwork.Chista.Trainer
{
    class TrainingProcess : ITrainingProcess
    {
        private readonly TrainingProcessHistory history;
        private int record_count;
        private double total_accuracy;

        public TrainingProcess(Brain brain, IAccurateGauge accurate)
        {
            Brain = brain ?? throw new ArgumentNullException(nameof(brain));
            Accurate = accurate ?? throw new ArgumentNullException(nameof(accurate));
            history = new TrainingProcessHistory();
        }
        public TrainingProcess(NetProcessInfo state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            Brain = new Brain(state.current_image);
            history = TrainingProcessHistory.Restore(state.accuracy_chain, state.best_image, state.accurate);
            record_count = state.record_count;
            total_accuracy = state.current_total_accruacy;
            Accurate = state.accurate;
        }

        public IAccurateGauge Accurate { get; }
        public NeuralNetworkImage Image { get; }
        public Brain Brain { get; private set; }
        public double Accuracy { get; private set; }
        public NeuralNetworkFlash LastPrediction { get; private set; }

        public void ChangeSatate(NeuralNetworkFlash predict)
        {
            record_count++;
            total_accuracy += Accurate.Accuracy(predict);
            Accuracy = total_accuracy / record_count;
            LastPrediction = predict;
        }
        public bool FinishCurrentState(bool is_training)
        {
            bool is_out_of_line;
            if (is_training) is_out_of_line = false;
            else is_out_of_line = history.AddProgress(this);

            record_count = 0;
            total_accuracy = 0;

            return is_out_of_line;
        }

        public NeuralNetworkImage BestBrainImage
        {
            get { return history.BestBrainInfo?.Image; }
        }
        public double BestBrainAccuracy
        {
            get { return history.BestBrainInfo?.Accuracy ?? 0; }
        }

        public NetProcessInfo ProcessInfo()
        {
            return new NetProcessInfo(Brain.Image(), record_count, total_accuracy,
                history.AccuracyChain(), history.BestBrainInfo?.Image, Accurate);
        }

        public override string ToString()
        {
            return $"accuracy: {Accuracy}, (flash:{LastPrediction})";
        }
        public string PrintInfo()
        {
            return $"{Brain.PrintInfo()}\ncurrent accuracy: {Accuracy}";
        }
    }
}