
using UnityEngine; 
[CreateAssetMenu(menuName ="Shop/Good")]
public class Good : ScriptableObject{


    [SerializeField] public string _name;
    [SerializeField] public int _price;

public string Name => _name;
    public int Price=> _price;
    

}
