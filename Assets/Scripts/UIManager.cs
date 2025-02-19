using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject player;

    private void Start()
    {
        progressBar.value = 0;

        levelText.text = "Level "  + (ChunkManager.instance.GetLevels() + 1).ToString();

        GameManager.onGameStateChanged += GameStateChangetCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangetCallBack;
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangetCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
            Show_GameOver_Panel();
        else if(gameState == GameManager.GameState.LevelComplete)
            Show_LevelComplete_Panel();
    }

    public void PLay_ButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void Retry_Button_Pressed()
    {
        SceneManager.LoadScene(0);
    }

    public void Show_LevelComplete_Panel()
    {
        player.SetActive(false);
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }
    
    public void Show_GameOver_Panel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if(!GameManager.instance.isGameState())
            return;


        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }
}
