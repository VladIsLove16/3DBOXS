using UnityEngine;
class Chamelion : MonoBehaviour, AInteractable
{
public void Action(Player player)
{
 GetComponent<MeshRenderer>().sharedMaterial.color=Random.ColorHSV();
}
}
