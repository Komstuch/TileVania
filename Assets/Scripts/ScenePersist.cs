using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    static int sceneIndexofTheLastScene = 0;

    int startingSceneIndex;
    int currentSceneIndex;
    private void Awake()
    {   //Singleton implementation
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndexofTheLastScene == actualSceneIndex)
        {
            int numScenePresists = FindObjectsOfType<ScenePersist>().Length;
            if (numScenePresists > 1)
            {
                Destroy(gameObject);
                Debug.Log("ScenePersist has been destoyed due to Singleton");
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    IEnumerator Singleton()
    {
        float yieldDuration;

        yield return new WaitForSecondsRealtime(Time.deltaTime);

        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
            Debug.Log("ScenePersist has been destoyed due to Singleton");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndexofTheLastScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        CheckIfStillInSameScene();
    }

    private void CheckIfStillInSameScene()
    {
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (actualSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
            Debug.Log("ScenePersist has been destoyed due to SceneChange");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
