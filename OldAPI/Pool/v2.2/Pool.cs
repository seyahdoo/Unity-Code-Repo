﻿/**
* This is an Pooling API that i wrote to reuse on my unity projects
* See my other reusable unity codes at:  https://github.com/seyahdoo/Unity-Code-Repo
* See my personal website at: http://seyahdoo.com
*
* 
* @author  Seyyid Ahmed Doğan (seyahdoo)
* @version 2.2.0
* @since   2017-01-07
*/

using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Just a big namespace for my big pooling system
/// </summary>
namespace seyahdoo.pooling
{
    /// <summary>
    /// A General API for pooling GameObjects
    /// </summary>
    public static class Pool
    {

        private static Dictionary<string, Setting> _settingDictionary = new Dictionary<string, Setting>();
        private static Dictionary<string, Cache> _cacheDictionary = new Dictionary<string, Cache>();

        /// <summary>
        /// Gets an object from pool regarding its name. Object must be located at /Assets/Resources/Prefabs/[Name]
        /// </summary>
        /// <param name="Name">Name of the object you want to get</param>
        /// <returns></returns>
        public static GameObject Get(string Name)
        {

            //if that particular object does not have an setting create a default one
            if (!_settingDictionary.ContainsKey(Name))
            {
                _settingDictionary.Add(Name, new Setting());
            }

            //if it is to be pooled, do the trick.
            if (_settingDictionary[Name].IsPooled)
            {
                //Create Cache if not yet cached
                if (!_cacheDictionary.ContainsKey(Name))
                {
                    //Load that particular GameObject Prefab from "Resources/Prefabs/[Name]"
                    GameObject go = (GameObject)Resources.Load("Prefabs/" + Name, typeof(GameObject));
                    if (!go)
                    {
                        //Dont forget to yell the error!!!
                        Debug.LogError("Error: No Pool nor Prefab for such object named '" + Name + "'. Returning error recognizer!");

                        //return an object that yells i have an error!!!
                        return createErrorObject(Name);
                    }
                    else
                    {
                        //There it is. Original one, create a Cache for it.
                        Cache cache = new Cache(go, _settingDictionary[Name].InitialSize);

                        //And Add it to the dictionary
                        _cacheDictionary.Add(Name, cache);
                    }


                }

                //And Finally Do The POOL
                return _cacheDictionary[Name].Pop();
            }
            else
            {
                //i dont want to create a Cache for it, just make a new one and send it to user.
                GameObject go = (GameObject)Resources.Load("Prefabs/" + Name, typeof(GameObject));
                if (!go)
                {
                    //Dont forget to yell the error!!!
                    Debug.LogError("Error: No Pool nor Prefab for such object named '" + Name + "'. Returning error recognizer!");

                    //return an object that yells i have an error!!!
                    return createErrorObject(Name);
                }
                else
                {
                    //Create a new object from original
                    GameObject created = GameObject.Instantiate(go);

                    //Get rid of from "(clone)"
                    created.name = Name;

                    //return safely
                    return created;
                }

            }

        }

        /// <summary>
        /// Releases the object you once get from Pool, if it didnt came from pool it will be destroyed.
        /// </summary>
        /// <param name="Garbage">The GameObject you are done with</param>
        public static void Release(GameObject Garbage)
        {
            //Check if there is a cache
            if (!_cacheDictionary.ContainsKey(Garbage.name))
            {
                if (_settingDictionary.ContainsKey(Garbage.name))
                {
                    if (_settingDictionary[Garbage.name].IsPooled == true)
                    {
                        Debug.LogError("Error: No cache for such object named '" + Garbage.name + "'. Destroying!");
                    }
                }

                //Sorry man, i have to destroy you :(
                GameObject.DestroyImmediate(Garbage);
                return;
            }
            else
            {
                //Release it to the Cache
                _cacheDictionary[Garbage.name].Release(Garbage);
                return;
            }

        }


        /// <summary>
        /// Releases all active objects back to the Pool.
        /// </summary>
        /// <param name="name">Name of the prefab you are working with</param>
        public static void ReleaseAll(string name)
        {
            //Check if there is a cache
            if (!_cacheDictionary.ContainsKey(name))
            {
                if (_settingDictionary.ContainsKey(name))
                {
                    if (_settingDictionary[name].IsPooled == true)
                    {
                        Debug.LogError("Error: No cache for such object named '" + name + "'. WTF?!");
                    }
                }
                
                //Nothing to do here
                return;
            }
            else
            {
                //Release ALL madafaka
                _cacheDictionary[name].ReleaseAll();
                return;
            }


        }

        /// <summary>
        /// Sets Settings for a particular object
        /// </summary>
        /// <param name="Name">Name of the object</param>
        /// <param name="IsPooled">Whether it will be Cached</param>
        /// <param name="InitialSize">Initial size of the Cache</param>
        public static void SetSetting(string Name, bool IsPooled = true, int InitialSize = 10)
        {

            if (Name == null || Name == "")
            {
                Debug.Log("You must specify a name to set the setting!");
                return;
            }

            if (!_settingDictionary.ContainsKey(Name))
            {
                _settingDictionary.Add(Name, new Setting());
            }

            _settingDictionary[Name].IsPooled = IsPooled;
            _settingDictionary[Name].InitialSize = InitialSize;

            if (IsPooled)
            {
                //Create Cache if not yet cached
                if (!_cacheDictionary.ContainsKey(Name))
                {
                    //Load that particular GameObject Prefab from "Resources/Prefabs/[Name]"
                    GameObject go = (GameObject)Resources.Load("Prefabs/" + Name, typeof(GameObject));
                    if (!go)
                    {
                        //Dont forget to yell the error!!!
                        Debug.LogError("Error: No Pool nor Prefab for such object named '" + Name + "'. Couldnt create a Cache for it!");
                    }
                    else
                    {
                        //There it is. Original one, create a Cache for it.
                        Cache cache = new Cache(go, _settingDictionary[Name].InitialSize);

                        //And Add it to the dictionary
                        _cacheDictionary.Add(Name, cache);
                    }

                }

            }

        }


        private static GameObject createErrorObject(string Name)
        {
            //create empty object
            GameObject go = new GameObject(Name);

            //Emit RED particles!!!
            //Its not possible to complately edit particle system in unity 5 script

            //Write ERROR!!!
            TextMesh tm = go.AddComponent<TextMesh>();
            tm.text = "[INVALID-NAME]";
            tm.alignment = TextAlignment.Center;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.color = Color.red;
            tm.characterSize = 0.05f;
            tm.fontSize = 50;

            //return an object that yells i have an error!!!
            return go;
        }
        private class Cache
        {

            private List<GameObject> active = new List<GameObject>();
            private Stack<GameObject> stack = new Stack<GameObject>();
            private GameObject original;

            public Cache(GameObject original, int InitialSize)
            {
                this.original = original;

                for (int i = 0; i < InitialSize; i++)
                {
                    GameObject go = GameObject.Instantiate(original);
                    go.name = original.name;
                    go.SetActive(false);
                    stack.Push(go);
                }

            }

            public GameObject Pop()
            {
                if (stack.Count <= 0)
                {
                    GameObject go = GameObject.Instantiate(original);
                    go.name = original.name;
                    active.Add(go);

                    return go;
                }
                else
                {
                    GameObject go = stack.Pop();
                    go.SetActive(true);
                    active.Add(go);

                    return go;
                }

                
            }

            public void Release(GameObject go)
            {
                active.Remove(go);

                go.SetActive(false);
                stack.Push(go);
            }

            public void ReleaseAll()
            {
                foreach (GameObject go in active)
                {
                    go.SetActive(false);
                    stack.Push(go);   
                }

                active.Clear();

            }

        }
        private class Setting
        {
            public bool IsPooled = true;
            public int InitialSize = 10;
        }

    }

    /// <summary>
    /// For use "this.Release();" inside scripts insted of "Pool.Relase(this);"
    /// and this.Get("[Asset_Name]");
    /// also this.Duplicate();
    /// </summary>
    public static class PoolExtention
    {
        public static void Release<T>(this T prefab) where T : Component
        {
            Pool.Release(prefab.gameObject);
        }

        public static void Release(this GameObject prefab)
        {
            Pool.Release(prefab);
        }

        public static GameObject Get<T>(this T prefab, string Name) where T : Component
        {
            return Pool.Get(Name);
        }

        public static GameObject Get(this GameObject prefab, string Name)
        {
            return Pool.Get(Name);
        }

    }

#if UNITY_EDITOR

    /// <summary>
    /// A small window for pool controll
    /// </summary>
    public class PoolWindow : EditorWindow
    {

        public string getFromPool = "";

        [MenuItem("Seyahdoo/Pool Controls")]
        static void Init()
        {
            EditorWindow window = GetWindow(typeof(PoolWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Seyahdoo Pool API");

            getFromPool = EditorGUILayout.TextField("To get from pool: ", getFromPool);
            if (GUILayout.Button("Get from Master Pool"))
            {
                Debug.Log(getFromPool);
                Pool.Get(getFromPool);
            }

            if (Selection.activeGameObject)
            {
                //i didnt like this code :(
                if (GUILayout.Button("Release to Master Pool"))
                {
                    List<GameObject> toLook = new List<GameObject>(Selection.gameObjects);
                    List<GameObject> toDestroy = new List<GameObject>(Selection.gameObjects);

                    foreach (GameObject go in toLook)
                    {
                        if (go.transform.parent)
                        {
                            if (toDestroy.Contains(go.transform.parent.gameObject))
                            {
                                toDestroy.Remove(go);
                            }
                        }

                    }
                    foreach (var item in toDestroy)
                    {
                        item.Release();
                    }
                }
            }

            this.Repaint();
        }


    }


#endif


}