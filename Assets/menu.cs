using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public void restartLevel()
    {
        SceneManager.LoadScene("prototype", LoadSceneMode.Single);
    }

    public void Quit()

    {
        Debug.Log("application quit");
        Application.Quit();
    }
}
