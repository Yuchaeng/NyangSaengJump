using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{
    None,
    OrangeTabby,
    Black,
    Calico,
    Gray,
    Siamese,
    SilverTabby,
    Tabby,
    Tuxedo,
    White
}


public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    //선택된 캐릭터
    public Character myCharacter;
    public Material myMaterial;

    //캐릭터 오브젝트 배열
    public List<GameObject> characterArray = new List<GameObject>(9);

    //캐릭터-enum 딕셔너리?
    //public Dictionary<GameObject, Character> objectOfCharacter = new Dictionary<GameObject, Character>();

    //캐릭터-머터리얼 딕셔너리
    //public Dictionary<Character, Material> materialOfCharacter = new Dictionary<Character, Material>();

    //public Dictionary<GameObject, Material> catAndMat = new Dictionary<GameObject, Material>();

    //머터리얼 인스펙터창에서 넣음
    public Material[] materials = new Material[9];

    public GameObject selectBtn;

    GameObject cat;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        selectBtn.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                GameObject touchedObject = hit.transform.gameObject;
                Debug.Log(touchedObject.name);
                
                int selectedIdx = characterArray.FindIndex(n => n == touchedObject);

                SelectCharacter selected = touchedObject.GetComponent<SelectCharacter>();
                //selected.cat.GetComponent<SkinnedMeshRenderer>().materials[1] = selected.material;

                Animator myAnim = touchedObject.GetComponent<Animator>();
                Animator other;

                myAnim.Play("sitting idle");

                for (int i = 0; i < characterArray.Count; i++)
                {
                    if (i == selectedIdx)
                        myAnim.Play("sitting idle");

                    else
                    {
                        other = characterArray[i].GetComponent<Animator>();
                        other.Play("none");
                    }

                }

                myCharacter = (Character)(selectedIdx + 1);
                myMaterial = materials[selectedIdx];

                selectBtn.SetActive(true);
            }
        }

    }

}
