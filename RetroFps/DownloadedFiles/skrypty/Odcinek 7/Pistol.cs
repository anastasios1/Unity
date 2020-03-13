using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{

    public Sprite idlePistol;
    public Sprite shotPistol;
    public float pistolDamage;
    public float pistolRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public Text ammoText;

    public int ammoAmount;
    public int ammoClipSize;

    public GameObject bulletHole;

    int ammoLeft;
    int ammoClipLeft;

    bool isShot;
    bool isReloading;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
    }

    void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
        // Tworzymy promień, który wychodzi od naszej kamery do środka ekranu
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        if (isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            DynamicCrosshair.spread += DynamicCrosshair.PISTOL_SHOOTING_SPREAD;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");
            //Jesli po wcisnieciu przycisku 'Fire1' promien wszedl w kolizje z jakims obiektem
            //Wykonuje ponizsze instrukcje
            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                Debug.Log("Wszedlem w kolizje z " + hit.collider.gameObject.name);
                // Wyslanie informacji do trafionego obiektu, ze go trafilismy
                // Trafiony obiekt powinien u siebie odpalic funkcje pistolHit z parametrem pistolDamage
                hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                //Tworzymy obiekt dziury po kuli na obiekcie, który trafiliśmy promieniem
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            //Gdy strzelimy, lecz nie mamy już amunicji, przeładowujemy broń
            isShot = false;
            Reload();
        }
    }

    // Funkcja odpowiedzialna za przeładowywanie broni
    void Reload()
    {
        //Obliczanie ile pocisków powinniśmy przeładować
        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if (ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }
    // Funkcja odtwarza dzwiek przeladowywania
    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }
    // Funkcja podczas strzalu zmienia grafikę broni na 0.1 sekundy
    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }

}