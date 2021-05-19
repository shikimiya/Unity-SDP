
using UnityEngine;
using UnityEngine.Assertions;

namespace sdp
{
    public class DirectionTestCase1
    {
        public string value;
        public Direction expected;
    }


    public class DirectionTestCase2
    {
        public Direction actual;

        public string expected;
    }

    public class direction_test : MonoBehaviour
    {
        public void Start()
        {
            TestNewDirection();

            TestDirection_String();
        }

        public void TestNewDirection()
        {
            var passingtest = new DirectionTestCase1[]
            {
                new DirectionTestCase1
                {
                    value = "sendrecv",
                    expected = Direction.DirectionSendRecv
                },
                
                new DirectionTestCase1
                {
                    value = "sendonly",
                    expected = Direction.DirectionSendOnly
                },
                
                new DirectionTestCase1
                {
                    value = "recvonly",
                    expected = Direction.DirectionRecvOnly,
                },
                
                new DirectionTestCase1
                {
                    value = "inactive",
                    expected = Direction.DircetionInactive
                }
            };


            var failingtests = new string[]
            {
                "",
                "notadirection"
            };

            for (var i = 0; i < passingtest.Length; i++)
            {
                var (dir, err) = DirectionExtended.NewDirection(passingtest[i].value);
                
                Assert.IsNull(err, err);
                
                Assert.AreEqual(passingtest[i].expected, dir, $"{i}: {passingtest[i]}");
            }
        }

        public void TestDirection_String()
        {
            var tests = new DirectionTestCase2[]
            {
                new DirectionTestCase2
                {
                    actual = Direction.unknown,
                    expected = DirectionExtended.directionUnknownStr,
                },
                
                new DirectionTestCase2
                {
                    actual = Direction.DirectionSendRecv,
                    expected = "sendrecv"
                },
                
                new DirectionTestCase2
                {
                    actual = Direction.DirectionSendOnly,
                    expected = "sendonly",
                },
                
                new DirectionTestCase2
                {
                    actual = Direction.DirectionRecvOnly,
                    expected = "recvonly"
                },
                
                new DirectionTestCase2
                {
                    actual = Direction.DircetionInactive,
                    expected = "inactive"
                }
            };

            for (var i = 0; i < tests.Length; i++)
            {
                Assert.AreEqual(tests[i].expected, DirectionExtended.String(tests[i].actual), $"{i}: {tests[i]}");
            }
        }
    }
}