using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMove : MonoBehaviour
{
    [SerializeField] private float distance = 4.6f;
    [SerializeField] private float depth = 0.1f;
    public GameObject treePrefab;
    public GameObject[] prefabs1;
    public GameObject[] prefabs2;
    public GameObject[] prefabs3;
    public GameObject[] prefabs4;
    public GameObject origin1;
    public GameObject origin2;
    public GameObject origin3;
    public GameObject origin4;

    private void Start()
    {
        prefabs1 = new GameObject[23];
        prefabs2 = new GameObject[23];
        prefabs3 = new GameObject[23];
        prefabs4 = new GameObject[23];

        CreateTree(origin1, prefabs1);
        CreateTree(origin2, prefabs2);
        CreateTree(origin3, prefabs3);
        CreateTree(origin4, prefabs4);

    }

    private void CreateTree(GameObject originTree, GameObject[] prefabs)
    {
        prefabs[0] = originTree;

        for (int i = 1; i < prefabs.Length; i++)
        {
            prefabs[i] = Instantiate(treePrefab, new Vector3(originTree.transform.position.x, prefabs[i - 1].transform.position.y + distance, prefabs[i - 1].transform.position.z + depth), transform.rotation);
        }

    }

    //private void LateUpdate()
    //{
    //    cameraPos = mainCamera.transform.position;
    //    targetPos = target.transform.position;
        
    //    if (target.transform.position.y >= 2f)
    //    {
    //        //transform.position = new Vector3(transform.position.x, targetPos.y - 1.7f, transform.position.z);
    //        transform.position = new Vector3(transform.position.x, cameraPos.y - 2f, transform.position.z);


    //    }


    //}
}
