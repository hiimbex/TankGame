using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {
	//Current issue. Causing janky movement. 
	public GameObject playerObject;
	public GameObject objectToFollowRotation;

	public float cameraHeight = 2.0f;
	public float cameraDistanceMaxOffset = 6.0f;
	public float cameraDistanceMinOffset = 1.0f;

	public float cameraPositiveYRotation = 80.0f;
	public float cameraNegativeYRotation = -20.0f;

	public float verticalLookOffset = 2.0f;
	public float cameraMoveSpeed = 10.0f;

	Transform playerTransform;
	float mouseXAxis;
	float mouseYAxis;

	private Vector3 positionToLookAt;

	float distance;

	// Use this for initialization
	void Start () {
		
		if (playerObject) {
			//Work on programmatically setting all children in playerObject to the correct layer.
			playerObject.layer = 2;
			playerTransform = playerObject.transform;
			transform.position = playerTransform.position + new Vector3(0.0f,cameraHeight,cameraDistanceMaxOffset);
		}

	}

	void LateUpdate () {
		//Checks to make sure that there is a transform.
		if (playerTransform) {
			mouseXAxis += Input.GetAxis ("Mouse X");
			mouseYAxis -= Input.GetAxis ("Mouse Y");

			mouseYAxis = Mathf.Clamp(mouseYAxis%360, cameraNegativeYRotation, cameraPositiveYRotation);

			Quaternion rotation = Quaternion.Euler(mouseYAxis, mouseXAxis, 0);

			//This is for bug testing. Hopefully in future, the camera controller will be able to
			//dynamically change position back to original distanceMaxOffset.
			//Commented out just in case it is needed for the future.
			//distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, cameraDistanceMinOffset, cameraDistanceMaxOffset);

			RaycastHit hitinfo;
			if (Physics.Raycast (playerTransform.position, transform.position, out hitinfo)) {
				distance = Mathf.Lerp (distance, hitinfo.distance, Time.deltaTime);
			} else {
				distance = Mathf.Lerp (distance, cameraDistanceMaxOffset, cameraMoveSpeed * Time.deltaTime);
			}
			if (distance > cameraDistanceMaxOffset) {
				distance = cameraDistanceMaxOffset;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			positionToLookAt = playerTransform.position + new Vector3 (0.0f, verticalLookOffset, 0.0f);
			Vector3 position = rotation * negDistance + positionToLookAt;

			objectToFollowRotation.transform.rotation = rotation;
			transform.rotation = rotation;
			transform.position = position;
		}
	}
}
