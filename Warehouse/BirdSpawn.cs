using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdSpawn : MonoBehaviour
{
    public GameObject spawnPosLeft;
    public GameObject bird;
    public GameObject player;

    public bool isCorouting;
    private Collider[] _birdColliders;



    private void Start()
    {
        Invoke("CoroutineStarter", 3f);
    }



    private void FixedUpdate()
    {
        spawnPosLeft.transform.position = new Vector3(player.transform.position.x - 2.5f, player.transform.position.y + 2f, 0f);

    }

    public void CoroutineStarter()
    {
        StartCoroutine(Spawn());
        isCorouting = true;

    }

    public IEnumerator Spawn()
    {
        bird.SetActive(true);
        bird.transform.position = spawnPosLeft.transform.position;
        Rigidbody birdRigid = bird.GetComponent<Rigidbody>();
        birdRigid.AddForce(new Vector3(1f, -0.5f, 0f).normalized * 4f, ForceMode.Impulse);

        yield return new WaitForSeconds(5f);

        isCorouting = false;
        bird.SetActive(false);

        Invoke("CoroutineStarter", 3f);

    }

  
}
