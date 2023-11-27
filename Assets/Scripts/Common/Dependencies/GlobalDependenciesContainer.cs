using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependency
{
    [SerializeField] private Pauser pauser;

    private static GlobalDependenciesContainer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneloaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<Pauser>(pauser, monoBehaviourInScene);
    }

    private void OnSceneloaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }
}
