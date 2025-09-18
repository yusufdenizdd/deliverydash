using UnityEngine;

public class Collision : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hittiing");    
    }


}
