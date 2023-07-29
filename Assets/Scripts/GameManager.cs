using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private InteractionManager interMan;
    public InteractionManager InterMan { get { return interMan; } }
    [SerializeField]
    private GameDesignScriptableObject gameDesignScriptObj;
    public GameDesignScriptableObject GameDesignScriptObj { get { return gameDesignScriptObj; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        if (interMan == null) { interMan = GetComponent<InteractionManager>(); }
    }


}
