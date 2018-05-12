using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStartBox : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GameController.Instance.ResetPlayerPosition();
    }
}
