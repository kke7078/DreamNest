using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public enum ItemCategory //�������� ����
    {
        Dog,
        Cat,
        Rabbit
    }

    public interface IItemData  //������ ���� ������ (���δ�Ʈ, ������ �������� �� ������)
    {
        Sprite Icon { get; }
        string Name { get; }
        string Description { get; }
        
    }
}
