using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string nombre;
        public Transform enemigo;
        public int contador;
        public float rate;
    }

    public Wave[] oleada;
    private int siguienteOleada = 0;

    //Puntos donde aparecen oleadas 
    public Transform[] spawnPoints;

    public float tiempoEntreOleadas = 5f;
    public float cuentaRegresiva = 0;

    public float buscarCuentaRegresiva = 1f;

    private SpawnState estado = SpawnState.COUNTING;
    
    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        cuentaRegresiva = tiempoEntreOleadas;
    }

    private void Update()
    {
        if (estado == SpawnState.WAITING)
        {
            if (!enemyIsAlive())
            {
                waveCompleted();
            }

            else
                return;
        }

        if (cuentaRegresiva<=0)
        {
            if (estado != SpawnState.SPAWNING)
            {
                //Aparición de enemigos
                StartCoroutine(SpawnWave(oleada[siguienteOleada]));
            }
        }

        else
        {
            cuentaRegresiva -= Time.deltaTime;
        }
    }

    bool enemyIsAlive()
    {
        buscarCuentaRegresiva -= Time.deltaTime;

        if (buscarCuentaRegresiva <= 0f)
        {
            buscarCuentaRegresiva = 1f;

            if (GameObject.FindGameObjectWithTag("Enemigo") == null)
            {
                return false;
            }
        }


        return true;
    }

    void waveCompleted()
    {
        Debug.Log("Oleada completada!");

        estado = SpawnState.COUNTING;

        cuentaRegresiva = tiempoEntreOleadas;

        if (siguienteOleada + 1 > oleada.Length - 1)
        {
            siguienteOleada = 0;

            Debug.Log("All waves complete! Looping...");
        }

        else
        {
            siguienteOleada++;
        }
    }

    IEnumerator SpawnWave(Wave _oleada)
    {
        Debug.Log("Spawing wave: " + _oleada.nombre);
        estado = SpawnState.SPAWNING;

        for (int i = 0; i <= _oleada.contador ; i++)
        {
            SpawnEnemy(_oleada.enemigo);
            yield return new WaitForSeconds(1f / _oleada.rate);
        }

        estado = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemigo)
    {
        Debug.Log("Spawning Enemy: " + _enemigo.name);

        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];

        Instantiate(_enemigo, _sp.position, _sp.rotation);
    }

}
