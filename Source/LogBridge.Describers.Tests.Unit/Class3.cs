using System.Collections;
using System.Collections.Generic;

namespace SoftwarePassion.LogBridge.Describers.Tests.Unit
{
    public class Class3 : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            yield return 27;
            yield return 42;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}