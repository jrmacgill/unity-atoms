#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;


namespace UnityAtoms.SceneMgmt.Editor
{
    /// <summary>
    /// Value List property drawer of type `SceneField`. Inherits from `AtomDrawer&lt;SceneFieldValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneFieldValueList))]
    public class SceneFieldValueListDrawer : AtomDrawer<SceneFieldValueList> { }
}
#endif
