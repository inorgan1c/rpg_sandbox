using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    public class InventorySlot
    {
        public InventoryItem _item;
        public int _quantity;

        public InventoryItem Item => _item;
        public int Quantity => _quantity;


        public UnityAction OnItemChange;


        public void StoreItem(InventoryItem item, int quantity)
        {
            if ((_item == null) || (_item == item))
            {
                _item = item;
                _quantity += quantity;

                OnItemChange?.Invoke();

            } else
            {
                Debug.LogError("InventorySlot cannot store mismatching items");
            }
        }

        public void Use(int quantity = 1)
        {
            if (quantity <= _quantity)
            {
                _quantity -= quantity;
                if (quantity == 0)
                {
                    Clear();
                }
                OnItemChange?.Invoke();

            }
            else
            {
                Debug.LogError("InventorySlot: not enough instance of item in inventory");
            }
        }

        public void Clear()
        {
            _item = null;
            _quantity = 0;

            OnItemChange?.Invoke();

        }


        public void MoveTo(InventorySlot destination, int quantity)
        {
            if ((destination != null) && (_quantity >= quantity))
            {
                if (destination.Item == _item || destination.Item == null)
                {
                    destination.StoreItem(_item, quantity);
                    _quantity -= quantity;

                    if (_quantity == 0)
                    {
                        Clear();
                    }

                    OnItemChange?.Invoke();
                }
                else
                {
                    Debug.LogError("Cannot MoveTo InventorySlot: mismatching items");
                }


            } else
            {
                Debug.LogError("Failed to MoveTo another slot: invalid destination or quantity");
            }
        }

        public void MoveAllTo(InventorySlot destination)
        {
            MoveTo(destination, _quantity);
        }
    }

}
