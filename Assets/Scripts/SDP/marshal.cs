
using System.Collections.Generic;
using System.Text;

namespace sdp
{
    // Marshal takes a SDP struct to text
    // https://tools.ietf.org/html/rfc4566#section-5
    // Session description
    //    v=  (protocol version)
    //    o=  (originator and session identifier)
    //    s=  (session name)
    //    i=* (session information)
    //    u=* (URI of description)
    //    e=* (email address)
    //    p=* (phone number)
    //    c=* (connection information -- not required if included in
    //         all media)
    //    b=* (zero or more bandwidth information lines)
    //    One or more time descriptions ("t=" and "r=" lines; see below)
    //    z=* (time zone adjustments)
    //    k=* (encryption key)
    //    a=* (zero or more session attribute lines)
    //    Zero or more media descriptions
    //
    // Time description
    //    t=  (time the session is active)
    //    r=* (zero or more repeat times)
    //
    // Media description, if present
    //    m=  (media name and transport address)
    //    i=* (media title)
    //    c=* (connection information -- optional if included at
    //         session level)
    //    b=* (zero or more bandwidth information lines)
    //    k=* (encryption key)
    //    a=* (zero or more media attribute lines)

    public partial class SessionDescription
    {
        public (List<byte>, string) Marshal()
        {
            var m = new Marshaller
            {
                marshaller = new List<byte>(1024)
            };
            
            m.addKetValue("v=", Version.String());
            
            m.addKetValue("o=", Origin.String());
            
            m.addKetValue("s=", SessionName.String());

            if (SessionInformation != null)
            {
                if (SessionInformation.information != null)
                {
                    m.addKetValue("i=", SessionInformation.String());
                }
            }

            if (URL != null)
            {
                m.addKetValue("u=", URL.ToString());
            }

            if (EmailAddress != null)
            {
                if (EmailAddress.emailAddress != null)
                {
                    m.addKetValue("e=", EmailAddress.String());
                }
            }

            if (PhoneNumber != null)
            {
                if (PhoneNumber.phoneNumber != null)
                {
                    m.addKetValue("p=", PhoneNumber.String());
                }
            }

            if (ConnectionInformation != null)
            {
                m.addKetValue("c=", ConnectionInformation.String());
            }

            foreach (var b in BandWidth)
            {
                m.addKetValue("b=", b.String());
            }

            foreach (var td in TimeDescriptions)
            {
                m.addKetValue("t=", td.Timing.String());

                foreach (var r in td.RepeatTime)
                {
                    m.addKetValue("r=", r.String());
                }
            }

            if (TimeZones.Count > 0)
            {
                var b = new StringBuilder();
                
                for (var i = 0; i < TimeZones.Count; i++)
                {
                    if (i > 0)
                    {
                        b.Append(" ");
                    }

                    b.Append(TimeZones[i].String());
                }

                m.addKetValue("z=", b.ToString());
            }


            if (EncryptionKey != null)
            {
                if (EncryptionKey.encryptionKey != null)
                {
                    m.addKetValue("k=", EncryptionKey.String());
                }
            }

            foreach (var a in Attributes)
            {
                m.addKetValue("a=", a.String());
            }

            foreach (var md in MediaDescriptions)
            {
                m.addKetValue("m=", md.MediaName.String());

                if (md.MediaTitle != null)
                {
                    if (md.MediaTitle.information != null)
                    {
                        m.addKetValue("i=", md.MediaTitle.String());
                    }
                }

                if (md.ConnectionInformation != null)
                {
                    if (md.ConnectionInformation.address != null)
                    {
                        m.addKetValue("c=", md.ConnectionInformation.String());
                    }
                }

                foreach (var b in md.Bandwidth)
                {
                    m.addKetValue("b=", b.String());
                }

                if (md.EncryptionKey != null)
                {
                    if (md.EncryptionKey.encryptionKey != null)
                    {
                        m.addKetValue("k=", md.EncryptionKey.String());
                    }
                }

                foreach (var a in md.Attributes)
                {
                    m.addKetValue("a=", a.String());
                }
            }

            return (m.bytes(), null);
        }
    }


    public class Marshaller
    {
        public Marshaller()
        {
            marshaller = new List<byte>();
        }
        
        public List<byte> marshaller;

        public void addKetValue(string key, string value)
        {
            if (value == "")
            {
                return;
            }

            if (key != null)
            {
                marshaller.AddRange(System.Text.Encoding.UTF8.GetBytes(key));
            }

            if (value != null)
            {
                marshaller.AddRange(System.Text.Encoding.UTF8.GetBytes(value));
            }
            
            marshaller.AddRange(System.Text.Encoding.UTF8.GetBytes("\r\n"));
        }

        public List<byte> bytes()
        {
            return marshaller;
        }
    }
    
    
    
    
    
    
}
