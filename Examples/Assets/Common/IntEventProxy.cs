using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace UnityAtoms.Examples
{

    public interface IVariableProxy
    {
        string key();
        void registerMe(AtomBaseVariable me);
        void unregisterMe(AtomBaseVariable me);
    }

    public abstract class AtomVariableProxy<T, P, E1, E2, F, ER, UER, ERL> : MonoBehaviour, IVariableProxy
        where P : struct, IPair<T>
        where E1 : AtomEvent<T>
        where E2 : AtomEvent<P>
        where ER : IGetEvent, ISetEvent
        where F : AtomFunction<T, T>
        where UER : UnityEvent<T>
        where ERL : AtomEventReferenceListener<T, E1, ER, UER>
    {
        public string CollectionKey;
        public string key()
        {
            return CollectionKey;
        }

        public void registerMe(AtomBaseVariable me)
        {
            registerMe((AtomVariable<T, P, E1, E2, F>)me);
        }
        
        public void unregisterMe(AtomBaseVariable me)
        {
            unregisterMe((AtomVariable<T, P, E1, E2, F>)me);
        }

        [SerializeField]
        private ERL proxied;

        public virtual void registerMe(AtomVariable<T, P, E1, E2, F> me)
        {
            proxied.Event = me.Changed;
            proxied.enabled = true;
        }

        public void unregisterMe<T>(T me)
        {
            proxied.Event = null;
            proxied.enabled = false;
        }
    }

    public class IntEventProxy : AtomVariableProxy<int, IntPair, IntEvent, IntPairEvent, IntIntFunction,IntEventReference, IntUnityEvent, IntEventReferenceListener>

    {

    }
}