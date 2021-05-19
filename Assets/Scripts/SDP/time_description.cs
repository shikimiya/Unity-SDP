using System;
using System.Collections.Generic;

namespace sdp
{
    // TimeDescriptionは、セッションの開始時刻と停止時刻、
    // およびスケジュールされたセッションの繰り返し間隔と期間を指定するために使用される、
    // セッション記述の「t =」、「r =」フィールドを記述します。
    public class TimeDescription
    {
        // t=<start-time> <stop-time>
        // https://tools.ietf.org/html/rfc4566#section-5.9
        public Timing Timing;

        // r=<repeat interval> <active duration> <offsets from start-time>
        // https://tools.ietf.org/html/rfc4566#section-5.10
        public List<RepeatTime> RepeatTime;

        public TimeDescription()
        {
            Timing = new Timing();
            
            RepeatTime = new List<RepeatTime>();
        }
    }

    //タイミングは、開始時間と停止時間の「t =」フィールドの構造化表現を定義します。
    public class Timing
    {
        public ulong StartTime;

        public ulong StopTime;

        public Timing()
        {
            StartTime = 0;

            StopTime = 0;
        }

        public string String()
        {
            var output = Convert.ToString(StartTime);

            output += " " + Convert.ToString(StopTime);

            return output;
        }
    }
    
    // RepeatTimeは、繰り返されるスケジュールされたセッションの間隔と
    // 期間を表すセッション記述の「r =」フィールドを記述します。
    public class RepeatTime
    {
        public long Interval;

        public long Duration;

        public List<long> Offsets;

        public RepeatTime()
        {
            Interval = 0;

            Duration = 0;
            
            Offsets = new List<long>();
        }

        public string String()
        {
            var fields = new List<string>();

            fields.Add(Convert.ToString(Interval));

            fields.Add(Convert.ToString(Duration));

            foreach (var value in Offsets)
            {
                fields.Add(Convert.ToString(value));
            }

            return string.Join(" ", fields);
        }
    }
}