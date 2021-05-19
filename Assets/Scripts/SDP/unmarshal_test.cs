

using UnityEngine;

namespace sdp
{
    public static class UnmarshalText
    {
        public static string BaseSDP = "v=0\r\n" +
                                      "o=jdoe 2890844526 2890842807 IN IP4 10.47.16.5\r\n" +
                                      "s=SDP Seminar\r\n";

        public static string SessionInformationSDP = BaseSDP +
                                                    "i=A Seminar on the session description protocol\r\n" + 
                                                    "t=3034423619 3042462419\r\n";
        
        // https://tools.ietf.org/html/rfc4566#section-5
        // Parsers SHOULD be tolerant and also accept records terminated
        // with a single newline character.
        public static string SessionInformationSDPLFOnly = "v=0\n" +
                                                          "o=jdoe 2890844526 2890842807 IN IP4 10.47.16.5\n" +
                                                          "s=SDP Seminar\n" +
                                                          "i=A Seminar on the session description protocol\n" +
                                                          "t=3034423619 3042462419\n";
        
        // Other SDP parsers (e.g. one in VLC media player) allow
        // empty lines.
        public static string SessionInformationSDPExtraCRLF = "v=0\r\n" +
                                                             "o=jdoe 2890844526 2890842807 IN IP4 10.47.16.5\r\n" +
                                                             "\r\n" +
                                                             "s=SDP Seminar\r\n" +
                                                             "\r\n" +
                                                             "i=A Seminar on the session description protocol\r\n" +
                                                             "\r\n" +
                                                             "t=3034423619 3042462419\r\n" +
                                                             "\r\n";

        public static string URISDP = BaseSDP +
                                     "u=http://www.example.com/seminars/sdp.pdf\r\n" +
                                     "t=3034423619 3042462419\r\n";

        public static string EmailAddressSDP = BaseSDP +
                                              "e=j.doe@example.com (Jane Doe)\r\n" +
                                              "t=3034423619 3042462419\r\n";

        public static string PhoneNumberSDP = BaseSDP +
                                             "p=+1 617 555-6011\r\n" +
                                             "t=3034423619 3042462419\r\n";

        public static string SessionConnectionInformationSDP = BaseSDP +
                                                              "c=IN IP4 224.2.17.12/127\r\n" +
                                                              "t=3034423619 3042462419\r\n";

        public static string SessionBandwidthSDP = BaseSDP +
                                                  "b=X-YZ:128\r\n" +
                                                  "b=AS:12345\r\n" +
                                                  "t=3034423619 3042462419\r\n";

        public static string TimingSDP = BaseSDP +
                                        "t=2873397496 2873404696\r\n";
        
        // Short hand time notation is converted into NTP timestamp format in
        // seconds. Because of that unittest comparisons will fail as the same time
        // will be expressed in different units.

        public static string RepeatTimeSDP = TimingSDP +
                                            "r=604800 3600 0 90000\r\n" +
                                            "r=3d 2h 0 21h\r\n";

        public static string RepeatTimesSDPExpected = TimingSDP +
                                                     "r=604800 3600 0 90000\r\n" +
                                                     "r=259200 7200 0 75600\r\n";

        public static string RepeatTimesSDPExtraCRLF = RepeatTimesSDPExpected +
                                                      "\r\n";
        // The expected value looks a bit different for the same reason as mentioned
        // above regarding RepeatTimes.
        public static string TimeZonesSDP = TimingSDP +
                                           "r=2882844526 -1h 2898848070 0\r\n";

        public static string TimeZonesSDPExpected = TimingSDP +
                                                   "r=2882844526 -3600 2898848070 0\r\n";

        public static string TimeZonesSDP2 = TimingSDP +
                                              "z=2882844526 -3600 2898848070 0\r\n";

        public static string TimeZonesSDP2ExtraCRLF = TimeZonesSDP2 +
                                                     "\r\n";

        public static string SessionEncryptionKeySDP = TimingSDP +
                                                      "k=prompt\r\n";

        public static string SessionEncryptionKeySDPExtraCRLF = SessionEncryptionKeySDP + "\r\n";

        public static string SessionAttributesSDP = TimingSDP +
                                                   "a=rtpmap:96 opus/48000\r\n";

        public static string MediaNameSDP = TimingSDP +
                                           "m=video 51372 RTP/AVP 99\r\n" +
                                           "m=audio 54400 RTP/SAVPF 0 96\r\n";

        public static string MediaNameSDPExtraCRLF = MediaNameSDP +
                                                    "\r\n";

        public static string MediaTitleSDP = MediaNameSDP +
                                            "i=Vivamus a posuere nisl\r\n";

        public static string MediaConnectionInformationSDP = MediaNameSDP +
                                                            "c=IN IP4 203.0.113.1\r\n";

        public static string MediaConnectionInformationSDPExtraCRLF = MediaConnectionInformationSDP +
                                                                     "\r\n";

        public static string MediaDescriptionOutOfOrderSDP = MediaNameSDP +
                                                            "a=rtpmap:99 h263-1998/90000\r\n" +
                                                            "a=candidate:0 1 UDP 2113667327 203.0.113.1 54400 typ host\r\n" +
                                                            "c=IN IP4 203.0.113.1\r\n" +
                                                            "i=Vivamus a posuere nisl\r\n";

        public static string MediaDescriptionOutOfOrderSDPActual = MediaNameSDP +
                                                                  "i=Vivamus a posuere nisl\r\n" +
                                                                  "c=IN IP4 203.0.113.1\r\n" +
                                                                  "a=rtpmap:99 h263-1998/90000\r\n" +
                                                                  "a=candidate:0 1 UDP 2113667327 203.0.113.1 54400 typ host\r\n";

        public static string MediaBandwidthSDP = MediaNameSDP +
                                                "b=X-YZ:128\r\n" +
                                                "b=AS:12345\r\n" +
                                                "b=TIAS:12345\r\n" +
                                                "b=RS:12345\r\n" +
                                                "b=RR:12345\r\n";

        public static string MediaEncryptionKeySDP = MediaNameSDP +
                                                    "k=prompt\r\n";

        public static string MediaEncryptionKeySDPExtraCRLF = MediaEncryptionKeySDP +
                                                             "\r\n";

        public static string MediaAttributesSDP = MediaNameSDP +
                                                 "a=rtpmap:99 h263-1998/90000\r\n" +
                                                 "a=candidate:0 1 UDP 2113667327 203.0.113.1 54400 typ host\r\n" +
                                                 "a=rtcp-fb:97 ccm fir\r\n" +
                                                 "a=rtcp-fb:97 nack\r\n" +
                                                 "a=rtcp-fb:97 nack pli\r\n";

        public static string CanonicalUnmarshalSDP = "v=0\r\n" +
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


    public class UnmarshalSDPTestCase1
    {
        public string Name;

        public string SDP;

        public string Actual;

        public UnmarshalSDPTestCase1()
        {
            Name = "";

            SDP = "";

            Actual = "";
        }
    }

    public class unmarshal_test : MonoBehaviour
    {
        void Start()
        {
            TestRoundTrip();

            TestUnmarshalRepeatTime();

            TestUnmarshalTimeZones();

            TestUnmarshalNonNilAddress();
        }

        public void TestRoundTrip()
        {
            var tests = new UnmarshalSDPTestCase1[]
            {
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionInformationSDPLFOnly",
                    SDP = UnmarshalText.SessionInformationSDPLFOnly,
                    Actual = UnmarshalText.SessionInformationSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionInformationSDPExtraCRLF",
                    SDP = UnmarshalText.SessionInformationSDPExtraCRLF,
                    Actual = UnmarshalText.SessionInformationSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionInformation",
                    SDP = UnmarshalText.SessionInformationSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "URI",
                    SDP = UnmarshalText.URISDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "EmailAddress",
                    SDP = UnmarshalText.EmailAddressSDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "PhoneNumber",
                    SDP = UnmarshalText.PhoneNumberSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "RepeatTimesSDPExtraCRLF",
                    SDP = UnmarshalText.RepeatTimesSDPExtraCRLF,
                    Actual = UnmarshalText.RepeatTimesSDPExpected,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionConnectionInformation",
                    SDP = UnmarshalText.SessionConnectionInformationSDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionBandwidth",
                    SDP = UnmarshalText.SessionBandwidthSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionEncryptionKey",
                    SDP = UnmarshalText.SessionEncryptionKeySDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionEncryptionKeyExtraCRLF",
                    SDP = UnmarshalText.SessionEncryptionKeySDPExtraCRLF,
                    Actual = UnmarshalText.SessionEncryptionKeySDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "SessionAttributes",
                    SDP = UnmarshalText.SessionAttributesSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "TimeZonesSDP2ExtraCRLF",
                    SDP = UnmarshalText.TimeZonesSDP2ExtraCRLF,
                    Actual = UnmarshalText.TimeZonesSDP2
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaName",
                    SDP = UnmarshalText.MediaNameSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaNameExtraCRLF",
                    SDP = UnmarshalText.MediaNameSDPExtraCRLF,
                    Actual = UnmarshalText.MediaNameSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaTitle",
                    SDP = UnmarshalText.MediaTitleSDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaConnectionInformation",
                    SDP = UnmarshalText.MediaConnectionInformationSDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaConnectionInformationExtraCRLF",
                    SDP = UnmarshalText.MediaConnectionInformationSDPExtraCRLF,
                    Actual = UnmarshalText.MediaConnectionInformationSDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaDescriptionOutOfOrder",
                    SDP = UnmarshalText.MediaDescriptionOutOfOrderSDP,
                    Actual = UnmarshalText.MediaDescriptionOutOfOrderSDPActual,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaBandwidth",
                    SDP = UnmarshalText.MediaBandwidthSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaEncryptionKey",
                    SDP = UnmarshalText.MediaEncryptionKeySDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaEncryptionKeyExtraCRLF",
                    SDP = UnmarshalText.MediaEncryptionKeySDPExtraCRLF,
                    Actual = UnmarshalText.MediaEncryptionKeySDP
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "MediaAttributes",
                    SDP = UnmarshalText.MediaAttributesSDP,
                },
                
                new UnmarshalSDPTestCase1
                {
                    Name = "CanonicalUnmarshal",
                    SDP = UnmarshalText.CanonicalUnmarshalSDP
                }
            };

            foreach (var test in tests)
            {
                var sd = new SessionDescription();

                var buf = System.Text.Encoding.UTF8.GetBytes(test.SDP);

                var err = sd.Unmarshal(buf);

                if (err != null)
                {
                    Debug.LogError(err);
                }

                var (actual, err2) = sd.Marshal();

                if (err2 != null)
                {
                    Debug.LogError(err2);
                }

                var want = test.SDP;

                if (test.Actual != "")
                {
                    want = test.Actual;
                }

                var got = System.Text.Encoding.UTF8.GetString(actual.ToArray());
                
                if (want != got)
                {
                    Debug.LogError($"Marshal:\ngot={got}\nwant={want}");
                }
            }
        }

        public void TestUnmarshalRepeatTime()
        {
            var sd = new SessionDescription();

            var buf = System.Text.Encoding.UTF8.GetBytes(UnmarshalText.RepeatTimeSDP);

            var err = sd.Unmarshal(buf);

            if (err != null)
            {
                Debug.LogError(err);
            }

            var (actual, err2) = sd.Marshal();

            if (err2 != null)
            {
                Debug.LogError(err2);
            }

            var s = System.Text.Encoding.UTF8.GetString(actual.ToArray());

            if (s != UnmarshalText.RepeatTimesSDPExpected)
            {
                Debug.LogError($"error:\n\nEXPECTED:\n{UnmarshalText.RepeatTimesSDPExpected}\nACTUAL:\n{s}");
            }
        }

        public void TestUnmarshalTimeZones()
        {
            var sd = new SessionDescription();

            var buf = System.Text.Encoding.UTF8.GetBytes(UnmarshalText.TimeZonesSDP);

            var err = sd.Unmarshal(buf);

            if (err != null)
            {
                Debug.LogError(err);
            }

            var (actual, err2) = sd.Marshal();

            if (err2 != null)
            {
                Debug.LogError($"Marshal(): ");
            }

            var s = System.Text.Encoding.UTF8.GetString(actual.ToArray());

            if (s != UnmarshalText.TimeZonesSDPExpected)
            {
                Debug.LogError($"error:\n\nEXPECTED:\n{UnmarshalText.TimeZonesSDPExpected}\n");
            }
        }

        public void TestUnmarshalNonNilAddress()
        {
            var input = "v=0\r\no=0 0 0 IN IP4 0\r\ns=0\r\nc=IN IP4\r\nt=0 0\r\n";

            var sd = new SessionDescription();

            var s = System.Text.Encoding.UTF8.GetBytes(input);

            var err = sd.Unmarshal(s);

            if (err != null)
            {
                Debug.Log(err);
            }

            var (output, err2) = sd.Marshal();

            if (err2 != null)
            {
                Debug.LogError(err2);
            }

            var s2 = System.Text.Encoding.UTF8.GetString(output.ToArray());

            if (input != s2)
            {
                Debug.LogError($"round trip = {s2} want {input}");
            }
        }
    }
}