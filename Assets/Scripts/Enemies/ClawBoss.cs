using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ClawBoss : MonoBehaviour
{
    [SerializeField] private Vector3[] moveDestinations;

    [SerializeField] private Animator clawAnimator;

    private Vector3 spawnPosition;
    private bool isFollowingPlayer = false;

    private float maxHp;
    private ObjectMoveInScene objectMoveInScene;
    private EnemyBulletSpawner enemyBulletSpawner;
    private Enemy enemy;
    private Tween move;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        objectMoveInScene = GetComponent<ObjectMoveInScene>();
        enemyBulletSpawner = GetComponent<EnemyBulletSpawner>();

        BossManager.instance.UpdateBossHp(enemy.HitPoint);
        BossManager.instance.BossSpawn();

        maxHp = enemy.HitPoint;

        StartCoroutine(WaitForStart());
    }

    protected IEnumerator WaitForStart()
    {
        while (!enemy.IsStartAction)
        {
            yield return null;
        }

        spawnPosition = transform.position;
        Attack();
        StartCoroutine(WaitForDoSpecial());

    }
    
    private IEnumerator WaitForDoSpecial()
    {
        while (enemy.HitPoint > (int)(maxHp / 2))
        {
            yield return null;
        }

        move.Pause();
        enemyBulletSpawner.StopFiring();

        transform.DOMove(spawnPosition, 3f).SetEase(Ease.InSine);
        clawAnimator.SetTrigger("ballAttack");
        yield return new WaitForSeconds(5f);
        DoSpecial();
    }
    
    private IEnumerator WaitForEndSpecial()
    {
        yield return new WaitForSeconds(15f);
        EndSpecial();
        BossManager.instance.BossSpawnHelper("WaveBossHelper"+ GameManager.instance.GetPlayerStage());

    }

    private void Update()
    {
        BossManager.instance.UpdateBossHp(enemy.HitPoint);
        DropLoop();
        FollowingPlayerHorizontal();
    }

    private void Attack()
    {
        enemyBulletSpawner.StartFiring();
        move = transform.DOLocalPath(moveDestinations, 30f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void DoSpecial()
    {
        isFollowingPlayer = true;
        objectMoveInScene.UpdateMoveType(ObjectMoveInScene.Move.Down);
        
        StartCoroutine(WaitForEndSpecial());
    }

    private void EndSpecial()
    {
        isFollowingPlayer = false;
        objectMoveInScene.UpdateMoveType(ObjectMoveInScene.Move.Still);
        clawAnimator.SetTrigger("idle");
        
        transform.DOMove(spawnPosition, 2f).OnComplete(() =>
        {
            Attack();
        });
    }

    private void DropLoop()
    {
        if (!ScreenBoundary.Instance.IsInsideScreen(transform.position))
        {
            transform.position = new Vector3(transform.position.x, ScreenBoundary.Instance.ScreenHeight, 0);
        }
    }

    private void FollowingPlayerHorizontal()
    {
        if (!isFollowingPlayer || Player.instance == null)
        {
            return;
        }
        float step = 5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.instance.transform.position.x, transform.position.y, transform.position.z), step);
    }

    private void OnDestroy()
    {
        if(Application.isPlaying)
            BossManager.instance.BossDestroy();
    }
}
