using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Dtos.Support;

namespace DG.SupportDesk.Application.Abstractions.Services.Support;

public interface ISupportTicketService
{
    Task<ServiceResponseDTO<SupportTicketResponseDTO>> Add(
        SupportTicketAddDTO dto,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<SupportTicketResponseDTO>> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<PagedResponseDTO<SupportTicketResponseDTO>>> GetAll(
        SupportTicketQueryDTO query,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<SupportTicketResponseDTO>> Update(
        SupportTicketUpdateDTO dto,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<SupportTicketResponseDTO>> UpdateStatus(
        long id,
        SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<SupportTicketResponseDTO>> Resolve(
        long id,
        SupportTicketStatusUpdateDTO dto,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> AddComment(
        SupportTicketCommentAddDTO dto,
        CancellationToken ct = default);

    Task<ServiceResponseDTO<bool>> AddAttachment(
        SupportTicketAttachmentAddDTO dto,
        CancellationToken ct = default);
}