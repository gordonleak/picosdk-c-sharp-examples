/******************************************************************************
 *
 *  Filename: PS4000PinnedArray.cs
 *
 *  Description:
 *    This file defines an object to hold an array in memory when 
 *    registering a data buffer with the ps4000 driver.
 *   
 * Copyright (C) 2009 - 2017 Pico Technology Ltd. See LICENSE file for terms.
 *   
 *****************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace PS4000PinnedArray
{
    public class PinnedArray<T> : IDisposable
    {
        GCHandle _pinnedHandle;
        private bool _disposed;

        public PinnedArray(int arraySize) : this(new T[arraySize]) { }

        public PinnedArray(T[] array)
        {
            _pinnedHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        }

        ~PinnedArray()
        {
            Dispose(false);
        }

        public T[] Target
        {
            get { return (T[])_pinnedHandle.Target; }
        }

        public static implicit operator T[](PinnedArray<T> a)
        {
            if (a == null)
                return null;

            return (T[])a._pinnedHandle.Target;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (disposing)
            {
                // Dispose of any IDisposable members
            }

            _pinnedHandle.Free();
        }
    }
}