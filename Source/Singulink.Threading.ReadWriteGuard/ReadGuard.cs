using System;
using System.Threading;

#pragma warning disable CA1815 // Override equals and operator equals on value types

namespace Singulink.Threading
{
    /// <summary>
    /// Represents a disposable read guard over a <see cref="ReaderWriterLockSlim"/>.
    /// </summary>
    public readonly struct ReadGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        internal ReadGuard(ReaderWriterLockSlim readerWriterLock)
        {
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterReadLock();
        }

        /// <summary>
        /// Releases the read guard.
        /// </summary>
        public void Dispose() => _readerWriterLock.ExitReadLock();
    }
}
