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
			//Will take mouse input and use it for movement.
			mouseXAxis += Input.GetAxis ("Mouse X") * 20;
			mouseYAxis -= Input.GetAxis ("Mouse Y") * 20;
			//Makes sure the mouseYAxis is within 360 degrees. Clamps to make sure the camera doesn't go crazy.
			mouseYAxis = Mathf.Clamp(mouseYAxis%360, cameraNegativeYRotation, cameraPositiveYRotation);
			//Using a Quaternion since they are easier to use.
			Quaternion rotation = Quaternion.Euler(mouseYAxis, mouseXAxis, 0);

			//This is for bug testing. Hopefully in future, the camera controller will be able to
			//dynamically change position back to original distanceMaxOffset.
			//Commented out just in case it is needed for the future.
			//distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, cameraDistanceMinOffset, cameraDistanceMaxOffset);
			//Creating a new RaycastHit for information about if the camera is doing something strange.
			RaycastHit hitinfo;
			//If a raycast from the attached object to the camera hits something, then move camera closer to player.
			if (Physics.Raycast (playerTransform.position, transform.position, out hitinfo)) {
				//Does movement via Lerp to ensure the camera doesn't jump to new position.
				distance = Mathf.Lerp (distance, hitinfo.distance, Time.deltaTime);
			//If there isn't anything to worry about then just return the camera to the original distance.
			} else {
				distance = Mathf.Lerp (distance, cameraDistanceMaxOffset, cameraMoveSpeed * Time.deltaTime);
			}
			//This is to ensure that the camera will always return to the original distance offset.
			if (distance > cameraDistanceMaxOffset) {
				distance = cameraDistanceMaxOffset;
			}
			//This places the camera behind the player.
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			//This is where the camera will look at.
			positionToLookAt = playerTransform.position + new Vector3 (0.0f, verticalLookOffset, 0.0f);
			//This is just compacting all this information into a vector3
			Vector3 position = rotation * negDistance + positionToLookAt;
			//An object will that needs to rotate with the camera will follow suit.
			objectToFollowRotation.transform.rotation = rotation;
			//This will transforms the camera's rotation and position to the new values.
			transform.rotation = rotation;
			transform.position = position;
		}
	}
}
