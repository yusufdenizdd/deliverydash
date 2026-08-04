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


// normalde triggerların hepsini tek bir dosyaya toplayıp driver'a vermek yanlış. 
// her bir ontriggerenter veya oncollisionenter olan nesneye özel ayrı bir fonksiyon yazmak lazım ki modüler olsun. 
// mesela sadece paket için Package.cs diye bi dosya açıp böyle bişey yapmak lazımdı:
// Package.cs (Paket prefab'ının üstündeki script)
// using UnityEngine;

// public class Package : MonoBehaviour
// {
//     [SerializeField] float destroySecond = 0.5f;

//     // Bu script SADECE paketin görevini bilir
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         // 1. Bana çarpan Sürücü mü?
//         if (other.CompareTag("Driver")) // Sürücünün etiketinin "Driver" olduğunu varsaydık
//         {
//             // 2. Sürücünün script'ine eriş
//             Driver driver = other.GetComponent<Driver>(); 

//             // 3. Sürücünün zaten paketi yoksa...
//             if (driver != null && !driver.PaketiVarMi())
//             {
//                 Debug.Log("paket alındı");

//                 // 4. Sürücü'ye "paketi al" komutunu yolla
//                 driver.AlPaketi();

//                 // 5. Kendini (paketi) yok et
//                 Destroy(gameObject, destroySecond); (other.gameObject diyince buna çarpan nesne, sadece gameObject diyince bu scripte sahip olan nesne)
//             }
//         }
//     }
// }
// gameObject. diyince o nesneye erişiyoruz, gameObject.GetComponent() diyince o nesnenin inspector penceresindeki componentlere erişiyoruz

// gameObject: Dediğinde, o script'in bağlı olduğu nesnenin (GameObject'in) kendisine erişirsin. Bu tam olarak doğru. Bu "konteynerin" kendisidir.

// gameObject.GetComponent<BileşenAdı>(): Dediğinde, o nesnenin Inspector penceresindeki component'lerin hepsine birden değil, istediğin spesifik bir tanesine erişirsin.