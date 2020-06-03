using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public int startingClicks;

    private void Update() {
        Click();
    }

    private void Click() {
        if (Input.GetMouseButtonDown(0) && this.startingClicks > 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                Transform target = hit.transform;
                Material targetMaterial = target.GetComponent<Renderer>().material;

                if (target.tag == "isAlive") {
                    targetMaterial.color = Color.black;
                    hit.transform.tag = "isDead";
                    this.startingClicks++;
                }
                else if (target.tag == "isDead") {
                    targetMaterial.color = Color.white;
                    hit.transform.tag = "isAlive";
                    this.startingClicks--;
                }

                Debug.Log("Hit");
            }
        }
    }
}
