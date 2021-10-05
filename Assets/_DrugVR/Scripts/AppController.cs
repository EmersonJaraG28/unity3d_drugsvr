using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject bannerPanel;
    public GameObject doorsPanel;
    public GameObject doorModels;

    [Header("Buttons")]
    public Button initBtn;
    public Button exitBtn;
    public Button continueBtn;

    [Header("App Data")]
    public AudioClip openDoorSound;
    public List<AppData> appDatas;

    private AppData currentData;
    private GameObject currentScene;

    private void Start()
    {
        initBtn.onClick.AddListener(InitOnClick);
        exitBtn.onClick.AddListener(ExitOnClick);
        continueBtn.onClick.AddListener(EnableDoorPanel);

        //set event to btns
        foreach (var item in appDatas)
        {
            item.doorBtn.onClick.AddListener(() => 
            {
                item.doorAnim.SetTrigger("open");
                item.opened = true;
                item.doorBtn.gameObject.SetActive(false);
                GetComponent<AudioSource>().PlayOneShot(openDoorSound);
                EnableDoorsBtn(false);
                currentData = item;
                StartCoroutine(LoadScene());
            });
        }
    }

    private void InitOnClick()
    {
        mainPanel.SetActive(false);
        bannerPanel.SetActive(true);
    }

    private void ExitOnClick()
    {
        Application.Quit();
    }

    private void EnableDoorPanel()
    {
        bannerPanel.SetActive(false);
        doorsPanel.SetActive(true);
        doorModels.SetActive(true);
    }

    private void EnableDoorsBtn(bool value)
    {
        if (value == false)
        {
            foreach (var item in appDatas)
            {
                item.doorBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var item in appDatas)
            {
                if(item.opened == false)
                item.doorBtn.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        currentScene = Instantiate(currentData.scene);
        currentScene.GetComponent<SceneController>().OnFinishScenAction += ChangeScene;
        doorModels.SetActive(false);
        doorsPanel.SetActive(false);
    }

    private void ChangeScene()
    {
        Destroy(currentScene);

        EnableDoorPanel();
        EnableDoorsBtn(true);
    }
}

[System.Serializable]
public class AppData
{
    public Button doorBtn;
    public Animator doorAnim;
    public GameObject scene;
    public bool opened;
}
