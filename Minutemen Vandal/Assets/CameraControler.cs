using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject overviewCamera;

    private List<GameObject> cameras = new List<GameObject>();

    private void Awake()
    {
        GameManager.Instance.OnGameStarted.AddListener(ActivateMainMenuCamera);
        GameManager.Instance.OnLevelStarted.AddListener(ActivatePlayerCamera);
        GameManager.Instance.OnLevelExited.AddListener(ActivateMainMenuCamera);

        cameras.Add(mainMenuCamera);
        cameras.Add(playerCamera);
        cameras.Add(overviewCamera);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted.RemoveListener(ActivateMainMenuCamera);
        GameManager.Instance.OnLevelStarted.RemoveListener(ActivatePlayerCamera);
        GameManager.Instance.OnLevelExited.RemoveListener(ActivateMainMenuCamera);
    }

    private void ActivatePlayerCamera()
    {
        ActivateCamera(playerCamera);
    }

    private void ActivateMainMenuCamera()
    {
        ActivateCamera(mainMenuCamera);
    }


    private void ActivateCamera(GameObject camera)
    {
        foreach (GameObject listCamera in cameras)
        {
            if (camera != listCamera)
            {
                listCamera.SetActive(false);
            }
            else
            {
                camera.SetActive(true);
            }
        }
    }
}
