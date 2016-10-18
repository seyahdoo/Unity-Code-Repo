using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class UdpTest {

	[Test]
	public void SendDataLocal()
	{
        UDP.SendData("127.0.0.1",21,"Hello World!!!");
        
        Assert.Pass();
	}


}
