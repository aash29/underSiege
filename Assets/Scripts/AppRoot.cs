using UnityEngine;
using System.Collections;

public class AppRoot : MonoBehaviour {


	///////////////////////////////////////////////////////////////////////////
	#region Variables
	
	// materials for highlight
	public Material SimpleMat;
	public Material HighlightedMat;
	static public int currentPlayer;
	

	// hotspots
	private string[] mGORoomsNames = new string[] 
	{
		"Room0",
		"Room1",
		"Room2"
	};

	// temp rectangle. It's create to do not re-create a new one on each frame
	private Rect mTmpRect = new Rect();
	
	// selected GameObject
	private GameObject mSelectedObject;

	// Use this for initialization
	void Start () 
	{
		//GameObject c1 = GameObject.Find("c1");
		//c1.rigidbody.AddForce(Vector3.right* 1,ForceMode.Impulse);



		mSelectedObject = GameObject.Find("floor");
	
	}
	#endregion
	///////////////////////////////////////////////////////////////////////////


	///////////////////////////////////////////////////////////////////////////
	#region Implementation
	
	private void SelectObjectByMousePos()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Constants.cMaxRayCastDistance))
		{
			// get game object
			GameObject rayCastedGO = hit.collider.gameObject;
			
			// select object
			this.SelectedObject = rayCastedGO;
		}
	}
	
	#endregion
	///////////////////////////////////////////////////////////////////////////


	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0))
		{
			SelectObjectByMousePos();
		}

	}

	///////////////////////////////////////////////////////////////////////////
	#region Properties
	
	/// <summary>
	/// Gets or sets selected GameObject
	/// </summary>
	public GameObject SelectedObject
	{
		get
		{
			return mSelectedObject;
		}
		set
		{
			// get old game object
			GameObject goOld = mSelectedObject;
			
			// assign new game object
			mSelectedObject = value;
			
			// if this object is the same - just not process this
			if (goOld == mSelectedObject)
			{
				return;
			}	
		

			parameters p1 = goOld.GetComponent<parameters>();

			if (p1.player != AppRoot.currentPlayer)
			{
				return;
			}

			
			// set material to non-selected object
			if (goOld != null)
			{
				goOld.renderer.material = SimpleMat;
			}
			
			// set material to selected object
			if ((mSelectedObject != null) && (mSelectedObject.name != "floor"))
			{
				mSelectedObject.renderer.material = HighlightedMat;
			}


			if ((goOld.name != "floor") && (mSelectedObject.name == "floor"))
			{
				Vector3 pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				goOld.rigidbody.AddForce(pt - goOld.transform.position,ForceMode.Impulse);
				AppRoot.currentPlayer=(AppRoot.currentPlayer+1)%2;

			}



		}
	}
	
	#endregion
	///////////////////////////////////////////////////////////////////////////





}
