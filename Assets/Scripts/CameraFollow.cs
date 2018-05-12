using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    GameObject target;
    [SerializeField]
    float yOffset;
    [SerializeField]
    float zOffset;
    [SerializeField]
    float angle;

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(0, yOffset, target.transform.position.z - zOffset);
        transform.rotation = Quaternion.Euler(angle, 0.0f, 0.0f);

    }
}
