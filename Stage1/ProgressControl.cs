using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressControl : MonoBehaviour
{
    public Slider progressSlider;
    public float MaxHeight { get; set; }

    private float currentProgress;
    private float maxProgress;
    

    private void Start()
    {
        maxProgress = MaxHeight;
    }

    private void Update()
    {
        currentProgress = MaxHeight - transform.position.y;
    }

    private void LateUpdate()
    {
        progressSlider.value = currentProgress / maxProgress;

    }
}
