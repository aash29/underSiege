using UnityEngine;
using System.Collections;

public class AppRoot : MonoBehaviour {


	///////////////////////////////////////////////////////////////////////////
	#region Variables
	
	// materials for highlight
	public Material SimpleMat;
	public Material HighlightedMat;
	static public int currentPlayer=0;
	public float baseforce = 35;
	public float maxforce = 700;
	

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



		mSelectedObject = GameObject.Find("frame");
	
	}
	#endregion
	///////////////////////////////////////////////////////////////////////////


	///////////////////////////////////////////////////////////////////////////
	#region Implementation
	
	private void SelectObjectByMousePos()
	{

		
		// Store the point where the user has clicked as a Vector3.
		Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Retrieve all raycast hits from the click position and store them in an array called "hits".
		RaycastHit2D[] hitInfo = Physics2D.LinecastAll (clickPosition, clickPosition);


		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		//RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
		
		if(hitInfo.Length > 0)
		{
			Debug.Log(hitInfo[0].collider.gameObject.name);

			GameObject rayCastedGO = hitInfo[0].collider.gameObject;

			this.SelectedObject = rayCastedGO;

		}

//		RaycastHit hit;
//		if (Physics2D.Raycast(ray, out hit, Constants.cMaxRayCastDistance))
//		{
//			// get game object
//			GameObject rayCastedGO = hit.collider.gameObject;
//			
//			// select object
//			this.SelectedObject = rayCastedGO;
//		}
	}
	
	#endregion
	///////////////////////////////////////////////////////////////////////////


	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0))
		{
			SelectObjectByMousePos();
		}

//		public static mouseSensitivity : float = 1.0;
//		static private Vector3 lastPosition ;
//		
//	
//	
//			if (Input.GetMouseButtonDown(0))
//			{
//				lastPosition = Input.mousePosition;
//			}
//			
//			if (Input.GetMouseButton(0))
//			{
//				var delta : Vector3 = Input.mousePosition - lastPosition;
//				transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
//				lastPosition = Input.mousePosition;
//			}


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

			
//			// set material to non-selected object
//			if (goOld != null)
//			{
//				goOld.renderer.material = SimpleMat;
//			}
			
//			// set material to selected object
//			if ((mSelectedObject != null) && (mSelectedObject.name != "frame"))
//			{
//				mSelectedObject.renderer.material = HighlightedMat;
//			}


			if ((goOld.name != "frame") && (mSelectedObject.name == "frame"))
			{
				Vector3 pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 hr = (pt - goOld.transform.position);
				hr.z=0;
				hr=hr*baseforce;
//				hr.Normalize();
				if ((hr).magnitude>maxforce) {
					hr.Normalize();
					hr = hr*maxforce;
				}
				goOld.rigidbody2D.AddForce(hr);
				AppRoot.currentPlayer=(AppRoot.currentPlayer+1)%2;

			}



		}
	}
	
	#endregion
	///////////////////////////////////////////////////////////////////////////





}
