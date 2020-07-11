using UnityEngine;

public class PlayerRaycastInteractor : MonoBehaviour
{
	public InteractionUI interactionUI = null;
	public Transform startingPoint;

	public float distance = 1f;
	public KeyCode interactKey;
	public KeyCode moveKey;

	public Material defaultMaterial;
	public Material outlineMaterial;
	GameObject obj;


	private void Start()
	{
		if (startingPoint == null)
		{
			Debug.LogError("No starting point found for the raycast : Assigning this gameObject's transform...");
			startingPoint = transform;
		}
	}


	// Update is called once per frame
	void Update()
	{
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(startingPoint.position, transform.GetComponent<PlayerController>().GetLastInputDir(), distance);
		Debug.DrawRay(startingPoint.position, transform.GetComponent<PlayerController>().GetLastInputDir(), Color.red);

		if (hit.collider != null && (hit.collider.GetComponent<Movable>() != null || hit.collider.GetComponent<IInteractable>() != null))
		{
			GameObject tmp = hit.collider.gameObject;
			if (tmp != obj)
			{
				if (obj != null) obj.GetComponent<SpriteRenderer>().material = defaultMaterial;
				obj = tmp;
			}
			obj.GetComponent<SpriteRenderer>().material = outlineMaterial;

			//Movable
			if (hit.collider.GetComponent<Movable>() != null)
			{
				//Start moving
				if (Input.GetKeyDown(moveKey))
				{
					obj = hit.collider.gameObject;
					obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
					obj.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
					obj.GetComponent<FixedJoint2D>().enabled = true;
					obj.GetComponent<Movable>().beingPushed = true;
				}
			}
			//End moving
			if (Input.GetKeyUp(moveKey) && obj != null && obj.GetComponent<Movable>().beingPushed)
			{
				obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
				obj.GetComponent<FixedJoint2D>().enabled = false;
				obj.GetComponent<Movable>().beingPushed = false;
				obj = null;
			}

			//Interactable
			else if (hit.collider.GetComponent<IInteractable>() != null)
			{
				if (Input.GetKeyDown(interactKey))
				{
					obj.GetComponent<IInteractable>().Interact();
				}
			}
		}
		else if (obj != null) obj.GetComponent<SpriteRenderer>().material = defaultMaterial;
	}
}
