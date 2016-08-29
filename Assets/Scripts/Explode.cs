using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public ParticleSystem[] effects;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        foreach (var effect in effects)
        {
            effect.transform.parent = null;
            effect.Stop();
            Destroy(effect.gameObject, 1f);
        }
        Destroy(gameObject);
    }
}
