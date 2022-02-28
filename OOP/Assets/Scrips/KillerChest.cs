using UnityEngine;

 class KillerChest : MonoBehaviour,AInteractable
 {
    private Player _target;
    public void Action(Player player){
        _target= player;
    }
    private void Update() 
    {
        if(_target==null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _target.Speed * Time.deltaTime*0.9f);
    }

}
