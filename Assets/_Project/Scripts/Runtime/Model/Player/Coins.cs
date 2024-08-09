using System;
using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class Coins : ICoins, IReset
    {
        public event Action<uint> UpdateCoinsEvent;

        private uint _initialCoins;
        private uint _coins;

        public Coins(uint initialCoins)
        {
            _initialCoins = initialCoins;
            _coins = initialCoins;
            UpdateCoinsEvent?.Invoke(_initialCoins);
        }

        public void Add(uint amount)
        {
            _coins += amount;
            UpdateCoinsEvent?.Invoke(_coins);
        }

        public bool Check(uint cost)
        {
            if (_coins < cost)
            {
                Debug.Log("coins is not enough");
                return false;
            }

            return true;
        }

        public bool TrySpend(uint cost)
        {
            if (Check(cost))
            {
                _coins -= cost;
                UpdateCoinsEvent?.Invoke(_coins);
                return true;
            }

            return false;
        }

        void IReset.Reset()
        {
            _coins = _initialCoins;
            UpdateCoinsEvent?.Invoke(_coins);
        }
    }
}