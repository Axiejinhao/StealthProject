using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadAsyncSence : MonoBehaviour
{
    private Slider slider;
    private Text hintText;
    private Text progressText;
    private AsyncOperation async;
    private float progress;

    private void Awake()
    {
        slider = transform.GetChild(1).GetComponent<Slider>();
        hintText = transform.GetChild(0).GetComponent<Text>();
        progressText = transform.Find("Slider/ProgressText").GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("Stealth");
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                progress = async.progress;
            }
            else
            {
                progress = 1.0f;
            }
            slider.value = progress;
            progressText.text = ((int)(slider.value * 100)).ToString() + "%";

            if (slider.value >= 0.9f)
            {
                hintText.text = "点击任意键,进入游戏";
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
