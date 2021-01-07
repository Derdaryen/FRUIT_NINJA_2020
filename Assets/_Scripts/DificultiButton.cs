using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultiButton : MonoBehaviour
{

    private Button button;
    public GameManager gameManager;
    [Range(1,3)]
    public int difficulty;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonLevel);
        gameManager = FindObjectOfType<GameManager>();
    }

   
    void ButtonLevel()
    {
        gameManager.StartGame(difficulty);
        Debug.Log("El boton " + gameObject.name + " ha sido pulsado");
    }
}
