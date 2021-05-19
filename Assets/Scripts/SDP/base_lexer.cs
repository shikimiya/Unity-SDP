using System;
using System.Linq;

namespace sdp
{
    public partial class error
    {
        public const string errDocumentStart = "already on document start";
    }

    public class syntaxError
    {
        public string s;

        public int i;

        public syntaxError()
        {
            s = null;

            i = 0;
        }

        public string Error()
        {
            if (i < 0)
            {
                i = 0;
            }

            var head = s.Substring(0, i);

            var middle = s.Substring(i, i + 1);

            var tail = s.Substring(i + 1);

            return $"{head} --> {middle} <-- {tail}";
        }
    }

    public class baseLexer
    {
        public byte[] value;

        public int pos;

        public baseLexer()
        {
            value = new byte[] {};

            pos = 0;
        }

        public string syntaxError()
        {
            var s = new syntaxError
            {
                s = System.Text.Encoding.UTF8.GetString(value),
                i = pos - 1,
            };

            return s.Error();
        }


        public string unreadBytes()
        {
            if (pos <= 0)
            {
                return error.errDocumentStart;
            }

            pos--;

            return null;
        }

        public (byte, string) readByte()
        {
            if (pos >= value.Length)
            {
                return (0, "io eof");
            }
            
            var ch = value[pos];

            pos++;

            return (ch, null);
        }

        public string nextLine()
        {
            for(;;)
            {
                var (ch, err) = readByte();

                if (err == "io eof")
                {
                    return null;
                } 
                else if (err != null)
                {
                    return err;
                }

                if (!baseLexerExtended.isNewline(ch))
                {
                    return unreadBytes();
                }
            }
        }


        public string readWhitespace()
        {
            for (;;)
            {
                var (ch, err) = readByte();

                if (err == "io eof")
                {
                    return null;
                }
                else if (err != null)
                {
                    return err;
                }

                if (!baseLexerExtended.isWhitespace(ch))
                {
                    return unreadBytes();
                }
            }
        }

        public (ulong, string) readUint64Field()
        {
            ulong i = 0;

            for (;;)
            {
                var (ch, err) = readByte();

                if (err == "io eof" && i > 0)
                {
                    break;
                }
                else if (err != null)
                {
                    return (i, err);
                }

                if (baseLexerExtended.isNewline(ch))
                {
                    err = unreadBytes();

                    if (err != null)
                    {
                        return (i, err);
                    }

                    break;
                }

                if (baseLexerExtended.isWhitespace(ch))
                {
                    err = readWhitespace();

                    if (err != null)
                    {
                        return (i, err);
                    }

                    break;
                }

                var r = Convert.ToChar(ch);

                switch (r)
                {
                    case '0':
                        i *= 10;
                        break;
                    
                    case '1':
                        i = i * 10 + 1;
                        break;
                    
                    case '2':
                        i = i * 10 + 2;
                        break;
                    
                    case '3':
                        i = i * 10 + 3;
                        break;
                    
                    case '4':
                        i = i * 10 + 4;
                        break;
                    
                    case '5':
                        i = i * 10 + 5;
                        break;
                    
                    case '6':
                        i = i * 10 + 6;
                        break;
                    
                    case '7':
                        i = i * 10 + 7;
                        break;
                    
                    case '8':
                        i = i * 10 + 8;
                        break;
                    
                    case '9':
                        i = i * 10 + 9;
                        break;
                    
                    default:
                        return (i, syntaxError());
                        
                }
            }

            return (i, null);
        }
        
        // Returns next field on this line or empty string if no more fields on line
        public (string, string) readField()
        {
            var start = pos;

            var stop = 0;

            for (;;)
            {
                stop = pos;

                var (ch, err) = readByte();

                if (err == "io eof" && stop > start)
                {
                    break;
                } 
                else if (err != null)
                {
                    return ("", err);
                }

                if (baseLexerExtended.isNewline(ch))
                {
                    err = unreadBytes();

                    if (err != null)
                    {
                        return ("", err);
                    }

                    break;
                }

                if (baseLexerExtended.isWhitespace(ch))
                {
                    err = readWhitespace();

                    if (err != null)
                    {
                        return ("", err);
                    }

                    break;
                }
            }

            var buf = value.Skip(start).Take(stop - start).ToArray();

            var s = System.Text.Encoding.UTF8.GetString(buf);

            return (s, null);
        }



        // Returns symbols until line end
        public (string, string) readLine()
        {
            var start = pos;

            var trim = 1;

            for (;;)
            {
                var (ch, err) = readByte();

                if (err != null)
                {
                    return ("", err);
                }

                if (ch == '\r')
                {
                    trim++;
                }

                if (ch == '\n')
                {
                    var buf = value.Skip(start).Take(pos - trim - start).ToArray();

                    var s = System.Text.Encoding.UTF8.GetString(buf);

                    return (s, null);
                }
            }
        }

        public (string, string) readString(byte until)
        {
            var start = pos;

            for (;;)
            {
                var (ch, err) = readByte();

                if (err != null)
                {
                    return ("", err);
                }

                if (ch == until)
                {
                    var buf = value.Skip(start).Take(pos - start).ToArray();

                    var s = System.Text.Encoding.UTF8.GetString(buf);

                    return (s, null);
                }
            }
        }


        public (string, string) readType()
        {
            for (;;)
            {
                var (b, err) = readByte();

                if (err != null)
                {
                    return ("", err);
                }

                if (baseLexerExtended.isNewline(b))
                {
                    continue;
                }

                err = unreadBytes();

                if (err != null)
                {
                    return ("", err);
                }

                var (key, err2) = readString(Convert.ToByte('='));

                if (err2 != null)
                {
                    return (key, err2);
                }
                
                if (key.Length == 2)
                {
                    return (key, null);
                }

                return (key, syntaxError());
            }
        }
    }



    public static class baseLexerExtended
    {
        public static bool isNewline(byte ch)
        {
            return ch == '\n' || ch == '\r';
        }

        public static bool isWhitespace(byte ch)
        {
            return ch == ' ' || ch == '\t';
        }

        public static bool anyOf(string element, params string[] data)
        {
            foreach (var v in data)
            {
                if (element == v)
                {
                    return true;
                }
            }

            return false;
        }
    }
}