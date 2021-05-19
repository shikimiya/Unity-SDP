namespace sdp
{
    public static class ExampleAttr
    {
        public const string exampleAttrExtmap1 = "extmap:1 http://example.com/082005/ext.htm#ttime";

        public const string exampleAttrExtmap1Line = exampleAttrExtmap1;

        public const string exampleAttrExtmap2 = "extmap:2/sendrecv http://example.com/082005/ext.htm#xmeta short";

        public const string exampleAttrExtmap2Line = exampleAttrExtmap2;

        public const string failingAttrExtmap1 = "extmap:257/sendrecv http://example.com/082005/ext.htm#xmeta short";

        public const string failingAttrExtmap1Line = util.attributeKey + failingAttrExtmap1;

        public const string failingAttrExtmap2 = "extmap:2/blorg http://example.com/082005/ext.htm#xmeta short";

        public const string failingAttrExtmap2Line = util.attributeKey + failingAttrExtmap2;
    }
}
