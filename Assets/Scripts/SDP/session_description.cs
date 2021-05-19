using System;
using System.Collections.Generic;

namespace sdp
{
    // SessionDescriptionは、マルチメディアセッションを検出して参加するのに
    // 十分な情報を伝達するための明確に定義された形式です。
    public partial class SessionDescription
    {
        // v=0
        // https://tools.ietf.org/html/rfc4566#section-5.1
        public Version Version;
        
        // o=<username> <sess-id> <sess-version> <nettype> <addrtype> <unicast-address>
        // https://tools.ietf.org/html/rfc4566#section-5.2
        public Origin Origin;
        
        // s=<session name>
        // https://tools.ietf.org/html/rfc4566#section-5.3
        public SessionName SessionName;
        
        // i=<session description>
        // https://tools.ietf.org/html/rfc4566#section-5.4
        public Information SessionInformation;
        
        // u=<uri>
        // https://tools.ietf.org/html/rfc4566#section-5.5
        public Uri URL;
        
        // e=<email-address>
        // https://tools.ietf.org/html/rfc4566#section-5.6
        public EmailAddress EmailAddress;
        
        // p=<phone-number>
        // https://tools.ietf.org/html/rfc4566#section-5.6
        public PhoneNumber PhoneNumber;
        
        // c=<nettype> <addrtype> <connection-address>
        // https://tools.ietf.org/html/rfc4566#section-5.7
        public ConnectionInformation ConnectionInformation;
        
        // b=<bwtype>:<bandwidth>
        // https://tools.ietf.org/html/rfc4566#section-5.8
        public List<BandWidth> BandWidth;
        
        // https://tools.ietf.org/html/rfc4566#section-5.9
        // https://tools.ietf.org/html/rfc4566#section-5.10
        public List<TimeDescription> TimeDescriptions;
        
        // z=<adjustment time> <offset> <adjustment time> <offset> ...
        // https://tools.ietf.org/html/rfc4566#section-5.11
        public List<TimeZone> TimeZones;
        
        // k=<method>
        // k=<method>:<encryption key>
        // https://tools.ietf.org/html/rfc4566#section-5.12
        public EncryptionKey EncryptionKey;
        
        // a=<attribute>
        // a=<attribute>:<value>
        // https://tools.ietf.org/html/rfc4566#section-5.13
        public List<Attribute> Attributes;
        
        // https://tools.ietf.org/html/rfc4566#section-5.14
        public List<MediaDescription> MediaDescriptions;

        public SessionDescription()
        {
            Version = new Version();
            
            Origin = new Origin();
            
            SessionName = new SessionName();
            
            SessionInformation = new Information();
            
            //URL = new Uri("");
            
            EmailAddress = new EmailAddress();
            
            PhoneNumber = new PhoneNumber();
            
            ConnectionInformation = new ConnectionInformation();
            
            BandWidth = new List<BandWidth>();
            
            TimeDescriptions = new List<TimeDescription>();
            
            TimeZones = new List<TimeZone>();
            
            EncryptionKey = new EncryptionKey();
            
            Attributes = new List<Attribute>();
            
            MediaDescriptions = new List<MediaDescription>();
        }
        
        // Attributeは存在する場合は、attributeの値を返します。
        public (string, bool) Attribute(string key)
        {
            foreach (var a in Attributes)
            {
                if (a.Key == key)
                {
                    return (a.Value, true);
                }
            }
            return ("", false);
        }
    }

    //バージョンは、セッション記述プロトコルのバージョンを提供する
    //「v =」フィールドによって提供される値を記述します。
    public class Version
    {
        public int version;

        public Version()
        {
            version = 0;
        }

        public string String()
        {
            return Convert.ToString(version);
        }
    }

    // Originは、セッションの発信者に加えてセッション識別子と
    // バージョン番号を提供する「o =」フィールドの構造を定義します。
    public class Origin
    {
        public string Username;
        public ulong SessionID;
        public ulong SessionVersion;
        public string NetworkType;
        public string AddressType;
        public string UnicastAddress;

        public Origin()
        {
            Username = null;

            SessionID = 0;

            SessionVersion = 0;

            NetworkType = null;

            AddressType = null;

            UnicastAddress = null;
        }

        public string String()
        {
            return $"{Username} {SessionID} {SessionVersion} {NetworkType} {AddressType} {UnicastAddress}";
        }
    }
    
    // SessionNameは、「s =」フィールドの構造化された表現を記述し、テキストのセッション名です。
    public class SessionName
    {
        public string sessionName;

        public SessionName()
        {
            sessionName = null;
        }

        public string String()
        {
            return sessionName;
        }
    }
    
    // EmailAddressは、会議の責任者の電子メール連絡先情報を
    // 指定する「e =」行の構造化された表現を記述します。
    public class EmailAddress
    {
        public string emailAddress;

        public EmailAddress()
        {
            emailAddress = null;
        }
        
        public string String()
        {
            return emailAddress;
        }
    }
    
    // PhoneNumberは、「p =」行の構造化された表現を記述し、
    // 会議の責任者の電話連絡先情報を指定します。
    public class PhoneNumber
    {
        public string phoneNumber;

        public PhoneNumber()
        {
            phoneNumber = null;
        }
        
        public string String()
        {
            return phoneNumber;
        }
    }
    
    // TimeZoneは、繰り返されるセッションのスケジューリングを説明する
    // 「z =」行の構造化オブジェクトを定義します。
    public class TimeZone
    {
        public ulong AdjustmentTime;

        public long Offset;

        public TimeZone()
        {
            AdjustmentTime = 0;

            Offset = 0;
        }

        public string String()
        {
            return AdjustmentTime + " " + Offset;
        }
    }
}