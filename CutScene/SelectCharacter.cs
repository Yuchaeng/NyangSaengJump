using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public Material material;
    public GameObject cat;


    private void Start()
    {
        cat = transform.GetChild(1).gameObject;
        

    }
    private void Update()
    {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    myAnimator.Play("sitting idle");

        //    //SelectManager.instance.myMaterial = SelectManager.instance.materialOfCharacter[character];
        //}
        //else if (!Input.GetMouseButtonDown(0))
        //{
        //    myAnimator.Play("none");
           
        //}
    }
}
