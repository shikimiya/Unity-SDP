using System;
using System.Collections.Generic;
using UnityEngine;

namespace sdp
{
    //情報は、セッションに関するテキスト情報を提供する「i =」フィールドを記述します。
    public class Information
    {
        public string information;

        public Information()
        {
            information = null;
        }

        public string String()
        {
            return Convert.ToString(information);
        }
    }
    
    // ConnectionInformationは、接続データを含む「c =」フィールドの表現を定義します。
    public class ConnectionInformation
    {
        public string NetworkType;

        public string AddressType;

        public Address address;
        public ConnectionInformation()
        {
            NetworkType = null;

            AddressType = null;
            
            address = new Address();
        }

        public string String()
        {
            var parts = new List<string>();

            if (NetworkType != null)
            {
                parts.Add(NetworkType);
            }

            if (AddressType != null)
            {
                parts.Add(AddressType);
            }

            if (address != null && address.String() != "")
            {
                parts.Add(address.String());
            }

            return string.Join(" ", parts);
        }
    }
    
    //アドレスは、「c =」フィールド内の構造化アドレストークンを記述します。
    public class Address
    {
        public string address;

        public int TTL;

        public int Range;

        public Address()
        {
            address = null;

            TTL = 0;

            Range = 0;
        }

        public string String()
        {
            var parts = new List<string>();
            
            parts.Add(address);

            if (TTL != 0)
            {
                parts.Add(Convert.ToString(TTL));
            }

            if (Range != 0)
            {
                parts.Add(Convert.ToString(Range));
            }


            return string.Join("/", parts);
        }
    }
    
    //帯域幅は、セッションまたはメディアによって使用される
    //提案された帯域幅を示すオプションのフィールドを記述します。
    public class BandWidth
    {
        public bool Experimental;

        public string Type;

        public ulong bandWidth;

        public BandWidth()
        {
            Experimental = false;

            Type = null;

            bandWidth = 0;
        }

        public string String()
        {
            var output = "";

            if (Experimental)
            {
                output += "X-";
            }

            output += Type + ":" + Convert.ToString(bandWidth);

            return output;
        }
    }
    
    // EncryptionKeyは、暗号化キー情報を伝達する「k =」を記述します。
    public class EncryptionKey
    {
        public string encryptionKey;

        public EncryptionKey()
        {
            encryptionKey = null;
        }
        
        public string String()
        {
            return encryptionKey;
        }
    }
    
    //属性は、SDPを拡張するための主要な手段を表す「a =」フィールドを記述します。
    public class Attribute
    {
        public string Key;

        public string Value;

        public Attribute()
        {
            Key = null;

            Value = null;
        }

        public void Equal(Attribute a)
        {
            if (Key != a.Key)
            {
                Debug.LogError($"{Key} : {a.Key}");
            }

            if (Value != a.Value)
            {
                Debug.LogError($"{Value} : {a.Value}");
            }
        }


        public string String()
        {
            var output = Key;

            if (Value != null)
            {
                if (Value.Length > 0)
                {
                    output += ":" + Value;
                }
            }

            return output;
        }
        
        //属性キーが「候補」と等しい場合、IsICECandidateはtrueを返します。
        public bool IsICECandidate()
        {
            return Key == "candidate";
        }
    }

    public static class AttributeExtended
    {
        // NewPropertyAttributeは新しい属性を構築します
        public static Attribute NewPropertyAttribute(string key)
        {
            return new Attribute
            {
                Key = key,
                Value = null
            };
        }
        
        // NewAttributeは新しい属性を構築します
        public static Attribute NewAttribute(string key, string value)
        {
            return new Attribute
            {
                Key = key,
                Value = value
            };
        }
    }
}