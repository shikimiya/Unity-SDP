using System;
using System.Collections.Generic;

namespace sdp
{
    public partial class error
    {
        public const string errExtractCodecRtpmap = "could not extract codec from rtpmap";

        public const string errExtractCodecFmtp = "could not extrcat codec from fmtp";

        public const string errExtractCodecRtcpFb = "could not extract codec form rtcp-fb";

        public const string errPayloadTypeNotFound = "payload type not found";

        public const string errCodecNotFound = "codec not found";

        public const string errSyntaxError = "SyntaxError";
    }
    
    // ConnectionRoleは、どのエンドポイントが接続の確立を開始する必要があるかを示します
    public enum ConnectionRole
    {
        // ConnectionRoleActiveは、エンドポイントが発信接続を開始することを示します。
        ConnectionRoleActive = 1,
        
        // ConnectionRolePassiveは、エンドポイントが着信接続を受け入れることを示します。
        ConnectionRolePassive,
        
        // ConnectionRoleActpassは、エンドポイントが着信接続を受け入れるか、
        // 発信接続を開始する意思があることを示します。
        ConnectionRoleActpass,
        
        // ConnectionRoleHoldconnは、エンドポイントが当面の間接続を確立することを望まないことを示します。
        ConnectionRoleHoldConn,
    }

    public static class ConnectionRoleExtension
    {
        public static string String(ConnectionRole t)
        {
            switch (t)
            {
                case ConnectionRole.ConnectionRoleActive:
                    return "active";
                
                case ConnectionRole.ConnectionRolePassive:
                    return "passive";
                
                case ConnectionRole.ConnectionRoleActpass:
                    return "actpass";
                
                case ConnectionRole.ConnectionRoleHoldConn:
                    return "holdconn";
                
                default:
                    return "Unknown";
            }
        }
    }
    
    // Codec represents a codec
    public class Codec
    {
        public byte PayloadType;

        public string Name;

        public uint ClockRate;

        public string EncodingParameters;

        public string Fmtp;

        public List<string> RTCPFeedback;

        public Codec()
        {
            PayloadType = 0;

            Name = null;

            ClockRate = 0;

            EncodingParameters = null;

            Fmtp = null;
            
            RTCPFeedback = new List<string>();
        }

        public string EqualCodec(Codec c)
        {
            string s = null;

            if (PayloadType != c.PayloadType)
            {
                s += $"PayloadType Error: {PayloadType} / {c.PayloadType}\n";
            }

            if (Name != c.Name)
            {
                s += $"Name Error: {Name} / {c.Name}\n";
            }

            if (ClockRate != c.ClockRate)
            {
                s += $"ClockRate Error: {ClockRate} / {c.ClockRate}\n";
            }

            if (EncodingParameters != c.EncodingParameters)
            {
                s += $"EncodingParameters Error: {EncodingParameters} / {c.EncodingParameters}\n";
            }

            if (Fmtp != c.Fmtp)
            {
                s += $"Fmpt Error: {Fmtp} / {c.Fmtp}\n";
            }

            for (var i = 0; i < RTCPFeedback.Count; i++)
            {
                if (RTCPFeedback[i] != c.RTCPFeedback[i])
                {
                    s += $"RTCPFeedback[{i}] Error: {RTCPFeedback[i]} / {c.RTCPFeedback[i]}\n";
                }
            }

            return s;
        }

        public string ShowCodec()
        {
            var s = "";

            s += $"Codec PayloadType: {PayloadType}\n";

            s += $"Codec Name: {Name}\n";

            s += $"Codec ClockRate: {ClockRate}\n";

            s += $"Codec EncodingParameters: {EncodingParameters}\n";

            s += $"Codec Fmtp: {Fmtp}\n";

            foreach (var i in RTCPFeedback)
            {
                s += $"Codec RTCPFeedback: {i}\n";
            }

            return s;
        }
        
        public string String()
        {
            return $"{PayloadType} {Name}/{ClockRate}/{EncodingParameters} ({Fmtp}) [{string.Join(",", RTCPFeedback)}]";
        }
    }


    public static class util
    {
        public const string attributeKey = "a=";
        public static (ulong, string) newSessionID()
        {
            // https://tools.ietf.org/html/draft-ietf-rtcweb-jsep-26#section-5.2.1
            //セッションIDは、最高ビットがゼロに設定され、
            //残りの63ビットが暗号的にランダムである64ビット量を
            //生成することによって構築することをお勧めします。
            var (id, err) = randutil.crypto.CryptoUint64();

            var r = ~(Convert.ToUInt64(1) << 63);

            return (id & r, err);
        }

        public static (Codec, string) parseRtpmap(string rtpmap)
        {
            var codec = new Codec();
            
            var parsingFailed = error.errExtractCodecRtpmap;
            
            // a=rtpmap:<payload type> <encoding name>/<clock rate>[/<encoding parameters>]
            var split = rtpmap.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var ptSplit = split[0].Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            if (ptSplit.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var ptInt = 0;

            try
            { 
                ptInt = Convert.ToInt32(ptSplit[1]);
            }
            catch(Exception)
            {
                return (codec, parsingFailed);
            }

            codec.PayloadType = Convert.ToByte(ptInt);

            split = split[1].Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

            codec.Name = split[0];

            var parts = split.Length;

            var rate = 0;

            if (parts > 1)
            {
                try
                {
                    rate = Convert.ToInt32(split[1]);
                }
                catch(Exception)
                {
                    return (codec, parsingFailed);
                }

                codec.ClockRate = Convert.ToUInt32(rate);
            }

            if (parts > 2)
            {
                codec.EncodingParameters = split[2];
            }

            return (codec, null);
        }

        public static (Codec, string) parseFmtp(string fmtp)
        {
            var codec = new Codec();

            var parsingFailed = error.errExtractCodecFmtp;
            
            // a=fmtp:<format> <format specific parameters>
            var split = fmtp.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var formatParams = split[1];

            split = split[0].Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var ptInt = 0;

            try
            {
                ptInt = Convert.ToInt32(split[1]);
            }
            catch(Exception)
            {
                return (codec, parsingFailed);
            }

            codec.PayloadType = Convert.ToByte(ptInt);
            
            codec.Fmtp = formatParams;


            return (codec, null);
        }

        public static (Codec, string) parseRtcpFb(string rtcpFb)
        {
            var codec = new Codec();

            var parsingFailed = error.errExtractCodecRtcpFb;
            
            // a=ftcp-fb:<payload type> <RTCP feedback type> [<RTCP feedback parameter>]
            var split = rtcpFb.Split(new char[] {' '} , 2, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var ptSplit = split[0].Split(':');

            if (ptSplit.Length != 2)
            {
                return (codec, parsingFailed);
            }

            var ptInt = 0;

            try
            {
                ptInt = Convert.ToInt32(ptSplit[1]);
            }
            catch (Exception)
            {
                return (codec, parsingFailed);
            }

            codec.PayloadType = Convert.ToByte(ptInt);
            
            codec.RTCPFeedback.Add(split[1]);

            return (codec, null);
        }

        public static void mergeCodec(Codec codec, Dictionary<byte, Codec> codecs)
        {
            var savedCodec = new Codec();
            
            if (codecs.ContainsKey(codec.PayloadType))
            {
                savedCodec = codecs[codec.PayloadType];
            }

            if (savedCodec.PayloadType == 0)
            {
                savedCodec.PayloadType = codec.PayloadType;
            }

            if (savedCodec.Name == null)
            {
                savedCodec.Name = codec.Name;
            }

            if (savedCodec.ClockRate == 0)
            {
                savedCodec.ClockRate = codec.ClockRate;
            }

            if (savedCodec.EncodingParameters == null)
            {
                savedCodec.EncodingParameters = codec.EncodingParameters;
            }

            if (savedCodec.Fmtp == null)
            {
                savedCodec.Fmtp = codec.Fmtp;
            }
            
            savedCodec.RTCPFeedback.AddRange(codec.RTCPFeedback);

            codecs[savedCodec.PayloadType] = savedCodec;
        }

        public static bool equivalentFmtp(string want, string got)
        {
            if (want == null)
            {
                want = "";
            }
            
            var wantSplit = want.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            var gotSplit = got.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            if (wantSplit.Length != gotSplit.Length)
            {
                return false;
            }

            Array.Sort(wantSplit);

            Array.Sort(gotSplit);

            for (var i = 0; i < wantSplit.Length; i++)
            {
                var wantPart = wantSplit[i].Trim();

                var gotPart = gotSplit[i].Trim();

                if (gotPart != wantPart)
                {
                    return false;
                }
            }

            return true;
        }


        public static bool codecsMatch(Codec wanted, Codec got)
        {
            if (wanted.Name != null && !string.Equals(wanted.Name, got.Name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (wanted.ClockRate != 0 && wanted.ClockRate !=  got.ClockRate)
            {
                return false;
            }

            if (wanted.EncodingParameters != null && wanted.EncodingParameters != got.EncodingParameters)
            {
                return false;
            }

            if (wanted.Fmtp != null && !equivalentFmtp(wanted.Fmtp, got.Fmtp))
            {
                return false;
            }

            return true;
        }
    }
    
    
    public partial class SessionDescription
    {
        public Dictionary<byte, Codec> buildCodecMap()
        {
            var codecs = new Dictionary<byte, Codec>();

            foreach (var m in MediaDescriptions)
            {
                foreach (var a in m.Attributes)
                {
                    var attr = a.String();

                    if (attr.StartsWith("rtpmap:"))
                    {
                        var (codec, err) = util.parseRtpmap(attr);
                        
                        if (err == null)
                        {
                            util.mergeCodec(codec, codecs);
                        }
                    }


                    if (attr.StartsWith("fmtp:"))
                    {
                        var (codec, err) = util.parseFmtp(attr);

                        if (err == null)
                        {
                            util.mergeCodec(codec, codecs);
                        }
                    }

                    if (attr.StartsWith("rtcp-fb:"))
                    {
                        var (codec, err) = util.parseRtcpFb(attr);

                        if (err == null)
                        {
                            util.mergeCodec(codec, codecs);
                        }
                    }
                }
            }

            return codecs;
        }
        
        // GetCodecForPayloadTypeは、指定されたペイロードタイプのSessionDescriptionをスキャンし、コーデックを返します
        public (Codec, string) GetCodecForPaykloadType(byte payloadType)
        {
            var codecs = buildCodecMap();

            try
            {
                var codec = codecs[payloadType];

                return (codec, null);
            }
            catch
            {
                return (null, error.errPayloadTypeNotFound);
            }
        }
        
        // GetPayloadTypeForCodecは、SessionDescriptionをスキャンして、
        // 提供されたコーデックに可能な限り一致するコーデックを探し、そのペイロードタイプを返します
        public (byte, string) GetPayloadTypeForCodec(Codec wanted)
        {
            var codecs = buildCodecMap();

            foreach (var codec in codecs)
            {
                if (util.codecsMatch(wanted, codec.Value))
                {
                    return (codec.Key, null);
                }
            }

            return (0, error.errCodecNotFound);
        }
    }

    public partial class lexer
    {
        public SessionDescription desc;

        public baseLexer baseLexer;

        public lexer()
        {
            desc = new SessionDescription();
            
            baseLexer = new baseLexer();
        }

        public (stateFn, string) handleType(keyToState fn)
        {
            var (key, err) = baseLexer.readType();

            if (err == "io eof" && key == "")
            {
                return (null, null);
            }
            else if (err != null)
            {
                return (null, err);
            }

            var res = fn(key);

            if (res != null)
            {
                return (res, null);
            }

            return (null, baseLexer.syntaxError());
        }
    }

    public delegate (stateFn, string) stateFn(lexer l);

    public delegate stateFn keyToState(string key);
}