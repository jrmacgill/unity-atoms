#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;


namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `AtomBaseVariable`. Inherits from `AtomEventEditor&lt;AtomBaseVariable, AtomBaseVariableEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(AtomBaseVariableEvent))]
    public sealed class AtomBaseVariableEventEditor : AtomEventEditor<AtomBaseVariable, AtomBaseVariableEvent> { }
}
#endif
