using UnityEngine;
using System.Collections;

public class gui : MonoBehaviour {
	
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), AppRoot.currentPlayer.ToString());

	}
}