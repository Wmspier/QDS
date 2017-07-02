/// <summary>
/// Controllers are essentially systems updated by the Application Facade.
/// The purpose of using basic classes for updating (rather than MonoBehaviors)
/// is to segregate the update logic of objects that do not literally exist within a scene.
/// 
/// Controllers should only delegate between models and views.
/// </summary>
public class Controller {
    virtual public void Update() { }
}
