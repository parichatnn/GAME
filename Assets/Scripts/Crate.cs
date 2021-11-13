using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
  public GameObject effect;
  private void Update() {

    if(hitpoint == 2) {
      // Debug.Log("hitpoint = 2 !!");
      Instantiate(effect, new Vector3(-1.142f, -0.256f, -0.059f), Quaternion.identity);
    }
    
  }
  protected override void Death() {
      Destroy(gameObject);
      // Instantiate(effect, new Vector3(-1.142f, -0.256f, -0.059f), Quaternion.identity);

  }
  
}
