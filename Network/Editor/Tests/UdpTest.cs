///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

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
