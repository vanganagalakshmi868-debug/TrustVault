using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrustVault.Api.DTOs;
using TrustVault.Application.Interfaces;
namespace TrustVault.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _docs;
    private readonly IAuditService _audit;
    public DocumentsController(IDocumentService docs, IAuditService audit) { _docs = docs; _audit = audit; }
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] DocumentUploadRequest req)
    {
        if (req.File == null || req.File.Length == 0) return BadRequest("No file");
        var storedName = Guid.NewGuid() + Path.GetExtension(req.File.FileName);
        var path = Path.Combine("storage", "uploads", storedName);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using (var stream = System.IO.File.Create(path)) await req.File.CopyToAsync(stream);
        var doc = await _docs.UploadAsync(UserId, req.File.FileName, storedName, req.File.ContentType, req.File.Length);
        await _audit.LogAsync(UserId, doc.Id, Domain.Enums.AuditAction.Upload, $"Uploaded {req.File.FileName}");
        return Ok(new DocumentResponse { Id = doc.Id, FileName = doc.FileName, FileSize = doc.FileSize, UploadedAt = doc.UploadedAt });
    }
    [HttpGet("my")]
    public async Task<IActionResult> My()
    {
        var docs = await _docs.GetMyDocumentsAsync(UserId);
        return Ok(docs.Select(d => new DocumentResponse { Id = d.Id, FileName = d.FileName, FileSize = d.FileSize, UploadedAt = d.UploadedAt }));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var doc = await _docs.GetByIdAsync(id, UserId);
        if (doc == null) return Forbid();
        return Ok(new DocumentResponse { Id = doc.Id, FileName = doc.FileName, FileSize = doc.FileSize, UploadedAt = doc.UploadedAt });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _docs.DeleteAsync(id, UserId);
        if (!ok) return NotFound();
        await _audit.LogAsync(UserId, id, Domain.Enums.AuditAction.Delete, "Deleted document");
        return Ok("Deleted");
    }
}