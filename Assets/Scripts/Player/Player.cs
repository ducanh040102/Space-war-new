using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private RuntimeAnimatorController[] runtimeAnimatorControllers;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private bool hasShield = false;
    [SerializeField] private float MoveSpeed = 200f;

    private Vector3 lastMousePosition;

    public event EventHandler OnPlayerHit;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadPlayerData();
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;
        MoveWithMouse();
        StayInScreen();
    }

    private void LoadPlayerData()
    {
        playerName.text = GameManager.instance.GetPlayerNameValue();

        if (runtimeAnimatorControllers[GameManager.instance.GetPlayerType()] == null)
        {
            PlayerAnimator.runtimeAnimatorController = runtimeAnimatorControllers[0];
            return;
        }

        PlayerAnimator.runtimeAnimatorController = runtimeAnimatorControllers[GameManager.instance.GetPlayerType()];
    }


    private void MoveWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = mousePos;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = mousePos - lastMousePosition;
            MoveWithDirection(direction);
            lastMousePosition = mousePos;
        }
    }

    private void StayInScreen()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -ScreenBoundary.Instance.ScreenWidth, ScreenBoundary.Instance.ScreenWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, -ScreenBoundary.Instance.ScreenHeight, ScreenBoundary.Instance.ScreenHeight);
        transform.position = newPosition;
    }

    private void MoveWithDirection(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * MoveSpeed;
    }

    public void Damage()
    {
        if (hasShield == true)
        {
            return;
        }

        OnPlayerHit?.Invoke(this, EventArgs.Empty);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasShield)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.GetComponent<EnemyBullet>().BackToPool();
            Damage();
        }
    }

    public void PowerupShield(float duration)
    {
        StartCoroutine(ShieldTimer(duration));
    }

    public IEnumerator ShieldTimer(float duration)
    {
        hasShield = true;
        GameObject shieldObj = Instantiate(shieldPrefab, transform);
        shieldObj.transform.position = transform.position;
        yield return new WaitForSeconds(duration);
        Destroy(shieldObj);
        hasShield = false;
    }

}
