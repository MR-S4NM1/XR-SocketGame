using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    #region References
    [SerializeField] protected XRGrabInteractable _doorXRGrabInteractable;
    [SerializeField] protected GameObject _treasureGO;
    [SerializeField] protected ParticleSystem[] _explosionsSystem;
    #endregion

    #region Runtime Variables
    [SerializeField] protected short _explosionTimer;
    [SerializeField] protected bool _playerHasKey;
    protected Coroutine _trapCoroutine;
    #endregion

    #region Unity Methods
    void Start()
    {
        _playerHasKey = false;
        _doorXRGrabInteractable.enabled = _playerHasKey;
        _treasureGO.SetActive(false);
    }
    #endregion

    #region Public Methods

    public void ActivateDoorOpening()
    {
        _playerHasKey = true;
        _doorXRGrabInteractable.enabled = _playerHasKey;
        _treasureGO.SetActive(true);
    }

    public void DeactivateGoldAndActivateTrap()
    {
        _trapCoroutine = StartCoroutine(trapCoroutine());
    }
    #endregion

    #region Coroutines

    protected IEnumerator trapCoroutine()
    {
        _explosionTimer = 10;
        _treasureGO.SetActive(false);

        while(_explosionTimer > 0)
        {
            _explosionTimer--;
            yield return new WaitForSeconds(1.0f);
        }

        foreach(ParticleSystem particleSystem in _explosionsSystem)
        {
            particleSystem.Play();
        }
    }

    #endregion

    #region Getters and Setters
    public bool SetPlayerHasKey
    {
        set { _playerHasKey = value; }
    }
    #endregion
}
