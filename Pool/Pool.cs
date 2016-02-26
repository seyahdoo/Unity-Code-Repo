using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

    public string ObjectName;
    
    /// <summary>
    /// List of objects in pool waiting to be used.
    /// </summary>
    public List<GameObject> Objects;

    /// <summary>
    /// List of objects out of the pool, in use.
    /// </summary>
    public List<GameObject> ObjectsInUse;

    /// <summary>
    /// The original object to pool
    /// </summary>
    public GameObject Original;
    
    void Awake()
    {
        if(Objects == null)
        {
            Objects = new List<GameObject>();
        }

        if(ObjectsInUse == null)
        {
            ObjectsInUse = new List<GameObject>();
        }
    }

    /// <summary>
    /// Gives One Object from pool.
    /// Usage: GameObject FromPool = Pool.GetFromPool();
    /// </summary>
    public GameObject GetFromPool()
    {
        
        GameObject ToReturn;

        if (Objects.Count > 0)
        {
            //if there is an object in pool
            ToReturn = Objects[0];
            Objects.RemoveAt(0);


            //Dont lose the referance
            //Why? i dont know!
            //nevermind, i deleted it

            ///TODO Maybe we should cashe PoolMember referance?
            //More event thingies YAAAY!!!
            ToReturn.GetComponent<PoolMember>().OnPoolOut();

            //dont lose referance
            ObjectsInUse.Add(ToReturn);

            return ToReturn;
        }

        if(Original == null)
        {
            Debug.LogError("Object Pool is empty. And we cant Instantiate new poll member because Original is missing. Returning A cube.");
            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        
        Debug.LogWarning("Object Pool For " + Original.name + "is empty!"+
            " Creating a new object and adding to the pool");
        
        //else then make a new object
        ToReturn = Instantiate(Original);

        //Add PoolMember Script
        ToReturn.AddComponent<PoolMember>();
        //PoolMember pm = ToReturn.AddComponent<PoolMember>();

        //its on the pool
        //pm.OnPoolEnter(); 
           
        //aaaand out
        //pm.OnPoolOut();

        //this "if" is for my compiler :] thanks for all the warnings you give me, i can write code more elegantly.
        //Really thanks... Much thanks..
        if(ToReturn != null)
        {
            //dont lose referance
            ObjectsInUse.Add(ToReturn);
            
            return ToReturn;
        }

            
        Debug.LogError("::ERROR WE CANT INSTANTIATE A NEW OBJECT IN POOL."+
                        " WE ARE PRETTY FUCKED UP RIGHT NOW:::");
        //aaw nevermind i can give him a cube. that will solve it
        return GameObject.CreatePrimitive(PrimitiveType.Cube); 
        
    }

    /// <summary>
    /// Adds An object to pool
    /// </summary>
    /// <param name="ToGive">Object to add to the pool. ATTENTION! it must be like original, or else...</param>
    public void AddNew(GameObject ToGive)
    {
        //Deactivate object for storage
        ToGive.SetActive(false);

        //Add PoolMember Script if there is NOTT
        PoolMember pm = ToGive.GetComponent<PoolMember>();
        if (pm == null)
        {
            pm = ToGive.AddComponent<PoolMember>();
            //and if we fail to add PoolMember to object. <EPIC-FAIL>
            if (pm == null)
                Debug.LogError("There is a problem on Pool.AddNew, We cant add PoolMember to it.  ''Sorry man. I just cant...''");
            
        }

        //set pool for new PoolMember
        pm.MyPool = this;

        //Trigger On Pool Enter. We want it to be deactivated and ready to be pooled
        pm.OnPoolEnter();

        //And Add to pool as usual
        Objects.Add(ToGive);
    }


    /// <summary>
    /// Returns used object to pool
    /// </summary>
    /// <param name="ToReturn"></param>
    public void ReturnOld(GameObject ToReturn)
    {
        //Trigger On Pool Enter. We want it to be deactivated and ready to be pooled
        ToReturn.GetComponent<PoolMember>().OnPoolEnter();

        //And Add to pool as usual
        Objects.Add(ToReturn);
    }

    /// <summary>
    /// Headstarts a pool and grows it.
    /// </summary>
    /// <param name="Size">Number of objects to instantiate</param>
    public void InstantiatePool(int Size)
    {
        //Debug.Log("Instantiating pool for , " + ObjectName + "," + Size + "Times");

        //What if there is no original object here
        if(Original == null)
        {
            Debug.LogError("Cannot Instantiate Pool, Original is empty");
        }

        for (int i = 0; i < Size; i++)
        {
            
            //make a new Object From Original
            GameObject ToAdd = Instantiate(Original);

            //deactivate object for storage
            ToAdd.SetActive(false);

            //Add PoolMember Script
            PoolMember pm = ToAdd.AddComponent<PoolMember>();
            pm.MyPool = this;

            //Trigger Event
            pm.OnPoolEnter();
            
            //Add to Pool
            Objects.Add(ToAdd);
        }

    }
    
    /// <summary>
    /// Returns all Pool members to pool. 
    /// </summary>
    public void ReturnAll()
    {
        foreach (GameObject member in ObjectsInUse)
        {
            //return object to pool
            member.GetComponent<PoolMember>().ReturnPool();
        }

    }


    //INFINITE LOOP ALERT!!!!!!!!
        //if created by pool manager...

    //On Created set yourself as pool in poolmanager
    //void Awake()
    //{
    //    PoolManager.Instance.SetPool(ObjectName, this);
    //}
    
}
