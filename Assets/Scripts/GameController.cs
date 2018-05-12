using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    private State currentState;
    public GameObject inGamePanel;
    public Text scoreText;
    public GameObject gameOverPanel;
    public PlayerController player;
    public SpawnBox spawnBox;

    public int score = 0;

    public void Awake() // wir setzen sicher dass es immer existier aber immer nur eins
    {
        if(Instance != null)
        {
            DestroyImmediate(Instance); // es kann passieren wenn wir eine neue Scene laden dass immer noch eine Instanz existiert
        }
        else
        {
            Instance = this;
        }
    }

    private enum State
    {
        INGAME,
        GAMEOVER
    }

    public void SetState(string _state)
    {
        switch (_state)
        {
            case "INGAME":
                currentState = State.INGAME;
                break;
            case "GAMEOVER":
                currentState = State.GAMEOVER;
                break;
            default:
                Debug.Log("wrong stateName");
                break;
        }
    }

    void Update () {
        switch (currentState)
        {
            case State.INGAME:
                //set the score text
                scoreText.text = score.ToString();
                break;
            case State.GAMEOVER:
                GameOver();
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
	}

    void GameOver()
    {
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void AddScore(int _score)
    {
        score += _score;
    }

    public void ResetPlayerPosition() //resets position and spawns other Rocks 
    {
        player.transform.position = new Vector3(player.transform.position.x, 5f, 0f);
        spawnBox.Reload(2, 5);
    }
}
