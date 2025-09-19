using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool hasPackage = false;

    
    [SerializeField] float destroySecond=0.5f;
    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Package") && !hasPackage)
        {

            Debug.Log("paket alındı");
            GetComponent<ParticleSystem>().Play();
            hasPackage = true;
            Destroy(collision.gameObject, destroySecond);

        }

        if (collision.CompareTag("Customer") && hasPackage)
        {

            Debug.Log("paket teslim edildi");
            GetComponent<ParticleSystem>().Stop();
            hasPackage = false;
    
        }
}
}
