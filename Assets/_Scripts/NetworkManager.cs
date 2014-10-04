using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	public bool offLineMode = false;
	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType <SpawnSpot>();
		Connect ();
	}
	
	// 
	void Connect () {
		if (offLineMode) {
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		} else {
				PhotonNetwork.ConnectUsingSettings ("ScatterJam Springs and Fences V002");	
		}
	}

	void OnGUI() {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinOrCreateRoom("MyRoom",null,null);
	}

	void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer() {
		//Debug.Log ("SpawnMyPlayer");

		if (spawnSpots == null) {
			Debug.Log(":Issues With SpawnSpot");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots[ Random.Range (0, spawnSpots.Length) ];
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);
		
		((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("CharacterMotor")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("Launch")).enabled = true;
		myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
		Destroy(mySpawnSpot);
	}


}
