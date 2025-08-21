using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public enum ItemCategory
    {
        //���� ī�װ�
        Plt,    //�Ĺ���
        Glw,    //��, ��
        Fab,    //õ, ����, ����
        Gem,    //����, ���� ����
        Alc,    //����, ȥ�չ�, ����
        Sha,    //���, �׸���
        Sky,    //����, ����, ����
        Aqa,    //��, ����
        Fir,    //��
        Mec,    //���, �¿�
        Mbs,    //ȯ����, ��

        //�ռ� ī�װ�
    }

    public enum ItemGrade 
    {
        Normal,    //�Ϲ�
        Rare,      //���
    }


    public interface IItemData
    {
        string ItemId { get; }          //���̵�
        string Itemname { get; }        //�̸�
        int ItemLevel { get; }          //����
        string ItemDesc { get; }        //����
        Sprite ItemIcon { get; }        //������
        bool IsNotMergeable { get; }    //���� ���� ����
        int ItemSellPrice { get; }      //�Ǹ� ����
        int ItemBuyPrice { get; }       //���� ����
    }
}
