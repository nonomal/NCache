//  Copyright (c) 2021 Alachisoft
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
namespace Alachisoft.NCache.Common.Protobuf
{
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LockInfo")]
    public partial class LockInfo : global::ProtoBuf.IExtensible
    {
      public LockInfo() {}
      

    private int _lockAccessType = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"lockAccessType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)][global::System.ComponentModel.DefaultValue(default(int))]
    public int lockAccessType
    {
      get { return _lockAccessType; }
      set { _lockAccessType = value; }
    }

    private string _lockId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"lockId", DataFormat = global::ProtoBuf.DataFormat.Default)][global::System.ComponentModel.DefaultValue("")]
    public string lockId
    {
      get { return _lockId; }
      set { _lockId = value; }
    }

    private long _lockTimeout = default(long);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"lockTimeout", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)][global::System.ComponentModel.DefaultValue(default(long))]
    public long lockTimeout
    {
      get { return _lockTimeout; }
      set { _lockTimeout = value; }
    }
      private global::ProtoBuf.IExtension extensionObject;
      global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
  
}
