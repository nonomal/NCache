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
using System;
using Alachisoft.NCache.Common.Enum;
using Alachisoft.NCache.Common.Stats;

namespace Alachisoft.NCache.Common.Sockets
{
    public class ReceiveContext : CommunicationContext
    {
        public int ChunkToReceive;
        public long RequestSize;
        public ArraySegment<byte> Buffer;
        public ReceivingState State;
        public HPTimeStats ReceiveTimeStats;
        public DateTime ReceiveStartTime;
    }
}
