﻿//  Copyright (c) 2021 Alachisoft
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//     http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License

namespace Alachisoft.NCache.Common.Caching.Statistics.CustomCounters
{
    public class NumberOfItemCounter : PerformanceCounterBase
    {
        public NumberOfItemCounter(string name, string instance) : base(name, instance)
        {

        }

        public NumberOfItemCounter(string category, string name, string instance) : base(category, name, instance)
        {

        }

        public override void Increment()
        {
            lock (this)
            {
                _value++;
            }
        }

        public override void IncrementBy(double value)
        {
            lock (this)
            {
                _value += value;
            }
        }


        public override void Decrement()
        {
            lock (this)
            {
                _value--;
            }
        }


        public override void DecrementBy(double value)
        {
            lock (this)
            {
                _value -= value;
            }
        }

        public override double Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
