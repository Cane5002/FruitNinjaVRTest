using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject whole;
    public GameObject sliced;

    private Rigidbody ballRigidbody;
    private Collider ballCollider;

    public float deathSpan = 1f;

    public int points = 1;

    private void Awake() {
        ballRigidbody = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
    }

    private void Slice(Quaternion direction, Vector3 position, float force) {
        whole.SetActive(false);
        sliced.SetActive(true);

        ballCollider.enabled = false;

        //Line up split with blade
        //*rotation needs to be adjusted by 90 degrees on z-axis
        sliced.transform.rotation = direction * Quaternion.AngleAxis(-90, Vector3.forward);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices) {
            slice.velocity = ballRigidbody.velocity;
            slice.AddForceAtPosition(sliced.transform.forward * force * -1, position, ForceMode.Impulse);
            Debug.Log(direction.eulerAngles * force);
        }

        Destroy(this.gameObject, deathSpan);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Blade")) {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.transform.rotation, blade.transform.position, blade.sliceForce);
        }
    }
}
