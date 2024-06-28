using OfficeManager.Domain.DataObjects.Cases.Responses;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.DataObjects.Documents.Responses
{
    public class DocumentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
        public CaseResponse Case { get; set; }

        public static DocumentResponse BuildWith(Document document)
        {
            return new DocumentResponse
            {
                Id = document.Id,
                Url = document.Url,
                Name = document.Name,
                UploadDate = document.UploadDate,
                Case = CaseResponse.BuildWith(document.Case)
            };
        }
    }
}
