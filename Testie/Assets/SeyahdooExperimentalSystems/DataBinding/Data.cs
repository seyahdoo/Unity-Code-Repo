using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif


[CreateAssetMenu]
public class Data : ScriptableObject   {

	public enum DataType
	{
		String,
		Integer,
		Float
	}

	public DataType type;

	public object value;

	public void SetValue(object value){
	
		this.value = value;
		TriggerValueChanged ();
	}


	public delegate void ValueChangedDelegate(Data data);
	public event ValueChangedDelegate OnValueChanged;

	private void TriggerValueChanged(){
		if (OnValueChanged != null)
			OnValueChanged (this);
	}

	// User-defined conversion from Data to T
	public static implicit operator string(Data d)
	{
		if (d.value.GetType() == typeof(string)) {
			return (string)d.value;
		}
		if (d.value.GetType() == typeof(float)) {
			return ((float)d.value).ToString ();
		}
		if (d.value.GetType() == typeof(int)) {
			return ((int)d.value).ToString ();
		}

		return null;
	}

	public static implicit operator int(Data d)
	{
		if (d.value.GetType() == typeof(string)) {
			return int.Parse((string)d.value);
		}
		if (d.value.GetType() == typeof(float)) {
			return (int)((float)d.value);
		}
		if (d.value.GetType() == typeof(int)) {
			return (int)d.value;
		}

		Debug.LogError ("Couldnt convert Data to int!");
		return 0;
	}

	public static implicit operator float(Data d)
	{
		if (d.value.GetType() == typeof(string)) {
			return float.Parse((string)d.value);
		}
		if (d.value.GetType() == typeof(float)) {
			return (float)d.value;
		}
		if (d.value.GetType() == typeof(int)) {
			return (float)((int)d.value);
		}

		Debug.LogError ("Couldnt convert Data to float!");
		return 0f;
	}

}

#if UNITY_EDITOR

[CustomEditor(typeof(Data))]
public class GameEventEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//GUI.enabled = Application.isPlaying;

		Data d = target as Data;

		//Draw Data Field
		switch (d.type) {
		case Data.DataType.Float:
			if (d.value == null)
				d.value = 0f;

			float floatvalue = EditorGUILayout.FloatField ((float)d.value);
			if (floatvalue != (float)d.value)
				d.SetValue (floatvalue);

			break;
		case Data.DataType.Integer:
			if (d.value == null)
				d.value = 0;

			int intvalue = EditorGUILayout.IntField ((int)d.value);
			if (intvalue != (int)d.value)
				d.SetValue (intvalue);

			break;
		case Data.DataType.String:
			if (d.value == null)
				d.value = "";

			string strvalue = EditorGUILayout.TextField ((string)d.value);
			if (strvalue != (string)d.value)
				d.SetValue (strvalue);

			break;

		}

		//Event Invoker Button
		if (GUILayout.Button("Invoke SetValue Event"))
			d.SetValue(d.value);
	}
}

#endif