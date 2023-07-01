using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        LoadSystems();
        LoadUIElements();
        GameObject.Destroy(gameObject);
    }

    void LoadSystems()
    {
        var types = Assembly.GetAssembly(typeof(Utils)).GetTypes().
                Where(T =>
                IsSubclassOfRawGeneric(typeof(Singleton<>), T)
                && !T.IsAbstract
                && T.GetCustomAttribute<ExcludeFromSingletonAutoLoadingAttribute>() == null);

        var singletons = GameObject.Find("Singletons");
        if (singletons == null) singletons = new GameObject("Singletons");
        foreach (var T in types)
        {
            var go = new GameObject(T.Name);
            go.AddComponent(T);
            go.transform.parent = singletons.transform;
        }
    }

    void LoadUIElements()
    {
        var types = Assembly.GetAssembly(typeof(Utils)).GetTypes().
                        Where(T =>
                        IsSubclassOfRawGeneric(typeof(UI.UIBaseElement), T)
                        && !T.IsAbstract
                        && T.GetCustomAttribute<ExcludeFromSingletonAutoLoadingAttribute>() == null);

        var document = GameObject.Find("UIDocument");
        if (document == null) document = new GameObject("UIDocument");
        foreach (var T in types)
        {
            document.AddComponent(T);
        }
    }

    static bool IsSubclassOfRawGeneric(System.Type generic, System.Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }
}
