using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrustVault.Application.Interfaces;
namespace TrustVault.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AuditController : ControllerBase
{
    private readonly IAuditService _audit;
    public AuditController(IAuditService audit) => _audit = audit;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var logs = await _audit.GetAllAsync();
        return Ok(logs.Select(l => new { l.Id, l.UserId, l.DocumentId, Action = l.Action.ToString(), l.Details, l.CreatedAt }));
    }
}