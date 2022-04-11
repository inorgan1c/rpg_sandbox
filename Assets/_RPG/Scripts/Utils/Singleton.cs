using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Singleton
 * 
 * A class implementing the Singleton pattern
 * 
 * It creates a game object and add to it a component of type T 
 * if no instance of T exists in the scene.
 * Destroys multiple instances of T: only one instance is permitted (_instance)
 * Uses DontDestroyOnLoad to keep its gameobject at scene load.
 *
 ***/
public class Singleton<T> : 
    MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
