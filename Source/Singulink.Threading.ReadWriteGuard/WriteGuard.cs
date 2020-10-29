using System;
using System.Threading;

#pragma warning disable CA1815 // Override equals and operator equals on value types

namespace Singulink.Threading
{
    /// <summary>
    /// Represents a disposable write guard over a <see cref="ReaderWriterLockSlim"/>.
    /// </summary>
    public readonly struct WriteGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        internal WriteGuard(ReaderWriterLockSlim readerWriterLock)
        {
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterWriteLock();
        }

        /// <summary>
        /// Releases the write guard.
        /// </summary>
        public void Dispose() => _readerWriterLock.ExitWriteLock();
    }
}
