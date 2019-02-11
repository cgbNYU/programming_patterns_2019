using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explode : MonoBehaviour
{
    
    //Script hookups
    public Target targetScript;
    
    //public
    public float explodeDistance;
    public float explosionSize;
    public float explosionSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        targetScript = GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
    }

    public void DistanceCheck()
    {
        if (targetScript.targetDif.magnitude <= explodeDistance)
        {
            TriggerExplosion();
        }
    }

    public void TriggerExplosion()
    {
        Destroy(GetComponent<Target>());
        Destroy(GetComponent<Follow>());
        Destroy(GetComponent<Movement>());

        transform.gameObject.tag = "EnemyBullet";
        
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        Tween myTween = transform.DOScale(explosionSize, explosionSpeed);
        
        yield return myTween.WaitForCompletion();
        
        Destroy(gameObject);
    }
}
