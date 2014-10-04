using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class Launch : MonoBehaviour {

	const int MaxForce = 12 * 1000;
	const float ForceAcceleration = 3;

	DateTime clickStart;
	Boolean showText = false;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown () {
		clickStart = DateTime.Now;
		showText = true;
	}

	void OnMouseUp() {
		int forceCoeff = currentForceCoeff();
		rigidbody.AddForce (new Vector3(0,1,1) * forceCoeff);
		audio.volume = (float) forceCoeff / (float) MaxForce;
		audio.Play ();
	}

	void OnGUI() {
		if (showText) {
			string forceString = String.Format("Force: {0}%", ((float) currentForceCoeff() / MaxForce) * 100);
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), forceString);
		}
	}

 	private int currentForceCoeff() {
		TimeSpan clickLength = DateTime.Now - clickStart;
		return (int)Math.Min(MaxForce, clickLength.TotalMilliseconds * ForceAcceleration);
	}


	

}
