using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    //variables aleatorias de fuerza e impulso
    private float randoRotation = 10, xRange = 4, yPawnPost=3;
    private float forceMax = 15, forceMin= 13;

    private Rigidbody _rigidbody;

    private GameManager gameManager;
    [Range(-20, 20)]

    public int pointValue;
    public ParticleSystem explotionParticle;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(),ForceMode.Impulse);
        _rigidbody.AddTorque(TorqueRandomPosition(),TorqueRandomPosition(),TorqueRandomPosition());
        transform.position = RandomPosition();

        //Formas de Comunicarse con un Script Diferente
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();


        
    }

    /// <summary>
    /// Genera un Vector aleatorio  con una fuerza minima y maxima hacia arriba
    /// </summary>
    /// <returns>Fuerza aleatoria minima y maxima hacia arriba</returns>
     private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(forceMin, forceMax);
    }
    /// <summary>
    /// Genera una Rotacion aleatoria para los objetos
    /// </summary>
    /// <returns>Una Rotacion aleatoria entre los Rangos dados</returns>
    private float TorqueRandomPosition()
    {
        return Random.Range(-randoRotation, randoRotation);
    }
    /// <summary>
    /// Genera una posicion aleatoria entre xRange X, YPawnsPost
    /// </summary>
    /// <returns>Posicion entre XRange y yPawnsPost</returns>
    private Vector3 RandomPosition()
    {
        return  new Vector3(Random.Range(-xRange, xRange), -yPawnPost);//z = 0
    }


    

    private void   OnMouseOver()
    {

        if (gameManager.gameState == GameManager.GameState.inGame) {
            Destroy(gameObject);
            Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);
            gameManager.UpDateScore(pointValue);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("GoodFood"))
            {
                //gameManager.UpDateScore(-10);
                gameManager.GameOverText();
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
