using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntectablePicker : MonoBehaviour
{
  [SerializeField] public Camera _inputcamera;
  [SerializeField] private Player _player;
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
        

        
      var ray =  _inputcamera.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z));
    var raycastHit = new RaycastHit();
    if(Physics.Raycast(ray, out raycastHit))
    {
        var interactable =raycastHit.collider.GetComponent<AInteractable>();
        if (interactable!= null)
        {
            interactable.Action(_player);

        }
    }
    }
    }
}
