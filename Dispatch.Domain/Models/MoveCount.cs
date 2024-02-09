using Dispatch.Domain.Models.Enums;

namespace Dispatch.Domain.Models
{
    public class MoveCount
    {
        public int KeptCount { get; set; }
        public int DeletedCount { get; set; }
        public int Count => KeptCount + DeletedCount;

        public void Reset()
        {
            KeptCount = 0;
            DeletedCount = 0;
        }
        public void Increment(MoveAction action)
        {
            switch (action)
            {
                case MoveAction.Keep: KeptCount++; break;
                case MoveAction.Delete: DeletedCount++; break;
            }
        }
        public override string ToString()
        {
            return $"{Count} ({KeptCount}+{DeletedCount})";
        }
    }
}
