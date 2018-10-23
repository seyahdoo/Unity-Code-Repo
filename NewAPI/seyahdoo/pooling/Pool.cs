/**
* This is an Pooling API that i wrote to reuse on my unity projects
* See my other reusable unity codes at:  https://github.com/seyahdoo/Unity-Code-Repo
* See my personal website at: http://seyahdoo.com
*
* 
* @author  Seyyid Ahmed Doğan (seyahdoo)
* @version 3.0.0
* @since   2018-10-23
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
        /// <param name="original">Original GameObject prefab that contains said Component</param>
        /// <param name="initialSize">Initial size of said Pool</param>
        /// <param name="maxSize">Max size of said pool</param>
        /// <returns>Success situation</returns>
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

                //if gameobject is found
                if (go == null)
                {
                    Debug.LogError("Couldnt create pool for " + typeof(T).FullName + ", and cannot find a prefab from \"Resources / Prefabs /[ClassName]\"");
                    return false;
                }
                else
                {
                    //Create pool with found GameObject
                    CreatePool<T>(go, initialSize, maxSize);
                }

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
        /// <typeparam name="T">Component type to be Released to pool</typeparam>
        /// <param name="garbage">Component to be released</param>
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
            /// active Components that are in game
            /// </summary>
            private List<Component> active = new List<Component>();
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
            public void CreateOne()
            {
                if ((active.Count + stack.Count + 1) > maxSize)
                {
                    Debug.LogError("Pool Reached max size of " + maxSize + " wont create new one.");
                    throw new Exception("Pool resize error exception");
                }

                GameObject go = GameObject.Instantiate(original);
                go.name = original.name;

                go.transform.SetParent(PoolRoot);

                go.SetActive(false);

                Component m = go.GetComponent(originalType);

                stack.Push(m);
            }
            

            public Component Get()
            {

                if (stack.Count <= 0)
                {
                    CreateOne();
                }

                Component m = stack.Pop();
                m.gameObject.SetActive(true);
                active.Add(m);

                m.gameObject.transform.SetParent(null);

                return m;
            }

            public void Release(Component m)
            {
                if (stack.Count >= maxSize)
                {
                    Debug.LogError("Pool Cache trying to grow bigger than its maxSize, please adress this issue");
                    return;
                }
                else
                {
                    if (!active.Contains(m))
                    {
                        Debug.LogError("This object is not a member of this pool or it is already released to pool, rejecting");
                        return;
                    }

                    if (stack.Contains(m))
                    {
                        Debug.LogError("This object is already released to pool, rejecting");
                        return;
                    }

                    active.Remove(m);

                    m.transform.SetParent(PoolRoot);

                    m.gameObject.SetActive(false);
                    stack.Push(m);
                }

            }

            public void ReleaseAll()
            {
                foreach (MonoBehaviour m in active)
                {
                    m.transform.SetParent(PoolRoot);

                    m.gameObject.SetActive(false);
                    stack.Push(m);
                }

                active.Clear();

            }

        }

        #endregion
    }
    
}


