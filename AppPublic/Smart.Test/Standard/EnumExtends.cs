using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Standard.Enum;
using Smart.Standard.Extends;

namespace Smart.Test.Standard
{
    [TestClass]
    public class EnumExtends
    {
        [TestMethod]
        public void AddEnumFlags()
        {
            var state = StatesKinds.Enable | StatesKinds.UnLock;

          
            Console.WriteLine(state.RemoveFlags(StatesKinds.UnLock | StatesKinds.DissEnable));
        }


        private IEnumerable<int> ToIntArray<TEnum>(TEnum statesKinds)where TEnum:struct
        {  
            var intList = new List<int>();
            foreach (var o in Enum.GetValues(statesKinds.GetType()))
            {
                var b = (statesKinds.CastTo<int>() & o.CastTo<int>()) == o.CastTo<int>();
                if(b) intList.Add(o.CastTo<int>());
            }
            return intList;
        }

    }
}
