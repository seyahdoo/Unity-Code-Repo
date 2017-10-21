///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///30.03.2017 -> Created by seyahdoo
///31.03.2017 -> Renamed to Target from CrosshairAware
///21.10.2017 -> Simplified for Easier usage

///usage:
///Attach to any object and then you can watch HasFocus property or subscribe to FocusOnEvent and FocusOffEvent

///Dependancies:
///seyahdoo.crosshair.Crosshair

using UnityEngine;
using UnityEngine.Events;

namespace seyahdoo.crosshair
{
	/// <summary>
	/// Just add this to object you wanted to act according to crosshair.
	/// Then you can watch HasFocus property.
	/// Or you can subscribe to FocusOnEvent and FocusOffEvent.
	/// You can also derive a class from this and use as such.
	/// </summary>
	[AddComponentMenu("Seyahdoo/Crosshair/Target")]
	[RequireComponent (typeof (Collider))]
	public class Target : MonoBehaviour
	{

		/// <summary>
		/// Is crosshair looking to me?
		/// Coution: this propery is read only. Writing this will have no effect 
		/// </summary>
		public bool HasFocus = false;

		/// <summary>
		/// object just get focused event
		/// Usage: target.FocusOffEvent.AddListener(MethodName);
		/// </summary>
		public UnityEvent FocusOnEvent;

		/// <summary>
		/// object just get focused event
		/// </summary>
		virtual public void FocusOn() { }

		/// <summary>
		/// object just get unfocused event
		/// Usage: target.FocusOffEvent.AddListener(MethodName);
		/// </summary>
		public UnityEvent FocusOffEvent;

		/// <summary>
		/// object just get unfocused event
		/// </summary>
		virtual public void FocusOff() { }

		/// <summary>
		/// object stays on focus event
		/// called every Update cycle
		/// Usage: target.FocusOffEvent.AddListener(MethodName);
		/// </summary>
		public UnityEvent FocusStayEvent;

		/// <summary>
		/// object stays on focus event
		/// called every Update cycle
		/// </summary>
		virtual public void FocusStay() { }

	
	}


}
