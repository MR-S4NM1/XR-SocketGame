using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Unity.XR.CoreUtils;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance;

    [SerializeField] protected XRGrabInteractable _doorXRGrabInteractable;

    [SerializeField] protected TeleportationProvider _teleportationProvider;
    [SerializeField] protected ActionBasedContinuousMoveProvider _continousMoveProvider;

    [SerializeField] protected ActionBasedContinuousTurnProvider _continousTurnProvider;
    [SerializeField] protected ActionBasedSnapTurnProvider _snapTurnProvider;

    [SerializeField] protected XROrigin _xROrigin;
    
    [SerializeField] protected GameObject _treasureGO;
    [SerializeField] protected GameObject _doorSocket;
    [SerializeField] protected GameObject _keyGO;
    [SerializeField] protected GameObject _doorKeyGO;
    [SerializeField] protected GameObject _finalTriggerGO;

    [SerializeField] protected ParticleSystem[] _explosionsSystem;

    [SerializeField] protected Canvas _victoryCanvas;
    [SerializeField] protected GameObject _rotationPanel;
    [SerializeField] protected GameObject _movementPanel;

    [SerializeField] protected Canvas _defeatCanvas;
    [SerializeField] protected GameObject _timerPanel;
    [SerializeField] protected GameObject _defeatPanel;
    [SerializeField] protected TextMeshProUGUI _timerTxt;
    #endregion

    #region Runtime Variables
    [SerializeField] protected short _explosionTimer;

    [SerializeField] protected bool _keyHasBeenSet;
    [SerializeField] protected bool _explosionHasBeenStopped;

    protected Coroutine _trapCoroutine;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        _rotationPanel.SetActive(true);
        _movementPanel.SetActive(false);

        _doorKeyGO.SetActive(false);
        _doorSocket.SetActive(false);
        _doorXRGrabInteractable.enabled = false;
        _treasureGO.SetActive(false);

        _finalTriggerGO.SetActive(false);
    }

    private void Update()
    {
        _defeatPanel.transform.rotation = _xROrigin.gameObject.transform.rotation;
    }
    #endregion

    #region Public Methods

    public void SetContinousMovement()
    {
        _teleportationProvider.enabled = false;
        _continousMoveProvider.enabled = true;
        _movementPanel.SetActive(false);
    }

    public void SetTeleportMovement()
    {
        _teleportationProvider.enabled = true;
        _continousMoveProvider.enabled = false;
        _movementPanel.SetActive(false);
    }

    public void SetContinousRotation()
    {
        _continousTurnProvider.enabled = true;
        _snapTurnProvider.enabled = false;
        _rotationPanel.SetActive(false);
        _movementPanel.SetActive(true);
    }

    public void SetSnapRotation()
    {
        _continousTurnProvider.enabled = false;
        _snapTurnProvider.enabled = true;
        _rotationPanel.SetActive(false);
        _movementPanel.SetActive(true);
    }

    public void ActivateDoorOpening()
    {
        if (_keyHasBeenSet)
        {
            _doorSocket.SetActive(false);
            _keyGO.SetActive(false);
            _doorKeyGO.SetActive(true);
            _doorXRGrabInteractable.enabled = true;
            _treasureGO.SetActive(true);
        }
    }

    public void ActivateDoorsSocket()
    {
        if (!_keyHasBeenSet)
        {
            _doorSocket.SetActive(true);
        }
    }

    public void DeactivateDoorsSocket()
    {
        if (!_keyHasBeenSet)
        {
            _doorSocket.SetActive(false);
        }
    }

    public void DeactivateGoldAndActivateTrap()
    {
        _trapCoroutine = StartCoroutine(trapCoroutine());
        _finalTriggerGO.SetActive(true);
    }

    public void ShowVictoryCanvasAndStopExplosion()
    {
        _defeatCanvas.gameObject.SetActive(false);
        _victoryCanvas.gameObject.SetActive(true);
        _explosionHasBeenStopped = true;
    }

    public void ChangeToGameScene()
    {
        SceneChanger.instance.ChangeSceneTo(0);
    }
    #endregion

    #region Coroutines

    protected IEnumerator trapCoroutine()
    {
        _defeatCanvas.gameObject.SetActive(true);
        _timerPanel.SetActive(true);
        _explosionTimer = 30;
        _timerTxt.text = "Time left before explosion: " + _explosionTimer.ToString();
        _treasureGO.SetActive(false);

        while(_explosionTimer > 0 && !_explosionHasBeenStopped)
        {
            _explosionTimer--;
            _timerTxt.text = "Time left before explosion: " + _explosionTimer.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        if(_explosionTimer <= 0)
        {
            _continousMoveProvider.enabled = false;
            _teleportationProvider.enabled = false;
            _continousTurnProvider.enabled = false;
            _snapTurnProvider.enabled = false;
            _continousMoveProvider.moveSpeed = 0.0f;

            foreach (ParticleSystem particleSystem in _explosionsSystem)
            {
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
                particleSystem.gameObject.GetComponent<AudioSource>().Play();
            }

            yield return new WaitForSeconds(1.0f);
            _defeatPanel.SetActive(true);
        }
    }

    #endregion

    #region Getters and Setters
    public bool SetKeyHasBeenSet
    {
        set { _keyHasBeenSet = value; }
    }
    #endregion
}
