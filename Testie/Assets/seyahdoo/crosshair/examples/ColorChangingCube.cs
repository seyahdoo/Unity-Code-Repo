using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace seyahdoo.crosshair.examples
{

	/// <summary>
	/// Crosshair example cube script, will go blue if stared by crosshair
	/// </summary>
	[RequireComponent (typeof (Target))]
	[RequireComponent (typeof (MeshRenderer))]
	public class ColorChangingCube : MonoBehaviour
	{

		private Target target;
		private Material m;

		void Awake()
		{

			m = GetComponent<MeshRenderer>().material;
			target = GetComponent<Target>();

			target.FocusOnEvent.AddListener(ColorBlue);
			target.FocusOffEvent.AddListener(ColorRed);

		}

		void ColorBlue()
		{
			m.color = Color.blue;
		}

		void ColorRed()
		{
			m.color = Color.red;
		}

	}



}

