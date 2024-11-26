using CEMS_Server.AppContext;
using CEMS_Server.DTOs;
using CEMS_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CEMS_Server.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly CemsContext _context;

        public VehicleController(CemsContext context)
        {
            _context = context;
        }

        // GET: api/Vehicle
        [HttpGet]
        public IActionResult GetVehicles()
        {
            // ดึงข้อมูลจากฐานข้อมูล
            var vehicles = _context.CemsVehicles.ToList();

            // ถ้าหากไม่พบข้อมูล
            if (vehicles == null || !vehicles.Any())
            {
                return NotFound("No vehicles found.");
            }

            // Mapping ข้อมูลให้ตรงกับชื่อที่ต้องการ
            var vehicleDetails = vehicles.Select(vehicle => new
            {
                Id = vehicle.VhId,             // ใช้ VhId แทน Id
                VehicleType = vehicle.VhType,   // ใช้ VhType แทน VehicleType
                LicensePlate = vehicle.VhVehicle, // ใช้ VhVehicle แทน LicensePlate
                PayRate = vehicle.VhPayrate    // ใช้ VhPayrate แทน PayRate
            }).ToList();

            // ส่งคืนข้อมูลในรูปแบบ JSON
            return Ok(vehicleDetails);
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            var vehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found.");
            }

            // Mapping ข้อมูลเพื่อให้ส่งไปยัง Frontend
            var vehicleDetail = new
            {
                Id = vehicle.VhId,
                VehicleType = vehicle.VhType,
                LicensePlate = vehicle.VhVehicle,
                PayRate = vehicle.VhPayrate
            };

            return Ok(vehicleDetail);
        }

        // POST: api/Vehicle
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] CemsVehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Invalid vehicle data.");
            }

            _context.CemsVehicles.Add(vehicle);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VhId }, vehicle);
        }

        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] CemsVehicle vehicle)
        {
            if (vehicle == null || id != vehicle.VhId)
            {
                return BadRequest("Vehicle data is invalid.");
            }

            var existingVehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);
            if (existingVehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found.");
            }

            existingVehicle.VhType = vehicle.VhType;
            existingVehicle.VhVehicle = vehicle.VhVehicle;
            existingVehicle.VhPayrate = vehicle.VhPayrate;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _context.CemsVehicles.FirstOrDefault(v => v.VhId == id);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found.");
            }

            _context.CemsVehicles.Remove(vehicle);
            _context.SaveChanges();

            return NoContent();
        }
    }
}