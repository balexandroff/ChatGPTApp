//namespace System.Collections.Generic
//{
//    public interface IAsyncEnumerable<out T>
//    {
//        IAsyncEnumerator<T> GetAsyncEnumerator(
//          CancellationToken cancellationToken = default);
//    }
//    public interface IAsyncEnumerator<out T> : IAsyncDisposable
//    {
//        ValueTask<bool> MoveNextAsync();
//        T Current { get; }
//    }
//}
//namespace System
//{
//    public interface IAsyncDisposable
//    {
//        ValueTask DisposeAsync();
//    }
//}
