///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///30.03.2017 -> Created by seyahdoo
///31.03.2017 -> Renamed to Target from CrosshairAware

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
    [RequireComponent (typeof (Collider))]
    public class Target : MonoBehaviour
    {

        private bool _hasFocus = false;

        /// <summary>
        /// Is crosshair looking to me?
        /// </summary>
        public bool HasFocus
        {
            get
            {
                return _hasFocus;
            }
        }

        public delegate void VoidDelegate();

        /// <summary>
        /// object just get focused event
        /// Usage: target.FocusOffEvent.AddListener(MethodName);

        /// </summary>
        public UnityEvent FocusOnEvent;

        /// <summary>
        /// Override me
        /// </summary>
        virtual protected void FocusOn() { }

        /// <summary>
        /// object just get unfocused event
        /// Usage: target.FocusOffEvent.AddListener(MethodName);
        /// </summary>
        public UnityEvent FocusOffEvent;
        
        /// <summary>
        /// Override me
        /// </summary>
        virtual protected void FocusOff() { }

        public void setFocus(bool value)
        {
            //Nothing to change? Cool.
            if (_hasFocus == value) return;

            //update focus
            _hasFocus = value;

            //trigger events
            if (value)
            {
                //internal event
                FocusOn();

                //external events
                FocusOnEvent.Invoke();

            }else
            {
                //internal event
                FocusOff();

                //external events
                FocusOffEvent.Invoke();

            }
            
        }
        
    }


}
