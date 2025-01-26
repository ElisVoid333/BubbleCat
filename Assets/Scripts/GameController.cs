using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Animator SceneFader;

    public bool IncreasedRange;
    public bool ExplosiveBubbles;
    //public bool bubbleJet;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        
    }

    public void GoToNextScene()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
       // SceneManager.LoadScene(sceneNumber+1);

        //TODO : add +1 when we have other Scenes
       StartCoroutine(SceneTrasition(sceneNumber+1));
    }

    public void GoToStart()
    {
        StartCoroutine(SceneTrasition(0));
    }


    IEnumerator SceneTrasition(int goToScene)
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneFader.SetTrigger("FadeStart");
        yield return new WaitForSeconds(.5f);
        //gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(goToScene);
        yield return new WaitForSeconds(.5f);
        SceneFader.SetTrigger("FadeEnd");
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GoToNextScene();
        }
    }

    public void PlayerDeath()
    {
        GoToStart();
    }

    public void GiveAbility(int num)
    {
        switch (num)
        {
            case 0:
                IncreasedRange = true;
                break;
            case 1:
                ExplosiveBubbles = true;
                break;
            case 2:
                //bubbleJet = true;
                break;

        }
    }

    public bool HasAbility(int num)
    {
        switch (num)
        {
            case 0:
                return IncreasedRange;
                break;
            case 1:
                return ExplosiveBubbles;
                break;
            case 2:
                //return bubbleJet;
                break;

        }
        return false;

    }

}
