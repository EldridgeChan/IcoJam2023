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
    private AITreeHead aiTree;
    public AITreeHead AITree { get { return aiTree; } }
    [SerializeField]
    private GameDesignScriptableObject gameDesignScriptObj;
    public GameDesignScriptableObject GameDesignScriptObj { get { return gameDesignScriptObj; } }
    [SerializeField]
    private OverlayCanvasController canvasCon;
    public OverlayCanvasController CanvasCon { get { return canvasCon; } }

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
        if (!interMan) { interMan = GetComponent<InteractionManager>(); }
        if (!aiTree) { aiTree = GetComponent<AITreeHead>(); }
    }
}
