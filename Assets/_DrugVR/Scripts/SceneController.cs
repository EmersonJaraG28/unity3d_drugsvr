using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public delegate void FinishSceneAction();
    public FinishSceneAction OnFinishScenAction;

    public GameObject popupPanel;
    public Text popupText;
    public AudioClip dialogSound;
    public List<string> messages;

    public Button exitBtn;

    public IEnumerator Start()
    {
        popupPanel.SetActive(false);
        popupPanel.GetComponent<CanvasGroup>().alpha = 0;
        exitBtn.gameObject.SetActive(false);
        popupPanel.GetComponent<CanvasGroup>().alpha = 0;

        popupText.GetComponent<CanvasGroup>().alpha = 0;

        GameObject sound = new GameObject();
        sound.transform.SetParent(transform);
        sound.AddComponent<AudioSource>();
        sound.GetComponent<AudioSource>().clip = dialogSound;
        sound.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(dialogSound.length);
        Destroy(sound);

        yield return new WaitForSeconds(1f);

        popupPanel.SetActive(true);
        popupPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        foreach (var item in messages)
        {
            popupText.text = item;
            popupText.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            yield return new WaitForSeconds(8f);
            popupText.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
        popupPanel.GetComponent<CanvasGroup>().DOFade(1,0.5f).OnComplete(()=> { popupPanel.SetActive(false); });

        yield return new WaitForSeconds(2f);
        exitBtn.onClick.AddListener(() => 
        {
            OnFinishScenAction?.Invoke();
        });
        exitBtn.gameObject.SetActive(true);
        popupPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnFinishScenAction?.Invoke();
        }
    }
}
