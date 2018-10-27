/**
* This is an Pooling API that i wrote to reuse on my unity projects
* See my other reusable unity codes at:  https://github.com/seyahdoo/Unity-Code-Repo
* See my personal website at: http://seyahdoo.com
*
* 
* @author  Seyyid Ahmed Doğan (seyahdoo)
* @version 3.2.2
* @since   2018-10-26
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Version 3 of seyahdoo general pool system
/// </summary>
namespace seyahdoo.pooling.v3
{

    /// <summary>
    /// A General API for pooling Components
    /// </summary>
    public class Pool
    {

        #region PoolRoot

        /// <summary>
        /// Root object to keep all pooled GameObjects
        /// </summary>
        private static Transform _poolRoot;

        /// <summary>
        /// Get Pool Root, Create one if not exists
        /// </summary>
        private static Transform PoolRoot
        {
            get
            {
                if (_poolRoot)
                {
                    return _poolRoot;
                }
                else
                {
                    GameObject go = new GameObject("Pool");
                    go.AddComponent<DontDestroy>();
                    go.transform.SetParent(null);

                    _poolRoot = go.transform;
                    return _poolRoot;
                }

            }
        }

        #endregion

        #region Dont Destroy Pool On Load

        /// <summary>
        /// this is for using Scene Unloaded Event and DontDestroyOnLoad
        /// </summary>
        private class DontDestroy : MonoBehaviour
        {

            private void Awake()
            {

                DontDestroyOnLoad(this.gameObject);

                SceneManager.sceneUnloaded += (Scene scene) =>
                {

                    //Sometimes in editor, this event is being fired with "Preview Scene", Even though there is no scene unloaded
                    if(scene.name != "Preview Scene")
                    {
                        Pool.RecoverAll();
                    }

                };

            }


        }

        #endregion

        #region Static Functions

        /// <summary>
        /// Create pool and set its initial settings
        /// </summary>
        /// <typeparam name="T">Type of component to be pooled</typeparam>
        /// <param name="original">Original GameObject prefab that contains the Component to be pooled</param>
        /// <param name="initialSize">Initial size of the pool</param>
        /// <param name="maxSize">Max size of the pool</param>
        /// <returns>Success</returns>
        public static bool CreatePool<T>(GameObject original = null, int initialSize = 10, int maxSize = 20) where T : Component
        {

            //if pool already created
            if (caches.ContainsKey(typeof(T)))
            {
                Debug.LogError("Pool Already Set! Returning.");
                return true;
            }

            //if original object not specified
            if(original == null)
            {
                //Load that particular GameObject Prefab from "Resources/Prefabs/[ClassName]"
                GameObject go = (GameObject)Resources.Load("Prefabs/" + typeof(T).Name, typeof(GameObject));

                //if gameobject is not found
                if (go == null)
                {
                    Debug.LogError("Couldnt create pool for " + typeof(T).FullName + ", and cannot find a prefab from \"Resources / Prefabs /[ClassName]\"");
                    return false;
                }

                //Set original object to found object
                original = go;
            }

            //if specified GameObject has required component
            if (original.GetComponent<T>() == null)
            {
                Debug.LogError("Couldnt create pool for " + typeof(T).FullName + ", because specified prefab doesnt have required component");
                return false;
            }

            //Create Cache object to store pooled objects
            Cache c = new Cache(original, typeof(T), initialSize, maxSize);

            //Store Cache to a dictionart to be found with type later
            caches.Add(typeof(T), c);

            //returns true for success
            return true;
        }

        /// <summary>
        /// Gets an Component from pool. 
        /// If no pool found, it will try to create a pool with a prefab from "Resources/Prefabs/[ClassName]"
        /// </summary>
        /// <typeparam name="T">Component type to be pulled from pool</typeparam>
        /// <returns>Component from pool</returns>
        public static T Spawn<T>() where T : Component
        {
            //Check if a cache exists for said Type
            if (!caches.ContainsKey(typeof(T)))
            {
                //Try to create pool from nothing, it may fail
                bool error = !CreatePool<T>();
                if (error)
                {
                    return null;
                }
            }

            //Find cache from dictionary
            Cache c = caches[typeof(T)];

            //Get a Component from Cache
            T t = c.Spawn() as T;
            
            //Return Component
            return t;
        }

        /// <summary>
        /// Recover the Component you once get from Pool. If it doesnt belong to a pool, it will do nothing.
        /// </summary>
        /// <typeparam name="T">Component type to be Recovered to the pool</typeparam>
        /// <param name="component">Component to be Recovered to the pool</param>
        public static void Recover<T>(T component) where T : Component
        {
            //Check if there is a pool for given Component Type
            if (!caches.ContainsKey(typeof(T)))
            {
                Debug.LogError("No pool found for this type " + typeof(T).ToString());
                return;
            }

            //Find cache from dictionary
            Cache c = caches[typeof(T)];

            //Recover the Component to found Cache
            c.Recover(component);

            return;
        }

        /// <summary>
        /// Recovers all active components of T type back to the Pool.
        /// </summary>
        /// <typeparam name="T">Component type to be Recovered</typeparam>
        public static void RecoverAll<T>() where T : Component
        {
            //Check if there is a pool for given Component Type
            if (!caches.ContainsKey(typeof(T)))
            {
                Debug.LogError("No pool found for this type " + typeof(T).ToString());
                return;
            }

            //Find cache from dictionary
            Cache c = caches[typeof(T)];

            //Collect all Component from game that originated from this pool
            c.RecoverAll();
        }

        /// <summary>
        /// Recovers all active components that belong to all pools
        /// </summary>
        public static void RecoverAll()
        {
            foreach (Cache c in caches.Values)
            {
                c.RecoverAll();
            }
        }

        /// <summary>
        /// Destroys pool for specified Type
        /// </summary>
        /// <typeparam name="T">Type of Component pool to be destroyed</typeparam>
        public static void DestroyPool<T>()
        {
            //Find cache from dictionary
            Cache c = caches[typeof(T)];

            c.DestroyAllBelongings();

            caches.Remove(typeof(T));

        }

        /// <summary>
        /// Destroys pool for all Types
        /// </summary>
        public static void DestroyAllPools()
        {

            foreach (Cache c in caches.Values)
            {
                c.DestroyAllBelongings();
            }

            caches.Clear();

        }

        #endregion

        #region Cache Class

        static readonly Dictionary<Type, Cache> caches = new Dictionary<Type, Cache>();

        /// <summary>
        /// Cache is a pool for a specific type of object, every pooled object will have their own Cache
        /// </summary>
        private class Cache
        {
            /// <summary>
            /// all components that belongs to this pool
            /// </summary>
            private List<Component> belongings = new List<Component>();

            /// <summary>
            /// pooled Componenets that are ready to be Get
            /// </summary>
            private Stack<Component> passives = new Stack<Component>();

            /// <summary>
            /// original gameobject prefab that contains said Component
            /// </summary>
            private GameObject prefab;

            /// <summary>
            /// original Type of Component to be pooled in this Cache
            /// </summary>
            private Type componentType;

            /// <summary>
            /// max size of this Cache
            /// </summary>
            private int maxSize;

            /// <summary>
            /// keeps track of components, says if this component is in the pool or not
            /// </summary>
            private Dictionary<Component, bool> inPool = new Dictionary<Component, bool>();

            public Cache(GameObject prefab, Type componentType, int initialSize = 10, int maxSize = 20)
            {

                this.prefab = prefab;
                this.componentType = componentType;
                this.maxSize = maxSize;

                for (int i = 0; i < initialSize; i++)
                {
                    Instantiate();
                }

            }

            /// <summary>
            /// Grows Cache by one
            /// </summary>
            /// <returns>success</returns>
            public bool Instantiate()
            {

                if ((belongings.Count) >= maxSize)
                {
                    Debug.LogError("Pool Reached max size of " + maxSize + " wont create new one.");
                    return false;
                }

                GameObject go = GameObject.Instantiate(prefab);
                Component c = go.GetComponent(componentType);

                go.SetActive(false);
                go.name = prefab.name;
                go.transform.SetParent(PoolRoot);

                belongings.Add(c);
                inPool.Add(c, true);
                passives.Push(c);

                IPoolable p = c as IPoolable;

                if(p != null)
                {
                    p.OnPoolInstantiate();
                }

                return true;
            }
            
            /// <summary>
            /// Gets a Component from pool
            /// </summary>
            /// <returns>a component removed from pool</returns>
            public Component Spawn()
            {
                if (passives.Count <= 0)
                {
                    if (!Instantiate())
                    {
                        return null;
                    }
                }

                Component c = passives.Pop();
                inPool[c] = false;

                c.gameObject.transform.SetParent(null);
                c.gameObject.SetActive(true);

                IPoolable p = c as IPoolable;

                if(p != null)
                {
                    p.OnPoolSpawn();
                }

                return c;
            }

            /// <summary>
            /// Recovers a Component to a pool once came from pool
            /// </summary>
            /// <param name="c">a Component came from pool</param>
            public void Recover(Component c)
            {

                if (!inPool.ContainsKey(c))
                {
                    Debug.LogError("This object is not a member of this pool, rejecting");
                    return;
                }

                if (inPool[c])
                {
                    Debug.LogError("This object is already recovered to pool, rejecting");
                    return;
                }

                c.gameObject.SetActive(false);
                c.transform.SetParent(PoolRoot);

                inPool[c] = true;
                passives.Push(c);

                IPoolable p = c as IPoolable;

                if (p != null)
                {
                    p.OnPoolRecover();
                }

            }

            /// <summary>
            /// Retracts all Components that came from pool
            /// </summary>
            public void RecoverAll()
            {

                foreach (Component c in belongings)
                {

                    if (inPool[c]) continue;

                    c.gameObject.SetActive(false);
                    c.transform.SetParent(PoolRoot);

                    inPool[c] = true;
                    passives.Push(c);

                    IPoolable p = c as IPoolable;

                    if (p != null)
                    {
                        p.OnPoolRecover();
                    }

                }

            }

            /// <summary>
            /// Destroys all gameobjects that belong to this pool
            /// </summary>
            public void DestroyAllBelongings()
            {

                foreach (Component c in belongings)
                {
                    GameObject.Destroy(c.gameObject);
                }

                belongings.Clear();
                passives.Clear();
                inPool.Clear();

            }

        }

        #endregion

    }

    #region IPoolable Interface

    /// <summary>
    /// Used for keeping track of Pool Events inside poolable Component
    /// </summary>
    public interface IPoolable
    {

        void OnPoolInstantiate();

        void OnPoolRecover();

        void OnPoolSpawn();

    }

    #endregion
}


