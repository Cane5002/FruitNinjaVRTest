using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private Collider spawnArea;

    public GameObject[] ballPrefabs;

    public float startSpawnDelay = 1f;
    public float spawnDelayRamp = .95f;

    public float startSpeed = 18f;
    public float speedRamp = 1.05f;

    public float maxLifetime = 10f;

    private void Awake() {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable() {
        StartCoroutine(Spawn());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
    private IEnumerator Spawn() {
        float spawnDelay = startSpawnDelay;
        float speed = startSpeed;
        while (enabled) {

            GameObject prefab = ballPrefabs[0];//ballPrefabs[Random.Range(0, ballPrefabs.Length)];

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = spawnArea.transform.position.z;

            prefab = Instantiate(prefab, position, Quaternion.identity);
            Destroy(prefab, maxLifetime);

            prefab.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, speed * -1);

            spawnDelay *= spawnDelayRamp;
            speed *= speedRamp;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

