using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Manager<T> : Singleton<T> where T: MonoBehaviour
{
    // Any common stuff between managers here.
}
