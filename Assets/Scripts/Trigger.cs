using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool hasPackage = false;

    public Driver driverScript;


    [SerializeField] float destroySecond = 0.5f;
    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Package") && !hasPackage)
        {

            Debug.Log("paket alındı");
            GetComponent<ParticleSystem>().Play();
            hasPackage = true;
            Destroy(other.gameObject, destroySecond);

        }

        if (other.CompareTag("Customer") && hasPackage)
        {

            Debug.Log("paket teslim edildi");
            GetComponent<ParticleSystem>().Stop();
            hasPackage = false;

        }

        if (other.CompareTag("Booster"))
        {
            driverScript.BoostCar();
            Debug.Log("booster");
            Destroy(other.gameObject);

        }
    }
}
