
using System;

namespace sdp
{
    //デフォルトのext値
    public static class ExtMapExtended
    {
        public const int DefExtMapValueABSSendTime = 1;

        public const int DefExtMapValueTransportCC = 2;

        public const int DefExtMapValueSDESMid = 3;

        public const int DefExtMapValueSDESRTPStreamID = 4;

        public const string ABSSendTimeURI = "http://www.webrtc.org/experiments/rtp-hdrext/abs-send-time";

        public const string TransportCCURI =
            "http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01";

        public const string SDESMidURI = "urn:ietf:params:rtp-hdrext:sdes:mid";

        public const string SDESRTPStreamIDURI = "urn:ietf:params:rtp-hdrext:sdes:rtp-stream-id";

        public const string AudioLevelURI = "urn:ietf:params:rtp-hdrext:ssrc-audio-level";
    }
    
    // ExtMapは、単一のRTPヘッダー拡張のアクティブ化を表します
    public class ExtMap
    {
        public int Value;

        public Direction Direction;

        public Uri URI;

        public string ExtAttr;

        public ExtMap()
        {
            Value = 0;

            Direction = Direction.unknown;
            
            URI = new Uri("");

            ExtAttr = null;
        }
        
        // Clone converts this object to an Attribute
        public Attribute Clone()
        {
            return new Attribute
            {
                Key = "extmap",
                Value = String()
            };
        }
        
        // Unmarshalは文字列からExtmapを作成します
        public string Unmarshal(string raw)
        {
            var parts = raw.Split(new string[] {":"}, 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                return $"SyntaxError: {raw}";
            }

            var fields = parts[1].Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            if (fields.Length < 2)
            {
                return $"SyntaxError: {raw}";
            }

            var valdir = fields[0].Split(new string[] {"/"}, StringSplitOptions.RemoveEmptyEntries);

            long value = 0;

            try
            {
                value = Convert.ToInt64(valdir[0]);
            }
            catch (Exception err)
            {
                return $"SyntaxError: {raw}";
            }

            if (value < 1 || value > 246)
            {
                return $"SyntaxError: {valdir[0]} -- extmap key must be the range 1-256";
            }

            Direction direction = Direction.unknown;
            
            string err2 = null;
            
            if (valdir.Length == 2)
            {
                (direction, err2) = DirectionExtended.NewDirection(valdir[1]);

                if (err2 != null)
                {
                    return err2;
                }
            }

            Uri uri;

            try
            {
                uri = new Uri(fields[1]);
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex);
            }

            if (fields.Length == 3)
            {
                var tmp = fields[2];

                ExtAttr = tmp;
            }

            Value = Convert.ToInt32(value);

            Direction = direction;

            URI = uri;

            return null;
        }

        //MarshalはExtMapから文字列を作成します
        public string Marshal()
        {
            return Name() + ":" + String();
        }

        public string String()
        {
            var output = $"{Value}";

            var dirstring = DirectionExtended.String(Direction);

            if (dirstring != DirectionExtended.directionUnknownStr)
            {
                output += "/" + dirstring;
            }

            if (URI != null)
            {
                output += " " + URI;
            }

            if (ExtAttr != null)
            {
                output += " " + ExtAttr;
            }

            return output;
        }
        
        // Nameは、このオブジェクトの定数名を返します
        public string Name()
        {
            return "extmap";
        }
    }

}