namespace Algorithm.Interop.Exceptions
{
    public class HashException : BasicException
    {
        /// <summary>
        /// 更新描述
        /// </summary>
        /// <param name="et">描述类型</param>
        private void UpdateDescription(ErrorType et)
        {
            switch (et)
            {
                case ErrorType.UndefinedCompressLevel:
                    Description = "Undefined Compress Level, Hash Error";
                    break;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="et">错误类型</param>
        public HashException(ErrorType et)
        {
            UpdateDescription(et);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public HashException(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="et">错误类型</param>
        public HashException(string message, ErrorType et) : base(message)
        {
            UpdateDescription(et);
        }

        /// <summary>
        /// 哈希错误类型
        /// </summary>
        public enum ErrorType
        {
            UndefinedCompressLevel = 0
        }
    }
}
