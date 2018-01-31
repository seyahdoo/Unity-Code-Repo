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

	public class GameEventListener : MonoBehaviour {

		[Tooltip("Event to register with.")]
		public GameEvent Event;

		[Tooltip("Response method name of invoke when Event is raised.")]
		public string ResponseMethodName;

		[Tooltip("Gameobject to invoke when Event is raised.")]
		public GameEventUser EventUser;

		private void OnEnable()
		{
			Event.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void OnEventRaised(object eventData)
		{
			EventUser.OnEventInvoked (ResponseMethodName, eventData);
		}

	}

}