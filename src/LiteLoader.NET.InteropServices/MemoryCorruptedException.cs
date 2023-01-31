namespace LiteLoader.NET
{
    public class MemoryCorruptedException : Exception
    {
        public MemoryCorruptedException(string message)
        : base(message)
        {
        }
    }
}
