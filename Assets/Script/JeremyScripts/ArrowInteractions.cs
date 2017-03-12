using UnityEngine;
using System.Collections;
public class ArrowInteractions : MonoBehaviour {
    public void Summon(Transform chara)
    {
        gameObject.layer = 8;
        transform.position = Vector3.MoveTowards(transform.position,chara.position,8f*Time.deltaTime);
        if (Vector2.Distance(transform.position, chara.position)<=1)
        {
            Debug.Log(Vector2.Distance(transform.position, chara.position));
        }
    }
}