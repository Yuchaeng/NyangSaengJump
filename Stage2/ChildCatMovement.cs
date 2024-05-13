using System.Collections.Generic;
using UnityEngine;


public class ChildCatMovement : PlayerMoveBase
{
    public bool crashMonster;
    public GameObject monster;
    private MonsterCatMove monsterCS;
    public List<Vector3> steppedStair = new List<Vector3>();
    public bool isfalling;


    protected override float RayUpCastingDistance => 1.5f;
    protected override float RayUpDistance => 1.3f;
    protected override float RayDownCastingDistance => 0.7f;

    protected override void Start()
    { 
        base.Start();
        monsterCS = monster.GetComponent<MonsterCatMove>();

    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (currentStair!=null && transform.position.y <= currentStair.transform.position.y && !isfalling && monsterCS.isSpawn)
        {
            isfalling = true;

            Debug.Log("불량냥 따라오는 중인데 떨어짐");
        }

    }


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.transform.CompareTag("Stair") && transform.position.y > collision.transform.position.y
            && !steppedStair.Contains(collision.transform.position))
        {
            if (currentStair != collision.gameObject)
            {
                steppedStair.Add(currentStair.transform.position);
            }
        }  

        if (collision.gameObject.CompareTag("Enemy"))
        {
            crashMonster = true;
            StageManager.Instance.StopGame();
        }

        
    }
}
