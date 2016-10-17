using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> {

    public List<PoolObject> PoolObjects;

    [System.Serializable]
    public class PoolObject
    {
        public string Name;
        public GameObject Sample;
        public bool ToBePooled;
        public int DesiredPoolSize = 20;
        public Pool Pool;
    } 
    
    /// <summary>
    /// Sets a Pool for given gameobject name
    /// Eg. SetPool("Enemy1",pool);
    /// </summary>
    /// <param name="TypeName">Name of gameobject to pool</param>
    /// <param name="Pool">Pool object to set</param>
    public void SetPool(string TypeName,Pool Pool)
    {
        foreach (PoolObject po in PoolObjects)
        {
            if(po.Name == TypeName)
            {
                po.Pool = Pool;
                po.ToBePooled = true;
                po.Sample = Pool.Original;
                Debug.Log("Pool is set for " + TypeName);
                return;
            }
            
        }

        Debug.LogError("There is no such object in PoolManager. Creating One");

        PoolObject poo = new PoolObject();
        poo.Name = TypeName;
        poo.Pool = Pool;
        poo.ToBePooled = true;
        poo.Sample = Pool.Original;
        PoolObjects.Add(poo);
        
    }
	

    /// <summary>
    /// Returns a gameobject for given name
    /// Creates one object from sample if there is no pool 
    /// Eg. Gameobject go = GetGameObject("Enemy1");
    /// </summary>
    /// <param name="Type">Name of Object</param>
    /// <returns>Gameobject like Sample </returns>
	public GameObject GetGameObject(string Type)
    {
        //try to find object in list
        PoolObject po;
        po = PoolObjects.Find(x => x.Name == Type);
        
        if(po == null)
        {
            Debug.LogWarning("PoolManager: There is no such object i recognize as " + Type + "??? Returning Cube");
            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        if(po.ToBePooled)
        {
            //if its being pooled

            //if there is pool
            if(po.Pool)
            {
                //Debug.Log("there is pool for " + po.Name);

                //then return from pool
                return po.Pool.GetFromPool();
            }
            
            //then make a pool

            //if there is a sample? 
            if(po.Sample != null)
            {
                //create a pool
                po.Pool = CreatePool(po.Sample,po.DesiredPoolSize);
                //and return from pool.
                return po.Pool.GetFromPool();
            }

            //there is no sample!? how do you expect me to return you something???
            //returning a cube.
            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        //if its not being pooled, tehn we make one and return it.

        if(po.Sample != null)
        {
            //if there is a sample
            //make and pack (meyk and peyk :)
            GameObject toReturn;
            toReturn = Instantiate(po.Sample);
            toReturn.AddComponent<NonPoolMember>();

            return toReturn;
        }
        
        //how do you expect me to give an object to you if there is no sample???
        return GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    
    /// <summary>
    /// Creates pool for Sample object
    /// </summary>
    /// <param name="Sample">Gameobject to create pool for</param>
    /// <param name="Size">Size of pool</param>
    /// <returns>New Pool object full of objects</returns>
    public Pool CreatePool(GameObject Sample,int Size)
    {
        GameObject go = new GameObject(Sample.name);
        go.transform.parent = this.transform;
        Pool p = go.AddComponent<Pool>();
        p.Original = Sample;
        p.ObjectName = Sample.name;
        p.InstantiatePool(Size);
        
        return p;
    }

    /// <summary>
    /// Use this to give back your poolable object
    /// Eg. ReturnObject(UsedEnemy);
    /// Destroys object if not poolable.
    /// </summary>
    /// <param name="ToReturn">A poolable object</param>
    public void ReturnObject(GameObject ToReturn)
    {
        //if its in a pool return it to his pool
        PoolMember pm = ToReturn.GetComponent<PoolMember>();
        if(pm != null)
        {
            pm.ReturnPool();
            return;
        }

        //than its a NonPoolMember
        NonPoolMember npm = ToReturn.GetComponent<NonPoolMember>();
        if(npm != null)
        {
            //make it gone
            npm.Begone();
            return;
        }

        //else destroy him
        Destroy(ToReturn);
    }

    public void ReturnAllObjects()
    {
        //For every Object type
        foreach (PoolObject po in PoolObjects)
        {
            //if there is a pool
            if (po.Pool != null)
            {
                //return all objects to pool
                po.Pool.ReturnAll();
            }
        }
    
    }

}

