using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region References
    public static SceneChanger instance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    #region Public Methods

    public void ChangeSceneTo(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    #endregion
}
