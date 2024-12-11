using UnityEngine;
using UnityEngine.UI;  // For UI components like Text and InputField
using UnityEngine.SceneManagement;  // For scene management

public class Console : MonoBehaviour
{
    private Canvas canvas;
    private Text consoleText;
    private InputField inputField;
    private GameObject inputFieldGO;
    private string[] scenes;

    private bool consoleActive = false;  // To track the console state (active/inactive)

    private void Start()
    {
        // Initialize the scenes list (this is a static example, modify if necessary)
        scenes = new string[] { "Scene1", "Scene2", "Scene3" }; // Modify with your actual scene names or dynamically gather them

        // Create the UI components
        CreateConsoleUI();

        // Initially hide the console
        SetConsoleVisibility(false);
    }

    // Create Canvas, Text, and InputField at runtime
    private void CreateConsoleUI()
    {
        // Create Canvas
        canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.gameObject.AddComponent<CanvasScaler>();
        canvas.gameObject.AddComponent<GraphicRaycaster>();

        // Create the console output Text
        GameObject textGO = new GameObject("ConsoleText");
        textGO.transform.SetParent(canvas.transform);
        consoleText = textGO.AddComponent<Text>();
        consoleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        consoleText.fontSize = 20;
        consoleText.color = Color.white;
        consoleText.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        consoleText.rectTransform.anchoredPosition = new Vector2(0, Screen.height / 4);
        consoleText.text = "Console Initialized\n";

        // Create the InputField
        inputFieldGO = new GameObject("InputField");
        inputFieldGO.transform.SetParent(canvas.transform);
        inputField = inputFieldGO.AddComponent<InputField>();
        inputField.textComponent = inputFieldGO.AddComponent<Text>();
        inputField.textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        inputField.textComponent.fontSize = 20;
        inputField.textComponent.color = Color.white;
        RectTransform inputFieldRectTransform = inputField.GetComponent<RectTransform>();
        inputFieldRectTransform.sizeDelta = new Vector2(Screen.width, 40);
        inputFieldRectTransform.anchoredPosition = new Vector2(0, -Screen.height / 4);

        // Add a listener to handle when the user presses Enter
        inputField.onEndEdit.AddListener(HandleCommandInput);
    }

    // Toggle the visibility of the console
    private void ToggleConsole()
    {
        consoleActive = !consoleActive;
        SetConsoleVisibility(consoleActive);
    }

    // Set visibility of the console (show/hide)
    private void SetConsoleVisibility(bool isVisible)
    {
        canvas.gameObject.SetActive(isVisible);
    }

    // Handle the command input by the user
    private void HandleCommandInput(string input)
    {
        inputField.text = "";  // Clear the input field after submission

        if (input.ToLower() == "scenes")
        {
            DisplayScenes();
        }
        else if (input.ToLower().StartsWith("loadscene"))
        {
            string sceneName = input.Substring(10).Trim(); // Get the scene name after "loadscene"
            LoadScene(sceneName);
        }
        else if (input.ToLower() == "retry")
        {
            RetryScene();
        }
        else if (input.ToLower() == "carrot")
        {
            CarrotResponse();
        }
        else if (input.ToLower() == "info")
        {
            ShowInfo();
        }
        else
        {
            consoleText.text += "\nUnknown command: " + input;
        }
    }

    // Display the available scenes
    private void DisplayScenes()
    {
        consoleText.text += "\nAvailable Scenes:\n";
        foreach (string scene in scenes)
        {
            consoleText.text += scene + "\n";
        }
    }

    // Load a scene by name
    private void LoadScene(string sceneName)
    {
        if (System.Array.Exists(scenes, scene => scene == sceneName))
        {
            SceneManager.LoadScene(sceneName);
            consoleText.text += "\nLoading scene: " + sceneName;
        }
        else
        {
            consoleText.text += "\nScene not found: " + sceneName;
        }
    }

    // Reload the current scene
    private void RetryScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        consoleText.text += "\nRetrying scene: " + currentScene;
    }

    // Respond to the "carrot" command
    private void CarrotResponse()
    {
        consoleText.text += "\nYou are not a carrot.";
    }

    // Show basic information about the console
    private void ShowInfo()
    {
        consoleText.text += "\nArticFox Console UA 0.2, This is for debugging only.";
    }

    // Update is called once per frame
    private void Update()
    {
        // Toggle console visibility with the ~ key
        if (Input.GetKeyDown(KeyCode.K)) // BackQuote is the ~ key
        {
            ToggleConsole();
        }
    }
}
