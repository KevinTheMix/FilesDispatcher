namespace Dispatch.WebAPI.Models
{
    public class DispatchSection
    {
        public required string InDirectory { get; set; }
        public required string OutDirectory { get; set; }
        public required string LocalMachineIp { get; set; }
    }
}
