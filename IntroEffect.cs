using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroEffect : MonoBehaviour
{
    public Image designName;
    public Image techName;
   // public Image changeImg;

    public TextMeshProUGUI startMessage;

    public LoopType loopType;


    private void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(designName.DOFade(1.0f, 1));
        mySequence.Append(designName.DOFade(0.0f, 1));

        mySequence.Append(techName.DOFade(1.0f, 1));
        mySequence.Append(techName.DOFade(0.0f, 1));

        mySequence.OnComplete(() =>
        {
            startMessage.gameObject.SetActive(true);
            startMessage.DOFade(0.0f, 1.5f).SetLoops(-1, loopType);
        });

    }

    //public void OnClickStart()
    //{
    //    Scene scene = SceneManager.GetActiveScene();

    //    changeImg.DOFade(1f, 0.3f)
    //        .OnComplete(() =>
    //        {
    //            SceneManager.LoadScene(scene.buildIndex + 1);
    //        });
    //}

}
