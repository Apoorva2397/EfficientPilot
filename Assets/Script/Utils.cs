using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Utils : MonoBehaviour {
    static int myIndex;
    static int nextIndex;
   public static void LoadNextLevel() {
         myIndex = SceneManager.GetActiveScene().buildIndex;
         nextIndex = (myIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextIndex);
    }
    public static void ResetLevel() {
        SceneManager.LoadScene(myIndex);
    }
}
