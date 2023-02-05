using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
        GameObject.Destroy(gameObject);
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
