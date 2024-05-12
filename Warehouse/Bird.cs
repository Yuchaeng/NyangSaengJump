using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject spawnManager;
    public BirdSpawn spawn;

    private void Start()
    {
        spawn = spawnManager.GetComponent<BirdSpawn>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.transform.tag == "Player")
                Debug.Log("≥…¿Ã");
            else
                Debug.Log("≤Œ");

            gameObject.SetActive(false);

            if (spawn.isCorouting)
            {
                StopCoroutine(spawn.Spawn());
            }
        }
    }
}
