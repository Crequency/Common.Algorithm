namespace Common.Algorithm.Interop.Exceptions
{
    public class BasicException : Exception
    {
        /// <summary>
        /// 异常描述
        /// </summary>
        public string? Description { get; protected set; }

        /// <summary>
        /// 是否可以被解决而不影响运行
        /// </summary>
        public bool CanHandle { get; set; }

        /// <summary>
        /// 异常严重性
        /// </summary>
        public ExceptionLevel Level { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BasicException()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public BasicException(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="descr">描述</param>
        public BasicException(string message, string descr) : base(message)
        {
            Description = descr;
        }

        /// <summary>
        /// 异常级别
        /// </summary>
        public enum ExceptionLevel
        {
            Info = 0, Warning = 1, Error = 2, Fatal = 3, Disaster = 4
        }
    }
}
