using UnityEngine;

public class LookPoint : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 1.3f, 0.15f);

     void Awake()
    {
        
       
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        target = playerObj.transform;
        
           
        
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
