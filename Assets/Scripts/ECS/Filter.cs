using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

namespace Fleetio.ECS
{
    [StructLayout(LayoutKind.Sequential)]
    [BurstCompatible(GenericTypeArguments = new[] { typeof(int) })]
    public struct Filter<T0, T1> : IDisposable
        where T0 : unmanaged
        where T1 : unmanaged
    {
        private NativeList<FilterResult<T0, T1>> _results;
        private NativeList<int> _ids;
        private NativeList<T0> _results0;
        private NativeList<T1> _results1;

        public Filter(NativeHashMap<int, T0> components0,
            NativeHashMap<int, T1> components1, Allocator allocator)
        {
            var count = Math.Min(components0.Count(), components1.Count());
            _results = new NativeList<FilterResult<T0, T1>>(count,allocator);
            _results0 = new NativeList<T0>(count,allocator);
            _results1 = new NativeList<T1>(count,allocator);
            _ids = new NativeList<int>(count, allocator);
            
            foreach (var set in components0)
            {
                if (components1.TryGetValue(set.Key, out var item1))
                {
                    _results.Add(new FilterResult<T0, T1>
                    {
                        Id = set.Key,
                        First = set.Value,
                        Second = item1
                    });
                    _results0.Add(set.Value);
                    _results1.Add(item1);
                    _ids.Add(set.Key);
                }
            }   
        }

        public void Dispose()
        {
            _results.Dispose();
            _results0.Dispose();
            _results1.Dispose();
        }

        public int Length => _results.Length;
        
        public FilterResult<T0, T1> this[int index] => _results[index];

        public NativeArray<FilterResult<T0,T1>> GetArray()
        {
            return _results.AsArray();
        }
        
        public NativeArray<T0> GetArray0()
        {
            return _results0.AsArray();
        }
        
        public NativeArray<T1> GetArray1()
        {
            return _results1.AsArray();
        }

        public NativeArray<int> GetIds()
        {
            return _ids.AsArray();
        }
    }
}