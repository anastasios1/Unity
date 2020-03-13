using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{
    public Sprite idlePistol;
    public Sprite shotPistol;
    public float pistolDamage;
    public float pistolRange;
    public AudioClip shotSound;
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // Tworzymy promień, który wychodzi od naszej kamery do pozycji myszki
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
        {
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
            }
        }
    }

    // Funkcja podczas strzalu zmienia grafikę broni na 0.1 sekundy
    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }

}