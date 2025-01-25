using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Animator SceneFader;


    public bool IncreasedRange;
    public bool ExplosiveBubbles;
    public bool bubbleJet;

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
       StartCoroutine(SceneTrasition(sceneNumber));
    }

    public void GoToStart()
    {
        StartCoroutine(SceneTrasition(1));
    }


    IEnumerator SceneTrasition(int goToScene)
    {
        SceneFader.SetTrigger("FadeStart");
        yield return new WaitForSeconds(.5f);
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


    
}
