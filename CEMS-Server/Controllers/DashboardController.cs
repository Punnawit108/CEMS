// using CEMS_Server.AppContext;
// using CEMS_Server.DTOs;
// using CEMS_Server.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace CEMS_Server.Controllers;


// [ApiController]
// [Route("api/user/dashboard")]
// public class DashboardController : ControllerBase
// {
//     private readonly CemsContext _context;

//     public DashboardController(CemsContext context)
//     {
//         _context = context;
//     }

// // DashboardUser
//     [HttpGet("user")]
//     public async Task<ActionResult<IEnumerable<DashboardUserGetDto>>> GetDashboardUser() //ติดไว้ก่อน
//     {
//         var requisition = await _context
//             .CemsRequisitions
//             .Include(e => e.RqPj)
//             .Include(e => e.RqRqt)
//             .Where(u => u.RqStatus == "waiting" || u.RqStatus == "accept" ) // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
//             .Select(u => new DashboardUserGetDto
//             {
//                 RqId = u.RqId,
//                 PjName = u.RqPj.PjName,
//                 RqExpenses = u.RqExpenses,
//                 PjAmountExpenses = u.RqPj.PjAmountExpenses,
//                 RqStatus = u.RqStatus,
//                 RqRqtId = u.RqRqtId,
//                 RqtName = u.RqRqt.RqtName,
//                 RqDateWithdraw = u.RqDateWithdraw,
//             })
//             .ToListAsync();

//         return Ok(requisition);
//     }

// //Approver
//     [HttpGet("approver")]
//     public async Task<ActionResult<IEnumerable<DashboardApproverGetDto>>> GetDashboardApprover()
//     {
//         var requisition = await _context
//             .CemsApproverRequistion.Include(e => e.AprRq)
//             .CemsRequisitions.Include(e => e.RqUsr)
//             .Include(e => e.RqPj)
//             .Include(e => e.RqRqt)
//             .Where(u => u.RqStatus == "waiting" || u.RqStatus == "accept" ) // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
//             .Select(u => new DashboardApproverGetDto
//             {
//                 AprRqId = u.AprRqId,
//                 RqStatus = u.AprRq.RqStatus,
//                 AprId = u.AprId,
//                 RqExpenses = u.AprRq.RqExpenses,
//                 RqPjId = u.AprRq.RqPjId,
//                 PjName = u.AprRq.RqPj.PjName,
//                 PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
//                 RqRqtId = u.AprRq.RqRqtId,
//                 RqtName = u.AprRq.RqRqt.RqtName,
//                 RqDateWithdraw = u.AprRq.RqDateWithdraw,
//             })
//             .ToListAsync();

//         return Ok(requisition);
//     }

// //Admin
//     [HttpGet("admin")]
//     public async Task<ActionResult<IEnumerable<DashboardAdminGetDto>>> GetDashboardAdmin()
//     {
//         var requisition = await _context
//             .CemsApproverRequistions.Include(e => e.AprRq)
//             .Include(e => e.RqUsr)
//             .Include(e => e.RqPj)
//             .Include(e => e.RqRqt)
//             .Where(u => u.RqStatus == "accept" ) // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
//             .Select(u => new DashboardAdminGetDto
//             {
//                 UsrId = u.AprRq.RqUsr.UsrId,
//                 AprRqId = u.AprRqId,
//                 RqStatus = u.AprRq.RqStatus,
//                 RqExpenses = u.AprRq.RqExpenses,
//                 RqPjId = u.AprRq.RqPjId,
//                 PjName = u.AprRq.RqPj.PjName,
//                 PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
//                 RqRqtId = u.AprRq.RqRqtId,
//                 RqtName = u.AprRq.RqRqt.RqtName,
//                 RqDateWithdraw = u.AprRq.RqDateWithdraw,
//             })
//             .ToListAsync();

//         return Ok(requisition);
//     }

// //Accountant
//     [HttpGet("accountant")]
//     public async Task<ActionResult<IEnumerable<DashboardAccountantGetDto>>> GetDashboardAccountant()
//     {
//         var requisition = await _context
//             .CemsApproverRequistion.Include(e => e.AprRq)
//             .CemsRequisitions.Include(e => e.RqUsr)
//             .Include(e => e.RqPj)
//             .Include(e => e.RqRqt)
//             .Where(u => u.RqStatus == "waiting" || u.RqStatus == "accept" ) // เพิ่มเงื่อนไข Where //ไม่มั่นใจว่าสถานะเสร็จสิ้นของคำขอเบิกค่าเดินทางคือ "accept" ไหม
//             .Select(u => new DashboardAccountantGetDto
//             {
//                 AprRqId = u.AprRqId,
//                 RqStatus = u.AprRq.RqStatus,
//                 AprId = u.AprId,
//                 RqExpenses = u.AprRq.RqExpenses,
//                 RqPjId = u.AprRq.RqPjId,
//                 PjName = u.AprRq.RqPj.PjName,
//                 PjAmountExpenses = u.AprRq.RqPj.PjAmountExpenses,
//                 RqRqtId = u.AprRq.RqRqtId,
//                 RqtName = u.AprRq.RqRqt.RqtName,
//                 RqDateWithdraw = u.AprRq.RqDateWithdraw,
//             })
//             .ToListAsync();

//         return Ok(requisition);
//     }

// }
