namespace MauiApp4.Models
{
    public class Complaint
    {
        public string? CaseID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Sex { get; set; }
        public string? Contact { get; set; }
        public string? City { get; set; }
        public string? Purok { get; set; }
        public string? RespLastName { get; set; }
        public string? RespFirstName { get; set; }
        public string? RespMiddleName { get; set; }
        public string? Relationship { get; set; }
        public string? ComplaintDetails { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string? Status { get; set; }
    }
}
