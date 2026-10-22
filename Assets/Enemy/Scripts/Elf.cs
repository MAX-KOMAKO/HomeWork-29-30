using UnityEngine;

public class Elf : Enemy
{
    private int _mana;
    private float _agility;

    public void Setup(int mana, float agility)
    {
        _mana = mana;
        _agility = agility;
    }

    public override void PrintStats()
    {
        Debug.Log($"Эльф создан. Мана: {_mana}, Ловкость: {_agility}");
    }
}