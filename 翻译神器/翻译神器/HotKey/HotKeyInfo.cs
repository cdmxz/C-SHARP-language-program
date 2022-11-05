namespace 翻译神器.HotKey
{
    /// <summary>
    /// 热键
    /// </summary>
    public class HotKeyInfo : CombinationKey
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hotKey">热键</param>
        /// <param name="id">热键Id</param>
        public HotKeyInfo(string hotKey, int id) : base(hotKey)
        {
            Id = id;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hotKey">热键</param>
        /// <param name="id">热键Id</param>
        public HotKeyInfo(string hotKey, HotKeyIds id) : base(hotKey)
        {
            Id = (int)id;
        }

        /// <summary>
        /// 热键Id
        /// </summary>
        public HotKeyIds HotKeyId { get; }
        /// <summary>
        /// 热键Id
        /// </summary>
        public int Id { get; }

        public override string ToString()
        {
            return base.CombinationKeyInfo;
        }
    }
}
