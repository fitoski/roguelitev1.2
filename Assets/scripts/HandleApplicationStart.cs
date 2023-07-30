using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleApplicationStart : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.LoadData();

        SceneManager.LoadScene(1);
    }
}
