
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

namespace sdp
{

    public class base_lexer_test : MonoBehaviour
    {
        void Start()
        {
            //SingleField();

            //SyntaxError();

            FirstLine();

            SecondLine();

            //LastVoid();
        }

        public void SingleField()
        {
            Dictionary<string, string> testCase = new Dictionary<string, string>
            {
                {"clean", "aaa"},
                {"with extra space", "aaa "},
                {"with linebreak", "aaa \n"},
                {"with linebreak 2", "aaa \r\n"}
            };

            foreach (var pair in testCase)
            {
                var l = new baseLexer
                {
                    value = System.Text.Encoding.UTF8.GetBytes(pair.Value)
                };

                var (field, err) = l.readField();

                if (err != null)
                {
                    Debug.LogError(err);
                }

                if (field != "aaa")
                {
                    Debug.LogError($"{pair.Key}: aaa not parsed, got: '{field}'");
                }
            }
        }

        public void SyntaxError()
        {
            var l = new baseLexer
            {
                value = System.Text.Encoding.UTF8.GetBytes("12NaN"),
            };

            var (_, err) = l.readUint64Field();

            if (err != null)
            {
                Debug.Log($"error message: {err}");
            }
            else
            {
                Debug.LogError("no error");
            }
        }

        public void FirstLine()
        {
            var l = new baseLexer
            { 
                pos = 0,
                value = System.Text.Encoding.UTF8.GetBytes("aaa  123\nf1 f2\nlast")
            };
            
            // first line
            var (field, err) = l.readField();

            if (err != null)
            {
                Debug.LogError(err);
            }

            if (field != "aaa")
            {
                Debug.LogError($"aaa not parsed, got: '{field}'");
            }

            var (value, err2) = l.readUint64Field();

            if (err2 != null)
            {
                Debug.LogError(err2);
            }
            
            if (value != 123)
            {
                Debug.LogError($"aaa not parsed, got: '{field}'");
            }

            err = l.nextLine();

            if (err != null)
            {
                Debug.LogError(err);
            }
        }

        public void SecondLine()
        {
            var l = new baseLexer
            {
                pos = 9,
                value = System.Text.Encoding.UTF8.GetBytes("aaa  123\nf1 f2\nlast")
            };

            var (field, err) = l.readField();

            if (err != null)
            {
                Debug.LogError(err);
            }

            if (field != "f1")
            {
                Debug.LogError($"value not parsed, got: '{field}'");
            }

            (field, err) = l.readField();

            if (err != null)
            {
                Debug.LogError(err);
            }

            if (field != "f2")
            {
                Debug.LogError($"value not parsed, got: '{field}'");
            }

            (field, err) = l.readField();

            if (err != null)
            {
                Debug.LogError(err);
            }

            if (field != "")
            {
                Debug.LogError($"value not parsed, got: '{field}'");
            }

            err = l.nextLine();

            if (err != null)
            {
                Debug.LogError(err);
            }
            
        }

        public void LastVoid()
        {
            var l = new baseLexer
            {
                pos = 15,
                value = System.Text.Encoding.UTF8.GetBytes("aaa  123\nf1 f2\nlast")
            };

            var (field, err) = l.readField();

            if (err != null)
            {
                Debug.LogError(err);
            }

            if (field != "last")
            {
                Debug.LogError($"value not parsed, got: '{field}'");
            }
        }
        
    }
}
