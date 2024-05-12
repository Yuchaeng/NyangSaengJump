using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour
{
    public Button[] ChapterButton = new Button[4];
    public Image changeImg;

    public void OnClickChapter()
    {
        SceneManager.LoadScene(2);
    }


    public void ChangeNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();


        if (changeImg != null)
        {
            if (!changeImg.gameObject.activeSelf)
            {
                changeImg.gameObject.SetActive(true);
            }
            changeImg.DOFade(1f, 0.3f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(scene.buildIndex + 1);
            });
        }
        else
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }


    }

    

    // 챕터에 따라 스테이지 로드맵/정보 넘겨주는 코드 작성 (데이터 받아오기로)
}
