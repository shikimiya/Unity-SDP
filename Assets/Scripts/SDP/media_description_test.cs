
using UnityEngine;

namespace sdp
{

    public class media_description_test : MonoBehaviour
    {
        public void Start()
        {
            TestWithFingerprint();
        }

        public void TestWithFingerprint()
        {
            var m = new MediaDescription();

            if (m.Attributes != null)
            {
                Debug.LogError("error");
            }

            m = m.WithFingerprint("testalgorithm", "testfingerprint");

            var a = new Attribute[]
            {
                new Attribute
                {
                    Key = "fingerprint",
                    Value = "testalgorithm testfingerprint"
                }
            };

            for (var i = 0; i < a.Length; i++)
            {
                a[i].Equal(m.Attributes[i]);
            }
        }
    }
}