using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private int _hp;

    public Image healthBar; 

    public int MaxHp => _maxHp;

    public int Hp
    {
        get => _hp;
        private set
        {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, min: 0, _maxHp);

            if (isDamage)
            {
                Damaged?.Invoke(_hp);
            }
            else
            {
                Healed?.Invoke(_hp);
            }

            if(_hp <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)_hp / _maxHp, 0f, 1f);
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;


    private void Awake()
    {
        _hp = _maxHp;
    }

    public void Damage(int amount) => Hp -= amount;
  

    public void Heal(int amount) => Hp += amount;


    public void HealFull() => Hp -= _maxHp;
    

    public void Kill() => Hp = 0;
    

    public void Adjust(int value) => Hp = value;

    
}
