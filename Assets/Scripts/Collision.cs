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

        Debug.Log(collision.gameObject);
        Debug.Log(collision.collider);
    }

    // OnTriggerEnter2D'den dönen collision parametresi direkt Collider2D bileşenidir.

    // OnCollisionEnter2D'den dönen collision parametresi ise Collision2D bileşenidir. Bu bileşenden collider'a ulaşmak için collision.collider demen gerekir.


}
