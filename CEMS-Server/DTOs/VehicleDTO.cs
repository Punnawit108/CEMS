namespace CEMS_Server.DTOs
{
    public class VehicleDTO
    {
        public int VhId { get; set; }
        public string VhType { get; set; } = null!;
        public string VhVehicle {get; set;} = null!;
        public double VhPayrate { get; set; }
        
    }
}
