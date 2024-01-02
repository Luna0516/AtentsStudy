using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    PowerUp,

}

public class ItemBase : PoolObject
{
    /// <summary>
    /// 이 아이템의 종류
    /// </summary>
    private ItemType type;

    /// <summary>
    /// 아이템 종류 확인용 프로퍼티
    /// </summary>
    public ItemType Type
    {
        get => type;
        protected set
        {
            if(type != value)
            {
                type = value;
            }
        }
    }
}
