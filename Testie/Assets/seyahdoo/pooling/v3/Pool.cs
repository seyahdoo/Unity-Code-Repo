/**
* This is an Pooling API that i wrote to reuse on my unity projects
* See my other reusable unity codes at:  https://github.com/seyahdoo/Unity-Code-Repo
* See my personal website at: http://seyahdoo.com
*
* 
* @author  Seyyid Ahmed Doğan (seyahdoo)
* @version 3.0.1
* @since   2018-10-26
*/

using System;
using System.Collections.Generic;
using UnityEngine;

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
        /// Root object to keep all pool GameObjects
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
                    GameObject go = GameObject.Find("PoolRoot");

                    if (!go)
                    {
                        go = new GameObject("PoolRoot");
                    }

                    _poolRoot = go.transform;
                    return _poolRoot;
                }

            }
        }

        private static Transform _poolRoot;
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
        public static bool CreatePool<T>(GameObject original = null, int initialSize = 10, int maxSize = 20) where T : MonoBehaviour
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
        public static T Get<T>() where T : MonoBehaviour
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
            T t = c.Get() as T;
            
            //Return Component
            return t;
        }

        /// <summary>
        /// Releases the Component you once get from Pool. If it doesnt belong to a pool, it will do nothing.
        /// </summary>
        /// <typeparam name="T">Component type to be Released to the pool</typeparam>
        /// <param name="garbage">Component to be released to the pool</param>
        public static void Release<T>(T garbage) where T : MonoBehaviour
        {
            //Check if there is a pool for given Component Type
            if (!caches.ContainsKey(typeof(T)))
            {
                Debug.LogError("No pool found for this type " + typeof(T).ToString());
                return;
            }

            //Find cache from dictionary
            Cache c = caches[typeof(T)];

            //Release Component to found Cache
            c.Release(garbage);

            return;
        }

        /// <summary>
        /// Releases all active components back to the Pool.
        /// </summary>
        /// <typeparam name="T">Component type to be Released</typeparam>
        public static void ReleaseAll<T>() where T : MonoBehaviour
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
            c.ReleaseAll();
        }

        #endregion

        #region Caches
        static readonly Dictionary<Type, Cache> caches = new Dictionary<Type, Cache>();

        private class Cache
        {
            /// <summary>
            /// all components that belongs to this pool
            /// </summary>
            private List<Component> belongings = new List<Component>();
            /// <summary>
            /// pooled Componenets that are ready to be Get
            /// </summary>
            private Stack<Component> stack = new Stack<Component>();
            /// <summary>
            /// original gameobject prefab that contains said Component
            /// </summary>
            private GameObject original;
            /// <summary>
            /// original Type of Component to be pooled in this Cache
            /// </summary>
            private Type originalType;
            /// <summary>
            /// max size of this Cache
            /// </summary>
            private int maxSize;
            /// <summary>
            /// keeps track of components, says if this component is in the pool or not
            /// </summary>
            private Dictionary<Component, bool> inPool = new Dictionary<Component, bool>();

            public Cache(GameObject original, Type originalType, int initialSize = 10, int maxSize = 20)
            {
                this.original = original;
                this.originalType = originalType;
                this.maxSize = maxSize;

                for (int i = 0; i < initialSize; i++)
                {
                    CreateOne();
                }

            }

            /// <summary>
            /// Grows Cache by one
            /// </summary>
            /// <returns>success</returns>
            public bool CreateOne()
            {
                if ((belongings.Count) >= maxSize)
                {
                    Debug.LogError("Pool Reached max size of " + maxSize + " wont create new one.");
                    return false;
                }

                GameObject go = GameObject.Instantiate(original);
                go.name = original.name;
                go.SetActive(false);

                go.transform.SetParent(PoolRoot);

                Component c = go.GetComponent(originalType);

                belongings.Add(c);

                inPool.Add(c, true);

                IPoolable p = c as IPoolable;

                if(p != null)
                {
                    p.OnPoolInstantiate();
                }

                stack.Push(c);

                return true;
            }
            
            /// <summary>
            /// Gets a Component from pool
            /// </summary>
            /// <returns>a component removed from pool</returns>
            public Component Get()
            {

                if (stack.Count <= 0)
                {
                    if (!CreateOne())
                    {
                        return null;
                    }
                }

                Component c = stack.Pop();

                c.gameObject.SetActive(true);
                c.gameObject.transform.SetParent(null);
                inPool[c] = false;

                IPoolable p = c as IPoolable;

                if(p != null)
                {
                    p.OnPoolGet();
                }

                return c;
            }

            /// <summary>
            /// Releases a Component to a pool once came from pool
            /// </summary>
            /// <param name="c">a Component came from pool</param>
            public void Release(Component c)
            {
                if (stack.Count >= maxSize)
                {
                    Debug.LogError("Pool Cache trying to grow bigger than its maxSize, please adress this issue");
                    return;
                }
                else
                {

                    if (!inPool.ContainsKey(c))
                    {
                        Debug.LogError("This object is not a member of this pool, rejecting");
                        return;
                    }

                    if (inPool[c])
                    {
                        Debug.LogError("This object is already released to pool, rejecting");
                        return;
                    }

                    c.gameObject.SetActive(false);
                    c.transform.SetParent(PoolRoot);
                    inPool[c] = true;

                    IPoolable p = c as IPoolable;

                    if (p != null)
                    {
                        p.OnPoolRelease();
                    }
                    
                    stack.Push(c);
                }

            }

            /// <summary>
            /// Retracts all Components that came from pool
            /// </summary>
            public void ReleaseAll()
            {
                foreach (Component c in belongings)
                {
                    if (inPool[c]) continue;

                    c.gameObject.SetActive(false);
                    c.transform.SetParent(PoolRoot);
                    inPool[c] = true;

                    IPoolable p = c as IPoolable;

                    if (p != null)
                    {
                        p.OnPoolRelease();
                    }

                    stack.Push(c);

                }

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
        void OnPoolRelease();
        void OnPoolGet();

    }
    #endregion
}


