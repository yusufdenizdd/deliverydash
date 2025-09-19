using UnityEngine;

public class Collision : MonoBehaviour
{
    public Driver driverScript;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hittiing");
        Debug.Log(collision.gameObject);
        driverScript.SlowCar();
        Debug.Log("slower");
        // Destroy(collision.gameObject);
    }

    //deneme
}
