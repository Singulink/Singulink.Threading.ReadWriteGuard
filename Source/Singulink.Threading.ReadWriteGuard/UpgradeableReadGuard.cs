using System;
using System.Threading;

#pragma warning disable CA1815 // Override equals and operator equals on value types

namespace Singulink.Threading
{
    /// <summary>
    /// Represents a disposable upgradeable read guard over a <see cref="ReaderWriterLockSlim"/>.
    /// </summary>
    public struct UpgradeableReadGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;
        private bool _isUpgraded;

        /// <summary>
        /// Gets a value indicating whether the guard has been upgraded to a write guard.
        /// </summary>
        public bool IsUpgraded => _isUpgraded;

        internal UpgradeableReadGuard(ReaderWriterLockSlim readerWriterLock)
        {
            _isUpgraded = false;
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterUpgradeableReadLock();
        }

        /// <summary>
        /// Upgrades this guard to a write guard.
        /// </summary>
        public void UpgradeToWriteGuard()
        {
            _readerWriterLock.EnterWriteLock();
            _isUpgraded = true;
        }

        /// <summary>
        /// Releases the upgradeable read guard (or write guard if it was upgraded).
        /// </summary>
        public void Dispose()
        {
            if (!_isUpgraded)
                _readerWriterLock.ExitUpgradeableReadLock();
            else
                _readerWriterLock.ExitWriteLock();
        }
    }
}
