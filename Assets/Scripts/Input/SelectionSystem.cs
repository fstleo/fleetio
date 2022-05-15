using Fleetio.ECS;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fleetio.Input
{
    public class SelectionSystem : ISystem
    {
        private Camera _camera;
        private readonly Mesh _selectionMesh;
        private readonly Material _selectionMaterial;
        private readonly ComponentsList<PositionData> _positions;
        private ComponentsList<Selection> _selections;

        private bool _selectionHappening;
        private Bounds _selectionBounds;

        private Vector3 _startSelection;

        public SelectionSystem(Camera camera, Mesh selectionMesh, Material selectionMaterial, 
            ComponentsList<PositionData> positions, ComponentsList<Selection> selections)
        {
            _camera = camera;
            _selectionMesh = selectionMesh;
            _selectionMaterial = selectionMaterial;
            _positions = positions;
            _selections = selections;
        }

        public void Run()
        {
            if (Mouse.current.leftButton.isPressed && !_selectionHappening)
            {
                StartSelection(Mouse.current.position.ReadValue());
            }

            if (Mouse.current.leftButton.isPressed && _selectionHappening)
            {
                DrawSelection(Mouse.current.position.ReadValue());    
            }
            
            if (!Mouse.current.leftButton.isPressed && _selectionHappening)
            {
                EndSelection(Mouse.current.position.ReadValue());
            }
                
        }

        private void DrawSelection(Vector2 mousePosition)
        {
            Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hit);
            var endSelection = hit.point + Vector3.up * 3;
            var start = new Vector3(Mathf.Min(_startSelection.x, endSelection.x),
                Mathf.Min(_startSelection.y, endSelection.y), Mathf.Min(_startSelection.z, endSelection.z));
            var end = new Vector3(Mathf.Max(_startSelection.x, endSelection.x),
                Mathf.Max(_startSelection.y, endSelection.y), Mathf.Max(_startSelection.z, endSelection.z));
            var bounds = new Bounds((start + end) / 2f, end - start);

            Graphics.DrawMesh(_selectionMesh, Matrix4x4.TRS(bounds.center, Quaternion.identity,  bounds.size), _selectionMaterial, 0);
        }
        
        private void StartSelection(Vector2 mousePosition)
        {
            _selectionHappening = true;
            Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hit);
            _startSelection = hit.point;
        }

        private void EndSelection(Vector2 mousePosition)
        {
            _selectionHappening = false;
            var positions = _positions.GetKeyValue(Allocator.TempJob);
            _selections.Map.Clear();
            Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hit);
            var endSelection = hit.point + Vector3.up * 3;
            var start = new Vector3(Mathf.Min(_startSelection.x, endSelection.x),
                Mathf.Min(_startSelection.y, endSelection.y), Mathf.Min(_startSelection.z, endSelection.z));
            var end = new Vector3(Mathf.Max(_startSelection.x, endSelection.x),
                Mathf.Max(_startSelection.y, endSelection.y), Mathf.Max(_startSelection.z, endSelection.z));
            var bounds = new Bounds((start + end) / 2f, end - start);
            var job = new SelectJob
            {
                Positions = positions,
                Selections = _selections.Map.AsParallelWriter(),
                Bounds = bounds
            };
            Graphics.DrawMesh(_selectionMesh, Matrix4x4.TRS(bounds.center, Quaternion.identity,  bounds.size), _selectionMaterial, 0);
            job.Schedule(positions.Length, 64).Complete();
            
            positions.Dispose();
        }


        
        [BurstCompile(CompileSynchronously = true)]
        private struct SelectJob : IJobParallelFor
        {
            [ReadOnly]
            public NativeKeyValueArrays<int, PositionData> Positions; 
            
            public NativeHashMap<int, Selection>.ParallelWriter Selections;
            
            [ReadOnly]
            public Bounds Bounds;
        
            public void Execute(int index)
            {
                if (Bounds.Contains(Positions.Values[index].objectToWorld.GetPosition()))
                {
                    Selections.TryAdd(Positions.Keys[index], new Selection());
                }
            }
        }
    }
}