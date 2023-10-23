using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyProjectile : MonoBehaviour
{

    //public float speed = 5f;       // Speed of the projectile
    //public GameObject target;     // Target monster
    //public float damage = 17f;    // Damage dealt by the projectile
    //public bool applyPoison; // 독 디버프 적용 여부
    //public int poisonStacks = 1; // 독 중첩 횟수
    //public float poisonDamagePercentage = 0.15f; // 독 데미지 퍼센트
    //void Update()
    //{
    //    if (target == null)
    //    {
    //        Destroy(gameObject);  // Destroy the projectile if the target is null
    //        return;
    //    }

    //    if (target != null)
    //    {
    //        Vector2 directionToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
    //        float distanceToTarget = directionToTarget.magnitude;

    //        // 1. 위치 설정
    //        transform.position = (Vector2)target.transform.position - 0.5f * directionToTarget.normalized * distanceToTarget;


    //        // 2. 스케일 설정
    //        transform.localScale = new Vector3(0.25f, distanceToTarget, 0.25f);

    //        // 3. 회전 설정
    //        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.Euler(0, 0, angle);
    //    }
    //    // Move the projectile towards the target


    //    // Check for collision with the target
    //    if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
    //    {
    //        HitTarget();
    //    }
    //}

    //void HitTarget()
    //{
    //    MonsterController monsterScript = target.GetComponent<MonsterController>();
    //    monsterScript.TakeDamage(damage);

    //    Destroy(gameObject);  // Destroy the projectile after hitting the target
    //}


    public float damage = 17f;
    public float laserDuration = 0.5f;
    public GameObject target;
    private Vector3 directionToTarget;
    private bool laserCreated = false;  // 레이저가 이미 생성되었는지 확인하는 변수

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Monster");
        }
        else if (!laserCreated)  // 레이저가 아직 생성되지 않았으면
        {
            CreateLaser();
        }
    }

    void CreateLaser()
    {
        directionToTarget = (target.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        // 길이 설정
        transform.localScale = new Vector3(0.25f, distanceToTarget / 2, 0.25f);  // 길이를 반으로 나누어서 레이저가 유닛과 몬스터 사이에만 나타나게 합니다.

        // 방향 설정
        transform.rotation = Quaternion.FromToRotation(Vector3.up, directionToTarget);

        // 위치 재조정
        transform.position = transform.position + directionToTarget * (distanceToTarget / 4);  // 레이저의 중심이 유닛과 몬스터 사이에 오도록 합니다.

        laserCreated = true;  // 레이저 생성 표시

        StartCoroutine(DestroyLaserAfterTime(laserDuration));  // 지속시간이 끝나면 레이저를 제거
    }

    private IEnumerator DestroyLaserAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        MonsterController monster = other.GetComponent<MonsterController>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
