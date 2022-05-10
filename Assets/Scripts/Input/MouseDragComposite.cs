using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;

namespace Fleetio.Input
{
    
        
    public struct MouseDragCompositeResult
    {
        public Vector2 Position;
        public Vector2 Delta;
 
        public override string ToString() => $"{{ Position: {Position}, Delta:{Delta} }}";
    }

    [Preserve]
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    [DisplayStringFormat("{Modifier}+{Delta}+{Position}")]
    public class MouseDragComposite : InputBindingComposite<MouseDragCompositeResult>
    {
        [RuntimeInitializeOnLoadMethod]
        static void Init() { }
 
        static MouseDragComposite()
        {
            InputSystem.RegisterBindingComposite<MouseDragComposite>();
        }
 
        [InputControl(layout = "Button")]
        public int Modifier;
 
        [InputControl(layout = "Vector2")]
        public int Delta;
 
        [InputControl(layout = "Vector2")]
        public int Position;
 
        private Vector2MagnitudeComparer _comparer = new Vector2MagnitudeComparer();

        public override MouseDragCompositeResult ReadValue(ref InputBindingCompositeContext context)
        {
            if (context.ReadValueAsButton(Modifier))
            {
                var delta = context.ReadValue<Vector2, Vector2MagnitudeComparer>(Delta, _comparer);
                var position = context.ReadValue<Vector2, Vector2MagnitudeComparer>(Position, _comparer);
                
                return new MouseDragCompositeResult
                {
                    Delta = delta,
                    Position = position,
                };
            }
            return default;
        }
 
        public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
        {
            var value = ReadValue(ref context);
            return value.Delta.magnitude;
        }
    }
}