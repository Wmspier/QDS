using UnityEngine;

/// <summary>
/// Views are MonoBehaviours because they represent objects that literally
/// exist within a scene.
/// 
/// They are intended to be attached to a Canvas and store references to all
/// GUI objects, delegating their interaction to the controllers.
/// 
/// The ONLY logic within a view should be event dispatching and listening.
/// </summary>
public class View : MonoBehaviour {
    
}
