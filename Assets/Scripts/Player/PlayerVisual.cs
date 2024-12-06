using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform thurst;

    public void Show()
    {
        body.gameObject.SetActive(true);
        thurst.gameObject.SetActive(true);
    }

    public void Hide()
    {
        body.gameObject.SetActive(false);
        thurst.gameObject.SetActive(false);
    }
}
