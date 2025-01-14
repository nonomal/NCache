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
using Alachisoft.NCache.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alachisoft.NCache.Runtime.Serialization.IO;
using System.Collections;
using Alachisoft.NCache.Common.Util;

namespace Alachisoft.NCache.Caching.Messaging
{
    public class TopicState : ICompactSerializable
    {

        private ArrayList _registeredTopicsState = new ArrayList();

        public ArrayList RegisteredTopicStates
        {
            get { return _registeredTopicsState; }
            set { _registeredTopicsState = value; }
        }


        public void Deserialize(CompactReader reader)
        {
            _registeredTopicsState = reader.ReadObject() as ArrayList;
        }

        public void Serialize(CompactWriter writer)
        {
            writer.WriteObject(_registeredTopicsState);
        }

      
    }
}
