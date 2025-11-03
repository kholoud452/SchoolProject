namespace SchoolProject.Core.Features.SubjectFeatures.Queries.Results
{
    public class GetSubjectResult
    {
        public int SubID { get; set; }
        public string SubjectNameAr { get; set; }
        public string SubjectNameEn { get; set; }
        public int? Period { get; set; }
    }
}
