using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LilyProjectile : MonoBehaviour
{
    
    //public float poisonDamagePercentage = 0.05f; // 도트 데미지의 백분율
    //public float damageInterval = 0.2f; // 데미지 간격
    //private bool isDealingDamage = false; // 데미지 적용 중인지 확인하는 플래그
    //public float damage = 17f;
    //public float laserDuration = 0.5f;
    //public GameObject target;
    //private Vector3 directionToTarget;
    //private bool laserCreated = false;  // 레이저가 이미 생성되었는지 확인하는 변수
    //public Transform originatingUnitTransform;
    //internal LilyAttack lilyAttack;

    //private void Start()
    //{
     
        
    //}
    //private void Update()
    //{
    //    Transform parentTransform = this.transform.parent;
    //    LilyAttack parentScript = parentTransform.GetComponent<LilyAttack>();
        
    //    //transform.localScale += new Vector3(0.25f, scalingSpeed, 0);
    //    if (target == null)
    //    {
    //        target = GameObject.FindGameObjectWithTag("Monster");
    //        Destroy(this.gameObject);

    //    }
    //    else   // 레이저가 아직 생성되지 않았으면
    //    {
    //        CreateLaser();
    //    }
    //    if (target && originatingUnitTransform)
    //    {
    //        directionToTarget = (target.transform.position - originatingUnitTransform.position).normalized;
    //        float distanceToTarget = Vector3.Distance(originatingUnitTransform.position, target.transform.position);

    //        // 레이저 스케일 동적 조절
    //        transform.localScale = new Vector3(0.25f, distanceToTarget / 5, 0.25f);

    //        // 레이저의 시작 위치 동적 조절
    //        transform.position = originatingUnitTransform.position;
    //    }

    //    //if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
    //    //{
    //    //    HitTarget();
    //    //}
    //}

    //void CreateLaser()
    //{
        
        
    //    directionToTarget = (target.transform.position - transform.position);
    //    float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

    //    //레이저 스케일 조절
    //    transform.localScale = new Vector3(0.25f, distanceToTarget/4.5f, 0.25f);

    //    //레이저의 시작 위치를 변경하지 않도록 오프셋 조정
    //    //transform.position = transform.position + directionToTarget * (distanceToTarget / 2);
    //    transform.position = this.transform.parent.position;

    //    // 레이저 회전 설정
    //    transform.rotation = Quaternion.FromToRotation(Vector3.up, directionToTarget);

        

    //    //HitTarget();
    //}

    ////private IEnumerator DestroyLaserAfterTime(float duration)
    ////{
    ////    yield return new WaitForSeconds(duration);
    ////    Destroy(gameObject);
    ////    laserCreated=false;
    ////}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //Debug.Log("LilyProjectile collided with: " + other.gameObject.name);
    //    //MonsterController monster = other.GetComponent<MonsterController>();
    //    //if (monster != null && !isDealingDamage)
    //    //{
    //    //    StartCoroutine(DealDotDamage(monster));  // 도트 데미지 로직 시작
    //    //}
    //    if (other.tag != "Monster") return;

    //    if (isDealingDamage)
    //        return;
        
    //    IDamageable damageableEntity = other.GetComponent<IDamageable>();
    //    if (damageableEntity != null && !isDealingDamage)
    //    {
    //        Debug.Log("LilyProjectile collided with: " + other.gameObject.name);
    //        StartCoroutine(DealDotDamage(damageableEntity));  // DOT 데미지 처리 시작
    //    }
    //}

    ////void HitTarget()
    ////{
    ////    MonsterController monsterScript = target.GetComponent<MonsterController>();
    ////    monsterScript.TakeDamage(damage);
    ////    Debug.Log("hit");
    ////    /*Destroy(gameObject); */ // Destroy the projectile after hitting the target

    ////}

    //private IEnumerator DealDotDamage(IDamageable damageableEntity)
    //{
       

    //    isDealingDamage = true;
    //    while (damageableEntity != null) // 대상이 존재하고 체력이 0 이상인 경우
    //    {
    //        yield return new WaitForSeconds(damageInterval);
    //        float dotDamage = damage * poisonDamagePercentage;
    //        damageableEntity.TakeDamage(dotDamage);
    //    }
    //    isDealingDamage = false;
    //    //Destroy(gameObject);

    //}
}
