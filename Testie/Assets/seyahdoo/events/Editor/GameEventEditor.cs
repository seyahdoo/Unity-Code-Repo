﻿// ----------------------------------------------------------------------------
// Fast Unity Event System
//
// Written using Ryan Hipple's Awesome Unity Game Architecture Talk
//
// https://youtu.be/raQ3iHhE_Kk
//
// Author: Seyyid Ahmed Doğan (seyahdoo)
// Date:   31/01/18
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace seyahdoo.events {

	[CustomEditor(typeof(GameEvent))]
	public class GameEventEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUI.enabled = Application.isPlaying;

			GameEvent e = target as GameEvent;
			//EventData d = target as EventData;

			if (GUILayout.Button("Raise"))
				e.Raise(null);
		}
	}

}