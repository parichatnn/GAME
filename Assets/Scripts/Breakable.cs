using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.gameObject.GetComponent<Player>() && other.contacts[0].normal.y > 0.5f) {
            StartCoroutine(Break());
        }
    }
    private IEnumerator Break() {
        particle.Play();
        sr.enabled = false;
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
