using System;


namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `AtomBaseVariable`. Inherits from `AtomBaseEventReference&lt;AtomBaseVariable, AtomBaseVariableEvent, AtomBaseVariableEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class AtomBaseVariableBaseEventReference : AtomBaseEventReference<
        AtomBaseVariable,
        AtomBaseVariableEvent,
        AtomBaseVariableEventInstancer>, IGetEvent
    { }
}
