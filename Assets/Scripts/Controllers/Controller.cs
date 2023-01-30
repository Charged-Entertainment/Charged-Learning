using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller<T>: Singleton<T> where T: MonoBehaviour
{   
    // Any common stuff between controllers here.
}
