# Singulink.Threading.ReadWriteGuard

[![Join the chat](https://badges.gitter.im/Singulink/community.svg)](https://gitter.im/Singulink/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![View nuget packages](https://img.shields.io/nuget/v/Singulink.Threading.ReadWriteGuard.svg)](https://www.nuget.org/packages/Singulink.Threading.ReadWriteGuard/)
[![Build](https://github.com/Singulink/Singulink.Threading.ReadWriteGuard/workflows/build/badge.svg)](https://github.com/Singulink/Singulink.Threading.ReadWriteGuard/actions?query=workflow%3A%22build%22)

**Singulink.Threading.ReadWriteGuard** provides disposable guards for `ReaderWriterLockSlim` that simplify entering and reliably exiting locks by allowing you to use the `using` statement.

### About Singulink

*Shameless plug*: We are a small team of engineers and designers dedicated to building beautiful, functional and well-engineered software solutions. We offer very competitive rates as well as fixed-price contracts and welcome inquiries to discuss any custom development / project support needs you may have.

This package is part of our **Singulink Libraries** collection. Visit https://github.com/Singulink to see our full list of publicly available libraries and other open-source projects.

## Installation

Simply install the `Singulink.Threading.ReadWriteGuard` package from NuGet into your project.

**Supported Runtimes**: Anywhere .NET Standard 2.0+ is supported, including:
- .NET Core 2.0+
- .NET Framework 4.6.1+
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Android 8.0+

## API

When you import the `Singulink.Threading` namespace, 3 new extension methods appear on `ReaderWriterLockSlim` instances:
- `EnterReadGuard()`
- `EnterWriteGuard()`
- `EnterUpgradeableReadGuard()`

You can view the API on [FuGet](https://www.fuget.org/packages/Singulink.Threading.ReadWriteGuard).

## Usage Examples

```c#
var lock = new ReaderWriterLockSlim();

// Locks are acquired inside the using blocks and released at the end:

using (lock.EnterReadGuard())
{
    // Safe to read here
    ReadData();
}

using (lock.EnterWriteGuard())
{
    // Safe to write here
    WriteData();
}

using (var upgradeableGuard = lock.EnterUpgradeableReadGuard())
{
    // Safe to read here
    ReadData();

    if (someCondition)
    {
        upgradeableGuard.UpgradeToWriteGuard();
        // Safe to write here
        WriteData();
    }
}
```