using OfficeManager.Domain.DataObjects.Documents.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class Document : EntityBase
    {
        public string Url { get; private set; }
        public string Name { get; private set; }
        public DateTime UploadDate { get; private set; }
        public Guid CaseId { get; private set; }
        public Case Case { get; private set; }

        public static Document BuildWith(DocumentRequest request)
        {
            return new Document
            {
                Url = request.Url,
                Name = request.Name,
                CaseId = request.CaseId,
                UploadDate = DateTime.Now,
            };
        }

        public void Update(Document document)
        {
            Url = document.Url;
            Name = document.Name;
            CaseId = document.CaseId;
            UploadDate = document.UploadDate;
        }

        public override bool IsValid()
        {
            var validator = new DocumentValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
