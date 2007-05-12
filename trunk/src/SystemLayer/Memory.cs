/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

//#define REPORTLEAKS

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Contains methods for allocating, freeing, and performing operations on memory 
    /// that is fixed (pinned) in memory.
    /// </summary>
    [CLSCompliant(false)]
    public unsafe static class Memory
    {
	static Memory ()
	{
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        /// <summary>
        /// Gets the total amount of physical memory (RAM) in the system.
        /// </summary>
        public static ulong TotalPhysicalBytes
        {
	    get {
		    Console.WriteLine ("PORT: TotalPhysicalBytes");
		    return Int32.MaxValue;
            }
        }

        private static void DestroyHeap()
        {
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            DestroyHeap();
        }

        /// <summary>
        /// Allocates a block of memory at least as large as the amount requested.
        /// </summary>
        /// <param name="bytes">The number of bytes you want to allocate.</param>
        /// <returns>A pointer to a block of memory at least as large as <b>bytes</b>.</returns>
        /// <exception cref="OutOfMemoryException">Thrown if the memory manager could not fulfill the request for a memory block at least as large as <b>bytes</b>.</exception>
        public static IntPtr Allocate(ulong bytes)
        {
		return System.Runtime.InteropServices.Marshal.AllocHGlobal ((IntPtr)bytes);
        }

        /// <summary>
        /// Allocates a block of memory at least as large as the amount requested.
        /// </summary>
        /// <param name="bytes">The number of bytes you want to allocate.</param>
        /// <returns>A pointer to a block of memory at least as large as bytes</returns>
        /// <remarks>
        /// This method uses an alternate method for allocating memory (VirtualAlloc in Windows). The allocation
        /// granularity is the page size of the system (usually 4K). Blocks allocated with this method may also
        /// be protected using the ProtectBlock method.
        /// </remarks>
        public static IntPtr AllocateLarge(ulong bytes)
        {
	    if (!warn_shown){
		    warn_shown = true;
		    Console.WriteLine ("PORT: Memory.AllocateLarge probably should call mmap to allocate");
	    }
	    return System.Runtime.InteropServices.Marshal.AllocHGlobal ((IntPtr) bytes);
        }
	static bool warn_shown;

        /// <summary>
        /// Allocates a bitmap of the given height and width. Pixel data may be read/written directly, 
        /// and it may be drawn to the screen using PdnGraphics.DrawBitmap().
        /// </summary>
        /// <param name="width">The width of the bitmap to allocate.</param>
        /// <param name="height">The height of the bitmap to allocate.</param>
        /// <param name="handle">Receives a handle to the bitmap.</param>
        /// <returns>A pointer to the bitmap's pixel data.</returns>
        /// <remarks>
        /// The following invariants may be useful for implementors:
        /// * The bitmap is always 32-bits per pixel, BGRA.
        /// * Stride for the bitmap is always width * 4.
        /// * The upper-left pixel of the bitmap (0,0) is located at the first memory location pointed to by the returned pointer.
        /// * The bitmap is top-down ("memory correct" ordering).
        /// * The 'handle' may be any type of data you want, but must be unique for the lifetime of the bitmap, and must not be IntPtr.Zero.
        /// * The handle's value must be understanded by PdnGraphics.DrawBitmap().
        /// * The bitmap is always modified by directly reading and writing to the memory pointed to by the return value.
        /// * PdnGraphics.DrawBitmap() must always render from this memory location (i.e. it must treat the memory as 'volatile')
        /// </remarks>
        public static IntPtr AllocateBitmap(int width, int height, out IntPtr handle)
        {
	    IntPtr pvBits = System.Runtime.InteropServices.Marshal.AllocHGlobal (width * height * 4);
	    if (pvBits == IntPtr.Zero)
		    throw new OutOfMemoryException("AllocHGlobal returned NULL (" + Marshal.GetLastWin32Error().ToString() + ") while attempting to allocate " + width + "x" + height + " bitmap");
	    
	    GdipCreateBitmapFromScan0 (width, height, width * 4, PixelFormat.Format32bppArgb, pvBits, out handle);
	    return pvBits;
        }
        [DllImport("gdiplus.dll", SetLastError = true)]
	internal static extern int GdipCreateBitmapFromScan0 (int width, int height, int stride, PixelFormat format, IntPtr scan0, out IntPtr bmp);

        /// <summary>
        /// Frees a bitmap previously allocated with AllocateBitmap.
        /// </summary>
        /// <param name="handle">The handle that was returned from a previous call to AllocateBitmap.</param>
        /// <param name="width">The width of the bitmap, as specified in the original call to AllocateBitmap.</param>
        /// <param name="height">The height of the bitmap, as specified in the original call to AllocateBitmap.</param>
        public static void FreeBitmap(IntPtr handle, int width, int height)
        {
	    GdipDisposeImage (handle);
        }
	[DllImport("gdiplus.dll")]
	internal static extern int GdipDisposeImage (IntPtr image);

        /// <summary>
        /// Frees a block of memory previously allocated with Allocate().
        /// </summary>
        /// <param name="block">The block to free.</param>
        /// <exception cref="InvalidOperationException">There was an error freeing the block.</exception>
        public static void Free(IntPtr block)
        {
		System.Runtime.InteropServices.Marshal.FreeHGlobal (block);
        }

        /// <summary>
        /// Frees a block of memory previous allocated with AllocateLarge().
        /// </summary>
        /// <param name="block">The block to free.</param>
        /// <param name="bytes">The size of the block.</param>
        public static void FreeLarge(IntPtr block, ulong bytes)
        {
	  System.Runtime.InteropServices.Marshal.FreeHGlobal (block);
        }

        /// <summary>
        /// Sets protection on a block previously allocated with AllocateLarge.
        /// </summary>
        /// <param name="block">The starting memory address to set protection for.</param>
        /// <param name="size">The size of the block.</param>
        /// <param name="readAccess">Whether to allow read access.</param>
        /// <param name="writeAccess">Whether to allow write access.</param>
        /// <remarks>
        /// You may not specify false for read access without also specifying false for write access.
        /// Note to implementors: This method is not guaranteed to actually set read/write-ability 
        /// on a block of memory, and may instead be implemented as a no-op after parameter validation.
        /// </remarks>
        public static void ProtectBlockLarge(IntPtr block, ulong size, bool readAccess, bool writeAccess)
        {
		Console.WriteLine ("PORT: Memory.ProtectBlockLarge: should use mmap instead of alloc, and use mprotect here");
        }

        /// <summary>
        /// Copies bytes from one area of memory to another. Since this function only
        /// takes pointers, it can not do any bounds checking.
        /// </summary>
        /// <param name="dst">The starting address of where to copy bytes to.</param>
        /// <param name="src">The starting address of where to copy bytes from.</param>
        /// <param name="length">The number of bytes to copy</param>
        public static void Copy(IntPtr dst, IntPtr src, ulong length)
        {
            Copy(dst.ToPointer(), src.ToPointer(), length);
        }

        /// <summary>
        /// Copies bytes from one area of memory to another. Since this function only
        /// takes pointers, it can not do any bounds checking.
        /// </summary>
        /// <param name="dst">The starting address of where to copy bytes to.</param>
        /// <param name="src">The starting address of where to copy bytes from.</param>
        /// <param name="length">The number of bytes to copy</param>
        public static void Copy(void *dst, void *src, ulong length)
        {
            SafeNativeMethods.memcpy(dst, src, new UIntPtr(length));
        }

        public static void SetToZero(IntPtr dst, ulong length)
        {
            SetToZero(dst.ToPointer(), length);
        }

        public static void SetToZero(void *dst, ulong length)
        {
            SafeNativeMethods.memset(dst, 0, new UIntPtr(length));
        }
    }
}
