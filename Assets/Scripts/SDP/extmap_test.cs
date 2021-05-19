
using System;
using UnityEngine;

namespace sdp
{

    public class ExtMapTestCase1
    {
        public string parameter;

        public string expected;
    }

    public class extmap_test : MonoBehaviour
    {
        public void Start()
        {
            TestExtmap();

            TestTransportCCExtMap();
        }

        public void TestExtmap()
        {
            var passingtests = new ExtMapTestCase1[]
            {
                new ExtMapTestCase1
                {
                    parameter = ExampleAttr.exampleAttrExtmap1,
                    expected = ExampleAttr.exampleAttrExtmap1Line,
                },

                new ExtMapTestCase1
                {
                    parameter = ExampleAttr.exampleAttrExtmap2,
                    expected = ExampleAttr.exampleAttrExtmap2Line,
                },
            };

            var failingstests = new ExtMapTestCase1[]
            {
                new ExtMapTestCase1
                {
                    parameter = ExampleAttr.failingAttrExtmap1,
                    expected = ExampleAttr.failingAttrExtmap1Line,
                },
                
                new ExtMapTestCase1
                {
                    parameter = ExampleAttr.failingAttrExtmap2,
                    expected = ExampleAttr.failingAttrExtmap2Line
                }
            };

            foreach (var i in passingtests)
            {
                var actual = new ExtMap();

                var err = actual.Unmarshal(i.parameter);

                if (err != null)
                {
                    Debug.LogError(err);
                }

                if (i.expected != actual.Marshal())
                {
                    Debug.LogError($"{i.parameter}: {i.expected}");
                }
            }

            foreach (var i in failingstests)
            {
                var actual = new ExtMap { };

                var err = actual.Unmarshal(i.parameter);

                if (err == null)
                {
                    Debug.LogError("error");
                }
            }
        }

        public void TestTransportCCExtMap()
        {
            // a=extmap:<value>["/"<direction>] <URI> <extensionattributes>
            // a=extmap:3 http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01
            var uri = new Uri("http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01");

            var e = new ExtMap
            {
                Value = 3,
                URI = uri,
            };

            if (e.Marshal() == "3 http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01")
            {
                Debug.LogError("TestTransportCC failed");
            }
        }
    }
}