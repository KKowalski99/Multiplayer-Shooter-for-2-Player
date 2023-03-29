using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Singletone<T> : NetworkBehaviour where T : Component
{

    private static T _instance;
 public static T Insctance
    {
        get 
        { 
        if(_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0) _instance = objs[0];
                if (objs.Length > 1)Debug.LogWarning($"There are two or more {typeof(T).Name} in one scene");
            }

            if(_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = string.Format("_{0}", typeof(T).Name);
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }



    }
}
