﻿using RealEstate.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services
{
    public class DictionaryManager<TKey, TValue> : IDictionaryManager<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public void Add(TKey key, TValue value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
            }
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool Update(TKey key, TValue value)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
                return true;
            }
            return false;
        }

        public TValue Get(TKey key)
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            return default(TValue);
        }

        public List<string> ToStringList()
        {
            return _dictionary.Values.Select(item => item.ToString()).ToList();
        }

        public int Count => _dictionary.Count;

        public List<TValue> GetAll()
        {
            return _dictionary.Values.ToList();
        }

    }


}
