// ----------------------------------------------------------------------------
// Fast Unity Event System
//
// Written using Ryan Ripple's Awesome Unity Game Architecture Talk
//
// Author: Seyyid Ahmed Doğan (seyahdoo)
// Date:   31/01/18
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.events {

	[CreateAssetMenu]
	public class GameEvent : ScriptableObject
	{
		/// <summary>
		/// The list of listeners that this event will notify if it is raised.
		/// </summary>
		private readonly List<GameEventListener> eventListeners = 
			new List<GameEventListener>();

		public void Raise(object eventData)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--) 
				eventListeners [i].OnEventRaised (eventData);
		}

		public void RegisterListener(GameEventListener listener)
		{
			if (!eventListeners.Contains(listener))
				eventListeners.Add(listener);
		}

		public void UnregisterListener(GameEventListener listener)
		{
			if (eventListeners.Contains(listener))
				eventListeners.Remove(listener);
		}

	}

}

