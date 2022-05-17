using Dispatch.Domain.Models.Enums;

namespace Dispatch.Domain.Models
{
    internal struct FileMove
    {
        public MoveAction Action { get; set; }
        public string Path { get; set; }
    }
}
