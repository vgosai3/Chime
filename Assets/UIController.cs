using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of UIController.
    /// </summary>
    public static UIController Instance { get; private set; }

    // Game objects for various UI elements
    private GameObject _HUD;
    private GameObject _Quest;
    private GameObject _PauseScreen;
    private GameObject _DeathScreen;
    private GameObject _DialogueScreen;
    private DialogueBoxController _DialogueBoxController;



    private void Awake()
    {
        // Initialize singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Get UI canvas
        var canvas = transform.Find("UI_Canvas");

        // Get pause screen
        _PauseScreen = canvas.Find("UI_PauseScreen").gameObject;
        _PauseScreen.SetActive(false);

        // Get death screen
        _DeathScreen = canvas.Find("UI_DeathScreen").gameObject;
        _DeathScreen.SetActive(false);
        Globals.Player.OnDeath += DeathScreenHandler;

        // Get dialogue screen
        _DialogueScreen = canvas.Find("UI_DialogueScreen").gameObject;
        _DialogueScreen.SetActive(false);
        // Get dialogue controller
        _DialogueBoxController = _DialogueScreen.GetComponent<DialogueBoxController>();
        // Attach function on dialogue end to show HUD and hide dialogue screen
        DialogueBoxController.OnDialogueEnd += () =>
        {
            _DialogueScreen.SetActive(false);
            _HUD.SetActive(true);
        };

        // Get HUD
        _HUD = canvas.Find("UI_HUD").gameObject;

        var _UI_TopRight = _HUD.transform.Find("UI_TopRight");

        _Quest = _UI_TopRight.transform.Find("UI_Quest").gameObject;
    }

    void Update()
    {
        // Disable HUD when in conversation
        _HUD.SetActive(!Globals.Player.IsInConversation);

        // Handle pause screen key inputs
        bool esc = Input.GetKeyDown(KeyCode.Escape);
        if (esc)
        {
            SetPauseScreen(Globals.IsPaused);
        }
    }

    /// <summary>
    /// Handler for death screen called when player dies.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="isDead">Whether the player is dead.</param>
    private void DeathScreenHandler(object sender, bool isDead)
    {
        if (isDead)
        {
            Time.timeScale = 0f;
            _DeathScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Set the pause screen state. If the game is currently paused (true), it will unpause, and vice versa.
    /// </summary>
    /// <param name="isPaused">The current pause state of the game.</param>
    public void SetPauseScreen(bool isPaused)
    {
        _PauseScreen.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        Globals.IsPaused = !isPaused;
    }

    /// <summary>
    /// Loads the specified scene.
    /// </summary>
    /// <param name="sceneName">The scene name.</param>
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene(sceneName);
    }

    public void StartDialogue(DialogueAsset dialogueAsset, int startPosition)
    {
        _DialogueScreen.SetActive(true);
        _HUD.SetActive(false);
        _DialogueBoxController.StartDialogue(dialogueAsset, startPosition);
    }

    public void SkipLine()
    {
        _DialogueBoxController.SkipLine();
    }
}
