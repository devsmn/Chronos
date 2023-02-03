//
//
//
//

namespace Chronos.DataModel.Core
{
    public class IntegerEntityKey : EntityKey
    {
        public static IntegerEntityKey TemporaryId { get { return new IntegerEntityKey(-1); } }

        public int Id { get; private set; }

        public IntegerEntityKey(int key)
        {
            this.Id = key;
        }

        public bool TryParse(string id, out IntegerEntityKey key)
        {
            key = null;

            return false;
        }
    }
}
