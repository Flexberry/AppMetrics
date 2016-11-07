﻿// Copyright (c) Allan hardy. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace App.Metrics.Core
{
    public class HistogramOptions : MetricValueWithSamplingOption
    {
        public HistogramOptions()
        {
            SamplingType = SamplingType.Default;
        }
    }
}