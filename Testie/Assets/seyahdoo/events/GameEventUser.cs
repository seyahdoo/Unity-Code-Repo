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

	public abstract class GameEventUser : MonoBehaviour {

		/// <summary>
		/// Invoke the specified methodname with eventData.
		/// </summary>
		/// <param name="methodname">Methodname.</param>
		/// <param name="eventData">Event data as object</param>
		public abstract void OnEventInvoked (string methodname, object eventData);

	}

}