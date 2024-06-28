namespace OfficeManager.Domain.DataObjects.Documents.Requests
{
    public class DocumentRequest
    {
        public DocumentRequest() { }

        public DocumentRequest(Guid caseId, string fileName, string filePath)
        {
            CaseId = caseId;
            FileName = fileName;
            FilePath = filePath;
        }

        public string Url { get; set; }
        public string Name { get; set; }
        public Guid CaseId { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileName { get; }
        public string FilePath { get; }
    }
}
