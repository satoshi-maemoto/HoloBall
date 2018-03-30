using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine;

public class Controller : MonoBehaviour, IInputClickHandler
{
    public GameObject ballPrefab;
    public AudioSource audioSource;
    private DateTime prevShoot = DateTime.MinValue;
    private TimeSpan minShootInterval = new TimeSpan(0, 0, 0, 0, 10);

    private void Start()
    {
        InputManager.Instance.AddGlobalListener(this.gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (DateTime.Now - this.prevShoot < this.minShootInterval)
        {
            return;
        }
        Debug.Log("Shoot!");
        var ball = GameObject.Instantiate(this.ballPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 300f, ForceMode.Force);
        this.audioSource.Play();
        this.prevShoot = DateTime.Now;
    }
}