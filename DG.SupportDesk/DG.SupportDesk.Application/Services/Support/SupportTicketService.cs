using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Application.Abstractions.Services.Support;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Support;
using DG.SupportDesk.Domain.Enums;
using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Services.Support;

public class SupportTicketService : ISupportTicketService
{
    private readonly ISupportTicketRepository _supportTicketRepository;
    private readonly IProductProjectRepository _productProjectRepository;
    private readonly ISupportTicketCommentRepository _commentRepository;
    private readonly ISupportTicketAttachmentRepository _attachmentRepository;
    private readonly ISupportTicketStatusHistoryRepository _statusHistoryRepository;

    public SupportTicketService(
        ISupportTicketRepository supportTicketRepository,
        IProductProjectRepository productProjectRepository,
        ISupportTicketCommentRepository commentRepository,
        ISupportTicketAttachmentRepository attachmentRepository,
        ISupportTicketStatusHistoryRepository statusHistoryRepository)
    {
        _supportTicketRepository = supportTicketRepository;
        _productProjectRepository = productProjectRepository;
        _commentRepository = commentRepository;
        _attachmentRepository = attachmentRepository;
        _statusHistoryRepository = statusHistoryRepository;
    }

    public async Task<ServiceResponseDTO<SupportTicketResponseDTO>> Add(
        SupportTicketAddDTO dto,
        CancellationToken ct = default)
    {
        var productProject = await _productProjectRepository.GetById(
            dto.ProductProjectId,
            dto.TenantId,
            ct);

        if (productProject is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Invalid product/project.");
        }

        var entity = new SupportTicket
        {
            TenantId = dto.TenantId,
            ProductProjectId = dto.ProductProjectId,

            IssueName = dto.IssueName,
            IssueTitle = dto.IssueTitle,
            IssueDescription = dto.IssueDescription,

            IssueCategoryTypeId = dto.IssueCategoryTypeId,
            PriorityTypeId = dto.PriorityTypeId,
            SupportLevelTypeId = dto.SupportLevelTypeId,
            TicketStatusTypeId = TicketStatusType.Submitted,

            IssuerUserId = dto.IssuerUserId,
            IssuerPhoneNo = dto.IssuerPhoneNo,
            IssuerEmail = dto.IssuerEmail,

            Remarks = dto.Remarks,
            EstimatedHours = dto.EstimatedHours,

            IssueDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.IssuerUserId,
            StatusTypeId = 1
        };

        var added = await _supportTicketRepository.Add(entity, ct);

        var history = new SupportTicketStatusHistory
        {
            TenantId = added.TenantId,
            SupportTicketId = added.Id,

            FromTicketStatusTypeId = null,
            ToTicketStatusTypeId = TicketStatusType.Submitted,

            ChangedByUserId = dto.IssuerUserId,
            Remarks = "Ticket submitted.",

            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.IssuerUserId,
            StatusTypeId = 1
        };

        await _statusHistoryRepository.Add(history, ct);

        var result = await _supportTicketRepository.GetDetailsById(
            added.Id,
            added.TenantId,
            ct);

        if (result is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket created but failed to load details.");
        }

        return ServiceResponseDTO<SupportTicketResponseDTO>.SuccessResponse(
            MapToDTO(result),
            "Support ticket created successfully.");
    }

    public async Task<ServiceResponseDTO<SupportTicketResponseDTO>> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var entity = await _supportTicketRepository.GetDetailsById(id, tenantId, ct);

        if (entity is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket not found.");
        }

        return ServiceResponseDTO<SupportTicketResponseDTO>.SuccessResponse(MapToDTO(entity));
    }

    public async Task<ServiceResponseDTO<PagedResponseDTO<SupportTicketResponseDTO>>> GetAll(
        SupportTicketQueryDTO query,
        CancellationToken ct = default)
    {
        var result = await _supportTicketRepository.GetAll(
            query.TenantId,
            query.ProductProjectId,
            query.TicketStatusTypeId,
            query.PriorityTypeId,
            query.SupportLevelTypeId,
            query.SearchText,
            query.PageNumber,
            query.PageSize,
            ct);

        var response = new PagedResponseDTO<SupportTicketResponseDTO>
        {
            PageNumber = query.PageNumber ?? 1,
            PageSize = query.PageSize ?? result.TotalCount,
            TotalCount = result.TotalCount,
            Items = result.Items.Select(MapToDTO).ToList()
        };

        return ServiceResponseDTO<PagedResponseDTO<SupportTicketResponseDTO>>.SuccessResponse(response);
    }

    public async Task<ServiceResponseDTO<SupportTicketResponseDTO>> Update(
        SupportTicketUpdateDTO dto,
        CancellationToken ct = default)
    {
        var existing = await _supportTicketRepository.GetById(
            dto.Id,
            dto.TenantId,
            ct);

        if (existing is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket not found.");
        }

        var productProject = await _productProjectRepository.GetById(
            dto.ProductProjectId,
            dto.TenantId,
            ct);

        if (productProject is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Invalid product/project.");
        }

        var entity = new SupportTicket
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            ProductProjectId = dto.ProductProjectId,

            IssueName = dto.IssueName,
            IssueTitle = dto.IssueTitle,
            IssueDescription = dto.IssueDescription,

            IssueCategoryTypeId = dto.IssueCategoryTypeId,
            PriorityTypeId = dto.PriorityTypeId,
            SupportLevelTypeId = dto.SupportLevelTypeId,

            IssuerUserId = dto.IssuerUserId,
            IssuerPhoneNo = dto.IssuerPhoneNo,
            IssuerEmail = dto.IssuerEmail,

            ResolverUserId = dto.ResolverUserId,
            ResolverPhoneNo = dto.ResolverPhoneNo,
            ResolverEmail = dto.ResolverEmail,

            Remarks = dto.Remarks,
            EstimatedHours = dto.EstimatedHours,

            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = dto.UpdatedBy,
            StatusTypeId = 1
        };

        var updated = await _supportTicketRepository.Update(entity, ct);

        if (updated is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket update failed.");
        }

        var result = await _supportTicketRepository.GetDetailsById(
            updated.Id,
            updated.TenantId,
            ct);

        if (result is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket updated but failed to load details.");
        }

        return ServiceResponseDTO<SupportTicketResponseDTO>.SuccessResponse(
            MapToDTO(result),
            "Support ticket updated successfully.");
    }

    public async Task<ServiceResponseDTO<SupportTicketResponseDTO>> UpdateStatus(
        long id,
        SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default)
    {
        var existing = await _supportTicketRepository.GetById(
            id,
            dto.TenantId,
            ct);

        if (existing is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Support ticket not found.");
        }

        var oldStatus = existing.TicketStatusTypeId;

        var updated = await _supportTicketRepository.UpdateStatus(
            id,
            dto.TenantId,
            dto.TicketStatusTypeId,
            dto.ChangedByUserId,
            dto.Remarks,
            ct);

        if (updated is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Ticket status update failed.");
        }

        var history = new SupportTicketStatusHistory
        {
            TenantId = dto.TenantId,
            SupportTicketId = id,

            FromTicketStatusTypeId = oldStatus,
            ToTicketStatusTypeId = dto.TicketStatusTypeId,

            ChangedByUserId = dto.ChangedByUserId,
            Remarks = dto.Remarks,

            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.ChangedByUserId,
            StatusTypeId = 1
        };

        await _statusHistoryRepository.Add(history, ct);

        var result = await _supportTicketRepository.GetDetailsById(
            id,
            dto.TenantId,
            ct);

        if (result is null)
        {
            return ServiceResponseDTO<SupportTicketResponseDTO>.ErrorResponse("Ticket status updated but failed to load details.");
        }

        return ServiceResponseDTO<SupportTicketResponseDTO>.SuccessResponse(
            MapToDTO(result),
            "Ticket status updated successfully.");
    }

    public async Task<ServiceResponseDTO<SupportTicketResponseDTO>> Resolve(
        long id,
        SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default)
    {
        dto.TicketStatusTypeId = TicketStatusType.Resolved;

        return await UpdateStatus(id, dto, ct);
    }

    public async Task<ServiceResponseDTO<bool>> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var deleted = await _supportTicketRepository.Delete(id, tenantId, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Support ticket not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Support ticket deleted successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var deleted = await _supportTicketRepository.HardDelete(id, tenantId, ct);

        if (!deleted)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Support ticket not found.");
        }

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Support ticket permanently deleted successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> AddComment(
        SupportTicketCommentAddDTO dto,
        CancellationToken ct = default)
    {
        var ticket = await _supportTicketRepository.GetById(
            dto.SupportTicketId,
            dto.TenantId,
            ct);

        if (ticket is null)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Support ticket not found.");
        }

        var entity = new SupportTicketComment
        {
            TenantId = dto.TenantId,
            SupportTicketId = dto.SupportTicketId,

            CommentedByUserId = dto.CommentedByUserId,
            Comment = dto.Comment,

            IsPublic = dto.IsPublic,

            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CommentedByUserId,
            StatusTypeId = 1
        };

        await _commentRepository.Add(entity, ct);

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Comment added successfully.");
    }

    public async Task<ServiceResponseDTO<bool>> AddAttachment(
        SupportTicketAttachmentAddDTO dto,
        CancellationToken ct = default)
    {
        var ticket = await _supportTicketRepository.GetById(
            dto.SupportTicketId,
            dto.TenantId,
            ct);

        if (ticket is null)
        {
            return ServiceResponseDTO<bool>.ErrorResponse("Support ticket not found.");
        }

        var entity = new SupportTicketAttachment
        {
            TenantId = dto.TenantId,
            SupportTicketId = dto.SupportTicketId,

            FileName = dto.FileName,
            FileUrl = dto.FileUrl,
            ContentType = dto.ContentType,
            FileSizeInBytes = dto.FileSizeInBytes,
            UploadedByUserId = dto.UploadedByUserId,

            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.UploadedByUserId,
            StatusTypeId = 1
        };

        await _attachmentRepository.Add(entity, ct);

        return ServiceResponseDTO<bool>.SuccessResponse(true, "Attachment added successfully.");
    }

    private static SupportTicketResponseDTO MapToDTO(SupportTicket entity)
    {
        return new SupportTicketResponseDTO
        {
            Id = entity.Id,
            TenantId = entity.TenantId,

            IssueNo = entity.IssueNo,
            TicketCode = entity.TicketCode,

            ProductProjectId = entity.ProductProjectId,
            ProductProjectName = entity.ProductProject?.Name,

            IssueName = entity.IssueName,
            IssueTitle = entity.IssueTitle,
            IssueDescription = entity.IssueDescription,
            IssueDate = entity.IssueDate,

            IssueCategoryTypeId = entity.IssueCategoryTypeId,
            PriorityTypeId = entity.PriorityTypeId,
            SupportLevelTypeId = entity.SupportLevelTypeId,
            TicketStatusTypeId = entity.TicketStatusTypeId,

            IssuerUserId = entity.IssuerUserId,
            IssuerPhoneNo = entity.IssuerPhoneNo,
            IssuerEmail = entity.IssuerEmail,

            ResolverUserId = entity.ResolverUserId,
            ResolverPhoneNo = entity.ResolverPhoneNo,
            ResolverEmail = entity.ResolverEmail,

            Remarks = entity.Remarks,
            IssueFinishingDate = entity.IssueFinishingDate,
            EstimatedHours = entity.EstimatedHours,

            CommentCount = entity.Comments?.Count(x => x.StatusTypeId == 1) ?? 0,
            AttachmentCount = entity.Attachments?.Count(x => x.StatusTypeId == 1) ?? 0,

            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy,
            StatusTypeId = entity.StatusTypeId
        };
    }
}