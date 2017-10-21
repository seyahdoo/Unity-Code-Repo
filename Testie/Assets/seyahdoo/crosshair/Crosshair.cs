///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

///Changelog:
///30.03.2017 -> Created by seyahdoo
///31.03.2017 -> Added Layermask to raycast
///21.10.2017 -> Edited so that Target is more simple and Event driven,
///21.10.2017 -> Also improved upon how Raycast checks will be called

///usage:
///Attach to any camera or pointer and this will trigger Target objects

///Dependancies:
///seyahdoo.crosshair.Target (For it to be useful of course!?)


using UnityEngine;

namespace seyahdoo.crosshair
{

	/// <summary>
	/// Attach this to a camera, and it will do its stuff
	/// </summary>
	[AddComponentMenu("Seyahdoo/Crosshair/Crosshair")]
	public class Crosshair : MonoBehaviour
	{
		/// <summary>
		/// If i will check for targets every Frame
		/// </summary>
		public bool CheckOnEveryFrame = true;

		/// <summary>
		/// If i will check for targets every Fixed Update
		/// </summary>
		public bool CheckOnFixedUpdate = false;

		/// <summary>
		/// If i will check for targets every Button Down
		/// </summary>
		public bool CheckOnButtonPressDown = false;

		/// <summary>
		/// If i will check for targets every frame that any Button that holds down
		/// </summary>
		public bool CheckOnButtonPress = false;

		/// <summary>
		/// If i will check for targets every mouse movement within certain treshold
		/// </summary>
		public bool CheckOnMouseMovement = false;
		/// <summary>
		/// Treshold for CheckOnMouseMovement
		/// </summary>
		public float CheckOnMouseMovementTreshold = 0.01f;
		/// <summary>
		/// Previous position of mouse
		/// </summary>
		private Vector3 _mousePosPrevious = new Vector3();


		/// <summary>
		/// target that im focused
		/// </summary>
		private Target _target;
		/// <summary>
		/// collider that im focused
		/// </summary>
		private Collider _lastCollider;

		/// <summary>
		/// What layer to look for?
		/// </summary>
		public LayerMask layerMask = -1; //Default to everything

		/// <summary>
		/// Checks if Crosshair is looking into a Target
		/// </summary>
		void RaycastCheck ()
		{
			Debug.Log ("RaycastCheck");

			//do the raycasting
			RaycastHit hit;

			//if i found something
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, float.MaxValue, layerMask))
			{
				//did i found a new thing?
				if (_lastCollider != hit.collider)
				{
					//i should focus to that
					focusToCollider(hit.collider);
				}
			}
			else
			{
				//im not hitting anything, i should focus to nothing
				//unless im already focused to nothing
				if (_lastCollider != null)
				{
					focusToCollider(null);
				}

			}
		}

		/// <summary>
		/// Change current focus
		/// </summary>
		/// <param name="collider">collider?</param>
		void focusToCollider(Collider collider)
		{
			//if i was focused to anything im not focused anymore
			if (_target)
			{
				//Fire up events
				_target.HasFocus = false;
				_target.FocusOffEvent.Invoke();
				_target.FocusOff ();

				_target = null;
			}

			//if im not hitting anything, will be null
			_lastCollider = collider;

			//if im not looking to nothingness
			if (collider)
			{
				//im checking if this collider is a Target?
				_target = collider.gameObject.GetComponent<Target>();

				//if so, im focused to that
				if (_target)
				{
					//Fire up events
					_target.HasFocus = true;
					_target.FocusOnEvent.Invoke ();
					_target.FocusOn ();
				}
			}

		}


		void Update()
		{

			//Focus Stay Event
			if (_target) 
			{
				_target.FocusStayEvent.Invoke ();
				_target.FocusStay ();
			}

			//Update Check
			if (CheckOnEveryFrame) {
				RaycastCheck ();
				return;
			}

			//Button Pressed Down Check
			if (CheckOnButtonPressDown) {
				if (Input.anyKeyDown) {
					RaycastCheck ();
					return;
				}
			}

			//Button Press Check
			if (CheckOnButtonPress) {
				if (Input.anyKey) {
					RaycastCheck ();
					return;
				}
			}

			//Mouse Movement Check
			if (CheckOnMouseMovement) {
				Vector3 posNow = Input.mousePosition;
				if ((posNow-_mousePosPrevious).magnitude > CheckOnMouseMovementTreshold ) {
					RaycastCheck ();
				}
				_mousePosPrevious = posNow;
			}

		}


		void FixedUpdate()
		{

			//fixed Update Check
			if (CheckOnFixedUpdate) {
				RaycastCheck ();
			}
		}


	}



}


