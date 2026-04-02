using System;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>Returned by GET /api/File/{businessBceid}/{userBceid}/documents/contract/{contractId},
    /// GET /api/File/{businessBceid}/{userBceid}/documents/account/{accountId}, and
    /// GET /api/File/contract_package/{businessBceid}/{userBceid}/{taskId}.</summary>
    public class DocumentCollectionResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }
        public DocumentItemDto[] DocumentCollection { get; set; }
    }

    /// <summary>A single document attachment returned inside DocumentCollectionResponseDto.</summary>
    public class DocumentItemDto
    {
        /// <summary>activitymimeattachmentid (the GUID used for downloads).</summary>
        public string activitymimeattachmentid { get; set; }
        public string filename { get; set; }
        public string subject { get; set; }
        public string subjectOther { get; set; }
        public string body { get; set; }
        public string overwritetime { get; set; }
    }

    /// <summary>Returned by GET /api/File/{businessBceid}/{userBceid}/document/{docId}.</summary>
    public class DownloadDocumentDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Body { get; set; }
        public string FileName { get; set; }
        public decimal? FileSize { get; set; }
        public string MimeType { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }

    /// <summary>Returned by POST upload endpoints on FileController.</summary>
    public class UploadResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
    }
}
