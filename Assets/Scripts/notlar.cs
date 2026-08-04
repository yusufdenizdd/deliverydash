
/*
2. GetComponent Performans Kuralı (Caching)
OnTriggerEnter2D içinde her seferinde GetComponent<ParticleSystem>() çağırmak Unity'de performans kaybına yol açar. 
Bunu Start() veya Awake() içinde hafızaya almak (Cache etmek) altın kuraldır.

C#
Doğru Yaklaşım:
private ParticleSystem particleSys;

void Start()
{
    particleSys = GetComponent<ParticleSystem>();
}

void OnTriggerEnter2D(Collider2D other)
{
    Artık her karede/tetiklenmede aramak yerine direkt hafızadaki değişkeni çağırıyoruz:
    particleSys.Play(); 
}
3. Coroutine Üst Üste Binme (Stacking) Sorunu
Şu anki kodunda peş peşe 2 tane Booster alırsan iki tane Coroutine aynı anda çalışır. 
İlk Booster'ın 5 saniyesi bittiğinde, ikinci Booster henüz bitmemiş olsa bile arabanın hızını normal hıza düşürür.

Bunu önlemek için çalışmakta olan Coroutine'i saklayıp yenisi geldiğinde öncekini durdurmak gerekir:

C#
private Coroutine boostCoroutine;

public void BoostCar()
{
    Eğer zaten çalışan bir boost varsa onu iptal et
    if (boostCoroutine != null)
    {
        StopCoroutine(boostCoroutine);
    }
    Yeni boost'u başlat ve referansını tut
    boostCoroutine = StartCoroutine(BoosterCoroutine());
}
*/

/*
💥 Çakışma Senaryosu: Yavaşlarken Boost Almak
Ayrı değişkenler kullandığımızı varsayalım:

0. Saniyede: Duvara çarptın (SlowCar()).

moveSpeed = 8 oldu.

slowCoroutine başladı. (5. saniyede bitip hızı tekrar 12 yapacak).

2. Saniyede: Yavaş yavaş ilerlerken önüne bir Booster çıktı ve aldın (BoostCar()).

moveSpeed = 18 oldu.

boostCoroutine başladı. (7. saniyede bitip hızı tekrar 12 yapacak).

5. Saniyeye Gelindiğinde (FACİA ANI):

saniyede başlayan slowCoroutine'in 5 saniyelik süresi doldu!

Yavaşlama fonksiyonunun en altındaki moveSpeed = normalSpeed; çalıştı ve hızın aniden 12'ye düştü!

Sonuç: Sen 2. saniyede Booster almış olmana ve 7. saniyeye kadar hızlı gitmen gerekmesine rağmen, 5. saniyede süresi dolan eski yavaşlama Coroutine'i hızını elinden aldı!
*/

/* 
mesela şöyle çözülür:
1. Değişken Tanımlama Alanı (Sınıfın Üst Kısmı)
TMP_Text slowText; değişkeninin hemen altına şu yeni değişkeni ekle:

C#
EKLENEN KISIM: O an çalışan Coroutine'i hafızada tutan kumanda
private Coroutine activeSpeedCoroutine;
2. Boost Fonksiyonları
BoostCar() ve BoosterCoroutine() metodlarını şu şekilde güncelle:

C#
public void BoostCar()
{
    EKLENEN: Zaten çalışan bir Coroutine varsa önce onu durdur
    if (activeSpeedCoroutine != null)
    {
        StopCoroutine(activeSpeedCoroutine);
    }

    DEĞİŞEN: Başlatılan Coroutine'i değişkene atıyoruz
    activeSpeedCoroutine = StartCoroutine(BoosterCoroutine());
}

IEnumerator BoosterCoroutine()
{
    Debug.Log($"hız {boastDuration}sn artacak");

    moveSpeed = boostSpeed;
    slowText.gameObject.SetActive(false);
    boostText.gameObject.SetActive(true);

    yield return new WaitForSeconds(boastDuration);

    Debug.Log("hızlandırma bitti");
    moveSpeed = normalSpeed;
    boostText.gameObject.SetActive(false);

    EKLENEN: Süre bittiğinde kumandayı boşaltıyoruz
    activeSpeedCoroutine = null;
}
3. Slow Fonksiyonları
SlowCar() ve SlowerCoroutine() metodlarını da aynı mantıkla güncelle:

C#
public void SlowCar()
{
    EKLENEN: Zaten çalışan bir Coroutine varsa önce onu durdur
    if (activeSpeedCoroutine != null)
    {
        StopCoroutine(activeSpeedCoroutine);
    }

    DEĞİŞEN: Başlatılan Coroutine'i değişkene atıyoruz
    activeSpeedCoroutine = StartCoroutine(SlowerCoroutine());
}

IEnumerator SlowerCoroutine()
{
    Debug.Log($"hız {slowDuration}sn azalacak");

    moveSpeed = slowSpeed;
    boostText.gameObject.SetActive(false);
    slowText.gameObject.SetActive(true);

    yield return new WaitForSeconds(slowDuration);

    Debug.Log("yavaşlama bitti");
    moveSpeed = normalSpeed;
    boostText.gameObject.SetActive(false);

    EKLENEN: Süre bittiğinde kumandayı boşaltıyoruz
    activeSpeedCoroutine = null;
}
*/