using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltdownEntity : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 1.0F);
    }

    private void End()
    {
        Destroy(this.gameObject);
    }

    public void Setup(Vector3 screenPos, float scale)
    {
        transform.position = screenPos;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
