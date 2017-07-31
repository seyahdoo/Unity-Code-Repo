/**
* This is an Pooling API that i wrote to reuse on my unity projects
* See my other reusable unity codes at:  https://github.com/seyahdoo/Unity-Code-Repo
* See my personal website at: http://seyahdoo.com
*
* 
* @author  Seyyid Ahmed Doğan (seyahdoo)
* @version 2.3.1
* @since   2017-03-30
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

        public static Transform PoolRoot
        {
            get
            {
                if (!_poolRoot)
                {
                    GameObject go = GameObject.Find("PoolRoot");

                    if (!go)
                    {
                        go = new GameObject("PoolRoot");
                    }

                    _poolRoot = go.transform;
                    
                }

                return _poolRoot;
            }
        }

        private static Transform _poolRoot;

        private static Dictionary<string, Setting> _settingDictionary = new Dictionary<string, Setting>();
        private static Dictionary<string, Cache> _cacheDictionary = new Dictionary<string, Cache>();

        /// <summary>
        /// Gets an object from pool regarding its name. Object prefab must be located at /Assets/Resources/Prefabs/[Name]
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
        /// Releases the object you once get from Pool. If it doesnt belong to a pool, it will be destroyed.
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
                    }else
                    {
                        Debug.LogError("Error: This object is not set to be pooled. name: " + name);
                    }
                }
                else
                {
                    Debug.LogError("Error: This object does not have a setting, did you forget to specify settings for ->"+name);
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

            //TODO: Emit RED particles!!!
            //note: Its not possible to complately edit particle system in unity 5 script

            //Write ERROR!!!
            TextMesh tm = go.AddComponent<TextMesh>();
            tm.text = "[INVALID-NAME]";
            tm.alignment = TextAlignment.Center;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.color = Color.red;
            tm.characterSize = 0.05f;
            tm.fontSize = 50;

            //And Log
            Debug.LogError("Created invalid name object! Sometings must be going crazy.");

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

                    go.transform.SetParent(PoolRoot);

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

                    go.transform.SetParent(null);

                    return go;
                }

                
            }

            public void Release(GameObject go)
            {
                active.Remove(go);

                go.transform.SetParent(PoolRoot);

                go.SetActive(false);
                stack.Push(go);
            }

            public void ReleaseAll()
            {
                foreach (GameObject go in active)
                {
                    go.transform.SetParent(PoolRoot);

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

        public string stringX = "";

        [MenuItem("Tools/Seyahdoo/Pool Controls")]
        static void Init()
        {
            EditorWindow window = GetWindow(typeof(PoolWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Seyahdoo Pool API");

            stringX = EditorGUILayout.TextField("x ->", stringX);
            if (GUILayout.Button("Pool.Get(x)"))
            {
                //Debug.Log(getFromPool);
                Pool.Get(stringX);
            }

            if (Selection.activeGameObject)
            {
                //i didnt like this code :(
                //i think i fixed the love issue here
                if (GUILayout.Button("Pool.Release(Selected)"))
                {

                    List<GameObject> toRelease = new List<GameObject>(Selection.gameObjects);
                    
                    foreach (var item in toRelease)
                    {
                        item.Release();
                    }
                }
            }
            else
            {
                GUILayout.Label("Select objects to use Release object function");
            }

            this.Repaint();
        }


    }


#endif


}