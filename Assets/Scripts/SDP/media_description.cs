using System;
using System.Collections.Generic;

namespace sdp
{
    // MediaDescriptionはメディアタイプを表します。
    // https://tools.ietf.org/html/rfc4566#section-5.14
    public partial class MediaDescription
    {
        // m=<media> <port>/<number of ports> <proto> <fmt> ...
        // https://tools.ietf.org/html/rfc4566#section-5.14
        public MediaName MediaName;
        
        // i=<session description>
        // https://tools.ietf.org/html/rfc4566#section-5.4
        public Information MediaTitle;
        
        // c=<nettype> <addrtype> <connection-address>
        // https://tools.ietf.org/html/rfc4566#section-5.7
        public ConnectionInformation ConnectionInformation;
        
        // b=<bwtype>:<bandwidth>
        // https://tools.ietf.org/html/rfc4566#section-5.8
        public List<BandWidth> Bandwidth;
        
        // k=<method>
        // k=<method>:<encryption key>
        // https://tools.ietf.org/html/rfc4566#section-5.12
        public EncryptionKey EncryptionKey;
        
        // a=<attribute>
        // a=<attribute>:<value>
        // https://tools.ietf.org/html/rfc4566#section-5.13
        public List<Attribute> Attributes;

        public MediaDescription()
        {
            MediaName = new MediaName();
            
            MediaTitle = new Information();
            
            ConnectionInformation = new ConnectionInformation();
            
            Bandwidth = new List<BandWidth>();
            
            EncryptionKey = new EncryptionKey();
            
            Attributes = new List<Attribute>();
        }
        
        //属性は、属性の値を返します。存在する場合は
        public (string, bool) Attribute(string key)
        {
            foreach (var a in Attributes)
            {
                if (a.Key == key)
                {
                    return (a.Value, true);
                }
            }

            return ("", false);
        }
    }
    
    // RangedPortは、メディアフィールド "m =" portvalueの特別な形式をサポートします。

    //複数のトランスポートポートを指定する必要がある場合、
    //プロトコルでは次のように記述できます。
    //<ポート> / <ポート数>ここで、ポート数はオフセット範囲です。
    public class RangedPort
    {
        public int Value;

        public int Range;

        public RangedPort()
        {
            Value = 0;

            Range = 0;
        }

        public string String()
        {
            var output = Convert.ToString(Value);

            if (Range != 0)
            {
                output += "/" + Convert.ToString(Range);
            }

            return output;
        }
    }

    // MediaNameは、「m = "フィールドストレージ構造」を記述します。
    public class MediaName
    {
        public string Media;

        public RangedPort Port;

        public List<string> Protos;

        public List<string> Formats;

        public MediaName()
        {
            Media = null;
            
            Port = new RangedPort();
            
            Protos = new List<string>();
            
            Formats = new List<string>();
        }

        public string String()
        {
            var s = new List<string>();

            s.Add(Media);
            
            s.Add(Port.String());

            var r = string.Join("/", Protos);

            s.Add(r);

            r = string.Join(" ", Formats);

            s.Add(r);

            var result = string.Join(" ", s);

            return result;
        }
    }
}