using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate_eff : Fighter
{
  protected override void Death() {
      Destroy(gameObject);
  }
  
}
