using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = 0.1f;
	public float rotateTime = 0.1f;
	public LayerMask blockingLayer;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;
	private float inverseMoveTime;
	private float inverseRotateTime;
	protected Animator animator;
	protected bool rotating = false;
	protected bool moving = false;

	[SerializeField]
	private float baseRotation;

	[SerializeField]
	private ArduinoGyroscope gyro;

	[SerializeField]
	[Range(1f, 4f)]
	private float rotationMultiplier = 1f;

	// Use this for initialization
	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1 / moveTime;
		inverseRotateTime = 1 / rotateTime;
		baseRotation = rb2d.rotation;
		animator = GetComponent<Animator> ();
	}

	protected bool Move(int xDir, int yDir, out RaycastHit2D hit){

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;
		if (hit.transform == null) {
			StartCoroutine (SmoothMovement (end));
			return true;
		} else {
			return false;
		}
	}

	protected virtual void Rotate(int dir){		
		float current = baseRotation;
		float end = (current + dir * -90);
		StartCoroutine (SmoothRotate (end));	
	}

	protected virtual void AttemptMove<T> (int xDir, int yDir)
		where T : Component {
		RaycastHit2D hit;
		bool canMove = Move(xDir, yDir, out hit);
		if (hit.transform == null) {
			return;
		}

		T hitComponent = hit.transform.GetComponent<T> ();
		if (!canMove && hitComponent != null) {
			OnCantMove(hitComponent);
		}
	}
	
	protected IEnumerator SmoothMovement(Vector3 end){
		if (!moving) {
			animator.SetTrigger ("PlayerMove");
			moving = true;
			float sqRemainingDistance = (transform.position - end).sqrMagnitude;
			while (sqRemainingDistance > float.Epsilon) {
				Vector3 newPosition = Vector3.MoveTowards (rb2d.position, end, inverseMoveTime * Time.deltaTime);
				rb2d.MovePosition (newPosition);
				sqRemainingDistance = (transform.position - end).sqrMagnitude;
				yield return null;
			}
			moving = false;
		}

	}

	protected IEnumerator SmoothRotate(float angle){
		if (!rotating) {
			rotating = true;
			rb2d.MoveRotation(baseRotation);
			float rotationRemaining = rb2d.rotation - angle;
			while (Mathf.Abs(rotationRemaining) > float.Epsilon)  {
				float newRotation = Mathf.MoveTowards(rb2d.rotation, angle, inverseRotateTime * Time.deltaTime);
				baseRotation = newRotation;
				rb2d.MoveRotation(newRotation);
				rotationRemaining = rb2d.rotation - angle;
				yield return null;
			}
			rb2d.rotation = angle;
			baseRotation = angle;
			while (rb2d.rotation > 360.0f) {
				rb2d.rotation -= 360.0f;
			}
			while (rb2d.rotation < -360.0f) {
				rb2d.rotation += 360.0f;
			}
			rotating = false;
		}
	}

	protected virtual void Update()
	{
		if(!rotating)
		{
			if(gyro.IsCalibrated)
			{
				rb2d.MoveRotation(baseRotation -  gyro.GetGyroData().X * rotationMultiplier);
			}
		}
	}

	protected abstract void OnCantMove<T> (T component)
		where T : Component;
}
