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

    

    // é�Ϳ� ���� �������� �ε��/���� �Ѱ��ִ� �ڵ� �ۼ� (������ �޾ƿ����)
}
