
using System;
using System.Collections.Generic;
using UnityEngine;

namespace sdp
{
    public class TestGetPayloadTypeForVP8TestCases
    {
        public Codec Codec;
        public byte Expected;
    }

    public class TestGetPayloadTypeForVP8TestCases2
    {
        public byte PayloadType;

        public Codec Expected;
    }

    public class util_test : MonoBehaviour
    {
        public void Start()
        {
            TestGetPayloadTypeForVP8();

            //TestGetCodecForPayload();

            //TestNewSessionID();
        }
        
        public SessionDescription getTestSessionDescription()
        {
            var sd = new SessionDescription
            {
                MediaDescriptions = new List<MediaDescription>
                {
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
                                "120", "121", "126", "97"
                            }
                        },
                        Attributes = new List<Attribute>
                        {
                            AttributeExtended.NewAttribute("fmtp:126 profile-level-id=42e01f;level-asymmetry-allowed=1;packetization-mode=1", ""),
                            AttributeExtended.NewAttribute("fmtp:97 profile-level-id=42e01f;level-asymmetry-allowed=1", ""),
                            AttributeExtended.NewAttribute("fmtp:120 max-fs=12288;max-fr=60", ""),
                            AttributeExtended.NewAttribute("fmtp:121 max-fs=12288;max-fr=60", ""),
                            AttributeExtended.NewAttribute("rtpmap:120 VP8/90000", ""),
                            AttributeExtended.NewAttribute("rtpmap:121 VP9/90000", ""),
                            AttributeExtended.NewAttribute("rtpmap:126 H264/90000", ""),
                            AttributeExtended.NewAttribute("rtpmap:97 H264/90000", ""),
                            AttributeExtended.NewAttribute("rtcp-fb:97 ccm fir", ""),
                            AttributeExtended.NewAttribute("rtcp-fb:97 nack", ""),
                            AttributeExtended.NewAttribute("rtcp-fb:97 nack pli", "")
                        }
                    }
                }
            };

            return sd;
        }

        public void TestGetPayloadTypeForVP8()
        {
            var tests = new TestGetPayloadTypeForVP8TestCases[]
            {
                new TestGetPayloadTypeForVP8TestCases
                {
                    Codec = new Codec
                    {
                        Name = "VP8"
                    },
                    Expected = 120,
                },
                new TestGetPayloadTypeForVP8TestCases
                {
                    Codec = new Codec
                    {
                        Name = "VP9",
                    },
                    Expected = 121,
                },
                new TestGetPayloadTypeForVP8TestCases
                {
                    Codec = new Codec
                    {
                        Name = "H264",
                        Fmtp = "profile-level-id=42e01f;level-asymmetry-allowed=1",
                    },
                    Expected = 97,
                },
                new TestGetPayloadTypeForVP8TestCases
                {
                    Codec = new Codec
                    {
                        Name = "H264",
                        Fmtp = "level-asymmetry-allowed=1;profile-level-id=42e01f",
                    },
                    Expected = 97,
                },
                new TestGetPayloadTypeForVP8TestCases
                {
                    Codec = new Codec
                    {
                        Name = "H264",
                        Fmtp = "profile-level-id=42e01f;level-asymmetry-allowed=1;packetization-mode=1",
                    },
                    Expected = 126
                }
            };

            foreach (var test in tests)
            {
                var sd = getTestSessionDescription();

                var (actual, err) = sd.GetPayloadTypeForCodec(test.Codec);

                if (err != null)
                {
                    Debug.LogError($"GetPayloadTypeForCodec(): err={err}");
                }

                if (actual != test.Expected)
                {
                    Debug.LogError($"error:\n\nEXPECTED:\n{test.Expected}\nACTUAL:\n{actual}");
                }
            }

            
        }

        public void TestGetCodecForPayload()
        {
            var tests = new TestGetPayloadTypeForVP8TestCases2[]
            {
                new TestGetPayloadTypeForVP8TestCases2
                {
                    PayloadType = 120,
                    Expected = new Codec
                    {
                        PayloadType = 120,
                        Name = "VP8",
                        ClockRate = 90000,
                        Fmtp = "max-fs=12288;max-fr=60",
                    }
                },
                
                new TestGetPayloadTypeForVP8TestCases2
                {
                    PayloadType = 121,
                    Expected = new Codec
                    {
                        PayloadType = 121,
                        Name = "VP9",
                        ClockRate = 90000,
                        Fmtp = "max-fs=12288;max-fr=60",
                    }
                },
                
                new TestGetPayloadTypeForVP8TestCases2
                {
                    PayloadType = 126,
                    Expected = new Codec
                    {
                        PayloadType = 126,
                        Name = "H264",
                        ClockRate = 90000,
                        Fmtp = "profile-level-id=42e01f;level-asymmetry-allowed=1;packetization-mode=1",
                    }
                },
                
                new TestGetPayloadTypeForVP8TestCases2
                {
                    PayloadType = 97,
                    Expected = new Codec
                    {
                        PayloadType = 97,
                        Name = "H264",
                        ClockRate = 90000,
                        Fmtp = "profile-level-id=42e01f;level-asymmetry-allowed=1",
                        RTCPFeedback = new List<string>
                        {
                            "ccm fir", "nack", "nack pli"
                        }
                    }
                }
            };

            foreach (var test in tests)
            {
                var sd = getTestSessionDescription();

                var (actual, err) = sd.GetCodecForPaykloadType(test.PayloadType);

                if (err != null)
                {
                    Debug.LogError($"GetCodecForPayloadType(): err={err}");
                }

                err = test.Expected.EqualCodec(actual);

                if (err != null)
                {
                    Debug.LogError(err);
                }
            }

            
        }


        public void TestNewSessionID()
        {
            ulong min = 0x7FFFFFFFFFFFFFFF;

            ulong max = 0;

            for (var i = 0; i < 10000; i++)
            {
                var (r, err) = util.newSessionID();

                if (err != null)
                {
                    Debug.LogError(err);
                }

                var t = (Convert.ToUInt64(1) << 63) - 1;

                if (r > t)
                {
                    Debug.LogError($"Session ID must be less than 2**64 - 1, got {r}");
                }

                if (r < min)
                {
                    min = r;
                }

                if (r > max)
                {
                    max = r;
                }
            }

            if (min > 0x1000000000000000)
            {
                Debug.LogError("Value around lower boundary was not generated");
            }

            if (max < 0x7000000000000000)
            {
                Debug.LogError("Value around upper boundary was not generated");
            }
        }
        
        
    }
}
