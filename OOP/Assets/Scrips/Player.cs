using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float _speed;
    [SerializeField] private AbstractWeapon _currentWeapon;
   [SerializeField] private GameObject _weapon;
   private IWeapon _currentIweapon; 
    private Vector3 _direction;
    public float Speed => _speed;
    #region MonoBegaviour
     private void OnValidate() {
        if (_speed <0)
        _speed=0; 
        if (_weapon!=null && _weapon.GetComponent<IWeapon>()==null) _weapon =null;
    }
    #endregion
        public void TurnLeft()
    {

    }
    public void TurnRight()
    {

    }
      public void Move()
    {
        
    }
    void Start()
    {
        if(_weapon!= null)
        _currentIweapon = _weapon.GetComponent<IWeapon>();
    }
}
public interface IWeapon
{
    void Shoot();
}
public abstract class AbstractWeapon : MonoBehaviour
 {
    
}