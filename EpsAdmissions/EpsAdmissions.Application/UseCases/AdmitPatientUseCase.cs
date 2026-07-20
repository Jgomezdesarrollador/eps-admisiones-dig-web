using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Application.DTOs.Responses;
using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Domain.Entities;
using EpsAdmissions.Domain.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EpsAdmissions.Application.UseCases;

public sealed class AdmitPatientUseCase(
    IClinicalHistoryStorage clinicalHistoryStorage,
    IAdmissionRepository admissionRepository,
    IOutboxRepository outboxRepository,
    IUnitOfWork unitOfWork,
    ILogger<AdmitPatientUseCase> logger)
    : IAdmitPatientUseCase
{
    public async Task<AdmissionResponse> HandleAsync(AdmissionRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting admission process for patient {Document}",request.Patient.Document);

        // 1. Guardar historia clínica en Mongo
        var mongoDocumentId = await clinicalHistoryStorage.SaveAsync(request, cancellationToken);
        logger.LogInformation("Clinical history stored in MongoDB. DocumentId: {MongoDocumentId}", mongoDocumentId);

        // 2. Crear la entidad del dominio
        var admission = new Admission(request.Patient.Document, request.Copayment.Amount, mongoDocumentId);

        // 3. Guardar en SQL
        await admissionRepository.AddAsync(admission, cancellationToken);
        logger.LogInformation("Admission registered in SQL Server. AdmissionId: {AdmissionId}", admission.Id);

        // 4. Crear evento Outbox
        var admissionCreatedEvent = new AdmissionCreatedEvent
        {
            AdmissionId = admission.Id,
            PatientDocument = admission.PatientDocument,
            MongoDocumentId = mongoDocumentId
        };

        var payload = JsonSerializer.Serialize(admissionCreatedEvent);

        var outboxMessage = new OutboxMessage(admission.Id.ToString(), nameof(AdmissionCreatedEvent), payload);

        // 5. Guardar Outbox
        await outboxRepository.AddAsync(outboxMessage, cancellationToken);
        logger.LogInformation("Outbox event created. EventType: {EventType}", nameof(AdmissionCreatedEvent));

        // 6. Commit SQL  
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Database transaction committed successfully.");

        logger.LogInformation("Admission completed successfully for patient {Document}", request.Patient.Document);

        // 7. Respuesta
        return new AdmissionResponse
        {
            AdmissionId = admission.Id,
            Status = admission.Status.ToString()
        };
    }
}