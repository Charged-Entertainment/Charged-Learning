using UnityEngine;

// Any class that inherits from this will be automatically loaded by SingletonLoader.
// Use the [ExcludeFromSingletonAutoLoading] attribute to prevent autoloading.
abstract public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T Instance;
    virtual protected void Awake()
    {
        if (Instance == null) Instance = this as T;
        else Destroy(gameObject);
    }

    static public void Enable()
    {
        Instance.enabled = true;
    }

    static public void Disable()
    {
        Instance.enabled = false;
    }
}
