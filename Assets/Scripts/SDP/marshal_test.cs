using System;
using System.Collections.Generic;
using UnityEngine;

namespace sdp
{
    public static class CanonicalMarshalSDP
    {
        public const string canonicalMarshalSDP = "v=0\r\n" +
                                                  "o=jdoe 2890844526 2890842807 IN IP4 10.47.16.5\r\n" +
                                                  "s=SDP Seminar\r\n" +
                                                  "i=A Seminar on the session description protocol\r\n" +
                                                  "u=http://www.example.com/seminars/sdp.pdf\r\n" +
                                                  "e=j.doe@example.com (Jane Doe)\r\n" +
                                                  "p=+1 617 555-6011\r\n" +
                                                  "c=IN IP4 224.2.17.12/127\r\n" +
                                                  "b=X-YZ:128\r\n" +
                                                  "b=AS:12345\r\n" +
                                                  "t=2873397496 2873404696\r\n" +
                                                  "t=3034423619 3042462419\r\n" +
                                                  "r=604800 3600 0 90000\r\n" +
                                                  "z=2882844526 -3600 2898848070 0\r\n" +
                                                  "k=prompt\r\n" +
                                                  "a=candidate:0 1 UDP 2113667327 203.0.113.1 54400 typ host\r\n" +
                                                  "a=recvonly\r\n" +
                                                  "m=audio 49170 RTP/AVP 0\r\n" +
                                                  "i=Vivamus a posuere nisl\r\n" +
                                                  "c=IN IP4 203.0.113.1\r\n" +
                                                  "b=X-YZ:128\r\n" +
                                                  "k=prompt\r\n" +
                                                  "a=sendrecv\r\n" +
                                                  "m=video 51372 RTP/AVP 99\r\n" +
                                                  "a=rtpmap:99 h263-1998/90000\r\n";
    }

    public class marshal_test : MonoBehaviour
    {
        public void Start()
        {
            TestMarshalCanonical();
        }

        public void TestMarshalCanonical()
        {
            var sd = new SessionDescription
            {
                Version = new Version
                {
                    version = 0,
                },
                Origin = new Origin
                {
                    Username = "jdoe",
                    SessionID = 2890844526,
                    SessionVersion = 2890842807,
                    NetworkType = "IN",
                    AddressType = "IP4",
                    UnicastAddress = "10.47.16.5"
                },
                SessionName = new SessionName
                {
                    sessionName = "SDP Seminar"
                },
                SessionInformation = new Information
                {
                    information = "A Seminar on the session description protocol"
                },
                URL = new Uri("http://www.example.com/seminars/sdp.pdf"),
                EmailAddress = new EmailAddress
                {
                    emailAddress = "j.doe@example.com (Jane Doe)"
                },
                PhoneNumber = new PhoneNumber
                {
                    phoneNumber = "+1 617 555-6011"
                },
                ConnectionInformation = new ConnectionInformation
                {
                    NetworkType = "IN",
                    AddressType = "IP4",
                    address = new Address
                    {
                        address = "224.2.117.12",
                        TTL = 127,
                    }
                },
                BandWidth = new List<BandWidth>
                {
                    new BandWidth
                    {
                        Experimental = true,
                        Type = "YZ",
                        bandWidth = 128,
                    },
                    
                    new BandWidth
                    {
                        Type = "AS",
                        bandWidth = 12345,
                    }
                },
                TimeDescriptions = new List<TimeDescription>
                {
                    new TimeDescription
                    {
                        Timing = new Timing
                        {
                            StartTime = 2873397496,
                            StopTime = 2873404696,
                        },
                        RepeatTime = null,
                    },
                    
                    new TimeDescription
                    {
                        Timing = new Timing
                        {
                            StartTime = 3034423619,
                            StopTime = 3042462419,
                        },
                        RepeatTime = new List<RepeatTime>
                        {
                            new RepeatTime
                            {
                                Interval = 604800,
                                Duration = 3600,
                                Offsets = new List<long>
                                {
                                    0, 90000
                                }
                            }
                        }
                    },
                },
                TimeZones = new List<TimeZone>
                {
                    new TimeZone
                    {
                        AdjustmentTime = 2882844526,
                        Offset = -3600,
                    },
                    
                    new TimeZone
                    {
                        AdjustmentTime = 2898848070,
                        Offset =0,
                    }
                },
                EncryptionKey = new EncryptionKey
                {
                    encryptionKey = "prompt"
                },
                Attributes = new List<Attribute>
                {
                    AttributeExtended.NewAttribute("candidate:0 1 UDP 2113667327 203.0.113.1 54400 typ host", ""),
                    AttributeExtended.NewAttribute("recvonly", "")
                },
                MediaDescriptions = new List<MediaDescription>
                {
                    new MediaDescription
                    {
                        MediaName = new MediaName
                        {
                            Media = "audio",
                            Port = new RangedPort
                            {
                                Value = 49170,
                            },
                            Protos = new List<string>
                            {
                                "RTP",
                                "AVP"
                            },
                            Formats = new List<string>
                            {
                                "0"
                            }
                        },
                        MediaTitle = new Information
                        {
                            information = "Vivamus a posuere nisl"
                        },
                        ConnectionInformation = new ConnectionInformation
                        {
                            NetworkType = "IN",
                            AddressType = "IP4",
                            address = new Address
                            {
                                address = "203.0.113.1"
                            }
                        },
                        Bandwidth = new List<BandWidth>
                        {
                            new BandWidth
                            {
                                Experimental = true,
                                Type = "YZ",
                                bandWidth = 128,
                            }
                        },
                        EncryptionKey = new EncryptionKey
                        {
                            encryptionKey = "prompt"
                        },
                        Attributes = new List<Attribute>
                        {
                            AttributeExtended.NewAttribute("sendrecv", "")
                        }
                    },
                    new MediaDescription
                    {
                        MediaName = new MediaName
                        {
                            Media = "video",
                            Port = new RangedPort
                            {
                                Value = 51372,
                            },
                            Protos = new List<string>
                            {
                                "RTP",
                                "AVP"
                            },
                            Formats = new List<string>
                            {
                                "99"
                            }
                            
                        },
                        Attributes = new List<Attribute>
                        {
                            AttributeExtended.NewAttribute("rtpmap:99 h263-1998/90000", "")
                        }
                    }
                },
            };

            var (actual, err) = sd.Marshal();

            if (err != null)
            {
                Debug.LogError($"Marshal(): {err}");
            }
        }
    }
}