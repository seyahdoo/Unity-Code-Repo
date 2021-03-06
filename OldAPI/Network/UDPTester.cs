﻿///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using System.Collections;

using System.Net;

public class UDPTester : MonoBehaviour {

	void Start(){
		//UDP.ReceiveDataEvent += OnReceive;

        UDP.SubscribeReceiveData(OnReceive);
	}

	void OnReceive(IPAddress Adress,int Port,string Message){
		print ("received from: " + Adress + ":" + Port + " -> " + Message);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.P)) {
			print ("Sending");
			UDP.SendData("127.0.0.1",21,"Java Test");
		}

	}
}
