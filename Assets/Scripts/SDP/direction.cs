
namespace sdp
{
    //方向は、エンドポイントの送信方向のマーカーです
    public enum Direction
    {
        // DirectionSendRecvは双方向通信用です
        DirectionSendRecv = 1,
        
        // DirectionSendOnlyは発信通信用です
        DirectionSendOnly,
        
        // DirectionRecvOnlyは着信通信用です
        DirectionRecvOnly,
        
        // DirectionInactiveは通信なしです
        DircetionInactive,
        
        unknown
    }


    public partial class error
    {
        public const string errDircationString = "invalid direction string";
    }
    
    public static class DirectionExtended 
    {
        public const string directionSendRecvStr = "sendrecv";
        
        public const string directionSendOnlyStr = "sendonly";
        
        public const string directionRecvOnlyStr = "recvonly";
        
        public const string directionInactiveStr = "inactive";
        
        public const string directionUnknownStr = "";

        // NewDirectionは、生の文字列から新しい方向を作成するためのプロシージャを定義します。
        public static (Direction, string) NewDirection(string raw)
        {
            switch (raw)
            {
                case directionSendRecvStr:
                    return (Direction.DirectionSendRecv, null);
                
                case directionSendOnlyStr:
                    return (Direction.DirectionSendOnly, null);
                
                case directionRecvOnlyStr:
                    return (Direction.DirectionRecvOnly, null);
                
                case directionInactiveStr:
                    return (Direction.DircetionInactive, null);
                
                default:
                    return (Direction.unknown, error.errDircationString);
            }
        }

        public static string String(Direction t)
        {
            switch (t)
            {
                case Direction.DirectionSendRecv:
                    return directionSendRecvStr;
                
                case Direction.DirectionSendOnly:
                    return directionSendOnlyStr;
                
                case Direction.DirectionRecvOnly:
                    return directionRecvOnlyStr;
                
                case Direction.DircetionInactive:
                    return directionInactiveStr;

                default:
                    return directionUnknownStr;
            }
        }
    }
}