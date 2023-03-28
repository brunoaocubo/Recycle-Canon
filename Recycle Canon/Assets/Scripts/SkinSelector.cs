using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] charactersSelect;
    [SerializeField] private int indexSelectedCharacter;

    public void PlayGame() 
    {
        PlayerPrefs.SetInt("indexSelectedCharacter", indexSelectedCharacter);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void NextCharacter() 
    {
        charactersSelect[indexSelectedCharacter].SetActive(false);
        indexSelectedCharacter = (indexSelectedCharacter + 1) % charactersSelect.Length;
        charactersSelect[indexSelectedCharacter].SetActive(true);
    }

    public void PreviousCharacter() 
    {
        charactersSelect[indexSelectedCharacter].SetActive(false);
        indexSelectedCharacter--;
        if(indexSelectedCharacter < 0) 
        {
            indexSelectedCharacter += charactersSelect.Length;
        }
        charactersSelect[indexSelectedCharacter].SetActive(true);
    }

}
