using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

[CreateAssetMenu]
public class SomeManager : ScriptableObject {

	#region brilliant_part_pluggable_manager

	public string SomeManagerSetting;

	public void logit(string log){

		//Some high level function
		Debug.Log ("SomeManager: " + log);
		Debug.Log ("SomeManager:SomeManagerSetting: " + SomeManagerSetting);

	}

	#endregion

	#region silly_part_singleton
	public static SomeManager Instance(){

		if (_instance)
			return _instance;

		_instance = Resources.Load<SomeManager> ("Managers/" + MethodBase.GetCurrentMethod ().DeclaringType);
		if (_instance) {
			Debug.Log ("Returning loaded manager");
			return _instance;
		}

		_instance = SomeManager.CreateInstance<SomeManager> ();
		if (_instance) {
			Debug.Log ("Returning created manager");
			return _instance;
		}
		
		return null;
	}

	static SomeManager _instance;

	#endregion


}
