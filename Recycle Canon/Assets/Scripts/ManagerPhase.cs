using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerPhase : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private City cityStatus;


    private void Update()
    {

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
