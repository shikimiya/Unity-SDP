using System;

namespace sdp
{
    public partial class error
    {
        public const string errSDPInvalidSyntax = "sdp: invalid syntax";

        public const string errSDPInvalidNumericValue = "sdp: invalid numeric value";

        public const string errSDPInvalidValue = "sdp: invalid value";

        public const string errSDPInvalidPortValue = "sdp: invalid port value";
    }
    
    // Unmarshalは、セッションの説明メッセージを逆シリアル化し、
    // 構造化されたSessionDescriptionオブジェクト内に格納する主要な関数です。
    //
    //状態遷移表は、rfc4566＃section-5およびJavaScriptセッション確立プロトコルによって
    //規定された仕様に準拠する解析手順の関数（つまり、s1、s2、s3、...）間の計算フローを記述します。 


    public partial class SessionDescription
    {
        public string Unmarshal(byte[] value)
        {
            var l = new lexer();

            l.desc = this;

            l.baseLexer.value = value;

            for (stateFn state = UnmarshalSDP.s1; state != null;)
            {
                string err = null;
                
                (state, err) = state(l);

                if (err != null)
                {
                    return err;
                }
            }


            return null;
        }
        
        
    }

    public static class UnmarshalSDP
    {
        public static (stateFn, string) s1(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                if (key == "v=")
                {
                    return unmarshalProtocolVersion;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s2(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                if (key == "o=")
                {
                    return unmarshalOrigin;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s3(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                if (key == "s=")
                {
                    return unmarshalSessionName;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s4(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "i=":
                        return unmarshalSessionInformation;
                    
                    case "u=":
                        return unmarshalURI;
                    
                    case "e=":
                        return unmarshalEmail;
                    
                    case "p=":
                        return unmarshalPhone;
                    
                    case "c=":
                        return unmarshalSessionConnectionInformation;
                    
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s5(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s6(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "p=":
                        return unmarshalPhone;
                    
                    case "c=":
                        return unmarshalSessionConnectionInformation;
                    
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s7(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "u=": 
                        return unmarshalURI;
                    
                    case "e=":
                        return unmarshalEmail;
                    
                    case "p=":
                        return unmarshalPhone;
                    
                    case "c=":
                        return unmarshalSessionConnectionInformation;
                    
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s8(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "c=":
                        return unmarshalSessionConnectionInformation;
                    
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s9(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "z=":
                        return unmarshalTimeZones;
                    
                    case "k=":
                        return unmarshalSessionEncryptionKey;
                    
                    case "a=":
                        return unmarshalSessionAttribute;
                    
                    case "r=":
                        return unmarshalRepeatTimes;
                    
                    case "t=":
                        return unmarshalTiming;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }
        
        public static (stateFn, string) s10(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "e=":
                        return unmarshalEmail;
                    
                    case "p=":
                        return unmarshalPhone;
                    
                    case "c=":
                        return unmarshalSessionConnectionInformation;
                    
                    case "b=":
                        return unmarshalSessionBandwidth;
                    
                    case "t=":
                        return unmarshalTiming;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s11(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalSessionAttribute;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s12(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalMediaAttribuye;
                    
                    case "k=":
                        return unmarshalMediaEncryptionKey;
                    
                    case "b=":
                        return unmarshalMediaBandwidth;
                    
                    case "c=":
                        return unmarshalMediaConnectionInformation;
                    
                    case "i=":
                        return unmarshalMediaTitle;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s13(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalSessionAttribute;
                    
                    case "k=":
                        return unmarshalSessionEncryptionKey;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s14(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalMediaAttribuye;
                    
                    case "k=":
                        // Non-spec ordering
                        return unmarshalMediaEncryptionKey;
                    
                    case "b=":
                        // Non-spec ordering
                        return unmarshalMediaBandwidth;
                    
                    case "c=":
                        // Non-spec ordering 
                        return unmarshalMediaConnectionInformation;
                    
                    case "i=":
                        // Non-spec ordering
                        return unmarshalMediaDescription;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };
            return l.handleType(fn);
        }

        public static (stateFn, string) s15(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalMediaAttribuye;
                    
                    case "k=":
                        return unmarshalMediaEncryptionKey;
                    
                    case "b=":
                        return unmarshalMediaBandwidth;
                    
                    case "c=":
                        return unmarshalMediaConnectionInformation;
                    
                    case "i=":
                        // Non-spec ordering
                        return unmarshalMediaTitle;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }

        public static (stateFn, string) s16(lexer l)
        {
            keyToState fn = delegate(string key)
            {
                switch (key)
                {
                    case "a=":
                        return unmarshalMediaAttribuye;
                    
                    case "k=":
                        return unmarshalMediaEncryptionKey;
                    
                    case "c=":
                        return unmarshalMediaConnectionInformation;
                    
                    case "b=":
                        return unmarshalMediaBandwidth;
                    
                    case "i=":
                        return unmarshalMediaTitle;
                    
                    case "m=":
                        return unmarshalMediaDescription;
                }

                return null;
            };

            return l.handleType(fn);
        }
        
        public static (stateFn, string) unmarshalProtocolVersion(lexer l)
        {
            var (version, err) = l.baseLexer.readUint64Field();

            if (err != null)
            {
                return (null, err);
            }
            
            // rfcの最新ドラフトから離れているため、この値は0である必要があります。
            // https://tools.ietf.org/html/draft-ietf-rtcweb-jsep-24#section-5.8.1
            if (version != 0)
            {
                return (null, $"{error.errSDPInvalidValue} `{version}`");
            }

            err = l.baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }
            
            return (s2, null);
        }

        public static (stateFn, string) unmarshalOrigin(lexer l)
        {
            string err = null;

            (l.desc.Origin.Username, err) = l.baseLexer.readField();
            if (err != null)
            {
                return (null, err);
            }

            (l.desc.Origin.SessionID, err) = l.baseLexer.readUint64Field();
            if (err != null)
            {
                return (null, err);
            }

            (l.desc.Origin.SessionVersion, err) = l.baseLexer.readUint64Field();
            if (err != null)
            {
                return (null, err);
            }

            (l.desc.Origin.NetworkType, err) = l.baseLexer.readField();
            if (err != null)
            {
                return (null, err);
            }

            // Set according to currently registered with IANA
            // https://tools.ietf.org/html/rfc4566#section-8.2.6
            if (!baseLexerExtended.anyOf(l.desc.Origin.NetworkType, "IN"))
            {
                return (null, $"{error.errSDPInvalidValue} '{l.desc.Origin.NetworkType}'");
            }

            (l.desc.Origin.AddressType, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }
            
            // Set according to currently registered with IANA
            // https://tools.ietf.org/html/rfc4566#section-8.2.7
            if (!baseLexerExtended.anyOf(l.desc.Origin.AddressType, "IP4", "IP6"))
            {
                return (null, $"{error.errSDPInvalidValue} '{l.desc.Origin.AddressType}'");
            }

            (l.desc.Origin.UnicastAddress, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            err = l.baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }

            return (s3, null);
        }

        public static (stateFn, string) unmarshalSessionName(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            l.desc.SessionName = new SessionName
            {
                sessionName = value
            };

            return (s4, null);
        }

        public static (stateFn, string) unmarshalSessionInformation(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var sessionInformation = new Information
            {
                information = value
            };

            l.desc.SessionInformation = sessionInformation;

            return (s7, null);
        }

        public static (stateFn, string) unmarshalURI(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            try
            {
                l.desc.URL = new Uri(value);
            }
            catch (Exception e)
            {
                return (null, e.ToString());
            }

            return (s10, null);
        }

        public static (stateFn, string) unmarshalEmail(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var emailAddress = new EmailAddress
            {
                emailAddress = value,
            };

            l.desc.EmailAddress = emailAddress;

            return (s6, null);
        }

        public static (stateFn, string) unmarshalPhone(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var phoneNumber = new PhoneNumber
            {
                phoneNumber = value
            };

            l.desc.PhoneNumber = phoneNumber;

            return (s8, null);
        }

        public static (stateFn, string) unmarshalSessionConnectionInformation(lexer l)
        {
            string err = "";

            (l.desc.ConnectionInformation, err) = l.unmarshalConnectionInformation();

            if (err != null)
            {
                return (null, err);
            }

            return (s5, null);
        }


        public static (stateFn, string) unmarshalSessionBandwidth(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var (bandwidth, err2) = unmarshalBandwidth(value);

            if (err2 != null)
            {
                return (null, $"{error.errSDPInvalidValue} `b={value}`");
            }
            
            l.desc.BandWidth.Add(bandwidth);

            return (s5, null);
        }

        public static (BandWidth, string) unmarshalBandwidth(string value)
        {
            var parts = value.Split(':');

            if (parts.Length != 2)
            {
                return (null, $"{error.errSDPInvalidValue} `b={parts}`");
            }

            var experimental = parts[0].StartsWith("X-");

            if (experimental)
            {
                parts[0] = parts[0].Replace("X-", "");
            }
            else if (!baseLexerExtended.anyOf(parts[0], "CT", "AS", "TIAS", "RS", "RR"))
            {
                // Set according to currently registered with IANA
                // https://tools.ietf.org/html/rfc4566#section-5.8
                // https://tools.ietf.org/html/rfc3890#section-6.2
                // https://tools.ietf.org/html/rfc3556#section-2
                return (null, $"{error.errSDPInvalidValue} `{parts[0]}`");
            }

            ulong bandwidth = 0;

            try
            {
                bandwidth = Convert.ToUInt64(parts[1]);
            }
            catch(Exception e)
            {
                return (null, e.ToString());
            }

            var b = new BandWidth
            {
                Experimental = experimental,
                Type = parts[0],
                bandWidth = bandwidth,
            };

            return (b, null);
        }

        public static (stateFn, string) unmarshalTiming(lexer l)
        {
            string err = "";
            
            var td = new TimeDescription();

            (td.Timing.StartTime, err) = l.baseLexer.readUint64Field();

            if (err != null)
            {
                return (null, err);
            }

            (td.Timing.StopTime, err) = l.baseLexer.readUint64Field();

            if (err != null)
            {
                return (null, err);
            }

            err = l.baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }
            
            l.desc.TimeDescriptions.Add(td);

            return (s9, null);
        }

        public static (stateFn, string) unmarshalRepeatTimes(lexer l)
        {
            string err = "";
            
            var newRepeatTime = new RepeatTime();

            var latestTimeDesc = l.desc.TimeDescriptions[l.desc.TimeDescriptions.Count - 1];

            string field = null;

            (field, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            (newRepeatTime.Interval, err) = parseTimeUnits(field);

            if (err != null)
            {
                return (null, $"{error.errSDPInvalidValue} `{field}`");
            }

            (field, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            (newRepeatTime.Duration, err) = parseTimeUnits(field);

            if (err != null)
            {
                return (null, $"{error.errSDPInvalidValue} `{field}`");
            }

            for (;;)
            {
                (field, err) = l.baseLexer.readField();

                if (err != null)
                {
                    return (null, err);
                }

                if (field == "")
                {
                    break;
                }

                var (offset, err2) = parseTimeUnits(field);

                if (err2 != null)
                {
                    return (null, $"{error.errSDPInvalidValue} `{field}`");
                }

                newRepeatTime.Offsets.Add(offset);
            }

            err = l.baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }
            
            latestTimeDesc.RepeatTime.Add(newRepeatTime);

            return (s9, null);
        }

        public static (stateFn, string) unmarshalTimeZones(lexer l)
        {
            for (;;)
            {
                string err = "";
                
                var timeZome = new TimeZone();

                (timeZome.AdjustmentTime, err) = l.baseLexer.readUint64Field();

                if (err != null)
                {
                    return (null, err);
                }

                string offset = "";

                (offset, err) = l.baseLexer.readField();

                if (err != null)
                {
                    return (null, err);
                }

                if (offset == "")
                {
                    break;
                }

                (timeZome.Offset, err) = parseTimeUnits(offset);

                if (err != null)
                {
                    return (null, err);
                }

                l.desc.TimeZones.Add(timeZome);
            }

            var err2 = l.baseLexer.nextLine();

            if (err2 != null)
            {
                return (null, err2);
            }

            return (s13, null);
        }


        public static (stateFn, string) unmarshalSessionEncryptionKey(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var encryptionKey = new EncryptionKey
            {
                encryptionKey = value
            };

            l.desc.EncryptionKey = encryptionKey;

            return (s11, null);
        }

        public static (stateFn, string) unmarshalSessionAttribute(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var i = value.IndexOf(':');
            
            Attribute a = new Attribute();

            if (i > 0)
            {
                a = AttributeExtended.NewAttribute(value.Substring(0, i), value.Substring(i + 1));
            } 
            else
            {
                a = AttributeExtended.NewPropertyAttribute(value);
            }
            
            l.desc.Attributes.Add(a);

            return (s11, null);
        }

        public static (stateFn, string) unmarshalMediaDescription(lexer l)
        {
            var newMediaDesc = new MediaDescription();

            var (field, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            if (!baseLexerExtended.anyOf(field, "audio", "video", "text", "application", "message"))
            {
                return (null, $"{error.errSDPInvalidValue} `{field}`");
            }

            newMediaDesc.MediaName.Media = field;

            (field, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            var parts = field.Split('/');

            (newMediaDesc.MediaName.Port.Value, err) = parsePort(parts[0]);

            if (err != null)
            {
                return (null, err);
            }

            if (parts.Length > 1)
            {
                var portRange = 0;

                try
                {
                    portRange = Convert.ToInt32(parts[1]);
                }
                catch(Exception e)
                {
                    return (null, $"{error.errSDPInvalidPortValue} `{parts}`");
                }

                newMediaDesc.MediaName.Port.Range = portRange;
            }
            
            // <proto>
            (field, err) = l.baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }
            
            // Set according to currently registered with IANA
            // https://tools.ietf.org/html/rfc4566#section-5.14
            foreach (var proto in field.Split('/'))
            {
                if (!baseLexerExtended.anyOf(proto, "UDP", "RTP", "AVP", "SAVP", "SAVPF", "TLS", "DTLS", "SCTP", "AVPF"))
                {
                    return (null, $"{error.errSDPInvalidNumericValue} `{proto}`");
                }
                newMediaDesc.MediaName.Protos.Add(proto);
            }
            
            // <fmt>
            for (;;)
            {
                (field, err) = l.baseLexer.readField();

                if (err != null)
                {
                    return (null, err);
                }

                if (field == "")
                {
                    break;
                }
                
                newMediaDesc.MediaName.Formats.Add(field);
            }

            err = l.baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }
            
            l.desc.MediaDescriptions.Add(newMediaDesc);

            return (s12, null);
        }

        public static (stateFn, string) unmarshalMediaTitle(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var lastMediaDesc = l.desc.MediaDescriptions[l.desc.MediaDescriptions.Count - 1];

            var mediaTitle = new Information
            {
                information = value,
            };

            lastMediaDesc.MediaTitle = mediaTitle;

            return (s16, null);
        }

        public static (stateFn, string) unmarshalMediaConnectionInformation(lexer l)
        {
            string err = null;

            var lastMediaDesc = l.desc.MediaDescriptions[l.desc.MediaDescriptions.Count - 1];

            (lastMediaDesc.ConnectionInformation, err) = l.unmarshalConnectionInformation();

            if (err != null)
            {
                return (null, err);
            }

            return (s15, null);
        }

        public static (stateFn, string) unmarshalMediaBandwidth(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var latestMediaDesc = l.desc.MediaDescriptions[l.desc.MediaDescriptions.Count - 1];

            var bandwidth = new BandWidth(); 

            (bandwidth, err) = unmarshalBandwidth(value);

            if (err != null)
            {
                return (null, $"{error.errSDPInvalidSyntax} `b={value}`");
            }
            
            latestMediaDesc.Bandwidth.Add(bandwidth);

            return (s15, null);
        }

        public static (stateFn, string) unmarshalMediaEncryptionKey(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var lastMediaDesc = l.desc.MediaDescriptions[l.desc.MediaDescriptions.Count - 1];

            var encryptionKey = new EncryptionKey
            {
                encryptionKey = value,
            };

            lastMediaDesc.EncryptionKey = encryptionKey;

            return (s14, null);
        }

        public static (stateFn, string) unmarshalMediaAttribuye(lexer l)
        {
            var (value, err) = l.baseLexer.readLine();

            if (err != null)
            {
                return (null, err);
            }

            var i = value.IndexOf(':');

            var a = new Attribute();

            if (i > 0)
            {
                a = AttributeExtended.NewAttribute(value.Substring(0, i), value.Substring(i + 1));
            }
            else
            {
                a = AttributeExtended.NewPropertyAttribute(value);
            }

            var latestMediaDesc = l.desc.MediaDescriptions[l.desc.MediaDescriptions.Count - 1];
            
            latestMediaDesc.Attributes.Add(a);

            return (s14, null);
        }

        public static (long, string) parseTimeUnits(string value)
        {
            long num = 0;

            string err = null;
            
            var k = timeShorthand(Convert.ToByte(value[value.Length - 1]));

            if (k > 0)
            {
                try
                {
                    num = Convert.ToInt64(value.Substring(0, value.Length - 1));
                }
                catch (Exception e)
                {
                    err = e.ToString();
                }
            }
            else
            {
                k = 1;

                try
                {
                    num = Convert.ToInt64(value);
                }
                catch(Exception e)
                {
                    err = e.ToString();
                }
            }

            if (err != null)
            {
                return (0, $"{error.errSDPInvalidPortValue} `{value}`");
            }

            return (num * k, null);
        }

        public static long timeShorthand(byte b)
        {
            var c = Convert.ToChar(b);
            
            switch (c)
            {
                case 'd':
                    return 86400;
                
                case 'h':
                    return 3600;
                
                case 'm':
                    return 60;
                
                case 's':
                    return 1;
                
                default:
                    return 0;
                
            }
        }

        public static (int, string) parsePort(string value)
        {
            var port = 0;

            try
            { 
                port = Convert.ToInt32(value);
            }
            catch(Exception err)
            {
                return (0, $"{error.errSDPInvalidPortValue} `{port}`");
            }

            if (port < 0 || port > 65536)
            {
                return (0, $"{error.errSDPInvalidPortValue} -- out of range `{port}`");
            }

            return (port, null);
        }
    }

    public partial class lexer
    {
        public (ConnectionInformation, string) unmarshalConnectionInformation()
        {
            string err = "";
            
            var c = new ConnectionInformation();

            (c.NetworkType, err) = baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            if (!baseLexerExtended.anyOf(c.NetworkType, "IN"))
            {
                return (null, $"{error.errSDPInvalidValue} '{c.NetworkType}'");
            }

            (c.AddressType, err) = baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }
            
            if (!baseLexerExtended.anyOf(c.AddressType, "IP4", "IP6"))
            {
                return (null, $"{error.errSDPInvalidValue} '{c.AddressType}'");
            }

            string address = "";

            (address, err) = baseLexer.readField();

            if (err != null)
            {
                return (null, err);
            }

            if (address != "")
            {
                c.address = new Address();

                c.address.address = address;
            }

            err = baseLexer.nextLine();

            if (err != null)
            {
                return (null, err);
            }

            return (c, null);
        }
        
        
    }
}
