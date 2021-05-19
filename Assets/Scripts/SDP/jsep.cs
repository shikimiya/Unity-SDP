using System;
using System.Collections.Generic;

namespace sdp
{
    // JSEPで使用されるSDP属性の定数
    public static class AttrKey
    {
        public const string AttrKeyCandidate = "candidate";

        public const string AttrKeyEndOfCandidates = "end-of-candidates";

        public const string AttrKeyIdentity = "identity";

        public const string AttrKeyGroup = "group";

        public const string AttrKeySSRC = "ssrc";

        public const string AttrKeySSRCGroup = "ssrc-group";

        public const string AttrKeyMsid = "msid";

        public const string AttrKeyMsidSemantic = "msid-semantic";

        public const string AttrKeyConnectionSetup = "setup";

        public const string AttrKeyMID = "mid";

        public const string AttrKeyICELite = "ice-lite";

        public const string AttrKeyRTCPMux = "rtcp-mux";

        public const string AttrKeyRTCPRsize = "rtcp-rsize";

        public const string AttrKeyInactive = "inactive";

        public const string AttrKeyRecvOnly = "recvonly";

        public const string AttrKeySendOnly = "sendonly";

        public const string AttrKeySendRecv = "sendrecv";

        public const string AttrKeyExtMap = "extmap";
    }
    
    // JSEPで使用されるセマンティックトークンの定数
    public static class SemanticToken
    {
        public const string SemanticTokenLipSynchronization = "LS";

        public const string SemanticTokenFlowIdentification = "FID";

        public const string SemanticTokenForwardErrorCorrection = "FEC";

        public const string SemanticTokenWebRTCMeidaStreams = "WMS";
    }
    
    // extmapキーの定数
    public static class ExtMapKey
    {
        public const int ExtMapValueTransportCC = 3;
    }

    public static class jsep
    {
        public static Dictionary<int, string> extMapURI()
        {
            var s = new Dictionary<int, string>
            {
                {ExtMapExtended.DefExtMapValueTransportCC, "http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01"}
            };

            return s;
        }
        
        // draft-ietf-rtcweb-jsepに一致するAPI
        // webrtcまたは独自のパッケージに移動しますか？

        // NewJSEPSessionDescriptionは、
        // JSEP仕様で必要ないくつかの設定を使用して
        // 新しいSessionDescriptionを作成します。
        //
        //注：v2.4.0以降、セッションIDはJSEP仕様に従って
        //暗号ランダムを使用するように修正されたため、
        //NewJSEPSessionDescriptionは
        //2番目の戻り値としてエラーを返すようになりました。
        public static (SessionDescription, string) NewJSEPSessionDescription(bool identity)
        {
            var (sid, err) = util.newSessionID();

            if (err != null)
            {
                return (null, err);
            }

            var d = new SessionDescription
            {
                Version = new Version
                {
                    version = 0,
                },
                Origin = new Origin
                {
                    Username = "-",
                    SessionID = sid,
                    SessionVersion = Convert.ToUInt64(DateTime.UtcNow),
                    NetworkType = "IN",
                    AddressType = "IP4",
                    UnicastAddress = "0.0.0.0"
                },
                SessionName = new SessionName
                {
                    sessionName = "-"
                },
                TimeDescriptions = new List<TimeDescription>
                {
                    new TimeDescription
                    {
                        Timing = new Timing
                        {
                            StartTime = 0,
                            StopTime = 0,
                        },
                        RepeatTime = null,
                    }
                },
                Attributes = new List<Attribute>
                {
                    
                },
                
            };

            if (identity)
            {
                d.WithPropertyAttribute(AttrKey.AttrKeyIdentity);
            }

            return (d, null);
        }

        // NewJSEPMediaDescriptionは、JSEP仕様で必要ないくつかの設定を使用して新しいMediaNameを作成します。
        public static MediaDescription NewJSEPMediaDescription(string codecType, string[] codecPrefs)
        {
            var md = new MediaDescription
            {
                MediaName = new MediaName
                {
                    Media = codecType,
                    Port = new RangedPort
                    {
                        Value = 9,
                    },
                    Protos = new List<string>
                    {
                        "UDP",
                        "TLS",
                        "RTP",
                        "SAVPF"
                    }
                },
                ConnectionInformation = new ConnectionInformation
                {
                    NetworkType = "IN",
                    AddressType = "IP4",
                    address = new Address
                    {
                        address = "0.0.0.0"
                    }
                }
            };
            
            return md;
        }
    }

    public partial class SessionDescription
    {
        // WithPropertyAttributeは、セッションの説明にプロパティ属性 'a = key'を追加します
        public SessionDescription WithPropertyAttribute(string key)
        {
            Attributes.Add(AttributeExtended.NewPropertyAttribute(key));

            return this;
        }
        
        // WithValueAttributeは、セッションの説明に値属性 'a = key：value'を追加します
        public SessionDescription WithValueAttribute(string key, string value)
        {
            Attributes.Add(AttributeExtended.NewAttribute(key, value));

            return this;
        }
        
        // WithFingerprintは、セッションの説明にフィンガープリントを追加します
        public SessionDescription WithFingerprint(string algorithm, string value)
        {
            return WithValueAttribute("fingerprint", algorithm + " " + value);
        }
        
        // WithMediaは、セッションの説明にメディアの説明を追加します
        public SessionDescription WithMedia(MediaDescription md)
        {
            MediaDescriptions.Add(md);

            return this;
        }
    }
    
    
    public partial class MediaDescription
    {
        // WithPropertyAttributeは、プロパティ属性 'a = key'をmedia descriptionに追加します
        public MediaDescription WithPropertyAttribute(string key)
        {
            Attributes.Add(AttributeExtended.NewPropertyAttribute(key));

            return this;
        }
        
        // WithValueAttributeは、値属性 'a = key：value'をmedia descriptionに追加します
        public MediaDescription WithValueAttribute(string key, string value)
        {
            if (Attributes == null)
            {
                Attributes = new List<Attribute>();
            }
            
            Attributes.Add(AttributeExtended.NewAttribute(key, value));

            return this;
        }
        
        // WithFingerprintは、media descriptionにfingerprintを追加します
        public MediaDescription WithFingerprint(string algorithm, string value)
        {
            return WithValueAttribute("fingerprint", algorithm + " " + value);
        }
        
        // WithICECredentialsは media descriptionにICE credentialsを追加します
        public MediaDescription WithICECredentials(string username, string password)
        {
            return WithValueAttribute("ice-ufrag", username).WithValueAttribute("ice-pwd", password);
        }
        
        // WithCodecは、Media Descriptionにコーデック情報を追加します
        public MediaDescription WithCodec(byte payloadType, string name, uint clockrate, ushort channels, string fmtp)
        {

            MediaName.Formats.Add(Convert.ToString(payloadType));

            var rtpmap = $"{payloadType} {name}/{clockrate}";

            if (channels > 0)
            {
                rtpmap += $"/{channels}";
            }

            WithValueAttribute("rtpmap", rtpmap);

            if (fmtp != "")
            {
                WithValueAttribute("fmtp", $"{payloadType} {fmtp}");
            }

            return this;
        }
        
        // WithMediaSourceは、メディアソース情報をMedia Descriptionに追加します
        public MediaDescription WithMediaDescription(uint ssrc, string cname, string streamLabel, string label)
        {
            return WithValueAttribute("ssrc", $"{ssrc} cname:{cname}")
                .WithValueAttribute("ssrc", $"{ssrc} msid: {streamLabel} {label}")
                .WithValueAttribute("ssrc", $"{ssrc} mslabel {streamLabel}")
                .WithValueAttribute("ssrc", $"{ssrc} label:{ssrc} {label}");
        }
        
        // WithCandidateはメディアの説明にICE候補を追加します
        //非推奨：代わりにWithICECandidateを使用
        public MediaDescription WithCandidate(string value)
        {
            return WithValueAttribute("candidate", value);
        }
        
        // WithExtMapは、Media Descriptionにextmapを追加します
        public MediaDescription WithExtMap(ExtMap e)
        {
            return WithPropertyAttribute(e.Marshal());
        }
        
        // WithTransportCCExtMapは、Media Description にextmapを追加します
        public MediaDescription WithTransportCCExtMap()
        {
            var uri = new Uri(jsep.extMapURI()[ExtMapKey.ExtMapValueTransportCC]);
            
            var e = new ExtMap
            {
                Value = ExtMapKey.ExtMapValueTransportCC,
                URI = uri,
            };

            return WithExtMap(e);
        }
    }
    
    
    
    
}