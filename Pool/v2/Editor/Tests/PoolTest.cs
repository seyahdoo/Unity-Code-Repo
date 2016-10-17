using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PoolTester
{
    [Test]
    public void SpawnFromPoolExceedingLimit()
    {
        Pool.SetSetting("BlueCapsule", true, 10);

        for (int i = 0; i < 12; i++)
        {
            
            GameObject go = Pool.Get("BlueCapsule");
            go.transform.position = new Vector3(0, 3, 0);
            go.transform.rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);

        }

        Assert.Pass();
    }


}