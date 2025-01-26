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
    public AudioClip mainMusic;
    public AudioClip bossMusic;
    public AudioSource soundMusic;

    [SerializeField] BossFightChecker FightChecker;

    private void Awake()
    {
        soundMusic.clip = mainMusic;
        soundMusic.Play();

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

        if (FightChecker.bossfightEnded == true && soundMusic.clip != mainMusic)
        {
            soundMusic.clip = mainMusic;
            soundMusic.Play();
        }
        else if (FightChecker.bossfightStarted == true && soundMusic.clip != bossMusic && FightChecker.bossfightEnded == false)
        {
            soundMusic.clip = bossMusic;
            soundMusic.Play();
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
