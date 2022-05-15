using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Fleetio.ECS;
using Unity.Collections.LowLevel.Unsafe;
 
 namespace Unity.Collections
{
    [NativeContainer]
    [NativeContainerSupportsMinMaxWriteRestriction]
    [StructLayout(LayoutKind.Sequential)]
    [BurstCompatible(GenericTypeArguments = new[] { typeof(int) })]
    [DebuggerDisplay(
        "Count = {" + nameof(Count) + "}. Capacity = {" + nameof(Capacity) + "}")]
    public unsafe struct ComponentsList<T> : IDisposable, IComponentsList
        where T : unmanaged
    {

        public NativeHashMap<int, T> Map;

        // These are all required when checks are enable`d
        // They must have these exact types, names, and attributes
        internal int m_Length;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal int m_MinIndex;
        internal int m_MaxIndex;
        internal AtomicSafetyHandle m_Safety;
        [NativeSetClassTypeToNullOnSchedule] internal DisposeSentinel m_DisposeSentinel;
#endif
 
        /// <summary>
        /// Create the list with an initial capacity. It initially has no elements.
        /// </summary>
        /// 
        /// <param name="capacity">
        /// Initial capacity. This will be doubled if too many elements are added.
        /// </param>
        /// 
        /// <param name="allocator">
        /// Allocator to allocate unmanaged memory with
        /// </param>
        /// 
        [BurstCompatible(GenericTypeArguments = new [] { typeof(AllocatorManager.AllocatorHandle) })]
        public ComponentsList(int capacity, Allocator allocator)
        {
            Map = new NativeHashMap<int, T>(capacity, allocator);
            m_Length = capacity;
 
            // Initialize fields for safety checks
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_MinIndex = 0;
            m_MaxIndex = capacity;
            DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, 0, allocator);
#endif
        }

        /// <summary>
        /// Get the capacity of the list. This is always greater than or equal to
        /// its <see cref="Count"/>.
        /// </summary>
        public int Capacity => m_Length;

        /// <summary>
        /// Get the number of elements currently in the list. This is always less
        /// than or equal to the <see cref="Capacity"/>.
        /// </summary>
        public int Count => Map.Count();

        /// <summary>
        /// Index into the list's elements
        /// </summary>
        /// 
        /// <param name="index">
        /// Index of the element to get or set. Must be greater than or equal to
        /// zero and less than <see cref="Count"/>.
        /// </param>
        public bool TryGet(int index, out T component)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
            // if (index < m_MinIndex || index > m_MaxIndex)
            // {
            //     FailOutOfRangeError(index);
            // }
#endif
            return Map.TryGetValue(index, out component);
        }

        /// <summary>
        /// Add an element to the end of the list. If the list is full, it will be
        /// automatically resized by allocating new unmanaged memory with double
        /// the <see cref="Capacity"/> and copying over all existing elements.
        /// </summary>
        /// <param name="index">Key of element</param>
        /// <param name="value">
        /// Element to add
        /// </param>
        public void Set(int index, T value)
        {
            Map[index] = value;
            // Mark the new maximum index that can be read
// #if ENABLE_UNITY_COLLECTIONS_CHECKS
//             m_MaxIndex = Count;
// #endif
        }
        
        /// <summary>
        /// Remove an element at a given index. Elements after it will be shifted
        /// toward the front of the list.
        /// </summary>
        /// 
        /// <param name="index">
        /// Index of the element to remove. Must be greater than or equal to zero
        /// and less than or equal to <see cref="Count"/>.
        /// </param>
        public void RemoveAt(int index)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif
            Map.Remove(index);
// #if ENABLE_UNITY_COLLECTIONS_CHECKS
//             m_MaxIndex = Count - 1;
// #endif
        }
 
        /// <summary>
        /// Allocate a managed array and copy all elements to it
        /// </summary>
        /// 
        /// <returns>
        /// A managed array with all of the list's elements
        /// </returns>
        public NativeArray<T> GetArray(Allocator allocator)
        {
            return Map.GetValueArray(allocator);
        }
 
        public NativeKeyValueArrays<int, T> GetKeyValue(Allocator allocator)
        {
            return Map.GetKeyValueArrays(allocator);
        }
        
        /// <summary>
        /// Release the list's unmanaged memory. Do not use it after this.
        /// </summary>
        public void Dispose()
        {
            Map.Dispose();
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(ref m_Safety, ref m_DisposeSentinel);
#endif
        }
 
        // Throw an appropriate exception when safety checks are enabled
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        private void FailOutOfRangeError(int index)
        {
            if (index < m_Length && (m_MinIndex != 0 || m_MaxIndex != m_Length - 1))
            {
                throw new IndexOutOfRangeException(
                    $"Index {index} is out of restricted IJobParallelFor range " +
                    $"[{m_MinIndex}...{m_MaxIndex}] in ReadWriteBuffer.\n" +
                    "ReadWriteBuffers are restricted to only read & write the " +
                    "element at the job index. You can use double buffering " +
                    "strategies to avoid race conditions due to reading & " +
                    "writing in parallel to the same elements from a job.");
            }
 
            throw new IndexOutOfRangeException(
                $"Index {index} is out of range of '{m_Length}' Length.");
        }
#endif
    }
}