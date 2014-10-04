using UnityEngine;
using System.Collections;

public class LaunchingBall : Photon.MonoBehaviour {
	
	public int teamId=0;
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	

	void Start () 
	{
		transform.position = Random.insideUnitSphere * 5;
	}

	// Update is called once per frame
	void Update () {
//		if (photonView.isMine) {
//			//Do Nothing - The Character motor/input/etc is moving us
//		} else {
//			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
//			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
//		}
		
		
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
				if (stream.isWriting) {
						//This is our player. We need to send our actual position to the network
						stream.SendNext (transform.position);
						stream.SendNext (transform.rotation);		
				} else {
						//This is someone else's player.  We need to recieve their position (as of a few 
						//milliseconds ago) and update our version of that player.
						realPosition = (Vector3)stream.ReceiveNext ();
						realRotation = (Quaternion)stream.ReceiveNext ();
				}
		
		}
}
