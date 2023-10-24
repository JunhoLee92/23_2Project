using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyProjectile : MonoBehaviour
{



    public float damage = 17f;
    public float laserDuration = 0.5f;
    public GameObject target;
    private Vector3 directionToTarget;
    private bool laserCreated = false;  // 레이저가 이미 생성되었는지 확인하는 변수
    public Transform originatingUnitTransform;
    float scalingSpeed = 10000.0f;
    private void Update()
    {
        
        //transform.localScale += new Vector3(0.25f, scalingSpeed, 0);
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Monster");
            Destroy(this.gameObject);
        }
        else if (!laserCreated)  // 레이저가 아직 생성되지 않았으면
        {
            CreateLaser();
        }
        if (target && originatingUnitTransform)
        {
            directionToTarget = (target.transform.position - originatingUnitTransform.position).normalized;
            float distanceToTarget = Vector3.Distance(originatingUnitTransform.position, target.transform.position);

            // 레이저 스케일 동적 조절
            transform.localScale = new Vector3(0.25f, Mathf.Min(transform.localScale.y + scalingSpeed, distanceToTarget / 10), 0.25f);

            // 레이저의 시작 위치 동적 조절
            transform.position = originatingUnitTransform.position;
        }

        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            HitTarget();
        }
    }

    void CreateLaser()
    {
        directionToTarget = (target.transform.position - transform.position);
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        //레이저 스케일 조절
        transform.localScale = new Vector3(0.25f, distanceToTarget / 10, 0.25f);

        //레이저의 시작 위치를 변경하지 않도록 오프셋 조정
        transform.position = transform.position + directionToTarget * (distanceToTarget / 2);

        // 레이저 회전 설정
        transform.rotation = Quaternion.FromToRotation(Vector3.up, directionToTarget);

        HitTarget();
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

    void HitTarget()
    {
        MonsterController monsterScript = target.GetComponent<MonsterController>();
        monsterScript.TakeDamage(damage);
        Debug.Log("hit");
        /*Destroy(gameObject); */ // Destroy the projectile after hitting the target
       
    }
}
