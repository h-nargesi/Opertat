﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Photon.NeuralNetwork.Opertat.Trainer;

namespace Photon.NeuralNetwork.Opertat.Debug.Config
{
    public class ProcessConfigHandler : ConfigHandler
    {
        public ProcessConfigHandler(JObject setting) : base(setting) { }

        public const string key = "process";
        private const string process_stage = "stage";
        private const string process_offset = "offset";

        public TraingingStages? Stage
        {
            get
            {
                var str = GetSetting<string>(process_stage, null);
                if (str == null) return null;

                str = str.ToLower();
                str = char.ToUpper(str[0]) + str.Substring(1);
                return (TraingingStages)Enum.Parse(typeof(TraingingStages), str);
            }
            set { SetSetting(process_stage, value?.ToString().ToLower()); }
        }

        public uint? Offset
        {
            get { return GetSetting<uint?>(process_offset, null); }
            set { SetSetting(process_offset, value); }
        }
    }
}
