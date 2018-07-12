using UnityEngine.SceneManagement;
using UnityEngine;


public class CreditsBackButton : MonoBehaviour {

    public void BackOnClick()
    {
        SceneManager.LoadScene("OpenningScene");
    }
}

